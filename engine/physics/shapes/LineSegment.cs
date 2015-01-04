using System;
using Microsoft.Xna.Framework;

namespace flatverse.physics
{
    /*
     * TODO - change topMost leftMost etc. to topLeft topRight bottomLeft bottomRight
     *        that way a point w/o a NaN can always be provided
     */
    public class LineSegment
    {
        private Vector2 a, b;
        private LineSegmentRep lineRep;

        public LineSegment(Vector2 a, Vector2 b)
        {
            this.a = a;
            this.b = b;

            if (a.X == b.X)
            {
                lineRep = new VertLineSegment(a, b);
            }
            else if (a.Y == b.Y)
            {
                lineRep = new HorLineSegment(a, b);
            }
            else
            {
                lineRep = new NonAxisAlignedLineSegment(a, b);
            }
        }

        public Vector2 getA()
        {
            return a;
        }

        public Vector2 getB()
        {
            return b;
        }

        public LineSegmentRep getLineRep()
        {
            return lineRep;
        }

        public float left()
        {
            return lineRep.left();
        }

        public float right()
        {
            return lineRep.right();
        }

        public float top()
        {
            return lineRep.top();
        }

        public float bottom()
        {
            return lineRep.bottom();
        }

        public Vector2 leftMost()
        {
            return lineRep.leftMost();
        }

        public Vector2 rightMost()
        {
            return lineRep.rightMost();
        }

        public Vector2 topMost()
        {
            return lineRep.topMost();
        }

        public Vector2 bottomMost()
        {
            return lineRep.bottomMost();
        }

        public Vector2 distance()
        {
            return lineRep.distance();
        }

        public float slope()
        {
            return lineRep.slope();
        }

        public float yIntercept()
        {
            return lineRep.yIntercept();
        }

        public float yAt(float x)
        {
            return lineRep.yAt(x);
        }

        public float xAt(float y)
        {
            return lineRep.xAt(y);
        }

        public bool contains(Vector2 point)
        {
            return lineRep.contains(point);
        }

        public bool containsX(float x)
        {
            return lineRep.containsX(x);
        }

        public bool containsY(float y)
        {
            return lineRep.containsY(y);
        }

        public bool intersects(LineSegment line)
        {
            LineSegmentRep otherRep = line.getLineRep();
            if (otherRep.isHorizontal())
            {
                return lineRep.intersects((HorLineSegment)otherRep);
            }
            else if (otherRep.isVertical())
            {
                return lineRep.intersects((VertLineSegment)otherRep);
            }
            else
            {
                return lineRep.intersects((NonAxisAlignedLineSegment)otherRep);
            }
        }

        public bool isAboveThis(Polygon polygon)
        {
            return lineRep.isAboveThis(polygon);
        }

        public bool isBelowThis(Polygon polygon)
        {
            return lineRep.isBelowThis(polygon);
        }

        public bool isRightOfThis(Polygon polygon)
        {
            return lineRep.isRightOfThis(polygon);
        }

        public bool isLeftOfThis(Polygon polygon)
        {
            return lineRep.isLeftOfThis(polygon);
        }

        public bool isVertical()
        {
            return lineRep.isVertical();
        }

        public bool isHorizontal()
        {
            return lineRep.isHorizontal();
        }

        public void move(Vector2 delta)
        {
            a += delta;
            b += delta;
            lineRep.move(delta);
        }

        public LineSegment clone()
        {
            return new LineSegment(a, b);
        }

        public static LineSegment operator +(LineSegment line, Vector2 delta)
        {
            line.move(delta);
            return line;
        }

        public static LineSegment operator -(LineSegment line, Vector2 delta)
        {
            delta = -delta;
            line.move(delta);
            return line;
        }

