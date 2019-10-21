using System;
using System.Collections;
using System.Collections.Generic;
using McKnight.Similization.Core;
using System.Drawing;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a Similization worker.  Workers are responsible for building roads and 
	/// railroads, irrigating land, and cleaning up pollution.  Workers do not have any 
	/// offensive or defensive characteristics.  
	/// </summary>
	public class Worker : Unit
	{		
		private int totalTurnsToComplete;
		private int turnsToComplete;
        private bool automated;
        private WorkerStatus status;        
        private Queue workQueue = new Queue();
        private WorkerActionInfo currentAction;
        private City parentCity;
		
		/// <summary>
		/// Intializes a new instance of the <see cref="Worker"/> class.
		/// </summary>
		public Worker() : base()
		{

		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class.
        /// </summary>
        /// <param name="unitClone"></param>
        public Worker(Unit unitClone) : base(unitClone)
        {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="Worker"/> class.
		/// </summary>
		/// <param name="coordinates"></param>
		/// <param name="unitClone"></param>
		public Worker(Point coordinates, Unit unitClone) :base(coordinates, unitClone)
		{
			this.totalTurnsToComplete = 3;
		}

		/// <summary>
		/// Takes a turn for the worker.
		/// </summary>
		public override void DoTurn()
		{
			if(this.status != WorkerStatus.Idle)
			{
				this.turnsToComplete--;
				if(this.turnsToComplete <= 0)
				{
                    GameRoot root = GameRoot.Instance;
                    GridCell currentCell = root.Grid.GetCell(this.Coordinates);
					switch(this.status)
					{
						case WorkerStatus.BuildingRailroad:
                            currentCell.HasRailroad = true;
							break;
						case WorkerStatus.BuildingRoad:
                            currentCell.HasRoad = true;
							break;
						case WorkerStatus.CleaningPollution:
                            currentCell.IsPolluted = false;
							break;
						case WorkerStatus.ClearingForest:
							throw new NotImplementedException();
						case WorkerStatus.ClearingJungle:
							throw new NotImplementedException();							
						case WorkerStatus.Irrigating:
                            currentCell.IsIrrigated = true;
							break;
						case WorkerStatus.Mining:
                            currentCell.HasMine = true;
							break;
					}

					this.status = WorkerStatus.Idle;
					Active = true;
					if(this.workQueue.Count > 0)
					{
						this.currentAction = (WorkerActionInfo)this.workQueue.Dequeue();
					}
					else
					{
						this.currentAction = null;
					}
					this.MovesLeft = this.MovesPerTurn;
					
				}

			}
			else if(this.currentAction != null)
			{
				InvokeCurrentAction();
			}
			else if(this.workQueue.Count > 0)
			{
				this.currentAction = (WorkerActionInfo)this.workQueue.Dequeue();
				InvokeCurrentAction();
			}
			else if(this.automated && this.workQueue.Count == 0)
			{
				//automated workers that are idle need to find something to 
				//do.
				this.currentAction = FindCellToImprove();
				InvokeCurrentAction();
			}

			base.DoTurn();
		}

		/// <summary>
		/// Invokes the action currently on the top of the list for the worker.
		/// </summary>
		protected virtual void InvokeCurrentAction()
		{
			if(this.currentAction.GridCell.Coordinates == this.Coordinates && this.status == WorkerStatus.Idle)			
				InvokeWorkerAction();				
			else if(this.Destination != this.currentAction.GridCell.Coordinates)			
				CreateJourney(this.currentAction.GridCell.Coordinates);			
		}


		private void InvokeWorkerAction()
		{
			switch(this.currentAction.WorkerAction)
			{
				case WorkerAction.BuildFortress:					
					break;
				case WorkerAction.BuildRailroad:
					BuildRailroad();
					break;
				case WorkerAction.BuildRoad:
					BuildRoad();
					break;
				case WorkerAction.CleanPollution:
					CleanPollution();
					break;
				case WorkerAction.Irrigate:
					Irrigate();
					break;
				case WorkerAction.Mine:
					Mine();
					break;
				case WorkerAction.None:
					break;
			}
		}
		
		/// <summary>
		/// The current status of the worker.
		/// </summary>
		public WorkerStatus Status
		{
			get { return this.status; }
		}
	
		/// <summary>
		/// Attempts to irrigate the land.
		/// </summary>
		/// <returns></returns>
		public bool Irrigate()
		{
            GameRoot root = GameRoot.Instance;
            GridCell currentCell = root.Grid.GetCell(this.Coordinates);
			if(currentCell.IsIrrigated)			
				return false;
			
			this.status = WorkerStatus.Irrigating;
			this.turnsToComplete = this.totalTurnsToComplete;
			Active = false;
			OnTurnFinished();
			return true;
		}

		/// <summary>
		/// Removes any pollution from the cell.  This can take several turns
		/// to complete.
		/// </summary>
		/// <returns><i>true</i> if the worker can clean the area, <i>false</i>
		/// otherwise (i.e. no pollution to clean)</returns>
		public bool CleanPollution()
		{
            GameRoot root = GameRoot.Instance;
            GridCell currentCell = root.Grid.GetCell(this.Coordinates);

			if(currentCell.IsPolluted)
				this.status = WorkerStatus.CleaningPollution;
			else
				return false;

			this.turnsToComplete = this.totalTurnsToComplete;
			Active = false;
			OnTurnFinished();
			return true;
		}

		/// <summary>
		/// Builds a road on the cell.
		/// </summary>
		/// <returns><i>true</i> if the worker can build a road on the cell,
		/// <i>false</i> otherwise (i.e. already has all the roads it can have)</returns>
		public bool BuildRoad()
		{
            GameRoot root = GameRoot.Instance;
            GridCell currentCell = root.Grid.GetCell(this.Coordinates);
            if (currentCell.HasRailroad)
                return false;
            else if (currentCell.HasRoad)
                return BuildRailroad();

			this.status = WorkerStatus.BuildingRoad;
			this.turnsToComplete = this.totalTurnsToComplete;
			this.Active = false;
			OnTurnFinished();
			return true;
		}

		/// <summary>
		/// Builds a RailRoad on the cell.
		/// </summary>
		/// <returns></returns>
		public bool BuildRailroad()
		{
			if(CanBuildRailroad())
			{
				this.status = WorkerStatus.BuildingRailroad;
				this.turnsToComplete = this.totalTurnsToComplete;
				Active = false;
				OnTurnFinished();
				return true;
			}

			return false;
		}

		/// <summary>
		/// Attempts to clear jungle from the land.
		/// </summary>
		/// <returns></returns>
		public bool ClearJungle()
		{
            GameRoot root = GameRoot.Instance;
            GridCell currentCell = root.Grid.GetCell(this.Coordinates); ;
            //FIXME: HARD-CODED NAME
			if(currentCell.Terrain.Name == "Jungle")
			{
				this.status = WorkerStatus.ClearingJungle;
				this.turnsToComplete = this.totalTurnsToComplete;
				Active = false;
				OnTurnFinished();
			}
			else
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Joins to the city.
		/// </summary>
		/// <param name="city"></param>
		public void JoinCity(City city)
		{
			if(city == null)
				throw new ArgumentNullException("city");

			city.Population++;
			this.ParentCountry.Units.Remove(this);
		}

		/// <summary>
		/// Attempts to mine the area.
		/// </summary>
		/// <returns></returns>
		public bool Mine()
		{
            GameRoot root = GameRoot.Instance;
            GridCell currentCell = root.Grid.GetCell(this.Coordinates);
			if(currentCell.HasMine)
				return false;

			this.status = WorkerStatus.Mining;
			Active = false;
			this.turnsToComplete = this.totalTurnsToComplete;
			OnTurnFinished();
			return true;
		}

		/// <summary>
		/// Builds a road to the specified coordinates on the grid..
		/// </summary>
		/// <param name="coordinates"></param>
		public void BuildRoadTo(Point coordinates)
		{            
            throw new NotImplementedException();
		}

		/// <summary>
		/// Determines whether or not the <see cref="Worker"/> can build a railroad on the current
		/// square.
		/// </summary>
		/// <returns></returns>
		public bool CanBuildRailroad()
		{
			GameRoot root = GameRoot.Instance;
            GridCell currentCell = root.Grid.GetCell(this.Coordinates);
            NamedObjectCollection<Resource> resources = root.Ruleset.Resources;
			bool hasResources = true;
			
			foreach(Resource resource in resources)
			{
				if(resource.RailroadPrerequisite)
				{
					foreach(City city in this.ParentCountry.Cities)
					{
						if(!city.HasAccessToResource(resource))
						{
							hasResources = false;
							break;
						}
					}
				}
			}

			if(hasResources)
			{
				//if the resources are available, we can build as long
				//as there is already a road here.
				return currentCell.HasRoad;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether or not the <see cref="Worker"/>
		/// can build a road to the specified coordinates.
		/// </summary>
		/// <param name="coordinates"></param>
		/// <returns></returns>
		public bool CanBuildRoadTo(Point coordinates)
		{			
            throw new NotImplementedException();
		}
		
		/// <summary>
		/// Gets a value indicating whether or not the worker is currently automated.
		/// </summary>
		public bool Automated
		{
			get { return this.automated; }
			set { this.automated = value; }
		}
		
		/// <summary>
		/// Gets or sets the city whose land the worker will improve.
		/// </summary>
		/// <remarks>When <i>null</i>, an automated worker will ask the <c>Country</c> 
		/// object to return the closest <see cref="City"/> that needs improvements to its' 
		/// land.  When non-null, the worker will make all possible improvements to the 
		/// cells within control of that city before moving on to a new city.</remarks>
		public City ParentCity
		{
			get { return this.parentCity; }
			set { this.parentCity = value; }
		}
				
		/// <summary>
		/// Gets the WorkQueue for the worker.
		/// </summary>
		public Queue WorkQueue
		{
			get { return this.workQueue; }
		}
		
		/// <summary>
		/// Gets the action currently being performed by the worker.  
		/// </summary>
		/// <remarks>This action will not always correspond to the current 
		/// status of the worker.  In some cases, the <c>CurrentAction</c> will 
		/// required the <see cref="Worker"/> to move some number of cells to get to 
		/// the <see cref="GridCell"/> that is to be improved.  This property can be used
		/// to determine the location and action that will be performed next by
		/// the <see cref="Worker"/>.</remarks>
		public WorkerActionInfo CurrentAction
		{
			get { return this.currentAction; }
			set { this.currentAction = value; }
		}

		/// <summary>
		/// Searches the entire area within control of the country and finds the closest 
		/// and highest priority job for this worker.
		/// </summary>
		/// <returns>A <see cref="WorkerActionInfo"/> object with information regarding
		/// the location and type of work to perform.  For more inforation, see the
		/// <see cref="WorkerActionInfo"/> object.</returns>
		protected virtual WorkerActionInfo FindCellToImprove()
		{			
			City next = this.parentCity;
            GameRoot root = GameRoot.Instance;
            GridCell currentCell = root.Grid.GetCell(this.Coordinates);
            WorkerActionInfo info = new WorkerActionInfo(WorkerAction.None, currentCell);
			if(next == null)
			{
                next = currentCell.FindClosestDomesticCity(ParentCountry);
			}
			else
			{
                NamedObjectCollection<City> exceptions = new NamedObjectCollection<City>();                
				bool found = false;
				while(!found)
				{
					info = next.RetrieveWorkItem();
					if(info.WorkerAction != WorkerAction.None)
					{
						found = true;
						break;
					}
					else
					{
						exceptions.Add(next);
					}

                    next = currentCell.FindClosestDomesticCity(ParentCountry, exceptions);
				}						
			}

			return info;
		}
	}

	
}
