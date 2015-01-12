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

        public static void AssertPolygonsAreEqual(Polygon expected, Polygon actual)
        {
            Vector2[] expPts = expected.points();
            Vector2[] actPts = actual.points();

            for (int i = 0; i < expPts.Length; i++)
            {
                if (VectorsAreEqual(actPts[0], expPts[i]) &&
                    AllPointsEqual(RotateArray<Vector2>(expPts, i), actPts))
                {
                    return;
                }
            }
            expPts = ReverseArray<Vector2>(expPts);
            for (int i = 0; i < expPts.Length; i++)
            {
                if (VectorsAreEqual(actPts[0], expPts[i]) &&
                    AllPointsEqual(RotateArray<Vector2>(expPts, i), actPts))
                {
                    return;
                }
            }

            Assert.Fail("Polygons not equal");
        }

        public static T[] RotateArray<T>(T[] array, int startIndex)
        {
            T[] rotatedArray = new T[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                int ix = (startIndex + i) % array.Length;
                rotatedArray[i] = array[ix];
            }

            return rotatedArray;
        }

        public static T[] ReverseArray<T>(T[] array)
        {
            T[] reversedArray = new T[array.Length];
            int i = reversedArray.Length;
            foreach (T ele in array)
            {
                reversedArray[--i] = ele;
            }

            return reversedArray;
        }

        public static bool VectorsAreEqual(Vector2 a, Vector2 b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool AllPointsEqual(Vector2[] expected, Vector2[] actual)
        {
            for (int i = 0; i < expected.Length; i++)
            {
                if (!VectorsAreEqual(expected[i], actual[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}