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

        protected uint Height => _dimensions.Height;
        protected uint Width => _dimensions.Width;

        /// <summary>
        /// Rasterisation via Bresenham's line algorithm
        /// </summary>
        /// <returns>Enumeration of the sweeping pixel</returns>
        protected static IEnumerable<(int, int)> CalculatePointsOnLine(int x0, int y0, int x1, int y1)
        {
            if (Math.Abs(y1 - y0) > Math.Abs(x1 - x0))
                return CalculatePointsOnLine(y0, x0, y1, x1).Select(i => (i.Item2, i.Item1));
            else if (x0 > x1)
                return CalculatePointsOnLine(x1, y1, x0, y0);
            else
                return Bresenham(x0, y0, x1, y1);

            static IEnumerable<(int, int)> Bresenham(int x0, int y0, int x1, int y1)
            {
                int deltaX = x1 - x0;
                int deltaY = Math.Abs(y1 - y0);
                int deltaError = deltaX / 2;
                int yStep = (y0 < y1) ? 1 : -1;

                for (int x = x0, y = y0; x <= x1; x++)
                {
                    yield return (x, y);
                    deltaError -= deltaY;

                    if (deltaError < 0)
                    {
                        y += yStep;
                        deltaError += deltaX;
                    }
                }
            }
        }
    }
}