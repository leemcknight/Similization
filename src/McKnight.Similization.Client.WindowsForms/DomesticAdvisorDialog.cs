using System;
using System.Drawing;
using System.Collections;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Server;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// This is the Domestic Advisor Dialog for the Windows Client version of
	/// Similization.
	/// </summary>
	public class DomesticAdvisorDialog : System.Windows.Forms.UserControl, IDomesticAdvisorControl
	{
		private System.Windows.Forms.ListView lvwCity;
		private System.Windows.Forms.ColumnHeader hdrCityName;
		private System.Windows.Forms.ColumnHeader hdrPopulation;
		private System.Windows.Forms.ColumnHeader hdrProducing;
		private System.Windows.Forms.Label lblInterestIncome;
		private System.Windows.Forms.Label lblInterestHeader;
		private System.Windows.Forms.Label lblNetGain;
		private System.Windows.Forms.Label lblTotalExpensePerTurn;
		private System.Windows.Forms.Label lblCivExpense;
		private System.Windows.Forms.Label lblOtherCivExpenseHeader;
		private System.Windows.Forms.Label lblUnitExpense;
		private System.Windows.Forms.Label lblUnitCostHeader;
		private System.Windows.Forms.Label lblMaintenanceExpense;
		private System.Windows.Forms.Label lblCorruptionExpense;
		private System.Windows.Forms.Label lblEntertainmentExpense;
		private System.Windows.Forms.Label lblScienceExpense;
		private System.Windows.Forms.Label lblMaintenanceHeader;
		private System.Windows.Forms.Label lblCorruptionHeader;
		private System.Windows.Forms.Label lblEntertainmentHeader;
		private System.Windows.Forms.Label lblScienceHeader;
		private System.Windows.Forms.Label lblTotalIncome;
		private System.Windows.Forms.Label lblOtherCivs;
		private System.Windows.Forms.Label lblOtherCivsHeader;
		private System.Windows.Forms.Label lblTaxIncome;
		private System.Windows.Forms.Label lblTaxmenIncomeHeader;
		private System.Windows.Forms.Label lblTreasury;
		private System.Windows.Forms.Label lblNetGainHeader;
		private System.Windows.Forms.Label lblCityIncomeHeader;
		private System.Windows.Forms.Label lblIncomeTotalHeader;
		private System.Windows.Forms.Label lblExpenseHeader;
		private System.Windows.Forms.Label lblTreasuryAmount;
		private System.Windows.Forms.Label lblCityIncome;
		private System.Windows.Forms.Label lblIncomeExpensesHeader;
		private System.Windows.Forms.Panel pnlOtherInformation;
        private System.Windows.Forms.Button btnMore;
		private System.Windows.Forms.Label lblAdvice;
		private System.Windows.Forms.Label lblLuxuryRatio;
		private System.Windows.Forms.Label lblSciencePercentage;
		private System.Windows.Forms.TrackBar trkBarScience;
		private System.Windows.Forms.Label lblEntertainment;
		private System.Windows.Forms.Label lblTechAdvanceCompletion;
		private System.Windows.Forms.TrackBar trkBarEntertainment;
		private System.Windows.Forms.Label lblResearchingHeader;
		private System.Windows.Forms.Label lblScienceRatio;
		private System.Windows.Forms.Label lblTechnology;
		private System.Windows.Forms.Label lblScientificResearchHeader;
		private System.Windows.Forms.Label lblAdviceCommentsHeader;
		private System.Windows.Forms.ComboBox cboMobilization;
		private System.Windows.Forms.ComboBox cboGovernment;
		private System.Windows.Forms.Label lblMobilization;
		private System.Windows.Forms.Label lblGovernment;
		private System.Windows.Forms.Label lblOtherInfoHeader;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel pnlIncomeExpenses;
		private System.Windows.Forms.Panel pnlAdviceComments;
        private PictureBox pbAdvice;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="DomesticAdvisorDialog"/> class.
		/// </summary>
		public DomesticAdvisorDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			ClientApplication client = ClientApplication.Instance;
			Country player = client.Player;
			trkBarScience.Value = player.SciencePercentage/10;
			lblSciencePercentage.Text = Convert.ToString(player.SciencePercentage, CultureInfo.CurrentCulture) + "%";
			trkBarEntertainment.Value = player.EntertainmentPercentage/10;
			lblEntertainment.Text = Convert.ToString(player.EntertainmentPercentage, CultureInfo.CurrentCulture) + "%";
			FillBalanceSheet();
			PopulateCityList();
			FillOtherInfo();			
		}

		private void FillBalanceSheet()
		{
			Country player = ClientApplication.Instance.Player;
            
			if(player.ResearchedTechnology != null)
			{
				lblTechnology.Text = player.ResearchedTechnology.Name;
			}

            string format = ClientResources.GetString("turnsUntilCompleteMinimal");
            string text = string.Format(CultureInfo.CurrentCulture, format, player.CalculateTurnsUntilTechnologyAdvance());
            lblTechAdvanceCompletion.Text = text;
            lblTreasuryAmount.Text = GenerateGoldAmountString(player.Gold);
            lblCityIncome.Text = GenerateGoldAmountString(player.CalculateIncomePerTurnFromCities());
			lblTaxIncome.Text = GenerateGoldAmountString(player.CalculateIncomePerTurnFromTaxmen());
			lblTotalIncome.Text = GenerateGoldAmountString(player.CalculateTotalIncomePerTurn());
			lblScienceExpense.Text = GenerateGoldAmountString(player.CalculateScienceExpensePerTurn());
            lblMaintenanceExpense.Text = GenerateGoldAmountString(player.CalculateMaintenanceExpensePerTurn());
            lblTotalExpensePerTurn.Text = GenerateGoldAmountString(player.CalculateTotalExpensePerTurn());
			
			int netGain = player.CalculateNetProfitPerTurn();
			lblNetGain.ForeColor = netGain >= 0 ? Color.Black : Color.Red;
			lblNetGain.Text = GenerateGoldAmountString(netGain);
		}

        private static string GenerateGoldAmountString(int goldAmount)
        {
            string format = ClientResources.GetString("goldAmount");
            string goldString = Convert.ToString(goldAmount, CultureInfo.CurrentCulture);
            return string.Format(CultureInfo.CurrentCulture, format, goldString);
        }

		private void PopulateCityList()
		{
			Country player = ClientApplication.Instance.Player;
			ListViewItem cityItem;
			foreach(City city in player.Cities)
			{
				cityItem = new ListViewItem(city.Name);
				lvwCity.Items.Add(cityItem);
				cityItem.SubItems.Add(city.Population.ToString(CultureInfo.CurrentCulture));
				cityItem.SubItems.Add(city.NextImprovement.Name);
			}
		}

		private void FillOtherInfo()
		{
			ClientApplication client = ClientApplication.Instance;
			cboGovernment.Items.Add(client.Player.Government);
			foreach(Government gov in client.Player.AvailableGovernments)			
				cboGovernment.Items.Add(gov);			

			cboGovernment.SelectedItem = client.Player.Government;
		}

		/// <summary>
		/// Shows the Similization Control.
		/// </summary>
		public void ShowSimilizationControl()
		{
			this.ParentForm.ShowDialog();
		}

		/// <summary>
		/// The text that the domestic advisor is telling the player.
		/// </summary>
		public string AdvisorText
		{
			get { return lblAdvice.Text; }
			set { lblAdvice.Text = value; }
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DomesticAdvisorDialog));
            this.lvwCity = new System.Windows.Forms.ListView();
            this.hdrCityName = new System.Windows.Forms.ColumnHeader();
            this.hdrPopulation = new System.Windows.Forms.ColumnHeader();
            this.hdrProducing = new System.Windows.Forms.ColumnHeader();
            this.lblInterestIncome = new System.Windows.Forms.Label();
            this.lblInterestHeader = new System.Windows.Forms.Label();
            this.lblNetGain = new System.Windows.Forms.Label();
            this.lblTotalExpensePerTurn = new System.Windows.Forms.Label();
            this.lblCivExpense = new System.Windows.Forms.Label();
            this.lblOtherCivExpenseHeader = new System.Windows.Forms.Label();
            this.lblUnitExpense = new System.Windows.Forms.Label();
            this.lblUnitCostHeader = new System.Windows.Forms.Label();
            this.lblMaintenanceExpense = new System.Windows.Forms.Label();
            this.lblCorruptionExpense = new System.Windows.Forms.Label();
            this.lblEntertainmentExpense = new System.Windows.Forms.Label();
            this.lblScienceExpense = new System.Windows.Forms.Label();
            this.lblMaintenanceHeader = new System.Windows.Forms.Label();
            this.lblCorruptionHeader = new System.Windows.Forms.Label();
            this.lblEntertainmentHeader = new System.Windows.Forms.Label();
            this.lblScienceHeader = new System.Windows.Forms.Label();
            this.lblTotalIncome = new System.Windows.Forms.Label();
            this.lblOtherCivs = new System.Windows.Forms.Label();
            this.lblOtherCivsHeader = new System.Windows.Forms.Label();
            this.lblTaxIncome = new System.Windows.Forms.Label();
            this.lblTaxmenIncomeHeader = new System.Windows.Forms.Label();
            this.lblTreasury = new System.Windows.Forms.Label();
            this.lblNetGainHeader = new System.Windows.Forms.Label();
            this.lblCityIncomeHeader = new System.Windows.Forms.Label();
            this.lblIncomeTotalHeader = new System.Windows.Forms.Label();
            this.lblExpenseHeader = new System.Windows.Forms.Label();
            this.lblTreasuryAmount = new System.Windows.Forms.Label();
            this.lblCityIncome = new System.Windows.Forms.Label();
            this.lblIncomeExpensesHeader = new System.Windows.Forms.Label();
            this.pnlOtherInformation = new System.Windows.Forms.Panel();
            this.cboMobilization = new System.Windows.Forms.ComboBox();
            this.cboGovernment = new System.Windows.Forms.ComboBox();
            this.lblMobilization = new System.Windows.Forms.Label();
            this.lblGovernment = new System.Windows.Forms.Label();
            this.btnMore = new System.Windows.Forms.Button();
            this.lblAdvice = new System.Windows.Forms.Label();
            this.lblLuxuryRatio = new System.Windows.Forms.Label();
            this.lblSciencePercentage = new System.Windows.Forms.Label();
            this.trkBarScience = new System.Windows.Forms.TrackBar();
            this.lblEntertainment = new System.Windows.Forms.Label();
            this.lblTechAdvanceCompletion = new System.Windows.Forms.Label();
            this.trkBarEntertainment = new System.Windows.Forms.TrackBar();
            this.lblResearchingHeader = new System.Windows.Forms.Label();
            this.lblScienceRatio = new System.Windows.Forms.Label();
            this.lblTechnology = new System.Windows.Forms.Label();
            this.lblScientificResearchHeader = new System.Windows.Forms.Label();
            this.lblAdviceCommentsHeader = new System.Windows.Forms.Label();
            this.lblOtherInfoHeader = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlIncomeExpenses = new System.Windows.Forms.Panel();
            this.pnlAdviceComments = new System.Windows.Forms.Panel();
            this.pbAdvice = new System.Windows.Forms.PictureBox();
            this.pnlOtherInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarScience)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarEntertainment)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlIncomeExpenses.SuspendLayout();
            this.pnlAdviceComments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdvice)).BeginInit();
            this.SuspendLayout();
            // 
            // lvwCity
            // 
            this.lvwCity.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrCityName,
            this.hdrPopulation,
            this.hdrProducing});
            resources.ApplyResources(this.lvwCity, "lvwCity");
            this.lvwCity.Name = "lvwCity";
            this.lvwCity.View = System.Windows.Forms.View.Details;
            // 
            // hdrCityName
            // 
            resources.ApplyResources(this.hdrCityName, "hdrCityName");
            // 
            // hdrPopulation
            // 
            resources.ApplyResources(this.hdrPopulation, "hdrPopulation");
            // 
            // hdrProducing
            // 
            resources.ApplyResources(this.hdrProducing, "hdrProducing");
            // 
            // lblInterestIncome
            // 
            this.lblInterestIncome.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lblInterestIncome, "lblInterestIncome");
            this.lblInterestIncome.Name = "lblInterestIncome";
            // 
            // lblInterestHeader
            // 
            this.lblInterestHeader.ForeColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.lblInterestHeader, "lblInterestHeader");
            this.lblInterestHeader.Name = "lblInterestHeader";
            // 
            // lblNetGain
            // 
            this.lblNetGain.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lblNetGain, "lblNetGain");
            this.lblNetGain.Name = "lblNetGain";
            // 
            // lblTotalExpensePerTurn
            // 
            resources.ApplyResources(this.lblTotalExpensePerTurn, "lblTotalExpensePerTurn");
            this.lblTotalExpensePerTurn.ForeColor = System.Drawing.Color.Red;
            this.lblTotalExpensePerTurn.Name = "lblTotalExpensePerTurn";
            // 
            // lblCivExpense
            // 
            this.lblCivExpense.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.lblCivExpense, "lblCivExpense");
            this.lblCivExpense.Name = "lblCivExpense";
            // 
            // lblOtherCivExpenseHeader
            // 
            resources.ApplyResources(this.lblOtherCivExpenseHeader, "lblOtherCivExpenseHeader");
            this.lblOtherCivExpenseHeader.Name = "lblOtherCivExpenseHeader";
            // 
            // lblUnitExpense
            // 
            this.lblUnitExpense.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.lblUnitExpense, "lblUnitExpense");
            this.lblUnitExpense.Name = "lblUnitExpense";
            // 
            // lblUnitCostHeader
            // 
            resources.ApplyResources(this.lblUnitCostHeader, "lblUnitCostHeader");
            this.lblUnitCostHeader.Name = "lblUnitCostHeader";
            // 
            // lblMaintenanceExpense
            // 
            this.lblMaintenanceExpense.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.lblMaintenanceExpense, "lblMaintenanceExpense");
            this.lblMaintenanceExpense.Name = "lblMaintenanceExpense";
            // 
            // lblCorruptionExpense
            // 
            this.lblCorruptionExpense.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.lblCorruptionExpense, "lblCorruptionExpense");
            this.lblCorruptionExpense.Name = "lblCorruptionExpense";
            // 
            // lblEntertainmentExpense
            // 
            this.lblEntertainmentExpense.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.lblEntertainmentExpense, "lblEntertainmentExpense");
            this.lblEntertainmentExpense.Name = "lblEntertainmentExpense";
            // 
            // lblScienceExpense
            // 
            this.lblScienceExpense.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.lblScienceExpense, "lblScienceExpense");
            this.lblScienceExpense.Name = "lblScienceExpense";
            // 
            // lblMaintenanceHeader
            // 
            resources.ApplyResources(this.lblMaintenanceHeader, "lblMaintenanceHeader");
            this.lblMaintenanceHeader.Name = "lblMaintenanceHeader";
            // 
            // lblCorruptionHeader
            // 
            resources.ApplyResources(this.lblCorruptionHeader, "lblCorruptionHeader");
            this.lblCorruptionHeader.Name = "lblCorruptionHeader";
            // 
            // lblEntertainmentHeader
            // 
            resources.ApplyResources(this.lblEntertainmentHeader, "lblEntertainmentHeader");
            this.lblEntertainmentHeader.Name = "lblEntertainmentHeader";
            // 
            // lblScienceHeader
            // 
            resources.ApplyResources(this.lblScienceHeader, "lblScienceHeader");
            this.lblScienceHeader.Name = "lblScienceHeader";
            // 
            // lblTotalIncome
            // 
            resources.ApplyResources(this.lblTotalIncome, "lblTotalIncome");
            this.lblTotalIncome.ForeColor = System.Drawing.Color.Black;
            this.lblTotalIncome.Name = "lblTotalIncome";
            // 
            // lblOtherCivs
            // 
            this.lblOtherCivs.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lblOtherCivs, "lblOtherCivs");
            this.lblOtherCivs.Name = "lblOtherCivs";
            // 
            // lblOtherCivsHeader
            // 
            this.lblOtherCivsHeader.ForeColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.lblOtherCivsHeader, "lblOtherCivsHeader");
            this.lblOtherCivsHeader.Name = "lblOtherCivsHeader";
            // 
            // lblTaxIncome
            // 
            this.lblTaxIncome.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lblTaxIncome, "lblTaxIncome");
            this.lblTaxIncome.Name = "lblTaxIncome";
            // 
            // lblTaxmenIncomeHeader
            // 
            this.lblTaxmenIncomeHeader.ForeColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.lblTaxmenIncomeHeader, "lblTaxmenIncomeHeader");
            this.lblTaxmenIncomeHeader.Name = "lblTaxmenIncomeHeader";
            // 
            // lblTreasury
            // 
            resources.ApplyResources(this.lblTreasury, "lblTreasury");
            this.lblTreasury.Name = "lblTreasury";
            // 
            // lblNetGainHeader
            // 
            resources.ApplyResources(this.lblNetGainHeader, "lblNetGainHeader");
            this.lblNetGainHeader.Name = "lblNetGainHeader";
            // 
            // lblCityIncomeHeader
            // 
            this.lblCityIncomeHeader.ForeColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.lblCityIncomeHeader, "lblCityIncomeHeader");
            this.lblCityIncomeHeader.Name = "lblCityIncomeHeader";
            // 
            // lblIncomeTotalHeader
            // 
            resources.ApplyResources(this.lblIncomeTotalHeader, "lblIncomeTotalHeader");
            this.lblIncomeTotalHeader.Name = "lblIncomeTotalHeader";
            // 
            // lblExpenseHeader
            // 
            resources.ApplyResources(this.lblExpenseHeader, "lblExpenseHeader");
            this.lblExpenseHeader.Name = "lblExpenseHeader";
            // 
            // lblTreasuryAmount
            // 
            this.lblTreasuryAmount.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lblTreasuryAmount, "lblTreasuryAmount");
            this.lblTreasuryAmount.Name = "lblTreasuryAmount";
            // 
            // lblCityIncome
            // 
            this.lblCityIncome.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lblCityIncome, "lblCityIncome");
            this.lblCityIncome.Name = "lblCityIncome";
            // 
            // lblIncomeExpensesHeader
            // 
            resources.ApplyResources(this.lblIncomeExpensesHeader, "lblIncomeExpensesHeader");
            this.lblIncomeExpensesHeader.Name = "lblIncomeExpensesHeader";
            // 
            // pnlOtherInformation
            // 
            this.pnlOtherInformation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlOtherInformation.Controls.Add(this.cboMobilization);
            this.pnlOtherInformation.Controls.Add(this.cboGovernment);
            this.pnlOtherInformation.Controls.Add(this.lblMobilization);
            this.pnlOtherInformation.Controls.Add(this.lblGovernment);
            resources.ApplyResources(this.pnlOtherInformation, "pnlOtherInformation");
            this.pnlOtherInformation.Name = "pnlOtherInformation";
            // 
            // cboMobilization
            // 
            this.cboMobilization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMobilization.FormattingEnabled = true;
            resources.ApplyResources(this.cboMobilization, "cboMobilization");
            this.cboMobilization.Name = "cboMobilization";
            // 
            // cboGovernment
            // 
            this.cboGovernment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGovernment.FormattingEnabled = true;
            resources.ApplyResources(this.cboGovernment, "cboGovernment");
            this.cboGovernment.Name = "cboGovernment";
            // 
            // lblMobilization
            // 
            this.lblMobilization.ForeColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.lblMobilization, "lblMobilization");
            this.lblMobilization.Name = "lblMobilization";
            // 
            // lblGovernment
            // 
            this.lblGovernment.ForeColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.lblGovernment, "lblGovernment");
            this.lblGovernment.Name = "lblGovernment";
            // 
            // btnMore
            // 
            resources.ApplyResources(this.btnMore, "btnMore");
            this.btnMore.Name = "btnMore";
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // lblAdvice
            // 
            resources.ApplyResources(this.lblAdvice, "lblAdvice");
            this.lblAdvice.Name = "lblAdvice";
            // 
            // lblLuxuryRatio
            // 
            resources.ApplyResources(this.lblLuxuryRatio, "lblLuxuryRatio");
            this.lblLuxuryRatio.Name = "lblLuxuryRatio";
            // 
            // lblSciencePercentage
            // 
            resources.ApplyResources(this.lblSciencePercentage, "lblSciencePercentage");
            this.lblSciencePercentage.Name = "lblSciencePercentage";
            // 
            // trkBarScience
            // 
            resources.ApplyResources(this.trkBarScience, "trkBarScience");
            this.trkBarScience.Name = "trkBarScience";
            this.trkBarScience.Value = 5;
            // 
            // lblEntertainment
            // 
            resources.ApplyResources(this.lblEntertainment, "lblEntertainment");
            this.lblEntertainment.Name = "lblEntertainment";
            // 
            // lblTechAdvanceCompletion
            // 
            resources.ApplyResources(this.lblTechAdvanceCompletion, "lblTechAdvanceCompletion");
            this.lblTechAdvanceCompletion.Name = "lblTechAdvanceCompletion";
            // 
            // trkBarEntertainment
            // 
            resources.ApplyResources(this.trkBarEntertainment, "trkBarEntertainment");
            this.trkBarEntertainment.Name = "trkBarEntertainment";
            // 
            // lblResearchingHeader
            // 
            resources.ApplyResources(this.lblResearchingHeader, "lblResearchingHeader");
            this.lblResearchingHeader.Name = "lblResearchingHeader";
            // 
            // lblScienceRatio
            // 
            resources.ApplyResources(this.lblScienceRatio, "lblScienceRatio");
            this.lblScienceRatio.Name = "lblScienceRatio";
            // 
            // lblTechnology
            // 
            resources.ApplyResources(this.lblTechnology, "lblTechnology");
            this.lblTechnology.Name = "lblTechnology";
            // 
            // lblScientificResearchHeader
            // 
            resources.ApplyResources(this.lblScientificResearchHeader, "lblScientificResearchHeader");
            this.lblScientificResearchHeader.Name = "lblScientificResearchHeader";
            // 
            // lblAdviceCommentsHeader
            // 
            resources.ApplyResources(this.lblAdviceCommentsHeader, "lblAdviceCommentsHeader");
            this.lblAdviceCommentsHeader.Name = "lblAdviceCommentsHeader";
            // 
            // lblOtherInfoHeader
            // 
            this.lblOtherInfoHeader.BackColor = System.Drawing.SystemColors.Control;
            this.lblOtherInfoHeader.ForeColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.lblOtherInfoHeader, "lblOtherInfoHeader");
            this.lblOtherInfoHeader.Name = "lblOtherInfoHeader";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.lblLuxuryRatio);
            this.panel2.Controls.Add(this.lblSciencePercentage);
            this.panel2.Controls.Add(this.trkBarScience);
            this.panel2.Controls.Add(this.lblEntertainment);
            this.panel2.Controls.Add(this.lblTechAdvanceCompletion);
            this.panel2.Controls.Add(this.trkBarEntertainment);
            this.panel2.Controls.Add(this.lblResearchingHeader);
            this.panel2.Controls.Add(this.lblScienceRatio);
            this.panel2.Controls.Add(this.lblTechnology);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // pnlIncomeExpenses
            // 
            this.pnlIncomeExpenses.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlIncomeExpenses.Controls.Add(this.lblInterestIncome);
            this.pnlIncomeExpenses.Controls.Add(this.lblInterestHeader);
            this.pnlIncomeExpenses.Controls.Add(this.lblNetGain);
            this.pnlIncomeExpenses.Controls.Add(this.lblTotalExpensePerTurn);
            this.pnlIncomeExpenses.Controls.Add(this.lblCivExpense);
            this.pnlIncomeExpenses.Controls.Add(this.lblOtherCivExpenseHeader);
            this.pnlIncomeExpenses.Controls.Add(this.lblUnitExpense);
            this.pnlIncomeExpenses.Controls.Add(this.lblUnitCostHeader);
            this.pnlIncomeExpenses.Controls.Add(this.lblMaintenanceExpense);
            this.pnlIncomeExpenses.Controls.Add(this.lblCorruptionExpense);
            this.pnlIncomeExpenses.Controls.Add(this.lblEntertainmentExpense);
            this.pnlIncomeExpenses.Controls.Add(this.lblEntertainmentHeader);
            this.pnlIncomeExpenses.Controls.Add(this.lblScienceHeader);
            this.pnlIncomeExpenses.Controls.Add(this.lblTotalIncome);
            this.pnlIncomeExpenses.Controls.Add(this.lblOtherCivs);
            this.pnlIncomeExpenses.Controls.Add(this.lblOtherCivsHeader);
            this.pnlIncomeExpenses.Controls.Add(this.lblTaxIncome);
            this.pnlIncomeExpenses.Controls.Add(this.lblScienceExpense);
            this.pnlIncomeExpenses.Controls.Add(this.lblMaintenanceHeader);
            this.pnlIncomeExpenses.Controls.Add(this.lblCorruptionHeader);
            this.pnlIncomeExpenses.Controls.Add(this.lblNetGainHeader);
            this.pnlIncomeExpenses.Controls.Add(this.lblTreasury);
            this.pnlIncomeExpenses.Controls.Add(this.lblTaxmenIncomeHeader);
            this.pnlIncomeExpenses.Controls.Add(this.lblCityIncomeHeader);
            this.pnlIncomeExpenses.Controls.Add(this.lblIncomeTotalHeader);
            this.pnlIncomeExpenses.Controls.Add(this.lblExpenseHeader);
            this.pnlIncomeExpenses.Controls.Add(this.lblTreasuryAmount);
            this.pnlIncomeExpenses.Controls.Add(this.lblCityIncome);
            resources.ApplyResources(this.pnlIncomeExpenses, "pnlIncomeExpenses");
            this.pnlIncomeExpenses.Name = "pnlIncomeExpenses";
            // 
            // pnlAdviceComments
            // 
            this.pnlAdviceComments.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlAdviceComments.Controls.Add(this.btnMore);
            this.pnlAdviceComments.Controls.Add(this.lblAdvice);
            this.pnlAdviceComments.Controls.Add(this.pbAdvice);
            resources.ApplyResources(this.pnlAdviceComments, "pnlAdviceComments");
            this.pnlAdviceComments.Name = "pnlAdviceComments";
            // 
            // pbAdvice
            // 
            this.pbAdvice.Image = McKnight.Similization.Client.WindowsForms.Properties.Resources.woman1;
            resources.ApplyResources(this.pbAdvice, "pbAdvice");
            this.pbAdvice.Name = "pbAdvice";
            this.pbAdvice.TabStop = false;
            // 
            // DomesticAdvisorDialog
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pnlAdviceComments);
            this.Controls.Add(this.pnlIncomeExpenses);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblOtherInfoHeader);
            this.Controls.Add(this.lblAdviceCommentsHeader);
            this.Controls.Add(this.lblScientificResearchHeader);
            this.Controls.Add(this.lblIncomeExpensesHeader);
            this.Controls.Add(this.lvwCity);
            this.Controls.Add(this.pnlOtherInformation);
            resources.ApplyResources(this, "$this");
            this.ForeColor = System.Drawing.Color.DimGray;
            this.Name = "DomesticAdvisorDialog";
            this.pnlOtherInformation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trkBarScience)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarEntertainment)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlIncomeExpenses.ResumeLayout(false);
            this.pnlAdviceComments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAdvice)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void cboGovernment_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ClientApplication client = ClientApplication.Instance;
			if(cboGovernment.SelectedItem != client.Player.Government)
			{
				//they have chosen a different government...
				
			}
		}

		private void btnMore_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
