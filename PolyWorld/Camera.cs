using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;

namespace PolyWorld
{
    public class Camera
    {
        GraphicsDevice graphicsDevice;

        // Let's start at X = 0 so we're looking at things head-on
        Vector3 position = new Vector3(0, 38, 16);

        float angle = 0;

        public Matrix ViewMatrix
        {
            get
            {
                var lookAtVector = new Vector3(0, -1, -.5f);
                // We'll create a rotation matrix using our angle
                var rotationMatrix = Matrix.CreateRotationZ(angle);
                // Then we'll modify the vector using this matrix:
                lookAtVector = Vector3.Transform(lookAtVector, rotationMatrix);
                lookAtVector += position;

                var upVector = Vector3.UnitZ;

                return Matrix.CreateLookAt(
                    position, lookAtVector, upVector);
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                float fieldOfView = MathHelper.PiOver4;
                float nearClipPlane = 1;
                float farClipPlane = 200;
                float aspectRatio = graphicsDevice.Viewport.Width / (float)graphicsDevice.Viewport.Height;

                return Matrix.CreatePerspectiveFieldOfView(
                    fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
            }
        }

        public Camera(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardstatecurrent = Keyboard.GetState();

            float oldAngle = angle;

            if (keyboardstatecurrent.IsKeyDown(Keys.Left))
                angle += 0.05f;

            if (keyboardstatecurrent.IsKeyDown(Keys.Right))
                angle -= 0.05f;

            if (keyboardstatecurrent.IsKeyDown(Keys.W))
            {
                var forwardVector = new Vector3(0, -1, 0);

                var rotationMatrix = Matrix.CreateRotationZ(angle);
                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);

                const float unitsPerSecond = 0.5f;

                this.position += forwardVector * unitsPerSecond;
            }

            if (keyboardstatecurrent.IsKeyDown(Keys.S))
            {
                var forwardVector = new Vector3(0, -1, 0);

                var rotationMatrix = Matrix.CreateRotationZ(angle);
                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);

                const float unitsPerSecond = 0.5f;

                this.position -= forwardVector * unitsPerSecond;
            }

            if (keyboardstatecurrent.IsKeyDown(Keys.D))
            {
                var forwardVector = new Vector3(-1, 0, 0);

                var rotationMatrix = Matrix.CreateRotationZ(angle);
                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);

                const float unitsPerSecond = 0.5f;

                this.position += forwardVector * unitsPerSecond;
            }

            if (keyboardstatecurrent.IsKeyDown(Keys.A))
            {
                var forwardVector = new Vector3(-1, 0, 0);

                var rotationMatrix = Matrix.CreateRotationZ(angle);
                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);

                const float unitsPerSecond = 0.5f;

                this.position -= forwardVector * unitsPerSecond;
            }


            /*
            if (angle != oldAngle)
            {
                var forwardVector = new Vector3(0, -1, 0);

                var rotationMatrix = Matrix.CreateRotationZ(angle);
                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);

                const float unitsPerSecond = 0.5f;

                this.position += forwardVector * unitsPerSecond;
            }
            */
        }
    }
}

