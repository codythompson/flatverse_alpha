using System;
using Microsoft.Xna.Framework;

namespace flatverse.physics
{
    public static class PhysicsUtils
    {
        public class Sequence
        {
            private int counter = 0;

            public virtual int increment()
            {
                return counter++;
            }

            public virtual int getNext()
            {
                return counter;
            }
        }
    }
}