using System;

namespace McKnight.Similization.Core
{
	/// <summary>
	/// Base class for the command pattern Command Object.
	/// </summary>
	public abstract class Command : NamedObject
	{				
		private bool enabled = true;		
		private event EventHandler enabledChanged;
		private event EventHandler invoking;
		private event EventHandler invoked;
		private event EventHandler canceled;

		/// <summary>
		/// Determines whether this command is enabled.
		/// </summary>
		public bool Enabled
		{
			get { return this.enabled; }
			set
			{
				if(this.enabled != value)
				{
					this.enabled = value;
					OnEnabledChanged();
				}
			}
		}

		/// <summary>
		/// Occurs when the value of the <i>Enabled</i> property changes.
		/// </summary>
		public event EventHandler EnabledChanged
		{
			add
			{
				this.enabledChanged += value;
			}

			remove
			{
				this.enabledChanged -= value;
			}
		}

		/// <summary>
		/// Fires the <i>EnabledChanged</i> event.
		/// </summary>
		protected virtual void OnEnabledChanged()
		{
			if(this.enabledChanged != null)
				this.enabledChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public abstract void Invoke();

		/// <summary>
		/// Event that fires when <i>Invoke</i> is first called.
		/// </summary>
		public event EventHandler Invoking
		{
			add
			{
				this.invoking += value;
			}

			remove
			{
				this.invoking -= value;
			}
		}

		/// <summary>
		/// Fires the <i>Invoking</i> event.
		/// </summary>
		protected virtual void OnInvoking()
		{
			if(this.enabled == false)
				throw new InvalidOperationException(McKnight_Similization_Core.CommandDisabled);

			if(this.invoking != null)
				this.invoking(this, EventArgs.Empty);
		}

		/// <summary>
		/// Event that fires after the command has been invoked;
		/// </summary>
		public event EventHandler Invoked
		{
			add
			{
				this.invoked += value;
			}

			remove
			{
				this.invoked -= value; 
			}
		}

		/// <summary>
		/// Fires the <i>Invoked</i> event.
		/// </summary>
		protected virtual void OnInvoked()
		{
			if(this.invoked != null)
			{
				this.invoked(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Occurs when the <see cref="Command"/> is canceled, either by the user, 
		/// or the program.
		/// </summary>
		public event EventHandler Canceled
		{
			add
			{
				this.canceled += value;
			}

			remove
			{
				this.canceled -= value;
			}
		}

		/// <summary>
		/// Fires the <i>Canceled</i> event.
		/// </summary>
		protected virtual void OnCanceled()
		{
			if(this.canceled != null)
				this.canceled(this, EventArgs.Empty);
		}
	}

}
