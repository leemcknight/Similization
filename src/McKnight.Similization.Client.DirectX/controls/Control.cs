using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectInput;
using D3D = Microsoft.DirectX.Direct3D;
using System.Diagnostics;

namespace LJM.Similization.Client.DirectX.Controls
{
	/// <summary>
	/// Base class for all DirectX controls.
	/// </summary>
	public class DXControl : IDisposable
	{        
        private string text;
        private int dockPadding;
        private bool visible;
        private bool focused;
        private bool hovered;
        private bool disposed;
        private GradientDirection gradientDirection;
		private Color foreColor;
		private Color backColor;
        private Color backColor2;
		private Size size;
		private Point location;
        private Rectangle screenBounds;
        private object tag;
		private System.Drawing.Font innerFont;
		private Microsoft.DirectX.Direct3D.Font font;
        private DXImage backgroundImage;
        private IDirectXControlHost controlHost;
		private DXControl parent;				
		private DockStyle dock = DockStyle.None;			
		private VertexBuffer vertexBuffer;        
		private event EventHandler locationChanged;
		private event EventHandler<DXMouseEventArgs> mouseMove;
		private event EventHandler<DXMouseEventArgs> mouseDown;
		private event EventHandler<DXMouseEventArgs> mouseLeave;
		private event EventHandler<DXMouseEventArgs> mouseUp;
        private event EventHandler<DXKeyboardEventArgs> keyPress;
        private event EventHandler<DXMouseEventArgs> mouseActionPerformed;
        private event EventHandler<DXKeyboardEventArgs> keyboardActionPerformed;
		private event EventHandler gotFocus;
		private event EventHandler lostFocus;				
        private event EventHandler textChanged;
        private event EventHandler sizeChanged;
        private event EventHandler fontChanged;
        private event EventHandler backgroundImageChanged;
        private event EventHandler visibleChanged;
        private event EventHandler click;
        private bool controlClicked;
		private const int BorderWidth=3;

        /// <summary>
        /// Initializes a new instance of the <see cref="DXControl"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public DXControl(IDirectXControlHost controlHost)
		{
            this.controlHost = controlHost;
            this.innerFont = new System.Drawing.Font("Veranda", 9.25F);
            this.font = new Microsoft.DirectX.Direct3D.Font(controlHost.Device, this.innerFont);
            this.mouseActionPerformed = new EventHandler<DXMouseEventArgs>(MouseActionPerformed);
            this.keyboardActionPerformed += new EventHandler<DXKeyboardEventArgs>(KeyboardActionPerformed);
            this.controlHost.MouseActionPerformed += this.mouseActionPerformed;
            this.controlHost.KeyboardActionPerformed += this.keyboardActionPerformed;
		}
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DXControl"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public DXControl(IDirectXControlHost controlHost, DXControl parent) : this(controlHost)
		{
			this.parent = parent;            
		}

        /// <summary>
        /// Cleans up managed resources.
        /// </summary>
		public void Dispose()
		{
            Dispose(true);
            GC.SuppressFinalize(this);
		}

