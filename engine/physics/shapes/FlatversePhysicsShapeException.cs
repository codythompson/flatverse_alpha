using System;
using Microsoft.Xna.Framework;

namespace flatverse
{
    public class FlatversePhysicsShapeException : Exception
    {
        const string LINE_EXCEPTION = "Failed to create an instance of the line between {0} and {1}";
        public static void throwLineException(Vector2 a, Vector2 b)
        {
            throw new FlatversePhysicsShapeException("LineSegment", string.Format(LINE_EXCEPTION, a, b));
        }

        const string WEIGHT_CLASS_EXCEPTION = "Incompatible weight classes for '{0}' method, weights given: {1} and {2}";
        public static void throwAwayWeightClassException(Object thrower, float weightA, float weightB)
        {
            throw new FlatversePhysicsShapeException(thrower, string.Format(WEIGHT_CLASS_EXCEPTION, "collideAway", weightA, weightB));
        }

        public FlatversePhysicsShapeException(string instanceType, string message)
            : base(string.Format("['{0}' instance]: {1}", instanceType, message))
        {}

        public FlatversePhysicsShapeException(Object thrower, string message)
            : this(thrower.GetType().ToString(), message)
        {}
    }
}