using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace PolyWorld.Terrain
{
    class Land
    {
        public static readonly float HEX_UNIT = ((float)Math.Sqrt(3)) / 2;
        private const int RADIUS = 8;

        private int[] numHexesForRadius;

        private int NumHexes { get { return GetNumHexesForRadius(RADIUS); } }
        private Hexagon[] hexes;

        public Land()
        {
            CreateLookupTables(); 
            
            hexes = new Hexagon[NumHexes];

            var layout = CalcLayoutArrayFromRadius(RADIUS);
            var diam = CalcDiameterFromRadius(RADIUS);
            var i = 0;
            var y = (RADIUS - diam);

            Random random = new Random();

            for (int r = 0; r<diam; r++)
            {
                var stop = -layout[r];

                for (int x=layout[r]; x>=stop; x-=2)
                {
                    float rand = RandomOrZero(random);
                    // For each row
                    hexes[i] = new Hexagon(rand, new Vector3(x * HEX_UNIT, y * 1.5f, 0), rand == 0 ? Color.Green : Color.Gray, i % 6 == 0 ? false : false);
                    i++; // increment the index
                }
                y++;
            }
        }

        private float RandomOrZero(Random random)
        {
            if (random.Next(0,2) == 0)
            {
                return 0;
            }
            else
            {
                return (float)random.NextDouble();
            }
        }

        private void CreateLookupTables()
        {
            numHexesForRadius = new int[RADIUS];

            for (int i=0; i<RADIUS; i++)
            {
                numHexesForRadius[i] = CalcNumHexesForRadius(i+1);
            }
        }

        public void Draw(GraphicsDeviceManager graphics, BasicEffect baseEffect)
        {
            for (int i=0; i<hexes.Length; i++)
            {
                hexes[i].Draw(graphics, baseEffect);
            }
        }

        private int CalcNumHexesForRadius(int r)
        {
            return 1 + 3 * r * (r - 1);
        }

        private int GetNumHexesForRadius(int r)
        {
            return numHexesForRadius[r-1];
        }

        private int CalcDiameterFromRadius(int r)
        {
            return 1 + 2 * (r - 1);
        }

        
        /// <summary>
        /// Generates an array, representing a land layout. 
        /// The values indicate the initial y-offset for the 
        /// first tile in each row. Further y-offsets can be 
        /// calculated by subtracting 2 from the offset once
        /// for each tile.
        /// </summary>
        /// <param name="r">The radius of the layout to be generated</param>
        /// <returns>The array, described in the above summary</returns>
        private int[] CalcLayoutArrayFromRadius(int r)
        {
            var d = CalcDiameterFromRadius(r);
            var res = new int[d];
            var c = r - 1;
            res[c] = d - 1; // the center row of the land will

            for (int i=1; i<r; i++)
            {
                var w = d - i - 1; // need an offset of -1
                res[c - i] = w;
                res[c + i] = w;
            }

            return res;
        }
    }
}