using System;
using System.Xml;
using McKnight.Similization.Core;
using System.Drawing;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Creates and returns units.
	/// </summary>
	internal static class UnitFactory
	{
        /// <summary>
        /// Creates and returns a new <see cref="Settler"/> object placed 
        /// at the specified coordinates.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="parentCountry">The <see cref="Country"/> creating the settler.</param>
        /// <returns></returns>
        internal static Settler CreateSettler(Point coordinates, Country parentCountry)
        {            
            Settler settler = null;
            GameRoot root = GameRoot.Instance;
            foreach (UnitBase unit in root.Ruleset.Units)
            {
                if (unit.CanSettle)
                {
                    if (parentCountry is AICountry)
                        settler = new AISettler(coordinates, unit);
                    else
                        settler = new Settler(coordinates, unit);
                    break;
                }
            }
            settler.ParentCountry = parentCountry;            
            return settler;
        }
         
        /// <summary>
        /// Creates a new <see cref="Worker"/> object.
        /// </summary>
        /// <param name="parentCity">The <see cref="City"/> creating the <see cref="Worker"/>.</param>
        /// <returns></returns>
        public static Worker CreateWorker(City parentCity)
        {
            Worker worker = null;
            GameRoot root = GameRoot.Instance;
            foreach (Unit unit in root.Ruleset.Units)
            {
                if (unit.CanWork)
                {                    
                    if (parentCity is AICity)
                        worker = new AIWorker(unit);
                    else
                        worker = new Worker(unit);
                    break;
                }
            }
            return worker;
        }

		/// <summary>
		/// Creates and returns units from cities and base units.
		/// </summary>
		/// <param name="city"></param>
		/// <param name="baseUnit"></param>
		/// <returns></returns>
		internal static Unit CreateUnit(City city, Unit baseUnit)
		{
			if(city == null)
				throw new ArgumentNullException("city");
			if(baseUnit == null)
				throw new ArgumentNullException("baseUnit");
            
			Unit newUnit = null;		
			if(city.ParentCountry.GetType() == typeof(AICountry))
			{
                if (baseUnit.CanSettle)				
                    newUnit = new AISettler(city.Coordinates, baseUnit);				
                else if (baseUnit.CanWork)				
                    newUnit = new AIWorker(city.Coordinates, baseUnit);				
				else				
                    newUnit = new AIUnit(city.Coordinates, baseUnit);				
			}
			else
			{
                if (baseUnit.CanSettle)				
                    newUnit = new Settler(city.Coordinates, baseUnit);				
                else if (baseUnit.CanWork)				
                    newUnit = new Worker(city.Coordinates, baseUnit);				
				else				
                    newUnit = new Unit(city.Coordinates, baseUnit);				
			}

			return newUnit;
		}

		/// <summary>
		/// Gets a new unit from the save game unit xml node.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="isAIUnit"></param>
		/// <returns></returns>
		internal static Unit FromSaveGameUnitNode(XmlReader reader, bool isAIUnit)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			UnitBase clone = null;
			Unit reHydrated = null;

			string unitName = reader.ReadElementString("Name");

			GameRoot root = GameRoot.Instance;
			clone = root.Ruleset.Units[unitName];

			if(clone == null)			
				return null;			

			if(isAIUnit && clone.CanSettle)
			{
				//ai settler
				reHydrated = new AISettler();
				
			}
			else if (isAIUnit && clone.CanWork)
			{
				//ai worker
				reHydrated = new AIWorker();
			}
			else if(clone.CanSettle)
			{
				//settler
				reHydrated = new Settler();
			}
			else if(clone.CanWork)
			{
				//worker
				reHydrated = new Worker();
			}
			else
			{
				//moveable unit
				reHydrated = new Unit();
			}

			reHydrated.Load(reader);
			return reHydrated;
			
		}
	}
}
