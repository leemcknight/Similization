using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectInput;
using D3D = Microsoft.DirectX.Direct3D;
using DirectInput = Microsoft.DirectX.DirectInput;
using LJM.Similization.Client.DirectX.Util;
using LJM.Similization.Client.DirectX.Controls;

namespace LJM.Similization.Client.DirectX
{
    /// <summary>
    /// Class responsible for managing the Direct3D objects.
    /// </summary>
	public class ControlHost : IDirectXControlHost, IDisposable
	{
        private bool startFullscreen = false;
        private bool windowed;
        private bool active;
        private bool ready;
        private bool deviceLost;
        private bool disposed;
		private HostForm owner;
		private D3D.Device localDevice;
		private D3DSettings settings;
		private PresentParameters parameters;
		private D3DEnumeration enumerationSettings;        		
		private DXControlCollection controls;
		private Collection<Surface> surfaces;                
        private Line line;
        private DirectInput.Device mouse;
        private DirectInput.Device keyboard;
        private KeyboardState keyboardState;
        private MouseState previousMouseState;   
        private event EventHandler<DXMouseEventArgs> mouseActionPerformed;
        private event EventHandler<DXKeyboardEventArgs> keyboardActionPerformed;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsMain"/> class.
        /// </summary>
        /// <param name="host"></param>
		public ControlHost(HostForm host)
		{
            this.owner = host;
			this.surfaces = new Collection<Surface>();
            this.controls = new DXControlCollection();
			this.settings = new D3DSettings();
			this.parameters = new PresentParameters();
			this.enumerationSettings =  new D3DEnumeration();
            InitializeInputDevices();
            InitDirect3D();
		}        

        ~ControlHost()
        {
            Dispose(false);
        }