        /// <summary>
        /// Cleans up managed resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if(disposing)
                {
                    if (this.innerFont != null)
                    {
                        this.innerFont.Dispose();
                        this.innerFont = null;
                    }
                    if (this.vertexBuffer != null)
                    {
                        this.vertexBuffer.Dispose();
                        this.vertexBuffer = null;
                    }                    

                    if (this.font != null)
                    {
                        this.font.Dispose();
                        this.font = null;
                    }
                    this.controlHost.MouseActionPerformed -= this.mouseActionPerformed;
                    this.controlHost.KeyboardActionPerformed -= this.keyboardActionPerformed;
                    this.controlHost.Controls.Remove(this);
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// The object responsible for hosting this <see cref="DXControl"/>.
        /// </summary>
        protected IDirectXControlHost ControlHost
        {
            get { return this.controlHost; }
        }

        void KeyboardActionPerformed(object sender, DXKeyboardEventArgs e)
        {
            if (!this.focused)
                return;
            switch (e.KeyboardAction)
            {
                case KeyboardAction.KeyHeld:
                    break;
                case KeyboardAction.KeyPress:
                    OnKeyPress(e);
                    break;
                case KeyboardAction.KeyUp:
                    break;
            }
        }

        private void MouseActionPerformed(object sender, DXMouseEventArgs e)
        {
            switch(e.MouseAction)
            {
                case MouseAction.Move:
                    if (IsMouseOnControl(e.MousePosition))
                    {
                        if (!this.hovered)
                            OnMouseMove(e);
                        else if (this.hovered)
                            OnMouseLeave(e);
                    }
                break;
                case MouseAction.Click:                
                    if (IsMouseOnControl(e.MousePosition))
                        OnMouseDown(e);
                    break;
                case MouseAction.Release:
                    if (IsMouseOnControl(e.MousePosition))
                    {
                        if (this.controlClicked)
                            OnClick();
                        OnMouseUp(e);                        
                    }
                    this.controlClicked = false;
                break;
            }
        }

		/// <summary>
		/// Shows the control.
		/// </summary>
		public virtual void Show()
		{
            if(!this.controlHost.Controls.Contains(this))
                this.controlHost.Controls.Add(this);
			this.Visible = true;						
		}

		/// <summary>
		/// Hides the control.
		/// </summary>
		public virtual void Hide()
		{
            if(this.controlHost.Controls.Contains(this))
                this.controlHost.Controls.Remove(this);
			this.Visible = false;			
		}

		/// <summary>
		/// Gets or sets the DockStyle of the control.  The dockstyle 
		/// will determine which sides of the parent control the control
		/// will be "bolted" to.
		/// </summary>
		public DockStyle Dock
		{
			get { return this.dock; }
			set { this.dock = value; }
		}

		/// <summary>
		/// Gets or sets a integer value indicating the amount of 
		/// padding to place between the control and the docked edge
		/// of the parent.
		/// </summary>
		public int DockPadding
		{
			get { return this.dockPadding; }
			set { this.dockPadding = value;}
		}

		/// <summary>
		/// Gets or sets a value indicating if the control is visible or not.
		/// </summary>
		public bool Visible 
		{
			get { return this.visible; }
			set 
            {
                if (this.visible != value)
                {
                    this.visible = value;
                    OnVisibleChanged();
                }
            }
		}

        /// <summary>
        /// Fires the <i>VisibleChanged</i> event.
        /// </summary>
        protected virtual void OnVisibleChanged()
        {
            if (this.visibleChanged != null)
                this.visibleChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when the <i>Visible</i> property of the <see cref="DXControl"/> changes.
        /// </summary>
        public event EventHandler VisibleChanged
        {
            add
            {
                this.visibleChanged += value;
            }

            remove
            {
                this.visibleChanged -= value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DXImage"/> to use for the background of the 
        /// <see cref="DXControl"/>.
        /// </summary>
        public DXImage BackgroundImage
        {
            get { return this.backgroundImage; }
            set 
            { 
                this.backgroundImage = value;
                OnBackgroundImageChanged();
            }
        }

        /// <summary>
        /// Occurs when the <i>BackgroundImage</i> property changes.
        /// </summary>
        public event EventHandler BackgroundImageChanged
        {
            add
            {
                this.backgroundImageChanged += value;
            }

            remove
            {
                this.backgroundImageChanged -= value;
            }
        }

        /// <summary>
        /// Raises the <i>BackgroundImageChanged</i> event.
        /// </summary>
        protected virtual void OnBackgroundImageChanged()
        {
            if (this.backgroundImageChanged != null)
                this.backgroundImageChanged(this, EventArgs.Empty);            
        }

		/// <summary>
		/// Gets or sets the font of the control.
		/// </summary>
		public System.Drawing.Font Font
		{
			get { return this.innerFont; }
			set 
			{
				this.innerFont = value;
                if (this.font != null)
                    this.font.Dispose();
                this.font = new Microsoft.DirectX.Direct3D.Font(this.controlHost.Device, this.innerFont);
                OnFontChanged();
			}
		}

        /// <summary>
        /// Occurs when the <i>Font</i> property changes.
        /// </summary>
        public event EventHandler FontChanged
        {
            add
            {
                this.fontChanged += value;
            }

            remove
            {
                this.fontChanged -= value;
            }
        }

        /// <summary>
        /// Fires the <i>FontChanged</i> event.
        /// </summary>
        protected virtual void OnFontChanged()
        {
            if (this.fontChanged != null)
                this.fontChanged(this, EventArgs.Empty);            
        }

        /// <summary>
        /// The DirectX font of the control.
        /// </summary>
		protected Microsoft.DirectX.Direct3D.Font D3DFont
		{
			get { return this.font; }
		}

		/// <summary>
		/// Gets or sets the Size of the control.
		/// </summary>
		public Size Size
		{
			get { return this.size; }
			set 
            {
                if (this.size != value)
                {
                    this.size = value;
                    OnSizeChanged();
                }
            }
		}

        /// <summary>
        /// Occurs when the <i>Size</i> property changes.
        /// </summary>
        public event EventHandler SizeChanged
        {
            add
            {
                this.sizeChanged += value;
            }

            remove
            {
                this.sizeChanged -= value;
            }
        }

        /// <summary>
        /// Fires the <i>SizeChanged</i> event.
        /// </summary>
        protected virtual void OnSizeChanged()
        {
            this.screenBounds = new Rectangle(PointToScreen(Point.Empty), this.size);             
            if (this.sizeChanged != null)
                this.sizeChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// The width of the control.
        /// </summary>
		public int Width
		{
			get { return this.size.Width; }
			set { this.Size = new Size(value, this.size.Height); }
		}

        /// <summary>
        /// The height of the control.
        /// </summary>
		public int Height
		{
			get { return this.size.Height; }
			set { this.Size = new Size(this.size.Width, value); }
		}

        /// <summary>
        /// The coordinates of the control on its' parent.
        /// </summary>
		public Point Location
		{
			get { return this.location; }
			set 
			{ 
				if(this.location != value)
				{
					this.location = value; 
					OnLocationChanged();
				}
			}
		}

        /// <summary>
        /// The foreground color of the control.
        /// </summary>
		public Color ForeColor
		{
			get { return this.foreColor; }
			set {this.foreColor = value; }
		}

        /// <summary>
        /// The background color of the control.
        /// </summary>
		public Color BackColor
		{
			get { return this.backColor; }
			set { this.backColor = value; }
		}

        /// <summary>
        /// The blended gradient color for the <see cref="DXControl"/>.
        /// </summary>
        public Color BackColor2
        {
            get { return this.backColor2; }
            set { this.backColor2 = value; }
        }

        /// <summary>
        /// The direction the gradient should be drawn for the background of the
        /// <see cref="DXControl"/>.
        /// </summary>
        public GradientDirection GradientDirection
        {
            get { return this.gradientDirection; }
            set { this.gradientDirection = value; }
        }
		
        /// <summary>
        /// The control this control belongs to.
        /// </summary>
		public DXControl Parent
		{
			get { return this.parent; }
			set {this.parent = value; }
		}

        /// <summary>
        /// The rectangle displaying this control in relative coordinates.
        /// </summary>
		public Rectangle DisplayRectangle
		{
			get 
			{
				if(this.parent == null)
					return new Rectangle(this.Location,this.Size);
				else
					return new Rectangle(PointToScreen(this.Location), this.Size);
			}
		}

        /// <summary>
        /// The rectangle displayig this control in screen coordinates.
        /// </summary>
        public Rectangle ScreenBounds
        {
            get { return this.screenBounds; }
        }

        /// <summary>
        /// Determines whether this control currently has focus.
        /// </summary>
		public bool Focused
		{
			get { return this.focused; }
			set 
			{ 
				if(this.focused != value)
				{
					this.focused = value;
					if(!this.focused)
						OnLostFocus();
					else
						OnGotFocus();
				}
			}
		}

        /// <summary>
        /// Determines whether the mouse cursor is currently over this control.
        /// </summary>
        protected bool Hovered
        {
            get { return this.hovered; }
        }

        /// <summary>
        /// Sets focus to the control.
        /// </summary>
		public void SetFocus()
		{
            this.Focused = true;
		}

        /// <summary>
        /// The text of the control.
        /// </summary>
		public virtual string Text
		{
			get { return this.text; }
			set 
            {
                if (this.text != value)
                {
                    this.text = value;
                    OnTextChanged();
                }
            }
		}

        /// <summary>
        /// Occurs when the <i>Text</i> property changes.
        /// </summary>
        public event EventHandler TextChanged
        {
            add
            {
                this.textChanged += value;
            }

            remove
            {
                this.textChanged -= value;
            }
        }

        /// <summary>
        /// Fires the <i>TextChanged</i> event.
        /// </summary>
        protected virtual void OnTextChanged()
        {
            if (this.textChanged != null)
                this.textChanged(this, EventArgs.Empty);
        }

		/// <summary>
		/// The Tag associated with the control.
		/// </summary>
		public object Tag
		{
			get { return this.tag; }
			set { this.tag = value; }
		}

		/// <summary>
		/// Converts relative control coordinates to screen coordinates
		/// </summary>
		/// <param name="pt"></param>
		/// <returns></returns>
		public System.Drawing.Point PointToScreen(System.Drawing.Point pt)
		{
			Point ptAbs;
            Point origin = this.controlHost.ScreenBounds.Location;
            int x = this.location.X + origin.X;
            int y = this.location.Y + origin.Y;            

            if (this.parent == null)
                ptAbs = new Point(x + pt.X, y + pt.Y);
            else
                ptAbs = this.parent.PointToScreen(new Point(x + pt.X, y + pt.Y));			
			return ptAbs;
		}

        /// <summary>
        /// Converts screen coordinates to relative client coordinates.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
		public System.Drawing.Point PointToClient(System.Drawing.Point screenPoint)
		{			
            Point origin = Point.Empty;
            int x = origin.X + this.location.X;
            int y = origin.Y + this.location.Y;
            Point clientPoint;
            if (this.parent == null)
                clientPoint = new Point(screenPoint.X - x, screenPoint.Y - y);
            else
                clientPoint = this.parent.PointToClient(new Point(screenPoint.X - x, screenPoint.Y - y));
			return clientPoint;
		}

        /// <summary>
        /// Fires the <i>KeyPress</i> event.
        /// </summary>
        /// <param name="e"></param>
		protected virtual void OnKeyPress(DXKeyboardEventArgs e)
		{
			if(this.keyPress != null)
				this.keyPress(this, e);			
		}

        /// <summary>
        /// Occurs when a key is pressed while the <see cref="DXControl"/> has focus.
        /// </summary>
        public event EventHandler<DXKeyboardEventArgs> KeyPress
        {
            add
            {
                this.keyPress += value;
            }

            remove
            {
                this.keyPress -= value;
            }
        }


        /// <summary>
        /// Determines whether the mouse cursor is currently within the 
        /// bounds of the control on the screen.
        /// </summary>
        /// <param name="mousePos"></param>
        /// <returns></returns>
		protected bool IsMouseOnControl(Point mousePos)
		{
			Point realPt = PointToScreen(Point.Empty);
			
			if((mousePos.X >= realPt.X) &&
				(mousePos.X <= realPt.X + this.size.Width)  &&
				(mousePos.Y >= realPt.Y) &&
				(mousePos.Y <= realPt.Y + this.size.Height))
				return true;
			else
				return false;
		}

        /// <summary>
        /// Fires the <i>MouseMove</i> event.
        /// </summary>
        /// <param name="mea"></param>
		protected virtual void OnMouseMove(DXMouseEventArgs mea)
		{
			this.hovered = true;
			if(this.mouseMove != null)
				this.mouseMove(this, mea);			
		}

        /// <summary>
        /// Occurs when the mouse cursor moves over the <see cref="DXControl"/>.
        /// </summary>
        public event EventHandler<DXMouseEventArgs> MouseMove
        {
            add
            {
                this.mouseMove += value;
            }

            remove
            {
                this.mouseMove -= value;
            }
        }

        /// <summary>
        /// Fires the <i>MouseDown</i> event.
        /// </summary>
        /// <param name="mea"></param>
		protected virtual void OnMouseDown(DXMouseEventArgs mea)
		{
			this.hovered = true;
            this.controlClicked = true;
			if(this.mouseDown != null)			
				this.mouseDown(this, mea);			
			OnGotFocus();
		}

        /// <summary>
        /// Occurs when a mouse button is clicked on the <see cref="DXControl"/>.
        /// </summary>
        public event EventHandler<DXMouseEventArgs> MouseDown
        {
            add
            {
                this.mouseDown += value;
            }

            remove
            {
                this.mouseDown -= value;
            }
        }

        /// <summary>
        /// Fires the <i>MouseUp</i> event.
        /// </summary>
        /// <param name="e"></param>
		protected virtual void OnMouseUp(DXMouseEventArgs mea)
		{
			if(this.mouseUp != null)
				this.mouseUp(this, mea);			
		}

        /// <summary>
        /// Occurs when a mouse button is released while the mouse cursor is hovered 
        /// over the <see cref="DXControl"/>.
        /// </summary>
        public event EventHandler<DXMouseEventArgs> MouseUp
        {
            add
            {
                this.mouseUp += value;
            }

            remove
            {
                this.mouseUp -= value;
            }
        }

        /// <summary>
        /// Fires the <i>MouseLeave</i> event.
        /// </summary>        
		protected virtual void OnMouseLeave(DXMouseEventArgs e)
		{
			this.hovered = false;
			if(this.mouseLeave != null)
				this.mouseLeave(this, e);			
		}

        /// <summary>
        /// Occurs when the mouse cursor leaves the area bound by the <see cref="DXControl"/>.
        /// </summary>
        public event EventHandler<DXMouseEventArgs> MouseLeave
        {
            add
            {
                this.mouseLeave += value;
            }

            remove
            {
                this.mouseLeave -= value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="DXControl"/> is clicked.
        /// </summary>
        public event EventHandler Click
        {
            add
            {
                this.click += value;
            }

            remove
            {
                this.click -= value;
            }
        }

        /// <summary>
        /// Fires the <i>Click</i> event.
        /// </summary>
        protected virtual void OnClick()
        {
            if (this.click != null)
                this.click(this, EventArgs.Empty);
        }

        /// <summary>
        /// Fires the <i>GotFocus</i> event.
        /// </summary>
		protected virtual void OnGotFocus()
		{
			this.focused = true;
			if(this.gotFocus != null)
				this.gotFocus(this, EventArgs.Empty);			
		}

        /// <summary>
        /// Occurs when the <see cref="DXControl"/> becomes the focuses control on the screen.
        /// </summary>
        public event EventHandler GotFocus
        {
            add
            {
                this.gotFocus += value;
            }

            remove
            {
                this.gotFocus -= value;
            }
        }

        /// <summary>
        /// Fires the <i>LostFocus</i> event.
        /// </summary>        
		protected virtual void OnLostFocus()
		{
			this.focused = false;
			if(this.lostFocus != null)
				this.lostFocus(this, EventArgs.Empty);
		}

        /// <summary>
        /// Occurs when the <see cref="DXControl"/> stops being the control with focus 
        /// on the screen.
        /// </summary>
        public event EventHandler LostFocus
        {
            add
            {
                this.lostFocus += value;
            }

            remove
            {
                this.lostFocus -= value;
            }
        }

        /// <summary>
        /// Fires the <i>LocationChanged</i> event.
        /// </summary>
        /// <param name="e"></param>
		protected virtual void OnLocationChanged()
		{
            this.screenBounds = new Rectangle(PointToScreen(Point.Empty), this.size);             
			if(this.locationChanged != null)			
				this.locationChanged(this, EventArgs.Empty);
		}

        /// <summary>
        /// Occurs when the <i>Location</i> property of the control changes.
        /// </summary>
		public event EventHandler LocationChanged
		{
			add
			{
				this.locationChanged += value; 
			}

			remove
			{
				this.locationChanged -= value; 
			}
		}        

        /// <summary>
        /// Refreshes the control.
        /// </summary>
		public virtual void Render(RenderEventArgs e)
		{
            this.screenBounds = new Rectangle(PointToScreen(Point.Empty), this.size);
            if (this.backgroundImage == null)
            {
                if (this.vertexBuffer == null)
                    this.vertexBuffer = CustomPainters.CreateColoredBuffer(e.ControlHost.Device);
                CustomPainters.PaintColoredRectangle(e.ControlHost.Device, this.screenBounds, this.vertexBuffer, this.backColor, this.backColor2, GradientDirection.Vertical);
            }
            else
            {
                if (this.vertexBuffer == null)
                    this.vertexBuffer = CustomPainters.CreateTexturedBuffer(e.ControlHost.Device);
                CustomPainters.PaintTexturedRectangle(e.ControlHost.Device, this.screenBounds, this.vertexBuffer, this.backgroundImage.Texture);
            }           
		}

	}	
}
