using System;
using System.Globalization;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// Command to sabotage the production of the currently produced improvement, unit, 
	/// or wonder in the foreign city.
	/// </summary>
	public class SabotageCommand : EspionageCommand
	{	
        /// <summary>
        /// Initializes a new instance of the <see cref="SabotageCommand"/> class.
        /// </summary>
        public SabotageCommand()
        {
            this.Name = "SabotageCommand"; 
        }

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
                        
            string cityPickerTitle = ClientResources.GetString("sabotage_cityPickerTitle");
            SelectCity(cityPickerTitle);
            if (this.City == null)
            {
                OnCanceled();
                return;
            }
            BuildableItem improvement = this.City.NextImprovement;
            EspionageResult result = this.DiplomaticTie.Sabotage(this.City);
            string text = string.Empty;
            switch (result)
            {
                case EspionageResult.Failure:
                    break;                                
                case EspionageResult.ImmuneToEspionage:
                    text = ClientResources.GetString("sabotage_immune");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.DiplomaticTie.ForeignCountry.Civilization.Adjective);
                    break;  
                case EspionageResult.SpyCaught:
                    text = ClientResources.GetString("sabotage_spycaught");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.City.Name, 
                        this.DiplomaticTie.ForeignCountry.Government.LeaderTitle, 
                        this.DiplomaticTie.ForeignCountry.LeaderName);
                    break;
                case EspionageResult.Success:
                    text = ClientResources.GetString("sabotage_success");                    
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.City.Name, 
                        improvement.Name);
                    break;
                case EspionageResult.SuccessWithCapturedSpy:
                    text = ClientResources.GetString("sabotage_success_spycaught");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.City.Name, 
                        improvement.Name, 
                        this.DiplomaticTie.ForeignCountry.Government.LeaderTitle, 
                        this.DiplomaticTie.ForeignCountry.LeaderName);
                    break;
            }
            IGameWindow gw = ClientApplication.Instance.GameWindow;
            gw.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
			OnInvoked();
		}       
	}
}