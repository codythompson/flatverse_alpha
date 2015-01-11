using System;
using Microsoft.Xna.Framework;
using flatverse.physics;

namespace flatverse.physics.test
{
    public class PhysicsBodyLocation_stub : PhysicsBodyLocation
    {
        public PhysicsBodyLocation_stub(Vector2 startLoc) : base(startLoc)
        {

        }

        public override Polygon getPath(float t)
        {
            throw new NotImplementedException();
        }

        public override FVRectangle getPathBoundingBox(float t)
        {
            throw new NotImplementedException();
        }

        public override Polygon getBounds(float t)
        {
            throw new NotImplementedException();
        }

        public override Polygon getPrevBounds(float t)
        {
            throw new NotImplementedException();
        }

        public override bool intersectsPath(Polygon other, float t)
        {
            throw new NotImplementedException();
        }

        public override bool intersectsPos(Polygon other, float t)
        {
            throw new NotImplementedException();
        }

        public override bool intersectsPrevPos(Polygon other)
        {
            throw new NotImplementedException();
        }

        public override Vector2 adjustPath(PhysicsBodyLocation other)
        {
            throw new NotImplementedException();
        }

        public override Vector2 selfAdjustPath(PhysicsBodyLocation heavier)
        {
            throw new NotImplementedException();
        }

        public override bool onPlatformAbove(Polygon other, float t)
        {
            throw new NotImplementedException();
        }

        public override bool onPlatformBelow(Polygon other, float t)
        {
            throw new NotImplementedException();
        }

        public override bool onPlatformLeftOf(Polygon other, float t)
        {
            throw new NotImplementedException();
        }

        public override bool onPlatformRightOf(Polygon other, float t)
        {
            throw new NotImplementedException();
        }
    }
}