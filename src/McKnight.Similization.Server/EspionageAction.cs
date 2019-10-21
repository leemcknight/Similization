using System;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Server
{
    /// <summary>
    /// Enumeration for different classes of espionage.
    /// </summary>
    public enum EspionageAction
    {
        /// <summary>
        /// Espionage where one country tries to steal a technology 
        /// from another country.
        /// </summary>
        StealTechnology,

        /// <summary>
        /// Espionage where one country tries to plant a disease in 
        /// the city of another country.
        /// </summary>
        PlantDisease,

        /// <summary>
        /// Espionage where one country tries to expose and send back the 
        /// spy from another country.
        /// </summary>
        ExposeSpy,

        /// <summary>
        /// Espionage where one country tries to plant a spy in the capital 
        /// of another country.
        /// </summary>
        PlantSpy,

        /// <summary>
        /// Espionage where one country tries to convert the citizens to their 
        /// civilization through the spreading of propaganda.
        /// </summary>
        SpreadPropaganda,

        /// <summary>
        /// Espionage where one country tries to sabotage the production of the 
        /// current unit, improvement, or wonder in a particular city of another 
        /// country.
        /// </summary>
        SabotageProduction,

        /// <summary>
        /// Espionage where one country tries to steal the military plans of another 
        /// country.
        /// </summary>        
        StealPlans,

        /// <summary>
        /// Espionage where one country tries to steal the world map of another country.
        /// </summary>
        StealWorldMap
    }
}
