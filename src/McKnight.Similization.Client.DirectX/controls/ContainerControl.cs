using System;
using System.Collections;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Drawing;
using Microsoft.DirectX;

namespace LJM.Similization.Client.DirectX.Controls
{
    /// <summary>
    /// Class representing a DirectX control that can be a container 
    /// for child controls.
    /// </summary>
	public class DXContainerControl : DXControl
	{
        private bool disposed;
		private DXControlCollection controls;		

        /// <summary>
        /// Initializes a new instance of the <see cref="DXContainerControl"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public DXContainerControl(IDirectXControlHost controlHost) : base(controlHost)
		{
            this.controls = new DXControlCollection();		
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="DXContainerControl"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public DXContainerControl(IDirectXControlHost controlHost, DXControl parent) : base(controlHost, parent)
		{
			this.controls = new DXControlCollection();			
		}

        /// <summary>
        /// Releases all resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed && disposing)
                {
                    foreach (DXControl ctl in this.Controls)
                        ctl.Dispose();
                }                
            }
            finally
            {
                this.disposed = true;
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Shows the control.
        /// </summary>
		public override void Show()
		{			
			foreach(DXControl ctl in this.controls)						
				ctl.Show();												
			base.Show();
		}

        /// <summary>
        /// Hides the <see cref="DXContainerControl"/>.
        /// </summary>
		public override void Hide()
		{
			foreach(DXControl ctl in this.controls)			
				ctl.Hide();			
			base.Hide();
		}

        protected override void OnLocationChanged()
        {
            base.OnLocationChanged();
            foreach (DXControl control in this.controls)
                control.Location = control.Location;
        }

		
        /// <summary>
        /// Renders the <see cref="DXContainerControl"/>.
        /// </summary>
		public override void Render(RenderEventArgs e)
		{			
			base.Render(e);
			foreach(DXControl control in this.Controls)
			{
				if(control.Visible)				
				    control.Render(e);									
			}	
		}
		
        /// <summary>
        /// A list of all the <see cref="DXControl"/> objects belonging 
        /// to this <see cref="DXContainerControl"/>.
        /// </summary>
		public DXControlCollection Controls
		{
			get { return this.controls; }
		}
	}
}
