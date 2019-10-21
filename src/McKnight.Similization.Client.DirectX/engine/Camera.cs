using System;
using System.Collections;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Engine
{
	/// <summary>
	/// Represents a camera object.
	/// </summary>
	public class Camera
	{		
		private ViewableObject lookAtObject;
		private Vector3 offset;
		private Vector3 eye;
		private Vector3 lookAt;
		private Attitude attitude;
		private string name;
		private ViewableObjectCollection visibleObjects;
		private float fieldOfView;
		private Matrix projectionMatrix;
		private Matrix viewMatrix;
		private Plane[] frustumPlanes;
		private Vector3[] frustumVectors;
		private float nearPlaneDistance;
        private float farPlaneDistance;
        private float aspectRatio;
		private float x;
		private float y;
		private float z;
		private const float LookAtVectorOffset = 10.0f;
		
        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
		public Camera()
		{
			this.fieldOfView = (float)Math.PI/4.0f;
            this.aspectRatio = 1.33f;
            this.nearPlaneDistance = 1.0f;
            this.farPlaneDistance = 800f;
            this.x = 1;
            this.y = 1;
            this.z = 1;
			this.offset = new Vector3(0,0,0);
			this.frustumPlanes = new Plane[6];
			this.frustumVectors = new Vector3[8];
            UpdateProjectionMatrix();
            this.visibleObjects = new ViewableObjectCollection();
		}

		/// <summary>
		/// Resets the camera.
		/// </summary>
		public void Reset()
		{
			this.visibleObjects.Clear();
		}

		/// <summary>
		/// Adjusts the pitch of the camera by the given number of degrees.
		/// </summary>
		/// <param name="deltaPitch"></param>
		public void AdjustPitch(float deltaPitch)
		{
			//convert degrees to radians and adjust pitch.
			this.attitude.Pitch += (deltaPitch * (float)Math.PI / 180.0f);

			//we want to keep the pitch between -pi/2 and pi/2.
			if(this.attitude.Pitch > (.5f * Math.PI))			
				this.attitude.Pitch = (float)(.5f * Math.PI);			

			if(this.attitude.Pitch < (-.5f * Math.PI))			
				this.attitude.Pitch = (float)(-.5f * Math.PI);			
		}

		/// <summary>
		/// Gets the heading of the camera (in degrees)
		/// </summary>
		/// <returns></returns>
		public float CalculateHeading()
		{
			return (float)(this.attitude.Heading*180/Math.PI);
		}

		/// <summary>
		/// Gets the pitch of the camera (in degrees)
		/// </summary>
		/// <returns></returns>
		public float CalculatePitch()
		{
			return (float)(this.attitude.Pitch*180/Math.PI);
		}

		/// <summary>
		/// Gets or sets the name of the camera.
		/// </summary>
		public string CameraName
		{
			get { return this.name; }
			set { this.name = value; }
		}

        /// <summary>
        /// Determines the "field of view" for the <see cref="Camera"/>.
        /// </summary>
        /// <remarks>Adjusting the field of view acts like the zoom feature on a camera. 
        /// Narrowing the field of view has the same effect as zooming in.  Objects in 
        /// the distance become larger.  Widening the field of view is the same as zooming out
        ///  (up to a point).  Widening the field of view much pas the 45-degree point 
        ///  will start to create a fishbowl look.</remarks>
        ///  <![CDATA[Remarks taken from "Introduction to 3D Game engine design usign c# and DirectX 9"]]>
        public float FieldOfView
        {
            get { return this.fieldOfView; }
            set
            {
                this.fieldOfView = value;
                UpdateProjectionMatrix();
            }
        }

        /// <summary>
        /// Determines the aspect ratio of the camera.
        /// </summary>
        /// <remarks>The aspect ratio is the ratio of the width of the viewing 
        /// frustum to the height.  The standard aspect ratio is 1.33.</remarks>
        public float AspectRatio
        {
            get { return this.aspectRatio; }
            set
            {
                this.aspectRatio = value;
                UpdateProjectionMatrix();
            }
        }

        /// <summary>
        /// Determines the location of the near plane to start clipping objects.
        /// </summary>
        public float NearClipPlane
        {
            get { return this.nearPlaneDistance; }
            set
            {
                this.nearPlaneDistance = value;
                UpdateProjectionMatrix();
            }
        }

        /// <summary>
        /// Determines the location of the far plane to start clipping objects.
        /// </summary>
        public float FarClipPlane
        {
            get { return this.farPlaneDistance; }
            set
            {
                this.farPlaneDistance = value;
                UpdateProjectionMatrix();
            }
        }

        private void UpdateProjectionMatrix()
        {
            this.projectionMatrix = Matrix.PerspectiveFieldOfViewLeftHanded(
                this.fieldOfView, 
                this.aspectRatio, 
                this.nearPlaneDistance, 
                this.farPlaneDistance);
        }

		/// <summary>
		/// Gets the view matrix of the camera.
		/// </summary>
		public Matrix ViewMatrix
		{
			get { return this.viewMatrix; }
		}

		/// <summary>
		/// Adjusts the heading of the camera by the given number of degrees.
		/// </summary>
		/// <param name="deltaHeading"></param>
		public void AdjustHeading(float deltaHeading)
		{
			this.attitude.Heading += (deltaHeading * (float)Math.PI/180.0f);

			if(this.attitude.Heading > (2f * Math.PI))			
				this.attitude.Heading -= (float)(2f * Math.PI);			

			if(this.attitude.Heading < 0f)			
				this.attitude.Heading += (float)(2f * Math.PI);			
		}				

		/// <summary>
		/// Gets a list of viewable objects in the cameras' field 
		/// of view.
		/// </summary>
		public ViewableObjectCollection VisibleObjects
		{
			get { return this.visibleObjects; }
		}

		/// <summary>
		/// Gets a vector representing the view of the 
		/// eye of the camera.
		/// </summary>
		public Vector3 EyeVector
		{
			get { return this.eye; }
		}

        /// <summary>
        /// The <i>Vector3</i> representing the Look At Vector.
        /// </summary>
		public Vector3 LookAtVector
		{
			get { return this.lookAt; }
		}

		/// <summary>
		/// Gets the cull state of an object given a position and bounding 
		/// radius.
		/// </summary>
		/// <param name="objectPosition">A <c>Vector3</c> variable representing the position of the object</param>
		/// <param name="boundingRadius">The radius of the object</param>
		/// <returns></returns>
		public CullState GetCullState(Vector3 objectPosition, float boundingRadius)
		{
			float distance;
			int count = 0;

			for(int plane = 0; plane < 4; plane++)
			{
				distance = Plane.DotCoordinate(this.frustumPlanes[plane], objectPosition);                                
				if(distance <= -boundingRadius)
					return CullState.AllOutside;
                if (distance > boundingRadius)
                    count++;
			}
			if(count == 4)			
				return CullState.AllInside;
			return CullState.PartiallyIn;
		}

		/// <summary>
		/// Gets the distance from a the camera to a viewable object.
		/// </summary>
		/// <param name="viewableObject"></param>
		/// <returns></returns>
		public float GetDistanceFrom(ViewableObject viewableObject)
		{
            if (viewableObject == null)
                throw new ArgumentNullException("viewableObject");

			float distance;
			distance = Plane.DotCoordinate(this.frustumPlanes[3], viewableObject.Position);
			distance += this.nearPlaneDistance;
			return distance;
		}

		/// <summary>
		/// Gets the cull state of a viewable object.
		/// </summary>
		/// <param name="viewableObject">The object to get the cull state for.</param>
		/// <returns></returns>
		public CullState GetCullState(ViewableObject viewableObject)
		{
            if (viewableObject == null)
                throw new ArgumentNullException("viewableObject");

			CullState state;
			state = GetCullState(viewableObject.Position, viewableObject.BoundingRadius);
			return state;
		}

		/// <summary>
		/// Renders the camera view
		/// </summary>
		public void Render()
		{
			Vector3 verticalVector = new Vector3(0.0f, 1.0f, 0.0f);
						
			this.eye = new Vector3(this.x, this.y, this.z);
			if(this.lookAtObject != null)				
				this.lookAt = this.lookAtObject.Position;				
			else
			{
				this.lookAt = this.eye;
				OffsetLookAtVector();
			}
			
            
			this.viewMatrix = Matrix.LookAtLeftHanded(this.eye, this.lookAt, verticalVector);            
			GameEngine.Device.Transform.View = this.viewMatrix;
			GameEngine.Device.Transform.Projection = this.projectionMatrix;

			//set up the matrix for the view frustum
			Matrix frustumMatrix;
			frustumMatrix = Matrix.Multiply(this.viewMatrix, this.projectionMatrix);
			frustumMatrix.Invert();

			//set up the frustum vector array.  This will contain 
			//Vectors representing the 8 points in the viewing
			//frustum.
			this.frustumVectors[0] = new Vector3(-1.0f,-1.0f,0.0f);
            this.frustumVectors[1] = new Vector3(1.0f, -1.0f, 0.0f);
            this.frustumVectors[2] = new Vector3(-1.0f, 1.0f, 0.0f);
            this.frustumVectors[3] = new Vector3(1.0f, 1.0f, 0.0f);
            this.frustumVectors[4] = new Vector3(-1.0f, -1.0f, 1.0f);
            this.frustumVectors[5] = new Vector3(1.0f, -1.0f, 1.0f);
            this.frustumVectors[6] = new Vector3(-1.0f, 1.0f, 1.0f);
            this.frustumVectors[7] = new Vector3(1.0f, 1.0f, 1.0f);

			//set up the frustum plane array.
			for(int i = 0; i <= 7; i++)
			{
                this.frustumVectors[i] = 
					Vector3.TransformCoordinate(
                    this.frustumVectors[i], frustumMatrix);
			}
			
			///7,3,5 right
			this.frustumPlanes[0] = 
				Plane.FromPoints(this.frustumVectors[7],
				this.frustumVectors[3], this.frustumVectors[5]);

			//2,6,4 left
			this.frustumPlanes[1] = 
				Plane.FromPoints(this.frustumVectors[2],
				this.frustumVectors[6], this.frustumVectors[4]);

			//6,7,5 far
			this.frustumPlanes[2] =
				Plane.FromPoints(this.frustumVectors[6],
				this.frustumVectors[7], this.frustumVectors[5]);

			//0,1,2 near
			this.frustumPlanes[3] =
				Plane.FromPoints(this.frustumVectors[0],
				this.frustumVectors[1], this.frustumVectors[2]);

			//2,3,6 top
			this.frustumPlanes[4] = 
				Plane.FromPoints(this.frustumVectors[2],
				this.frustumVectors[3], this.frustumVectors[6]);

			//1,0,4 bottom
			this.frustumPlanes[5] =
				Plane.FromPoints(this.frustumVectors[1],
				this.frustumVectors[0], this.frustumVectors[4]);
		}

		/// <summary>
		/// offsets the lookat vector a constant distance, based on the
		/// current attitude
		/// </summary>
		private void OffsetLookAtVector()
		{
			this.lookAt.X += (float)Math.Sin(this.attitude.Heading)*LookAtVectorOffset;
			this.lookAt.Y += (float)Math.Sin(this.attitude.Pitch)*LookAtVectorOffset;
			this.lookAt.Z += (float)Math.Cos(this.attitude.Heading)*LookAtVectorOffset;
		}

		/// <summary>
		/// Gets or sets the X location of the camera.
		/// </summary>
		public float X
		{
			get { return this.x; }
			set { this.x = value; }
		}

		/// <summary>
		/// Gets or sets the Y location (altitude) of the camera.
		/// </summary>
		public float Y
		{
			get { return this.y; }
			set { this.y = value; }
		}

		/// <summary>
		/// Gets or sets the Z location of the camera.
		/// </summary>
		public float Z
		{
			get { return this.z; }
			set { this.z = value; }
		}
	}

	/// <summary>
	/// Cull states of a camera
	/// </summary>
	public enum CullState
	{
		AllInside,
		AllOutside,
		PartiallyIn
	}
}
