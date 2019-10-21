using System;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// A simple class containing properties related to a location and action
	/// for a job to perform by a Worker object.
	/// </summary>
	public class WorkerActionInfo
	{
        private int priority;
        private WorkerAction action;
        private GridCell cell;

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkerActionInfo"/> class..
		/// </summary>
		/// <param name="action"></param>
		/// <param name="cell"></param>
		public WorkerActionInfo(WorkerAction action, GridCell cell)
		{	
			this.action = action;
			this.cell = cell;
		}
		
		/// <summary>
		/// Gets or sets the action to perform.
		/// </summary>
		public WorkerAction WorkerAction
		{
			get { return this.action; }
			set { this.action = value; }
		}
		
		/// <summary>
		/// Gets or sets the location of the job.
		/// </summary>
		public GridCell GridCell
		{
			get { return this.cell; }
			set { this.cell = value; }
		}
		
		/// <summary>
		/// Gets or sets the priority of the job.
		/// </summary>
		public int Priority
		{
			get { return this.priority; }
			set { this.priority = value; }
		}		
	}
}