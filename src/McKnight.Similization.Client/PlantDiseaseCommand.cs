using System;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Class representing a command to plant a deadly disease 
    /// in a foreign country.
    /// </summary>
    public class PlantDiseaseCommand : EspionageCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantDiseaseCommand"/> class.
        /// </summary>
        public PlantDiseaseCommand()
        {
            this.Name = "PlantDiseaseCommand";
        }

        /// <summary>
        /// Invokes the command.
        /// </summary>
        public override void Invoke()
        {
            throw new NotImplementedException();
        }
    }
}
