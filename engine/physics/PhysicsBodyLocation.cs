using System;
using Microsoft.Xna.Framework;

namespace flatverse.physics
{
    public abstract class PhysicsBodyLocation
    {
        public bool selfAdjusting = false;

        protected Vector2 pos;
        protected Vector2 prevPos;

        public PhysicsBodyLocation(Vector2 pos)
        {
            this.pos = pos;
            this.prevPos = pos;
        }

        public virtual void moveTo(Vector2 newPos)
        {
            prevPos = pos;
            pos = newPos;
        }

        public virtual Vector2 getPos()
        {
            return pos;
        }

        public virtual Vector2 getPrevPos()
        {
            return prevPos;
        }

        public virtual Vector2 getIntermediate(float t)
        {
            if (t == 0) return prevPos;
            if (t == 1) return pos;

            Vector2 diff = pos - prevPos;
            diff *= t;

            return prevPos + ((pos - prevPos) * t);
        }

        public virtual float getDistance(float t1, float t2)
        {
            Vector2 p1 = getIntermediate(t1);
            Vector2 p2 = getIntermediate(t2);

            return Vector2.Distance(p1, p2);
        }

        public abstract Polygon getPath(float t);

        public abstract Rectangle getPathBoundingBox(float t);

        public abstract Polygon getBounds(float t);

        public abstract Polygon getPrevBounds(float t);

        public abstract bool intersectsPath(Polygon other, float t);

        public abstract bool intersectsPos(Polygon other, float t);

        public abstract bool intersectsPrevPos(Polygon other, float t);

        public abstract Vector2 adjustPath(PhysicsBodyLocation other);

        public abstract Vector2 selfAdjustPath(PhysicsBodyLocation heavier);

        public abstract bool onPlatformAbove(Polygon other);

        public abstract bool onPlatformBelow(Polygon other);

        public abstract bool onPlatformLeftOf(Polygon other);

        public abstract bool onPlatformRightOf(Polygon other);
    }
}
