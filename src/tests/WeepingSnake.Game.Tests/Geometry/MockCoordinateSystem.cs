using System.Collections.Generic;
using WeepingSnake.Game.Geometry;

namespace WeepingSnake.Game.Tests.Geometry
{
    public class MockCoordinateSystem :CoordinateSystem
    {
        public MockCoordinateSystem():base(new MockCoordinateSystemDimensions())
        {

        }

        public new IEnumerable<(int, int)> CalculatePointsOnLine(int x0, int y0, int x1, int y1)
        {
            return base.CalculatePointsOnLine(x0, y0, x1, y1);
        }
    
    }
}
