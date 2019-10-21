using System;
using System.Drawing;
using System.Collections.ObjectModel;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Engine
{
	/// <summary>
	/// Base class for all viewable 3D directX objects used by
	/// this engine.  This class encapsulates all the information
	/// needed to render an object to a 3D world.
	/// </summary>
	public class ViewableObject : IDisposable
	{
		protected Vector3 position;
		private float _radius;
		private float _range;
		private bool culled;
		private Collection<QuadTreeNode> quadTreeNodes;
		private Attitude _attitude;
		private ViewableObject parent;
		private ViewableObjectCollection children;
		private Matrix worldMatrix;
        private string name;
		
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewableObjectCollection"/> class.
        /// </summary>
		public ViewableObject()
		{
			this.quadTreeNodes = new Collection<QuadTreeNode>();
			this.children = new ViewableObjectCollection();
		}

        /// <summary>
        /// Renders the <see cref="ViewableObject"/> to the specified <see cref="Camera"/>.
        /// </summary>
        /// <param name="camera"></param>
		public virtual void Render(Camera camera)
		{
		}

        /// <summary>
        /// Releases all resources.
        /// </summary>
		public void Dispose()
		{
            
		}

        /// <summary>
        /// Releases all resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            
        }

        /// <summary>
        /// The name of the <see cref="ViewableObject"/>.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

		public virtual bool HasCollidedWith(ViewableObject otherObject)
		{
			return false;
		}

		public Vector3 Position
		{
			get { return this.position; }
			set { this.position = value; }
		}

		public float BoundingRadius
		{
			get { return _radius; }
			set { _radius = value; }
		}

		public float Range
		{
			get { return _range; }
			set { _range = value; }
		}

        /// <summary>
        /// Determines whether this object is culled when drawing it on the screen.
        /// </summary>
		public bool IsCulled
		{
			get { return this.culled; }
			set { this.culled = value; }
		}

		public Attitude Attitude
		{
			get { return _attitude; }
			set { _attitude = value; }
		}

        /// <summary>
        /// Determines whether this <see cref="ViewableObject"/> is inside the bounds 
        /// of the specified <i>Rectangle</i>l
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
		public virtual bool IsInsideRectangle(RectangleF rectangle)
		{
            return false;
            
		}

        public Collection<QuadTreeNode> QuadTreeNodes
		{
			get { return this.quadTreeNodes; }
		}

        /// <summary>
        /// The world matrix for this object.
        /// </summary>
		public Matrix WorldMatrix
		{
			get { return this.worldMatrix; }
		}

		/// <summary>
		/// The parent object of this viewable objects.  Complex objects
		/// are likely to contain many viewable objects, each which can
		/// move independently.  Simpler viewable objects may be alone,
		/// with no parent, and no children.
		/// </summary>
		public ViewableObject Parent
		{
			get { return this.parent; }
			set { this.parent = value; }
		}

		/// <summary>
		/// Gets a collection of child objects to this viewable object.
		/// </summary>
		public ViewableObjectCollection Children
		{
			get { return this.children; }
		}
	}

}
