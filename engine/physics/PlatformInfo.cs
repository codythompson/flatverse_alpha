using System;
using Microsoft.Xna.Framework;

namespace flatverse.physics
{
    public class PlatformInfo
    {
        public enum Position
        {
            ABOVE, BELOW, LEFTOF, RIGHTOF
        }

        public Position position;
        //public PhysicsBody platform;

        public PlatformInfo(Position position)
        {
            this.position = position;
        }
    }
}