using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace flatverse.physics
{
    public class Polygon
    {
        private LineSegment[] lines;
        private FVRectangle bnds;
        private List<Vector2> leftmostPoints;
        private List<Vector2> rightmostPoints;
        private List<Vector2> topmostPoints;
        private List<Vector2> botmostPoints;

        public Polygon(Vector2[] points)
        {
            lines = new LineSegment[points.Length];
            float left = float.PositiveInfinity;
            float right = float.NegativeInfinity;
            float top = float.PositiveInfinity;
            float bot = float.NegativeInfinity;
            for (int i = 1; i < points.Length; i++)
            {
                Vector2 pointA = points[i - 1];
                lines[i - 1] = new LineSegment(pointA, points[i]);
                if (pointA.X < left)
                {
                    left = pointA.X;
                    leftmostPoints = new List<Vector2>();
                    leftmostPoints.Add(pointA);
                }
                else if (pointA.X == left) {
                    leftmostPoints.Add(pointA);
                }
                
                if (pointA.X > right)
                {
                    right = pointA.X;
                    rightmostPoints = new List<Vector2>();
                    rightmostPoints.Add(pointA);
                }
                else if (pointA.X == right)
                {
                    rightmostPoints.Add(pointA);
                }

                if (pointA.Y < top)
                {
                    top = pointA.Y;
                    topmostPoints = new List<Vector2>();
                    topmostPoints.Add(pointA);
                }
                else if (pointA.Y == top) {
                    topmostPoints.Add(pointA);
                }
                
                if (pointA.Y > bot)
                {
                    bot = pointA.Y;
                    botmostPoints = new List<Vector2>();
                    botmostPoints.Add(pointA);
                }
                else if (pointA.Y == bot) {
                    botmostPoints.Add(pointA);
                }
            }
            Vector2 lastPoint = points[points.Length - 1];
            lines[points.Length - 1] = new LineSegment(lastPoint, points[0]);
            if (lastPoint.X < left)
            {
                left = lastPoint.X;
            }
            else if (lastPoint.X > right)
            {
                right = lastPoint.X;
            }

            if (lastPoint.Y < top)
            {
                top = lastPoint.Y;
            }
            else if (lastPoint.Y > bot)
            {
                bot = lastPoint.Y;
            }
            bnds = new FVRectangle(left, top, right - left, bot - top);
        }

        public FVRectangle bounds()
        {
            return bnds;
        }

        public float left()
        {
            return bnds.left();
        }

        public float right()
        {
            return bnds.right();
        }

        public float top()
        {
            return bnds.top();
        }

        public float bottom()
        {
            return bnds.bottom();
        }

        public bool contains(Vector2 point)
        {
            if (!bnds.contains(point))
            {
                return false;
            }

            LineSegment leftCheck = new LineSegment(new Vector2(float.NegativeInfinity, point.Y), point);
            //LineSegment rightCheck = new LineSegment(point, new Vector2(float.PositiveInfinity, point.Y));
            int leftCnt = 0;
            foreach (LineSegment line in lines)
            {
                if (line.intersects(leftCheck))
                {
                    leftCnt++;
                }
            }

            return leftCnt % 2 == 1;
        }

        public bool intersects(LineSegment line)
        {
            if (!bnds.intersects(line))
            {
                return false;
            }

            if (contains(line.getA()) || contains(line.getB()))
            {
                return true;
            }

            foreach (LineSegment edgeLine in lines)
            {
                if (edgeLine.intersects(line))
                {
                    return true;
                }
            }

            //if we've gotten this far the line is within the bounds but does not intersect.
            return false;
        }

        public bool intersects(FVRectangle rect)
        {
            if (!bnds.intersects(rect))
            {
                return false;
            }

            foreach (Vector2 point in points())
            {
                if (rect.contains(point))
                {
                    return true;
                }
            }

            return contains(rect.topLeft()) ||
                contains(rect.topRight()) ||
                contains(rect.bottomLeft()) ||
                contains(rect.bottomRight());
        }

        public LineSegment[] segments()
        {
            return lines;
        }

        public Vector2[] points()
        {
            Vector2[] pts = new Vector2[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                pts[i] = lines[i].getA();
            }
            return pts;
        }

        public List<Vector2> leftmost()
        {
            return leftmostPoints;
        }

        public List<Vector2> rightmost()
        {
            return rightmostPoints;
        }

        public List<Vector2> topmost()
        {
            return topmostPoints;
        }

        public List<Vector2> bottommost()
        {
            return botmostPoints;
        }
    }
}