        /*
         * Interface for line segment representations used by this class
         */
        public interface LineSegmentRep
        {
            float left();
            float right();
            float top();
            float bottom();
            Vector2 leftMost();
            Vector2 rightMost();
            Vector2 topMost();
            Vector2 bottomMost();
            Vector2 distance();
            float slope();
            float yIntercept();
            float yAt(float x);
            float xAt(float y);
            bool contains(Vector2 point);
            bool containsX(float x);
            bool containsY(float y);
            bool intersects(NonAxisAlignedLineSegment line);
            bool intersects(VertLineSegment line);
            bool intersects(HorLineSegment line);
            bool isAboveThis(Polygon polygon);
            bool isBelowThis(Polygon polygon);
            bool isLeftOfThis(Polygon polygon);
            bool isRightOfThis(Polygon polygon);
            bool isVertical();
            bool isHorizontal();
            void move(Vector2 delta);
        }

        /*
         * Horizontal line representation
         */
        public class HorLineSegment : LineSegmentRep
        {
            private float y, leftX, rightX;

            public HorLineSegment(Vector2 a, Vector2 b)
            {
                setup(a, b);
            }

            private void setup(Vector2 a, Vector2 b)
            {
                if (a.Y != b.Y)
                {
                    FlatversePhysicsShapeException.throwLineException(a, b);
                }

                y = a.Y;
                if (a.X <= b.X)
                {
                    leftX = a.X;
                    rightX = b.X;
                }
                else
                {
                    leftX = b.X;
                    rightX = a.X;
                }
            }

            public float left()
            {
                return leftX;
            }

            public float right()
            {
                return rightX;
            }

            public float top()
            {
                return y;
            }

            public float bottom()
            {
                return y;
            }

            public Vector2 leftMost()
            {
                return new Vector2(leftX, y);
            }

            public Vector2 rightMost()
            {
                return new Vector2(rightX, y);
            }

            public Vector2 topMost()
            {
                return new Vector2(float.NaN, y);
            }

            public Vector2 bottomMost()
            {
                return new Vector2(float.NaN, y);
            }

            public Vector2 distance()
            {
                return new Vector2(leftX - rightX, 0);
            }

            public float slope()
            {
                return 0;
            }

            public float yIntercept()
            {
                return y;
            }

            public float yAt(float x)
            {
                if (x < leftX || x > rightX)
                {
                    return float.NaN;
                }
                return y;
            }

            public float xAt(float y)
            {
                return float.NaN;
            }

            public bool contains(Vector2 point)
            {
                return point.Y == y && point.X >= leftX && point.X <= rightX;
            }

            public bool containsX(float x)
            {
                return x >= leftX && x <= rightX;
            }

            public bool containsY(float y)
            {
                return this.y == y;
            }

            public bool intersects(NonAxisAlignedLineSegment line)
            {
                return containsX(line.xAt(y));
            }

            public bool intersects(VertLineSegment line)
            {
                return containsX(line.xAt(y));
            }

            public bool intersects(HorLineSegment line)
            {
                Vector2 otherLeft = line.leftMost();
                return y == otherLeft.Y && (containsX(otherLeft.X) || containsX(line.right()));
            }

            public bool isAboveThis(Polygon polygon)
            {
                return polygon.right() >= leftX && polygon.left() <= rightX && polygon.bottom() <= y;
            }

            public bool isBelowThis(Polygon polygon)
            {
                return polygon.right() >= leftX && polygon.left() <= rightX && polygon.top() >= y;
            }

            public bool isLeftOfThis(Polygon polygon)
            {
                return polygon.top() <= y && polygon.bottom() >= y && polygon.right() <= leftX;
            }

            public bool isRightOfThis(Polygon polygon)
            {
                return polygon.top() <= y && polygon.bottom() >= y && polygon.left() >= rightX;
            }

            public bool isVertical()
            {
                return false;
            }

            public bool isHorizontal()
            {
                return true;
            }

            public void move(Vector2 delta)
            {
                leftX += delta.X;
                rightX += delta.X;
                y += delta.Y;
            }
        }

        /*
         * Vertical line representation
         */
        public class VertLineSegment : LineSegmentRep
        {
            private float x, topY, botY;

