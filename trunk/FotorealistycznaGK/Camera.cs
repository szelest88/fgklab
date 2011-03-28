using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    abstract public class Camera
    {
        private Vector position;

        public Vector Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector target;

        public Vector Target
        {
            get { return target; }
            set { target = value; }
        }

        private Vector up;

        public Vector Up
        {
            get { return up; }
            set { up = value; }
        }

        private float nearPlane;

        public float NearPlane
        {
            get { return nearPlane; }
            set { nearPlane = value; }
        }

        private float farPlane;

        public float FarPlane
        {
            get { return farPlane; }
            set { farPlane = value; }
        }

        private float fov;

        public float Fov
        {
            get { return fov; }
            set { fov = value; }
        }

        public Camera()
        {
            this.position = new Vector(0,0,0);
            this.target = new Vector(0,0,1);
            this.nearPlane = 1;
            this.farPlane = 1000;
            this.up = new Vector(0,1,0);
        }

        public Camera(Vector position, Vector target)
        {
            this.position = position;
            this.target = target;
            this.nearPlane = 1;
            this.farPlane = 1000;
            this.up = new Vector(0, 1, 0);
        }


        abstract public void renderScene();

    }
}
