using System;
using System.Collections.Generic;
using Xunit;

namespace WeepingSnake.Game.Tests.Geometry
{
    public class CoordinateSystemTests
    {
        [Fact]
        public void CalculatePointsOnHorizontalLineTest()
        {
            var actualA = MockCoordinateSystem.CalculatePointsOnLine(-2, 0, 2, 0);
            var expectedA = new List<(int, int)>()
            {
                (-2,0), (-1,0), (0,0), (1,0), (2,0)
            };
            Assert.Equal(expectedA, actualA);

            var actualB = MockCoordinateSystem.CalculatePointsOnLine(2, 0, -2, 0);
            var expectedB = new List<(int, int)>()
            {
                 (-2,0), (-1,0), (0,0), (1,0), (2,0)
            };
            Assert.Equal(expectedB, actualB);

            var actualC = MockCoordinateSystem.CalculatePointsOnLine(-5, 9, -1, 9);
            var expectedC = new List<(int, int)>()
            {
                (-5,9), (-4,9), (-3,9), (-2,9), (-1,9)
            };
            Assert.Equal(expectedC, actualC);

            var actualD = MockCoordinateSystem.CalculatePointsOnLine(1, -4, 5, -4);
            var expectedD = new List<(int, int)>()
            {
                (1,-4),(2,-4),(3,-4),(4,-4),(5,-4)
            };
            Assert.Equal(expectedD, actualD);
        }
        [Fact]
        public void CalculatePointsOnVerticalLineTest()
        {
            var actualA = MockCoordinateSystem.CalculatePointsOnLine(0, -2, 0, 2);
            var expectedA = new List<(int, int)>()
            {
                (0,-2), (0,-1), (0,0), (0,1), (0,2)
            };
            Assert.Equal(expectedA, actualA);

            var actualB = MockCoordinateSystem.CalculatePointsOnLine(0, 2, 0, -2);
            var expectedB = new List<(int, int)>()
            {
                 (0,-2), (0,-1), (0,0), (0,1), (0,2)
            };
            Assert.Equal(expectedB, actualB);

            var actualC = MockCoordinateSystem.CalculatePointsOnLine(9, -5, 9, -1);
            var expectedC = new List<(int, int)>()
            {
                (9,-5), (9,-4), (9,-3), (9,-2), (9,-1)
            };
            Assert.Equal(expectedC, actualC);

            var actualD = MockCoordinateSystem.CalculatePointsOnLine(-4, 1, -4, 5);
            var expectedD = new List<(int, int)>()
            {
                (-4,1),(-4,2),(-4,3),(-4,4),(-4,5)
            };
            Assert.Equal(expectedD, actualD);
        }

        [Fact]
        public void CalculatePointsOnBisectorTest()
        {
            var actualA = MockCoordinateSystem.CalculatePointsOnLine(-4, -4, 2, 2);
            var expectedA = new List<(int, int)>()
            {
                (-4,-4), (-3,-3), (-2,-2), (-1,-1), (0,0), (1,1), (2,2)
            };
            Assert.Equal(expectedA, actualA);

            var actualB = MockCoordinateSystem.CalculatePointsOnLine(3, 3, -2, -2);
            var expectedB = new List<(int, int)>()
            {
                 (-2,-2), (-1,-1), (0,0), (1,1), (2,2), (3,3)
            };
            Assert.Equal(expectedB, actualB);

            var actualC = MockCoordinateSystem.CalculatePointsOnLine(-5, 5, -1, 1);
            var expectedC = new List<(int, int)>()
            {
                (-5,5), (-4,4), (-3,3), (-2,2), (-1,1)
            };
            Assert.Equal(expectedC, actualC);

            var actualD = MockCoordinateSystem.CalculatePointsOnLine(4, -4, -4, 4);
            var expectedD = new List<(int, int)>()
            {
                (-4,4),(-3,3),(-2,2),(-1,1),(0,0),(1,-1),(2,-2),(3,-3),(4,-4)
            };
            Assert.Equal(expectedD, actualD);
        }

        [Fact]
        public void CalculatePointsOnFlatLineTest()
        {
            var coordinateSystem = new MockCoordinateSystem();
            //throw new NotImplementedException();
        }

        [Fact]
        public void CalculatePointsOnSteepLineTest()
        {
            var coordinateSystem = new MockCoordinateSystem();
            //throw new NotImplementedException();
        }
    }
}
