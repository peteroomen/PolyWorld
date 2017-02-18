using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PolyWorld.Terrain
{
    class Hexagon
    {
        private static float SCALE = 5;

        private Vector3 offset;

        private VertexPositionColorNormal[] vertices;

        private Color color;
        private bool highlighted;

        public Hexagon(float height, Vector3 offset)
        {
            this.offset = offset;

            SetUpVertices(height);
            CalculateNormals();
        }

        public Hexagon(float height, Vector3 offset, Color color)
        {
            this.offset = offset;

            SetUpVertices(height);
            CalculateNormals();
            SetColor(color);
        }

        public Hexagon(float height, Vector3 offset, Color color, bool highlighted)
        {
            this.offset = offset;

            SetUpVertices(height);
            CalculateNormals();
            SetColor(color);

            this.highlighted = highlighted;
        }

        private void SetUpVertices(float height)
        {
            vertices = new VertexPositionColorNormal[6 * 3];
            /*
            var a = (new Vector3(-0.5f, Land.HEX_UNIT, 0) + offset) * SCALE;
            var b = (new Vector3(0.5f, Land.HEX_UNIT, 0) + offset) * SCALE;
            var c = (new Vector3(1, 0, 0) + offset) * SCALE;
            var d = (new Vector3(0.5f, -Land.HEX_UNIT, 0) + offset) * SCALE;
            var e = (new Vector3(-0.5f, -Land.HEX_UNIT, 0) + offset) * SCALE;
            var f = (new Vector3(-1, 0, 0) + offset) * SCALE;
            var g = (new Vector3(0, 0, height) + offset) * SCALE;
            */
            var a = (new Vector3(0, -1, 0) + offset) * SCALE;
            var b = (new Vector3(-Land.HEX_UNIT, -0.5f, 0) + offset) * SCALE;
            var c = (new Vector3(-Land.HEX_UNIT, 0.5f, 0) + offset) * SCALE;
            var d = (new Vector3(0, 1, 0) + offset) * SCALE;
            var e = (new Vector3(Land.HEX_UNIT, 0.5f, 0) + offset) * SCALE;
            var f = (new Vector3(Land.HEX_UNIT, -0.5f, 0) + offset) * SCALE;
            var g = (new Vector3(0, 0, height) + offset) * SCALE;

            // Triangle ABG
            vertices[0].Position = g;
            vertices[1].Position = a;
            vertices[2].Position = b;
            
            // Triangle BCG
            vertices[3].Position = g;
            vertices[4].Position = b;
            vertices[5].Position = c;

            // Triangle CDG
            vertices[6].Position = g;
            vertices[7].Position = c;
            vertices[8].Position = d;

            // Triangle DEG
            vertices[9].Position = g;
            vertices[10].Position = d;
            vertices[11].Position = e;

            // Triangle EFG
            vertices[12].Position = g;
            vertices[13].Position = e;
            vertices[14].Position = f;

            // Triangle FAG
            vertices[15].Position = g;
            vertices[16].Position = f;
            vertices[17].Position = a;
            
        }

        private void CalculateNormals()
        {
            for (int i = 0; i < vertices.Length; i += 3)
            {
                // Get the sides of the triangle
                var side1 = vertices[i + 1].Position - vertices[i].Position;
                var side2 = vertices[i + 2].Position - vertices[i].Position;

                // Take the cross product of the sides, and normalize
                var n = Vector3.Cross(side2, side1);
                n.Normalize();

                // Assign the normal to each point
                vertices[i].Normal = n;
                vertices[i + 1].Normal = n;
                vertices[i + 2].Normal = n;
            }
        }

        public void SetColor(Color color)
        {
            this.color = color;
            for (int i=0; i<vertices.Length; i++)
            {
                vertices[i].Color = color;
            }

            if (vertices[0].Position.Z > (0.5*SCALE))
            {
                vertices[0].Color = Color.White;
                vertices[3].Color = Color.White;
                vertices[6].Color = Color.White;
                vertices[9].Color = Color.White;
                vertices[12].Color = Color.White;
                vertices[15].Color = Color.White;
            }
        }

        public void Draw(GraphicsDeviceManager graphics, BasicEffect baseEffect)
        {
            if (highlighted)
            {
                baseEffect.DirectionalLight0.DiffuseColor = new Vector3(2f, 2f, 4f);
            }

            foreach (var pass in baseEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.GraphicsDevice.DrawUserPrimitives(
                    PrimitiveType.TriangleList, vertices, 0, vertices.Length / 3, VertexPositionColorNormal.VertexDeclaration);
            }

            if (highlighted)
            {
                baseEffect.DirectionalLight0.DiffuseColor = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
