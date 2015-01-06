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

        public virtual Tuple<float, float> fixCollision(PhysicsBody other) {
            throw new NotImplementedException();
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