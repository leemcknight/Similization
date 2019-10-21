using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace McKnight.SimilizationRulesEditor
{
	public enum FormState
	{
		Display,
		PendingChange,
		Edit
	}

	/// <summary>
	/// Base Class for all Editor user controls
	/// </summary>
	public class EditorUserControl : UserControl
	{
		private FormState formState;
		private event EventHandler formStateChanged;
		private event EventHandler allowAddChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="EditorUserControl"/> class.
		/// </summary>
		public EditorUserControl()
		{
			InitializeComponent();
		}

		public FormState FormState
		{
			get { return this.formState; }
			set 
			{ 
				if(this.formState != value)
				{
					this.formState = value; 
					OnFormStateChanged();
				}
			}
		}

		private bool allowAdd;

		public bool AllowAdd
		{
			get { return this.allowAdd; }
			set
			{
				if(this.allowAdd != value)
				{
					this.allowAdd = value;
					OnAllowAddChanged();
				}
			}
		}

		public virtual void ShowData()
		{
		}

		public virtual void AddNew()
		{
		}

		public virtual void Delete()
		{
		}

		public virtual void SaveData()
		{
		}

		public virtual void UndoChanges()
		{
		}

		public event EventHandler FormStateChanged
		{
			add
			{
				this.formStateChanged += value; 
			}
			remove
			{
				this.formStateChanged -= value;
			}
		}

		protected virtual void OnFormStateChanged()
		{
			if(this.formStateChanged != null)
			{
				this.formStateChanged(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Occurs when the <i>AllowAdd</is> property changes.
		/// </summary>
		public event EventHandler AllowAddChanged
		{
			add
			{
				this.allowAddChanged += value;
			}

			remove
			{
				this.allowAddChanged -= value; 
			}
		}

		/// <summary>
		/// Fires the <c>AllowAddChanged</c> event.
		/// </summary>
		protected virtual void OnAllowAddChanged()
		{
			if(this.allowAddChanged != null)
			{
				this.allowAddChanged(this, EventArgs.Empty);
			}
		}

		private void InitializeComponent()
		{
			// 
			// EditorUserControl
			// 
			this.AutoScroll = true;
			this.Name = "EditorUserControl";
			this.Size = new System.Drawing.Size(480, 360);

		}

	}
}
