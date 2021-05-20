using System;
using System.Collections.Generic;
using System.Linq;

namespace WeepingSnake.Game.Geometry
{
    public abstract class CoordinateSystem
    {
        private readonly ICoordinateSystemDimensions _dimensions;

        protected CoordinateSystem(ICoordinateSystemDimensions dimensions)
        {
            _dimensions = dimensions;
        }

        public uint Height => _dimensions.Height;
        public uint Width => _dimensions.Width;

        /// <summary>
        /// Rasterisation via Bresenham's line algorithm
        /// </summary>
        /// <returns>Enumeration of the sweeping pixel</returns>
        protected IEnumerable<(int, int)> CalculatePointsOnLine(int x0, int y0, int x1, int y1)
        {
            if (Math.Abs(y1 - y0) > Math.Abs(x1 - x0))
                return CalculatePointsOnLine(y0, x0, y1, x1).Select(i => (i.Item2, i.Item1));
            else if (x0 > x1)
                return CalculatePointsOnLine(x1, y1, x0, y0);
            else
                return SimpleBresenham(x0, y0, x1, y1);

            static IEnumerable<(int, int)> SimpleBresenham(int x0, int y0, int x1, int y1)
            {
                var deltaX = x1 - x0;
                var deltaY = Math.Abs(y1 - y0);
                var deltaError = deltaX / 2;
                var yStep = y0 < y1 ? 1 : -1;

                for (int x = x0, y = y0; x <= x1; x++)
                {
                    yield return (x, y);
                    deltaError -= deltaY;

                    if (deltaError >= 0) continue;

                    y += yStep;
                    deltaError += deltaX;
                }
            }
        }

        /// <summary>
        /// Rasterisation via Bresenham's line algorithm
        /// </summary>
        /// <returns>Enumeration of the sweeping pixel</returns>
        protected IEnumerable<(int, int)> CalculatePointsOnLine(GameDistance path)
        {
            return CalculatePointsOnLine((int)path.StartX, (int)path.StartY, (int)path.EndX, (int)path.EndY);
        }
    }
}