        /// <summary>
        /// Release all managed resources.
        /// </summary>
		public void Dispose()
		{
            Dispose(true);
            GC.SuppressFinalize(this);
		}

        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                foreach (Surface surface in this.surfaces)                
                    surface.Dispose();                
                foreach (DXControl control in this.controls)                
                    control.Dispose();                
                if (this.localDevice != null)
                    this.localDevice.Dispose();
                if (this.line != null)
                    this.line.Dispose();
            }
            this.disposed = true;
        }

		//Initializes the Direct3D subsystem.
		private void InitDirect3D()
		{
			this.enumerationSettings.ConfirmDeviceCallback = new D3DEnumeration.ConfirmDeviceCallbackType(this.ConfirmDevice);
			this.enumerationSettings.Enumerate();
			ChooseInitialSettings();
			InitializeEnvironment();
            
			this.active = true;
			this.ready = true;			
		}

        
        /// Initializes the DirectX environment.        
		private void InitializeEnvironment()
		{            					
			GraphicsDeviceInfo deviceInfo = this.settings.DeviceInfo;
			this.windowed = this.settings.IsWindowed;

			// Set up the presentation parameters
			BuildPresentParametersFromSettings();

			if (deviceInfo.Caps.PrimitiveMiscCapabilities.IsNullReference)
			{
				// Warn user about null ref device that can't render anything
				//TODO: implement
			}

			CreateFlags createFlags = new CreateFlags();
			if (this.settings.VertexProcessingType == VertexProcessingType.Software)
				createFlags = CreateFlags.SoftwareVertexProcessing;
			else if (this.settings.VertexProcessingType == VertexProcessingType.Mixed)
				createFlags = CreateFlags.MixedVertexProcessing;
			else if (this.settings.VertexProcessingType == VertexProcessingType.Hardware)
				createFlags = CreateFlags.HardwareVertexProcessing;
			else if (this.settings.VertexProcessingType == VertexProcessingType.PureHardware)
			{
				createFlags = CreateFlags.HardwareVertexProcessing | CreateFlags.PureDevice;
			}						

			try
			{
				// Create the device
                this.localDevice = new D3D.Device(this.settings.AdapterOrdinal, this.settings.DevType, this.owner.Handle, createFlags, this.parameters);

				// Cache our local objects
				//this.renderState = this.localDevice.RenderState;
				//this.sampleState = this.localDevice.SamplerState;
				//this.textureStates = this.localDevice.TextureState;
				// When moving from fullscreen to windowed mode, it is important to
				// adjust the window size after recreating the device rather than
				// beforehand to ensure that you get the window size you want.  For
				// example, when switching from 640x480 fullscreen to windowed with
				// a 1000x600 window on a 1024x768 desktop, it is impossible to set
				// the window size to 1000x600 until after the display mode has
				// changed to 1024x768, because windows cannot be larger than the
				// desktop.
				if(this.windowed)
				{
					// Make sure main window isn't topmost, so error message is visible
					System.Drawing.Size currentClientSize = this.owner.ClientSize;
					this.owner.Size = this.owner.ClientSize;
					this.owner.ClientSize = currentClientSize;
					this.owner.SendToBack();
					this.owner.BringToFront();
				}

				BehaviorFlags behaviorFlags = new BehaviorFlags(createFlags);
				this.localDevice.DeviceReset += new System.EventHandler(this.RestoreDeviceObjects);				
				// Initialize the app's device-dependent objects
				try
				{
					//InitializeDeviceObjects();
					RestoreDeviceObjects(null, null);
					this.active = true;
					return;
				}
				catch
				{
					this.localDevice.Dispose();
					this.localDevice = null;					
					return;
				}
			}
			catch
			{
				// If that failed, fall back to the reference rasterizer
				if( deviceInfo.DevType == D3D.DeviceType.Hardware )
				{
					if (FindBestWindowedMode(false, true))
					{
						this.windowed = true;
						// Make sure main window isn't topmost, so error message is visible
						System.Drawing.Size currentClientSize = this.owner.ClientSize;
						this.owner.Size = this.owner.ClientSize;
						this.owner.ClientSize = currentClientSize;
						this.owner.SendToBack();
						this.owner.BringToFront();

						// Let the user know we are switching from HAL to the reference rasterizer
						//HandleSampleException( null, ApplicationMessage.WarnSwitchToRef);

						InitializeEnvironment();
					}
				}
			}
		}

		protected virtual bool ConfirmDevice(Microsoft.DirectX.Direct3D.Capabilities caps, VertexProcessingType processingType, Format format) 
        { 
            return true; 
        }

		/// <summary>
		/// Build presentation parameters from the current settings
		/// </summary>
		private void BuildPresentParametersFromSettings()
		{
			this.parameters.IsWindowed = this.settings.IsWindowed;
			this.parameters.BackBufferCount = 1;
			this.parameters.MultiSampleType = this.settings.MultisampleType;
			this.parameters.MultiSampleQuality = this.settings.MultisampleQuality;
			this.parameters.SwapEffect = SwapEffect.Discard;
			this.parameters.EnableAutoDepthStencil = this.enumerationSettings.AppUsesDepthBuffer;
			this.parameters.AutoDepthStencilFormat = this.settings.DepthStencilBufferFormat;
			this.parameters.PresentFlag = PresentFlag.None;
			if( this.windowed )
			{
				this.parameters.BackBufferWidth  = this.owner.ClientRectangle.Right - this.owner.ClientRectangle.Left;
				this.parameters.BackBufferHeight = this.owner.ClientRectangle.Bottom - this.owner.ClientRectangle.Top;
				this.parameters.BackBufferFormat = this.settings.DeviceCombo.BackBufferFormat;
				this.parameters.FullScreenRefreshRateInHz = 0;
				this.parameters.PresentationInterval = PresentInterval.Immediate;
				this.parameters.DeviceWindowHandle = this.owner.Handle;
			}
			else
			{
				this.parameters.BackBufferWidth  = this.settings.DisplayMode.Width;
				this.parameters.BackBufferHeight = this.settings.DisplayMode.Height;
				this.parameters.BackBufferFormat = this.settings.DeviceCombo.BackBufferFormat;
				this.parameters.FullScreenRefreshRateInHz = this.settings.DisplayMode.RefreshRate;
				this.parameters.PresentationInterval = this.settings.PresentInterval;
				this.parameters.DeviceWindowHandle = this.owner.Handle;
			}
		}

		
		/// Choose the initial settings for the application		
		private bool ChooseInitialSettings()
		{
			bool foundFullscreenMode = FindBestFullScreenMode(false, false);
			bool foundWindowedMode = FindBestWindowedMode(false, false);
			if (this.startFullscreen && foundFullscreenMode)
				this.settings.IsWindowed = false;

			if (!foundFullscreenMode && !foundWindowedMode)
				throw new DirectXException(DirectXClientResources.NoModeAvailable);

			return (foundFullscreenMode || foundWindowedMode);
		}

		
		/// Sets up graphicsSettings with best available windowed mode, subject to 
		/// the doesRequireHardware and doesRequireReference constraints.  		
		private bool FindBestWindowedMode(bool doesRequireHardware, bool doesRequireReference)
		{
			// Get display mode of primary adapter (which is assumed to be where the window 
			// will appear)
			DisplayMode primaryDesktopDisplayMode = D3D.Manager.Adapters[0].CurrentDisplayMode;
			GraphicsAdapterInfo bestAdapterInfo = null;
			GraphicsDeviceInfo bestDeviceInfo = null;
			DeviceCombo bestDeviceCombo = null;

			foreach (GraphicsAdapterInfo adapterInfo in this.enumerationSettings.AdapterInfoList)
			{
				foreach (GraphicsDeviceInfo deviceInfo in adapterInfo.DeviceInfoList)
				{
					if (doesRequireHardware && deviceInfo.DevType != D3D.DeviceType.Hardware)
						continue;
					if (doesRequireReference && deviceInfo.DevType != D3D.DeviceType.Reference)
						continue;
					foreach (DeviceCombo deviceCombo in deviceInfo.DeviceComboList)
					{
						bool adapterMatchesBackBuffer = (deviceCombo.BackBufferFormat == deviceCombo.AdapterFormat);
						if (!deviceCombo.IsWindowed)
							continue;
						if (deviceCombo.AdapterFormat != primaryDesktopDisplayMode.Format)
							continue;
						// If we haven't found a compatible DeviceCombo yet, or if this set
						// is better (because it's a HAL, and/or because formats match better),
						// save it
						if (bestDeviceCombo == null || 
							bestDeviceCombo.DevType != D3D.DeviceType.Hardware && deviceInfo.DevType == D3D.DeviceType.Hardware ||
							deviceCombo.DevType == D3D.DeviceType.Hardware && adapterMatchesBackBuffer )
						{
							bestAdapterInfo = adapterInfo;
							bestDeviceInfo = deviceInfo;
							bestDeviceCombo = deviceCombo;
							if (deviceInfo.DevType == D3D.DeviceType.Hardware && adapterMatchesBackBuffer)
							{
								// This windowed device combo looks great -- take it
								goto EndWindowedDeviceComboSearch;
							}
							// Otherwise keep looking for a better windowed device combo
						}
					}
				}
			}
			EndWindowedDeviceComboSearch:
				if (bestDeviceCombo == null )
					return false;

			this.settings.WindowedAdapterInfo = bestAdapterInfo;
			this.settings.WindowedDeviceInfo = bestDeviceInfo;
			this.settings.WindowedDeviceCombo = bestDeviceCombo;
			this.settings.IsWindowed = true;
			this.settings.WindowedDisplayMode = primaryDesktopDisplayMode;
			this.settings.WindowedWidth = this.owner.ClientRectangle.Right - this.owner.ClientRectangle.Left;
			this.settings.WindowedHeight = this.owner.ClientRectangle.Bottom - this.owner.ClientRectangle.Top;
			if (this.enumerationSettings.AppUsesDepthBuffer)
				this.settings.WindowedDepthStencilBufferFormat = (DepthFormat)bestDeviceCombo.DepthStencilFormatList[0];
			this.settings.WindowedMultisampleType = (MultiSampleType)bestDeviceCombo.MultiSampleTypeList[0];
			this.settings.WindowedMultisampleQuality = 0;
			this.settings.WindowedVertexProcessingType = (VertexProcessingType)bestDeviceCombo.VertexProcessingTypeList[0];
			this.settings.WindowedPresentInterval = (PresentInterval)bestDeviceCombo.PresentIntervalList[0];
			return true;
		}

		
		/// Sets up graphicsSettings with best available fullscreen mode, subject to 
		/// the doesRequireHardware and doesRequireReference constraints.  		
		private bool FindBestFullScreenMode(bool doesRequireHardware, bool doesRequireReference)
		{
			// For fullscreen, default to first HAL DeviceCombo that supports the current desktop 
			// display mode, or any display mode if HAL is not compatible with the desktop mode, or 
			// non-HAL if no HAL is available
			DisplayMode adapterDesktopDisplayMode = new DisplayMode();
			DisplayMode bestAdapterDesktopDisplayMode = new DisplayMode();
			DisplayMode bestDisplayMode = new DisplayMode();
			bestAdapterDesktopDisplayMode.Width = 0;
			bestAdapterDesktopDisplayMode.Height = 0;
			bestAdapterDesktopDisplayMode.Format = 0;
			bestAdapterDesktopDisplayMode.RefreshRate = 0;

			GraphicsAdapterInfo bestAdapterInfo = null;
			GraphicsDeviceInfo bestDeviceInfo = null;
			DeviceCombo bestDeviceCombo = null;

			foreach (GraphicsAdapterInfo adapterInfo in this.enumerationSettings.AdapterInfoList)
			{
				adapterDesktopDisplayMode = D3D.Manager.Adapters[adapterInfo.AdapterOrdinal].CurrentDisplayMode;
				foreach (GraphicsDeviceInfo deviceInfo in adapterInfo.DeviceInfoList)
				{
					if (doesRequireHardware && deviceInfo.DevType != D3D.DeviceType.Hardware)
						continue;
					if (doesRequireReference && deviceInfo.DevType != D3D.DeviceType.Reference)
						continue;
					foreach (DeviceCombo deviceCombo in deviceInfo.DeviceComboList)
					{
						bool adapterMatchesBackBuffer = (deviceCombo.BackBufferFormat == deviceCombo.AdapterFormat);
						bool adapterMatchesDesktop = (deviceCombo.AdapterFormat == adapterDesktopDisplayMode.Format);
						if (deviceCombo.IsWindowed)
							continue;
						// If we haven't found a compatible set yet, or if this set
						// is better (because it's a HAL, and/or because formats match better),
						// save it
						if (bestDeviceCombo == null ||
							bestDeviceCombo.DevType != D3D.DeviceType.Hardware && deviceInfo.DevType == D3D.DeviceType.Hardware ||
							bestDeviceCombo.DevType == D3D.DeviceType.Hardware && bestDeviceCombo.AdapterFormat != adapterDesktopDisplayMode.Format && adapterMatchesDesktop ||
							bestDeviceCombo.DevType == D3D.DeviceType.Hardware && adapterMatchesDesktop && adapterMatchesBackBuffer )
						{
							bestAdapterDesktopDisplayMode = adapterDesktopDisplayMode;
							bestAdapterInfo = adapterInfo;
							bestDeviceInfo = deviceInfo;
							bestDeviceCombo = deviceCombo;
							if (deviceInfo.DevType == D3D.DeviceType.Hardware && adapterMatchesDesktop && adapterMatchesBackBuffer)

							{
								// This fullscreen device combo looks great -- take it
								goto EndFullscreenDeviceComboSearch;
							}
							// Otherwise keep looking for a better fullscreen device combo
						}
					}
				}
			}
			EndFullscreenDeviceComboSearch:
				if (bestDeviceCombo == null)
					return false;

			// Need to find a display mode on the best adapter that uses pBestDeviceCombo->AdapterFormat
			// and is as close to bestAdapterDesktopDisplayMode's res as possible
			bestDisplayMode.Width = 0;
			bestDisplayMode.Height = 0;
			bestDisplayMode.Format = 0;
			bestDisplayMode.RefreshRate = 0;
			
			foreach( DisplayMode displayMode in bestAdapterInfo.DisplayModeList )
			{
				if( displayMode.Format != bestDeviceCombo.AdapterFormat )
					continue;
				if( displayMode.Width == bestAdapterDesktopDisplayMode.Width &&
					displayMode.Height == bestAdapterDesktopDisplayMode.Height && 
					displayMode.RefreshRate == bestAdapterDesktopDisplayMode.RefreshRate )
				{
					// found a perfect match, so stop
					bestDisplayMode = displayMode;
					break;
				}
				else if( displayMode.Width == bestAdapterDesktopDisplayMode.Width &&
					displayMode.Height == bestAdapterDesktopDisplayMode.Height && 
					displayMode.RefreshRate > bestDisplayMode.RefreshRate )
				{
					// refresh rate doesn't match, but width/height match, so keep this
					// and keep looking
					bestDisplayMode = displayMode;
				}
				else if( bestDisplayMode.Width == bestAdapterDesktopDisplayMode.Width )
				{
					// width matches, so keep this and keep looking
					bestDisplayMode = displayMode;
				}
				else if( bestDisplayMode.Width == 0 )
				{
					// we don't have anything better yet, so keep this and keep looking
					bestDisplayMode = displayMode;
				}
			}
			
			this.settings.FullscreenAdapterInfo = bestAdapterInfo;
			this.settings.FullscreenDeviceInfo = bestDeviceInfo;
			this.settings.FullscreenDeviceCombo = bestDeviceCombo;
			this.settings.IsWindowed = false;
			this.settings.FullscreenDisplayMode = bestDisplayMode;
			if (this.enumerationSettings.AppUsesDepthBuffer)
				this.settings.FullscreenDepthStencilBufferFormat = (DepthFormat)bestDeviceCombo.DepthStencilFormatList[0];
			this.settings.FullscreenMultisampleType = (MultiSampleType)bestDeviceCombo.MultiSampleTypeList[0];
			this.settings.FullscreenMultisampleQuality = 0;
			this.settings.FullscreenVertexProcessingType = (VertexProcessingType)bestDeviceCombo.VertexProcessingTypeList[0];
			this.settings.FullscreenPresentInterval = (PresentInterval)bestDeviceCombo.PresentIntervalList[0];
			return true;
		}

		private void RestoreDeviceObjects(System.Object sender, System.EventArgs e)
		{
			// Restore the device objects for the meshes and fonts

			// Set the transform matrices (view and world are updated per frame)
			Matrix matProj;            
			float fAspect = this.parameters.BackBufferWidth / (float)this.parameters.BackBufferHeight;
			matProj = Matrix.PerspectiveFieldOfViewLeftHanded( (float)Math.PI/4, fAspect, 1.0f, 100.0f );
			this.localDevice.Transform.Projection = matProj;
            
			// Set up the default texture states
            /*
			this.localDevice.TextureState[0].ColorOperation = TextureOperation.Modulate;
			this.localDevice.TextureState[0].ColorArgument1 = TextureArgument.TextureFactor;                        
			this.localDevice.TextureState[0].ColorArgument2 = TextureArgument.Diffuse;
			this.localDevice.TextureState[0].AlphaOperation = TextureOperation.SelectArg1;
			this.localDevice.TextureState[0].AlphaArgument1 = TextureArgument.TextureFactor;						            
            this.localDevice.SamplerState[0].MinFilter = TextureFilter.Linear;
            this.localDevice.SamplerState[0].MagFilter = TextureFilter.Linear;
            this.localDevice.SamplerState[0].MipFilter = TextureFilter.Linear;
            this.localDevice.SamplerState[0].AddressU = TextureAddress.Clamp;
            this.localDevice.SamplerState[0].AddressV = TextureAddress.Clamp;
            this.localDevice.RenderState.DitherEnable = true;
             * */

            this.localDevice.SamplerState[0].MinFilter = TextureFilter.Linear;
            this.localDevice.SamplerState[0].MagFilter = TextureFilter.Linear;
            this.localDevice.SamplerState[0].MipFilter = TextureFilter.Linear;
            this.localDevice.SamplerState[0].AddressU = TextureAddress.Clamp;
            this.localDevice.SamplerState[0].AddressV = TextureAddress.Clamp;
            this.localDevice.RenderState.DitherEnable = true;
		}

        /// <summary>
        /// Refreshes the DirectX display.
        /// </summary>
		public void Refresh()
		{
			if(this.active && this.ready)
			{
				if (this.deviceLost)
				{
					try
					{
						// Test the cooperative level to see if it's okay to render						
                        this.localDevice.CheckCooperativeLevel();
					}
					catch (DeviceLostException)
					{
						// If the device was lost, do not render until we get it back
						return;
					}
					catch (DeviceNotResetException)
					{
						// Check if the device needs to be resized.
						// If we are windowed, read the desktop mode and use the same format for
						// the back buffer
						if(this.windowed)
						{
							GraphicsAdapterInfo adapterInfo = this.settings.AdapterInfo;
							this.settings.WindowedDisplayMode = D3D.Manager.Adapters[adapterInfo.AdapterOrdinal].CurrentDisplayMode;
							this.parameters.BackBufferFormat = this.settings.WindowedDisplayMode.Format;
						}

						// Reset the device and resize it                        
						this.localDevice.Reset(this.parameters);
						EnvironmentResized(this.localDevice, null);
					}
					this.deviceLost = false;
				}

                ProcessKeyboardDevice();
                ProcessMouseDevice();      
				this.localDevice.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
				this.localDevice.BeginScene();
				foreach(DXControl control in this.controls)									
					control.Render(new RenderEventArgs(this));					
													
				this.localDevice.EndScene();
                
				try
				{
					// Show the frame on the primary surface.
					this.localDevice.Present();
				}
				catch(DeviceLostException)
				{
					this.deviceLost = true;
				}
			}
		}

    
        private void ProcessKeyboardDevice()
        {
            bool loop = true;
            do
            {
                try
                {
                    this.keyboard.Poll();
                    loop = false;
                }
                catch (InputLostException)
                {
                    this.keyboard.Acquire();
                    this.keyboard.Poll();
                    this.keyboardState = this.keyboard.CurrentKeyboardState;
                }
                catch (Exception)
                {
                    loop = false;
                    throw;
                }
            } while (loop);

            KeyboardState state = this.keyboard.CurrentKeyboardState;
            DXKeyboardEventArgs args = GetKeyboardEventData(state);
            if (args != null)
                OnKeyboardActionPerformed(args);
            this.keyboardState = state;                       
        }

        private DXKeyboardEventArgs GetKeyboardEventData(KeyboardState keyStates)
        {
            DXKeyboardEventArgs eventArgs = null;

            for (Key k = Key.Escape; k <= Key.MediaSelect; k++)
            {
                if (keyStates[k])
                {
                    if(keyStates[k])
                        eventArgs = new DXKeyboardEventArgs(keyStates, KeyboardAction.KeyPress,k);
                    else
                        eventArgs = new DXKeyboardEventArgs(keyStates, KeyboardAction.KeyUp, k);
                }
            }

            return eventArgs;
        }

        //Polls the mouse device and alerts the controls to any changes in its' status.
        private void ProcessMouseDevice()
        {
            try
            {
                this.mouse.Poll();
            }
            catch (InputLostException)
            {
                //reacquire the mouse
                this.mouse.Acquire();                
                this.mouse.Poll();
                this.previousMouseState = this.mouse.CurrentMouseState;
            }
            MouseState state = this.mouse.CurrentMouseState;
            DXMouseEventArgs clickArgs = GetClickEventData(state);
            DXMouseEventArgs moveArgs = GetMoveEventData(state);
            if (clickArgs != null)
                OnMouseActionPerformed(clickArgs);
            if (moveArgs != null)
                OnMouseActionPerformed(moveArgs);
            this.previousMouseState = this.mouse.CurrentMouseState;
        }

        //Creates and returns event data for the movement of the mouse.  If the 
        //mouse hasn't move, null is returned.
        private DXMouseEventArgs GetMoveEventData(MouseState newMouseState)
        {
            if (newMouseState.X == 0 && newMouseState.Y == 0)
                return null;
            DXMouseEventArgs args = new DXMouseEventArgs(
                MouseAction.Move,
                newMouseState,
                Control.MousePosition,
                0,
                this);
            return args;
        }

        //Creates and returns event data to send to the controls if the mouse has been clicked.
        //Returns null if it hasn't.
        private DXMouseEventArgs GetClickEventData(MouseState newMouseState)
        {
            bool[] oldButtonData = this.previousMouseState.GetButtons();
            bool[] newButtonData = newMouseState.GetButtons();

            MouseAction action = MouseAction.None;
            int buttonIndex = 0;
            for (int i = 0; i < oldButtonData.Length; i++)
            {
                if (oldButtonData[i] != newButtonData[i])
                {
                    buttonIndex = i;
                    if (oldButtonData[i])
                        action = MouseAction.Release;
                    else
                        action = MouseAction.Click;
                    break;
                }
            }

            if (action == MouseAction.None)
                return null;
            else
                return new DXMouseEventArgs(
                    action, 
                    newMouseState, 
                    Control.MousePosition, 
                    buttonIndex, 
                    this);
        }

        //fires the MouseActionPerformed event.
        private void OnMouseActionPerformed(DXMouseEventArgs e)
        {
            if (this.mouseActionPerformed != null)
                this.mouseActionPerformed(this, e);
        }

        /// <summary>
        /// Occurs when the user performs an action with the mouse.
        /// </summary>
        public event EventHandler<DXMouseEventArgs> MouseActionPerformed
        {
            add
            {
                this.mouseActionPerformed += value;
            }

            remove
            {
                this.mouseActionPerformed -= value;
            }
        }

        private void OnKeyboardActionPerformed(DXKeyboardEventArgs e)
        {
            if (this.keyboardActionPerformed != null)
                this.keyboardActionPerformed(this, e);
        }

        /// <summary>
        /// Occurs when a some action is peformed with the keyboard, such as pressing, holding, 
        /// or releasing a key.
        /// </summary>
        public event EventHandler<DXKeyboardEventArgs> KeyboardActionPerformed
        {
            add
            {
                this.keyboardActionPerformed += value;
            }

            remove
            {
                this.keyboardActionPerformed -= value;
            }
        }

		private void EnvironmentResized(object sender, System.ComponentModel.CancelEventArgs e)
		{

		}

        /// <summary>
        /// The collection of <see cref="DXControl"/> objects currently 
        /// displayed on the screen.
        /// </summary>
		public DXControlCollection Controls
		{
			get { return this.controls; }
		}

        /// <summary>
        /// The Windows Forms Control that is the parent form to the 
        /// DirectX screen.
        /// </summary>
		public HostForm Owner
		{
			get { return this.owner; }
		}

        //Initializes the input devices used in the game, namely the mouse and the keyboard.
        private void InitializeInputDevices()
        {
            foreach (Microsoft.DirectX.DirectInput.DeviceInstance device in Microsoft.DirectX.DirectInput.Manager.Devices)
            {                
                if (device.InstanceName =="Mouse")
                    this.mouse = new Microsoft.DirectX.DirectInput.Device(device.Instance);
                else if (device.InstanceName == "Keyboard")
                    this.keyboard = new Microsoft.DirectX.DirectInput.Device(device.Instance);
            }            
            
            //set the cooperative levels
            this.keyboard.SetCooperativeLevel(this.owner.Handle, CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Foreground | CooperativeLevelFlags.NoWindowsKey);            
            this.mouse.SetCooperativeLevel(this.owner.Handle, CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Foreground);
            this.keyboard.SetDataFormat(DeviceDataFormat.Keyboard);
            this.mouse.SetDataFormat(DeviceDataFormat.Mouse);            

            //acquire the devices
            this.keyboard.Acquire();            
            this.mouse.Acquire();

            //poll them to get the current states
            this.mouse.Poll();
            this.keyboard.Poll();
            //save the current states
            this.keyboardState = this.keyboard.CurrentKeyboardState;
            this.previousMouseState = this.mouse.CurrentMouseState;
        }

        /// <summary>
        /// The <i>Device</i> the game is drawing to.
        /// </summary>
		public D3D.Device Device
		{
			get { return this.localDevice; }
		}

        /// <summary>
        /// Draws a line in the specified color.
        /// </summary>
        /// <param name="vertexList"></param>
        /// <param name="lineColor"></param>
        public void DrawLine(Vector2[] vertexList, Color lineColor)
        {
            if (this.line == null)
                this.line = new Line(this.localDevice);
            this.line.Draw(vertexList, lineColor);
        }
        
        /// <summary>
        /// The bounds (in screen coordinates) of the screen space available to the <see cref="ControlHost"/>.
        /// </summary>
        public Rectangle ScreenBounds
        {
            get 
            {                 
                return this.owner.ClientRectangle;
            }
        }       
    }
}
