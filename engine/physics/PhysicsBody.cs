using System;
using Microsoft.Xna.Framework;

namespace flatverse.physics
{
    public class PhysicsBody
    {
        private static PhysicsUtils.Sequence _ID_SEQUENCE = new PhysicsUtils.Sequence();

        protected PhysicsBodyLocation location;
        protected PhysicsBodyMotion motion;
        protected float weight;
        public readonly int ID;

        public PhysicsBody(PhysicsBodyLocation location, PhysicsBodyMotion motion)
        {
            this.location = location;
            this.motion = motion;
            ID = _ID_SEQUENCE.increment();
        }

        public virtual float fixCollision(PhysicsBody other) {
            PhysicsBodyLocation otherLoc = other.getLocation();

            if (!location.intersectsPath(otherLoc.getPath(1), 1))
            {
                return 1;
            }

            float t = 0.5f;
            float prevT = 1;
            float prevNon = 0;
            float tDelt = 0.25f;

            while (location.getDistance(t, prevT) >= 1 && otherLoc.getDistance(t, prevT) >= 1)
            {
                prevT = t;
                if (location.intersectsPath(otherLoc.getPath(t), t))
                {
                    t -= tDelt;
                }
                else
                {
                    prevNon = t;
                    t += tDelt;
                }
                tDelt = tDelt / 2;
            }

            if (location.intersectsPath(otherLoc.getPath(t), t))
            {
                t = prevNon;
            }

            return t;
        }

        public virtual Vector2 adjustPath(PhysicsBody other)
        {
            return location.adjustPath(other.getLocation());
        }

        public virtual void moveTo(Vector2 newPos)
        {
            location.moveTo(newPos);
        }

        public virtual PlatformInfo getPlatformInfo(PhysicsBody other)
        {
            return new PlatformInfo(this, other.getLocation().getBounds(1));
        }

        public Vector2 updatePos(PlatformInfo platformInfo, Vector2 newPos)
        {
            return motion.updatePos(platformInfo, newPos);
        }

        public virtual PhysicsBodyLocation getLocation()
        {
            return location;
        }

        public virtual PhysicsBodyMotion getMotion()
        {
            return motion;
        }

        public virtual float getWeight()
        {
            return weight;
        }

        public virtual void setWeight(float weight)
        {
            this.weight = weight;
        }
    }
}