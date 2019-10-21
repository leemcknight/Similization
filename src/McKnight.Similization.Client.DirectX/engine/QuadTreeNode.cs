using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Engine
{    
	public class QuadTreeNode : IDisposable
	{
        private bool disposed;
		private Rectangle quadBounds;		
        private SortedList<string, ViewableObject> viewableObjects;

		//child quad tree nodes
		private QuadTreeNode northeastNode;
		private QuadTreeNode northwestNode;
		private QuadTreeNode southeastNode;
		private QuadTreeNode southwestNode;

		//parent quad tree node
		private QuadTreeNode parentNode;

		private int _treeLevel;
		private static QuadTreeNode BaseNode;
		private Vector3 _position;
		private float boundingRadius;

		public QuadTreeNode(Rectangle bounds, int level, int maxLevel, QuadTreeNode parentNode)
		{
            if (BaseNode == null)
                BaseNode = this;

			this.quadBounds = bounds;
			_treeLevel = level;
			this.parentNode = parentNode;

            this.viewableObjects = new SortedList<string, ViewableObject>();            			

			_position.X = (this.quadBounds.Left + this.quadBounds.Right) / 2.0f;
			_position.Y = 0.0f;
			_position.Z = (this.quadBounds.Top + this.quadBounds.Bottom) / 2.0f;

			double dx, dz;
			dx = bounds.Width;
			dz = bounds.Height;
			this.boundingRadius = (float)Math.Sqrt(dx * dx + dz * dz) / 2.0f;

			if(level < maxLevel)
			{
				int halfHeight = (int)dz / 2;
				int halfWidth = (int)dx / 2;

				this.northeastNode = new QuadTreeNode(new Rectangle(bounds.Left + halfWidth,
					bounds.Top, bounds.Right,bounds.Top + halfHeight),level+1,maxLevel,this);

				this.northwestNode = new QuadTreeNode(new Rectangle(bounds.Left, bounds.Top,
					bounds.Left + halfWidth, bounds.Top + halfHeight), level+1,maxLevel,this);

				this.southwestNode = new QuadTreeNode(new Rectangle(bounds.Left, bounds.Top + halfHeight,
					bounds.Left + halfWidth, bounds.Bottom),level+1,maxLevel,this);

				this.southeastNode = new QuadTreeNode(new Rectangle(bounds.Left + halfWidth,
					bounds.Top + halfHeight, bounds.Right, bounds.Bottom), level+1, maxLevel, this);
			}
		}

		public void Cull(Camera camera)
		{
			CullState cullState;

            camera.Reset();
			if(this.viewableObjects.Count == 0)			
				return;			

			cullState = camera.GetCullState(_position, this.boundingRadius);

            ViewableObject vo;
			switch(cullState)
			{
				case CullState.AllInside:
					for(int i = 0; i < this.viewableObjects.Count; i++)
					{
                        vo = this.viewableObjects.Values[i];
						vo.Range = camera.GetDistanceFrom(vo);
						vo.IsCulled = false;                        
						camera.VisibleObjects.Add(vo);
					}
					break;
				case CullState.AllOutside:
                    if (this.parentNode == null)
                        goto case CullState.PartiallyIn;
					break;
				case CullState.PartiallyIn:
                    if (this.northeastNode != null)
                    {
                        this.northeastNode.Cull(camera);
                        this.northwestNode.Cull(camera);
                        this.southwestNode.Cull(camera);
                        this.southeastNode.Cull(camera);
                    }
                    else
                    {
                        for(int i = 0; i < this.viewableObjects.Count; i++)
                        {
                            vo = this.viewableObjects.Values[i];
                            vo.IsCulled = false;                            
                            camera.VisibleObjects.Add(vo);
                        }
                    }
					break;
			}
		}

		public void Update(ViewableObject viewableObject)
		{
			bool _needReset = false;

			foreach(QuadTreeNode node in viewableObject.QuadTreeNodes)
			{
				if(!viewableObject.IsInsideRectangle(node.Bounds))
				{
					//there's at least 1 object that's no longer in 
					//the view.  Need a reset.
					_needReset = true;
					break;
				}
			}

			if(_needReset)
			{				
                BaseNode.RemoveObject(viewableObject);
                BaseNode.AddObject(viewableObject);				
			}
		}

        /// <summary>
        /// Releases all resources.
        /// </summary>
		public void Dispose()
		{
            Dispose(true);
            GC.SuppressFinalize(this);
		}

        private void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed && disposing)
                {
                    if (this.northeastNode != null)
                        this.northeastNode.Dispose();
                    if (this.northwestNode != null)
                        this.northwestNode.Dispose();
                    if (this.southeastNode != null)
                        this.southeastNode.Dispose();
                    if (this.southwestNode != null)
                        this.southwestNode.Dispose();
                }
            }
            finally
            {
                this.disposed = true;
            }
        }

		/// <summary>
		/// Gets the bounds of the quad tree node.
		/// </summary>
		public Rectangle Bounds
		{
			get { return this.quadBounds; }
		}

        public void AddObject(ViewableObject viewable)
        {
            if (!viewable.IsInsideRectangle(this.quadBounds))
                return;
            if (this.viewableObjects.ContainsKey(viewable.Name))
                return;
                
            this.viewableObjects.Add(viewable.Name, viewable);

            viewable.QuadTreeNodes.Add(this);
            if (this.northeastNode != null && viewable.IsInsideRectangle(this.northeastNode.Bounds))
                this.northeastNode.AddObject(viewable);

            if (this.northwestNode != null && viewable.IsInsideRectangle(this.northwestNode.Bounds))
                this.northwestNode.AddObject(viewable);

            if (this.southeastNode != null && viewable.IsInsideRectangle(this.southeastNode.Bounds))
                this.southeastNode.AddObject(viewable);

            if (this.southwestNode != null && viewable.IsInsideRectangle(this.southwestNode.Bounds))
                this.southwestNode.AddObject(viewable);                                    
        }

        public void RemoveObject(ViewableObject viewable)
        {
            if (!this.viewableObjects.ContainsKey(viewable.Name))
                return;
            int idx = this.viewableObjects.IndexOfKey(viewable.Name);
            this.viewableObjects.Remove(viewable.Name);
            if (viewable.QuadTreeNodes.Count > 0)
                viewable.QuadTreeNodes.Clear();
            this.viewableObjects.RemoveAt(idx);
            if (this.northeastNode != null)
                this.northeastNode.RemoveObject(viewable);
            if (this.northwestNode != null)
                this.northwestNode.RemoveObject(viewable);
            if (this.southeastNode != null)
                this.southeastNode.RemoveObject(viewable);
            if (this.southwestNode != null)
                this.southwestNode.RemoveObject(viewable);
        }	
	}	
}
