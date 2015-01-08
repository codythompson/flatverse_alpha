using System;
using Microsoft.Xna.Framework;

namespace flatverse.physics
{
    public class PlatformInfo
    {
        public enum PositionY
        {
            NONE, ABOVE, BELOW, BOTH // both is not necessarily a valid state,
                                     // but I don't think that this is the best
                                     // place to make that decision
        }

        public enum PositionX
        {
            NONE, LEFTOF, RIGHTOF, BOTH
        }

        public PositionX positionX;
        public PositionY positionY;
        public PhysicsBody platform;

        public PlatformInfo(PositionX positionX, PositionY positionY, PhysicsBody platform)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.platform = platform;
        }

        public PlatformInfo(PhysicsBody platform, Polygon other)
        {
            this.platform = platform;

            PhysicsBodyLocation pLoc = platform.getLocation();

            // figure out positionY
            if (pLoc.onPlatformAbove(other, 1))
            {
                if (pLoc.onPlatformBelow(other, 1))
                {
                    positionY = PositionY.BOTH;
                }
                else
                {
                    positionY = PositionY.ABOVE;
                }
            }
            else if (pLoc.onPlatformBelow(other, 1))
            {
                positionY = PositionY.BELOW;
            }
            else
            {
                positionY = PositionY.NONE;
            }

            // figure out positionX
            if (pLoc.onPlatformLeftOf(other, 1))
            {
                if (pLoc.onPlatformRightOf(other, 1))
                {
                    positionX = PositionX.BOTH;
                }
                else
                {
                    positionX = PositionX.LEFTOF;
                }
            }
            else if (pLoc.onPlatformRightOf(other, 1))
            {
                positionX = PositionX.RIGHTOF;
            }
            else
            {
                positionX = PositionX.NONE;
            }
        }

        public bool isNoPlatform()
        {
            return positionY == PositionY.NONE && positionX == PositionX.NONE;
        }

        public bool isAboveLeft()
        {
            return positionY == PositionY.ABOVE && positionX == PositionX.LEFTOF;
        }

        public bool isAboveRight()
        {
            return positionY == PositionY.ABOVE && positionX == PositionX.RIGHTOF;
        }

        public bool isBelowLeft()
        {
            return positionY == PositionY.BELOW && positionX == PositionX.LEFTOF;
        }

        public bool isBelowRight()
        {
            return positionY == PositionY.BELOW && positionX == PositionX.RIGHTOF;
        }

        public bool isAboveOnly()
        {
            return positionY == PositionY.ABOVE && positionX == PositionX.NONE;
        }

        public bool isBelowOnly()
        {
            return positionY == PositionY.BELOW && positionX == PositionX.NONE;
        }

        public bool isLeftOfOnly()
        {
            return positionX == PositionX.LEFTOF && positionY == PositionY.NONE;
        }

        public bool isRightOfOnly()
        {
            return positionX == PositionX.RIGHTOF && positionY == PositionY.NONE;
        }
    }
}