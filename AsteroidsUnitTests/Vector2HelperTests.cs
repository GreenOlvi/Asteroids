using System;
using NUnit.Framework;
using Asteroids.Utils;
using FluentAssertions;
using Microsoft.Xna.Framework;

namespace AsteroidsUnitTests
{
    [TestFixture]
    public class Vector2HelperTests
    {
        [Test]
        [TestCase(0, 0, -1)]
        [TestCase(Math.PI/2, 1, 0)]
        [TestCase(Math.PI, 0, 1)]
        [TestCase(3*Math.PI/2, -1, 0)]
        [TestCase(Math.PI/6, 0.5f, -0.866f)]
        public void VectorAtAngleTest(double angle, float x, float y)
        {
            var v = Vector2Helper.VectorAtAngle((float) angle);
            v.X.Should().BeApproximately(x, 0.0001f);
            v.Y.Should().BeApproximately(y, 0.0001f);
        }


        [Test]
        [TestCase(10, 10, 10, 10)]
        [TestCase(0, 0, 0, 0)]
        [TestCase(100, 100, 0, 0)]
        [TestCase(210, 210, 10, 10)]
        [TestCase(-10, -10, 90, 90)]
        [TestCase(-1010, 1010, 90, 10)]
        public void WrapInsideTest(float x, float y, float rx, float ry)
        {
            Vector2Helper.WrapInside(new Vector2(x, y), new Rectangle(0, 0, 100, 100))
                .Should().Be(new Vector2(rx, ry));
        }
    }
}