            public VertLineSegment(Vector2 a, Vector2 b)
            {
                setup(a, b);
            }

            public void setup(Vector2 a, Vector2 b)
            {
                if (a.X != b.X)
                {
                    FlatversePhysicsShapeException.throwLineException(a, b);
                }

                x = a.X;
                if (a.Y <= b.Y)
                {
                    topY = a.Y;
                    botY = b.Y;
                }
                else
                {
                    topY = b.Y;
                    botY = a.Y;
                }
            }

            public float left()
            {
                return x;
            }

            public float right()
            {
                return x;
            }

            public float top()
            {
                return topY;
            }

            public float bottom()
            {
                return botY;
            }

            public Vector2 leftMost()
            {
                return new Vector2(x, float.NaN);
            }

            public Vector2 rightMost()
            {
                return new Vector2(x, float.NaN);
            }

            public Vector2 topMost()
            {
                return new Vector2(x, topY);
            }

            public Vector2 bottomMost()
            {
                return new Vector2(x, botY);
            }

            public Vector2 distance()
            {
                return new Vector2(0, botY - topY);
            }

            public float slope()
            {
                return float.PositiveInfinity;
            }

            public float yIntercept()
            {
                return float.NaN;
            }

            public float yAt(float x)
            {
                return float.NaN;
            }

            public float xAt(float y)
            {
                if (y < topY || y > botY)
                {
                    return float.NaN;
                }
                return x;
            }

            public bool contains(Vector2 point)
            {
                return point.X == x && point.Y >= topY && point.Y <= botY;
            }

            public bool containsX(float x)
            {
                return x == this.x;
            }

            public bool containsY(float y)
            {
                return y >= topY && y <= botY;
            }

            public bool intersects(NonAxisAlignedLineSegment line)
            {
                float yIntersection = line.yAt(x);
                return containsY(yIntersection);
            }

            public bool intersects(VertLineSegment line)
            {
                Vector2 otherTop = line.topMost();
                return containsX(otherTop.X) && (containsY(line.bottomMost().Y) || containsY(otherTop.Y));
            }

            public bool intersects(HorLineSegment line)
            {
                return containsY(line.yAt(x));
            }

            public bool isAboveThis(Polygon polygon)
            {
                return polygon.left() <= x && polygon.right() >= x && polygon.bottom() <= topY;
            }

            public bool isBelowThis(Polygon polygon)
            {
                return polygon.left() <= x && polygon.right() >= x && polygon.top() >= botY;
            }

            public bool isLeftOfThis(Polygon polygon)
            {
                return polygon.bottom() >= topY && polygon.top() <= botY && polygon.right() <= x;
            }

            public bool isRightOfThis(Polygon polygon)
            {
                return polygon.bottom() >= topY && polygon.top() <= botY && polygon.left() >= x;
            }

            public bool isVertical()
            {
                return true;
            }

            public bool isHorizontal() {
                return false;
            }

            public void move(Vector2 delta)
            {
                x += delta.X;
                topY += delta.Y;
                botY += delta.Y;
            }
        } 

        /*
         * Non - axis aligned line segment representation (A.K.A. not vertical or horizontal)
         */
        public class NonAxisAlignedLineSegment : LineSegmentRep
        {
            private Vector2 a, b;
            private float slp, yIcpt;

            public NonAxisAlignedLineSegment(Vector2 a, Vector2 b)
            {
                setup(a, b);
            }

            public void setup(Vector2 a, Vector2 b)
            {
                if (a.X == b.X)
                {
                    FlatversePhysicsShapeException.throwLineException(a, b);
                }

                this.a = a;
                this.b = b;

                slp = (b.Y - a.Y) / (b.X - a.X);
                yIcpt = a.Y - (a.X * slp);
            }

            public float left()
            {
                if (a.X > b.X)
                {
                    return b.X;
                }
                return a.X;
            }

            public float right()
            {
                if (a.X > b.X)
                {
                    return a.X;
                }
                return b.X;
            }

