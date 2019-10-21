using System;
using System.Collections.Generic;
using System.Text;
using LJM.Similization.Client;
using LJM.Similization.Client.DirectX.Controls;
using System.Drawing;

namespace LJM.Similization.Client.DirectX
{
    /// <summary>
    /// DirectX Client implementation of the <see cref="IOptionsControl"/> interface.
    /// </summary>
    public class OptionsWindow : DXWindow, IOptionsControl
    {
        private DXButton btnOK;
        private DXButton btnCancel;
        private DXCheckBox chkShowKilledMessage;
        private DXCheckBox chkWaitAfterTurn;
        private DXLabel lblRuleset;
        private DXTextBox txtRuleset;
        private DXLabel lblTileset;
        private DXTextBox txtTileset;        
        private Font cityNameFont;
        private Color cityNameFontColor;
        private event EventHandler<ControlClosedEventArgs> closed;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsWindow"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        public OptionsWindow(IDirectXControlHost controlHost)
            :base(controlHost)
        {
            InitializeComponent();

            Options options = ClientApplication.Instance.Options;
            this.chkShowKilledMessage.Checked = options.ShowKilledMessage;
            this.chkWaitAfterTurn.Checked = options.WaitAfterTurn;
            this.txtRuleset.Text = options.StartingRulesetPath;
            this.txtTileset.Text = options.TilesetPath;            
        }

        private void InitializeComponent()
        {
            this.Text = DirectXClientResources.OptionsTitle;
            this.BackColor = Color.Green;
            this.BackColor2 = Color.Green;

            //lblRuleset
            lblRuleset = new DXLabel(this.ControlHost, this);
            lblRuleset.Text = DirectXClientResources.RulesetLabel;
            lblRuleset.AutoSize = true;
            lblRuleset.Location = new Point(20, 100);
            this.Controls.Add(lblRuleset);

            //txtRuleset
            txtRuleset = new DXTextBox(this.ControlHost, this);
            txtRuleset.Size = new Size(200, 20);
            txtRuleset.Location = new Point(150, 100);
            this.Controls.Add(txtRuleset);

            //lblTileset
            lblTileset = new DXLabel(this.ControlHost, this);
            lblTileset.Text = DirectXClientResources.TilesetLabel;
            lblTileset.AutoSize = true;
            lblTileset.Location = new Point(20, 130);
            this.Controls.Add(lblTileset);

            //txtTileset
            txtTileset = new DXTextBox(this.ControlHost, this);
            txtTileset.Size = new Size(200, 20);
            txtTileset.Location = new Point(150, 130);
            this.Controls.Add(txtTileset);

            //chkShowKilledMessage
            chkShowKilledMessage = new DXCheckBox(this.ControlHost, this);
            chkShowKilledMessage.Text = "Show Killed Message";
            chkShowKilledMessage.Size = new Size(300, 25);
            chkShowKilledMessage.Location = new Point(20, 160);
            this.Controls.Add(chkShowKilledMessage);

            //chkWaitAfterTurn
            chkWaitAfterTurn = new DXCheckBox(this.ControlHost, this);
            chkWaitAfterTurn.Text = "Wait After Turn";
            chkWaitAfterTurn.Size = new Size(300, 25);
            chkWaitAfterTurn.Location = new Point(20, 190);
            this.Controls.Add(chkWaitAfterTurn);

            //btnOK
            btnOK = new DXButton(this.ControlHost, this);
            btnOK.Text = DirectXClientResources.OK;
            btnOK.Size = new Size(100, 25);
            btnOK.Location = new Point(100, 500);
            btnOK.Click += new EventHandler(OkButtonPressed);
            this.Controls.Add(btnOK);

            //btnCancel
            btnCancel = new DXButton(this.ControlHost, this);
            btnCancel.Text = DirectXClientResources.Cancel;
            btnCancel.Size = new Size(100, 25);
            btnCancel.Location = new Point(220, 500);
            btnCancel.Click += new EventHandler(CancelButtonPressed);
            this.Controls.Add(btnCancel);            
        }

        void CancelButtonPressed(object sender, EventArgs e)
        {
            this.Close();
            OnClosed(new ControlClosedEventArgs(ControlCloseResult.Cancel));
        }

        private void OkButtonPressed(object sender, EventArgs e)
        {
            this.Close();
            OnClosed(new ControlClosedEventArgs(ControlCloseResult.Ok));
        }

        public bool WaitAfterTurn
        {
            get { return chkWaitAfterTurn.Checked; }
        }        

        public bool ShowKilledMessage
        {
            get { return this.chkShowKilledMessage.Checked; }
        }

        public string StartingRulesetPath
        {
            get { return this.txtRuleset.Text; }
        }

        public System.Drawing.Font CityNameFont
        {
            get { return this.cityNameFont; }
        }

        public System.Drawing.Color CityNameFontColor
        {
            get { return this.cityNameFontColor; }
        }

        public string TilesetPath
        {
            get { return this.txtTileset.Text; }
        }        

        public void ShowSimilizationControl()
        {
            this.Show();
        }

        private void OnClosed(ControlClosedEventArgs e)
        {
            if (this.closed != null)
                this.closed(this, e);
        }

        /// <summary>
        /// Occurs when the control is closed.
        /// </summary>
        public event EventHandler<ControlClosedEventArgs> Closed
        {
            add
            {
                this.closed += value;
            }

            remove
            {
                this.closed -= value;
            }
        }
    }
}
