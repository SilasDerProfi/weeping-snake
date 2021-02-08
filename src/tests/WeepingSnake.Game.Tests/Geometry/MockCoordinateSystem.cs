using WeepingSnake.Game.Geometry;

namespace WeepingSnake.Game.Tests.Geometry
{
    public class MockCoordinateSystem :CoordinateSystem
    {
        public MockCoordinateSystem():base(new MockCoordinateSystemDimensions())
        {

        }
    }
}