            public float top()
            {
                if (a.Y > b.Y)
                {
                    return b.Y;
                }
                return a.Y;
            }

            public float bottom()
            {
                if (a.Y > b.Y)
                {
                    return a.Y;
                }
                return b.Y;
            }

            public Vector2 leftMost()
            {
                if (a.X > b.X)
                {
                    return b;
                }
                return a;
            }

            public Vector2 rightMost()
            {
                if (a.X > b.X)
                {
                    return a;
                }
                return b;
            }

            public Vector2 topMost()
            {
                if (a.Y > b.Y)
                {
                    return b;
                }
                return a;
            }

            public Vector2 bottomMost()
            {
                if (a.Y > b.Y)
                {
                    return a;
                }
                return b;
            }

            public Vector2 distance()
            {
                return rightMost() - leftMost();
            }

            public float slope()
            {
                return slp;
            }

            public float yIntercept()
            {
                return yIcpt;
            }

            public float yAt(float x)
            {
                float y = (x * slp) + yIcpt;

                if (y > bottom() || y < top())
                {
                    return float.NaN;
                }
                return y;
            }

            public float xAt(float y)
            {
                float x = (y - yIcpt) / slp;
                
                if (x < left() || x > right())
                {
                    return float.NaN;
                }
                return x;
            }

            public bool contains(Vector2 point)
            {
                return yAt(point.X) == point.Y;
            }

            public bool containsX(float x)
            {
                return leftMost().X <= x && rightMost().X >= x;
            }

            public bool containsY(float y)
            {
                return topMost().Y <= y && bottomMost().Y >= y;
            }

            public bool intersects(NonAxisAlignedLineSegment line)
            {
                if (line.slope() == slp) // parallel
                {
                    if (line.yIntercept() == yIcpt) // coinciding lines
                    {
                        return ((line.rightMost().X >= leftMost().X && line.leftMost().X <= leftMost().X) ||
                            (line.rightMost().X >= rightMost().X && line.leftMost().X <= rightMost().X));
                    }
                    else // parallel non coinciding lines
                    {
                        return false;
                    }
                }

                float intersectionX = (line.yIntercept() - yIcpt) / (slp - line.slope());

                return containsX(intersectionX) && line.containsX(intersectionX);
            }

            public bool intersects(VertLineSegment line)
            {
                return line.containsY(yAt(line.left()));
            }

            public bool intersects(HorLineSegment line)
            {
                return line.containsX(xAt(line.top()));
            }

            public bool isAboveThis(Polygon polygon)
            {
                foreach (Vector2 point in polygon.points())
                {
                    float yAtPt = yAt(point.X);
                    if (!float.IsNaN(yAtPt) && yAtPt < point.Y)
                    {
                        return false;
                    }
                }
                return true;
            }

            public bool isBelowThis(Polygon polygon)
            {
                foreach (Vector2 point in polygon.points())
                {
                    float yAtPt = yAt(point.X);
                    if (!float.IsNaN(yAtPt) && yAtPt > point.Y)
                    {
                        return false;
                    }
                }
                return true;
            }

            public bool isRightOfThis(Polygon polygon)
            {
                foreach (Vector2 point in polygon.points())
                {
                    float xAtPt = xAt(point.Y);
                    if (!float.IsNaN(xAtPt) && xAtPt > point.X)
                    {
                        return false;
                    }
                }
                return true;
            }

            public bool isLeftOfThis(Polygon polygon)
            {
                foreach (Vector2 point in polygon.points())
                {
                    float xAtPt = xAt(point.Y);
                    if (!float.IsNaN(xAtPt) && xAtPt < point.X)
                    {
                        return false;
                    }
                }
                return true;
            }

            public bool isVertical()
            {
                return false;
            }

            public bool isHorizontal() {
                return a.Y == b.Y;
            }

            public void move(Vector2 delta)
            {
                setup(a + delta, b + delta);
            }
        }
    }
}