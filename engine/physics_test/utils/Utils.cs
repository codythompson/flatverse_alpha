using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using flatverse.physics;
using Microsoft.Xna.Framework;

namespace flatverse.physics.test
{
    public static class Utils
    {
        public static void AssertVectorsAreEqual(Vector2 expected, Vector2 actual)
        {
            Assert.AreEqual(expected.X, actual.X);
            Assert.AreEqual(expected.Y, actual.Y);
        }
    }
}