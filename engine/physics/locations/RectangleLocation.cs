using System;
using Microsoft.Xna.Framework;

namespace flatverse.physics
{
    public class RectangleLocation : PhysicsBodyLocation
    {
        public readonly float width, height;

        public RectangleLocation(Vector2 pos, float width, float height) : base(pos)
        {
            this.width = width;
            this.height = height;
        }

        public override Polygon getPath(float t)
        {
            Vector2 curPos = getIntermediate(t);
            Vector2 delta = curPos - prevPos;

            FVRectangle rect = new FVRectangle(curPos, new Vector2(width, height));
            FVRectangle prevRect = new FVRectangle(prevPos, new Vector2(width, height));

            Vector2[] pts;
            if (curPos == prevPos) // no movement
            {
                pts = new Vector2[] {
                    rect.topLeft(),
                    rect.topRight(),
                    rect.bottomRight(),
                    rect.bottomLeft()
                };
            }
            else if (curPos.X == prevPos.X) // moved in y direction only
            {
                // moving downwards
                if (delta.Y > 0)
                {
                    pts = new Vector2[] {
                        prevRect.topLeft(),
                        prevRect.topRight(),
                        rect.bottomRight(),
                        rect.bottomLeft()
                    };
                }
                else // moving upwards
                {
                    pts = new Vector2[] {
                        rect.topLeft(),
                        rect.topRight(),
                        prevRect.bottomRight(),
                        prevRect.bottomLeft(),
                    };
                }
            }
            else if (curPos.Y == prevPos.Y) // moved in x direction only
            {
                if (delta.X > 0) // moving right
                {
                    pts = new Vector2[] {
                        prevRect.topLeft(),
                        rect.topRight(),
                        rect.bottomRight(),
                        prevRect.bottomLeft()
                    };
                }
                else // moving left
                {
                    pts = new Vector2[] {
                        rect.topLeft(),
                        prevRect.topRight(),
                        prevRect.bottomRight(),
                        rect.bottomLeft()
                    };
                }
            }
            else
            {
                pts = new Vector2[6];

                if (delta.X >= 0)
                {
                    // going down and to the right
                    if (delta.Y >= 0)
                    {
                        pts[0] = prevRect.bottomLeft();
                        pts[1] = prevRect.topLeft();
                        pts[2] = prevRect.topRight();

                        pts[3] = rect.topRight();
                        pts[4] = rect.bottomRight();
                        pts[5] = rect.bottomLeft();
                    }
                    // going up and to the right
                    else
                    {
                        pts[0] = prevRect.bottomRight();
                        pts[1] = prevRect.bottomLeft();
                        pts[2] = prevRect.topLeft();

                        pts[3] = rect.topLeft();
                        pts[4] = rect.topRight();
                        pts[5] = rect.bottomRight();
                    }
                }
                else
                {
                    // going down and to the left
                    if (delta.Y >= 0)
                    {
                        pts[0] = prevRect.topLeft();
                        pts[1] = prevRect.topRight();
                        pts[2] = prevRect.bottomRight();

                        pts[3] = rect.bottomRight();
                        pts[4] = rect.bottomLeft();
                        pts[5] = rect.topLeft();
                    }
                    // going up and to the left
                    else
                    {
                        pts[0] = prevRect.bottomLeft();
                        pts[1] = prevRect.bottomRight();
                        pts[2] = prevRect.topRight();

                        pts[3] = rect.topRight();
                        pts[4] = rect.topLeft();
                        pts[5] = rect.bottomLeft();
                    }
                }

            }
            return new Polygon(pts);
        }

        /*
         * 
         */
        public override FVRectangle getPathBoundingBox(float t)
        {
            Vector2 curPos = getIntermediate(t);
            Vector2 delta = curPos - prevPos;

            FVRectangle rect = new FVRectangle(curPos, new Vector2(width, height));
            FVRectangle prevRect = new FVRectangle(prevPos, new Vector2(width, height));

            float topmost = rect.top() < prevRect.top() ? rect.top() : prevRect.top();
            float bottommost = rect.bottom() > prevRect.bottom() ? rect.bottom() : prevRect.bottom();
            float leftmost = rect.left() < prevRect.left() ? rect.left() : prevRect.left();
            float rightmost = rect.right() > prevRect.right() ? rect.right() : prevRect.right();

            return new FVRectangle(leftmost, topmost, rightmost - leftmost, bottommost - topmost);
        }

        public override Polygon getBounds(float t)
        {
            Vector2 curPos = getIntermediate(t);
            return new Polygon(new Vector2[] {
                curPos,
                curPos + new Vector2(width, 0),
                curPos + new Vector2(width, height),
                curPos + new Vector2(0, height)
            });
        }

        public override Polygon getPrevBounds(float t)
        {
            Vector2 prevPos = getIntermediate(t);
            return new Polygon(new Vector2[] {
                prevPos,
                prevPos + new Vector2(width, 0),
                prevPos + new Vector2(width, height),
                prevPos + new Vector2(0, height)
            });
        }

        public override bool intersectsPath(Polygon other, float t)
        {
            Polygon path = getPath(t);
            return path.intersects(other);
        }

        public override bool intersectsPos(Polygon other, float t)
        {
            FVRectangle rect = new FVRectangle(getIntermediate(t), new Vector2(width, height));
            return other.intersects(rect);
        }

        public override bool intersectsPrevPos(Polygon other)
        {
            FVRectangle prevRect = new FVRectangle(prevPos, new Vector2(width, height));
            return other.intersects(prevRect);
        }

        public override Vector2 adjustPath(PhysicsBodyLocation other)
        {
            return new Vector2(0);
        }

        public override Vector2 selfAdjustPath(PhysicsBodyLocation heavier)
        {
            return new Vector2(0);
        }

        public override bool onPlatformAbove(Polygon other, float t)
        {
            Vector2 curPos = getIntermediate(t);
            FVRectangle spaceAbove = new FVRectangle(curPos.X, curPos.Y - 1, width, 1);
            return other.intersects(spaceAbove);
        }

        public override bool onPlatformBelow(Polygon other, float t)
        {
            Vector2 curPos = getIntermediate(t);
            FVRectangle spaceBelow = new FVRectangle(curPos.X, curPos.Y + height, width, 1);
            return other.intersects(spaceBelow);
        }

        public override bool onPlatformLeftOf(Polygon other, float t)
        {
            Vector2 curPos = getIntermediate(t);
            FVRectangle spaceLeft = new FVRectangle(curPos.X - 1, curPos.Y, 1, height);
            return other.intersects(spaceLeft);
        }

        public override bool onPlatformRightOf(Polygon other, float t)
        {
            Vector2 curPos = getIntermediate(t);
            FVRectangle spaceRight = new FVRectangle(curPos.X + width, curPos.Y, 1, height);
            return other.intersects(spaceRight);
        }
    }
}