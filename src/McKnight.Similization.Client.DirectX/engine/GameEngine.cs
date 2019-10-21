using System;
using System.Drawing;
using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using LJM.Similization.Server;
using LJM.Similization.Client.DirectX.Util;

namespace LJM.Similization.Client.DirectX.Engine
{
	/// <summary>
	/// Class representing the 3D graphics engine in the game.
	/// </summary>
	public class GameEngine  : IDisposable
	{
        private bool disposed;
        private bool ready;
		private static Device device;
		private static QuadTreeNode topNode;
		private Camera currentCamera;
		private Collection<Camera> cameras;
		private Terrain terrain;
		private ViewableObjectCollection viewableObjects;
		private Grid grid;		
		private SkyBox skyBox;
		private event EventHandler<StatusChangedEventArgs> statusEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameEngine"/> class.
        /// </summary>
        /// <param name="dev"></param>
        /// <param name="grid"></param>
		public GameEngine(Device dev, Grid grid)
		{			
			device = dev;
			this.grid = grid;
		}

        /// <summary>
        /// Destructor for the <see cref="GameEngine"/> class.
        /// </summary>
        ~GameEngine()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all managed resources used by the engine.
        /// </summary>
		public void Dispose()
		{
            Dispose(true);
            GC.SuppressFinalize(this);
		}

        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                if(this.skyBox != null)
                    this.skyBox.Dispose();
                if(topNode != null)
                    topNode.Dispose();                
                this.cameras.Clear();                
                this.currentCamera = null;
                foreach (ViewableObject o in this.viewableObjects)
                    o.Dispose();
                this.grid = null;
            }
            this.disposed = true;
        }

        /// <summary>
        /// Builds the terrain for the engine based upon the information about 
        /// the <see cref="LJM.Similization.Server.Grid"/>.
        /// </summary>
        /// <param name="tileset"></param>
		public void InitializeEngine(Tileset tileset)
		{
			float[,] elevationData;
			elevationData = new float[this.grid.Size.Width, this.grid.Size.Height];
			OnStatusChanged(new StatusChangedEventArgs("Reading Elevation Data...",0));

			for(int i = 0; i < this.grid.Size.Width; i++)			
				for(int j = 0; j < this.grid.Size.Height; j++)				
					elevationData[i,j] = this.grid.GetCell(new Point(i,j)).Altitude;							

			OnStatusChanged(new StatusChangedEventArgs("Building Quad Tree...",10));
			Rectangle bounds = new Rectangle(0,0,(int)(this.grid.Size.Width*10.9),(int)(this.grid.Size.Height*10.9));
			topNode = new QuadTreeNode(bounds,0,7,null);

			OnStatusChanged(new StatusChangedEventArgs("Building terrain...",25));
			this.terrain = new Terrain(this.grid);
			this.terrain.LoadProgress += new EventHandler<StatusChangedEventArgs>(this.TerrainLoadProgress);
			this.terrain.Load(tileset);

			OnStatusChanged(new StatusChangedEventArgs("Creating Camera...",80));
			this.currentCamera = new Camera();                     
            this.cameras = new Collection<Camera>();
			this.cameras.Add(this.currentCamera);
          
			OnStatusChanged(new StatusChangedEventArgs("Building SkyBox...",85));
			this.skyBox = new SkyBox(
                @"SkyBox\Dunes_Front.tga", 
				@"SkyBox\Dunes_Right.tga", 
				@"SkyBox\Dunes_Back.tga", 
				@"SkyBox\Dunes_Left.tga", 
				@"SkyBox\Dunes_Top.tga",  
				@"SkyBox\Dunes_Bottom.tga");
       
			OnStatusChanged(new StatusChangedEventArgs("Adding Objects...", 90));
			this.viewableObjects = new ViewableObjectCollection();

			this.ready = true;
			OnStatusChanged(new StatusChangedEventArgs("Engine Loaded Successfully.", 100));

		}

		private void TerrainLoadProgress(object sender, StatusChangedEventArgs e)
		{			
			double scaled = ((double)e.PercentDone /100d ) * 55d;
			int percent = 25 + (int)scaled;
			OnStatusChanged(new StatusChangedEventArgs(e.Message, percent));
		}

        /// <summary>
        /// Determines whether the <see cref="GameEngine"/> is able to be rendered.
        /// </summary>
		public bool Ready
		{
			get { return this.ready; }
		}

		public static Device Device
		{
			get { return device; }
			set { device = value; }
		}

		public static QuadTreeNode TopQuadTreeNode
		{
			get { return topNode; }
			set { topNode = value; }
		}

		public event EventHandler<StatusChangedEventArgs> StatusChanged
		{
			add
			{
				statusEvent += value; 
			}

			remove
			{
				statusEvent -= value; 
			}
		}


        /// <summary>
        /// Renders the graphics in the engine.
        /// </summary>
		public void Render()
		{
            GameEngine.device.RenderState.Lighting = false;
			this.currentCamera.Render();
			topNode.Cull(this.currentCamera);
			this.skyBox.Render(this.currentCamera);
			this.terrain.Render(this.currentCamera);
			foreach(ViewableObject viewableObject in this.viewableObjects)			
				if(!viewableObject.IsCulled)				
					viewableObject.Render(this.currentCamera);		             
		}
		
		protected virtual void OnStatusChanged(StatusChangedEventArgs e)
		{
			if(statusEvent != null)
			{
				statusEvent(this, e);
			}
		}
	}	
}
