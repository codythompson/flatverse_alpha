using System;
using Microsoft.Xna.Framework;

namespace flatverse.physics
{
    public class FVRectangle
    {
        private float x, y;
        private float wdth, hght;

        public FVRectangle(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.wdth = width;
            this.hght = height;
        }

        public FVRectangle(Vector2 topLeft, Vector2 dims)
            : this(topLeft.X, topLeft.Y, dims.X, dims.Y)
        {}

        public Vector2 topLeft()
        {
            return new Vector2(x, y);
        }

        public Vector2 topRight()
        {
            return new Vector2(x + wdth, y);
        }

        public Vector2 bottomLeft()
        {
            return new Vector2(x, y + hght);
        }

        public Vector2 bottomRight()
        {
            return new Vector2(x + wdth, y + hght);
        }

        public float left()
        {
            return x;
        }

        public float right()
        {
            return x + wdth;
        }

        public float top()
        {
            return y;
        }

        public float bottom()
        {
            return y + hght;
        }

        public float width()
        {
            return wdth;
        }

        public float height()
        {
            return hght;
        }

        public bool contains(Vector2 point)
        {
            return point.X >= x && point.X <= x + wdth &&
                point.Y >= y && point.Y <= y + hght;
        }

        public bool intersects(LineSegment line)
        {
            float leftY = line.yAt(x);
            float rightY = line.yAt(x + wdth);
            
            float topX= line.xAt(y);
            float botX = line.xAt(y + hght);
            
            float thisBotY = y + hght;
            float thisRightX = x + wdth;

            return (leftY >= y && leftY <= thisBotY) ||
                (rightY >= y && rightY <= thisBotY) ||
                (topX >= x && topX <= thisRightX) ||
                (botX >= x && botX <= thisRightX) ||
                contains(line.getA()) ||
                contains(line.getB());
        }

        public bool intersects(FVRectangle rect)
        {
            return contains(rect.topLeft()) ||
                contains(rect.topRight()) ||
                contains(rect.bottomLeft()) ||
                contains(rect.bottomRight()) ||
                rect.contains(topLeft()) ||
                rect.contains(topRight()) ||
                rect.contains(bottomLeft()) ||
                rect.contains(bottomRight());
        }

        public void move(Vector2 delta)
        {
            x += delta.X;
            y += delta.Y;
        }

        public void moveTo(Vector2 pos)
        {
            x = pos.X;
            y = pos.Y;
        }

        public static FVRectangle operator +(FVRectangle rect, Vector2 delta)
        {
            rect.move(delta);
            return rect;
        }

        public static FVRectangle operator -(FVRectangle rect, Vector2 delta)
        {
            rect.move(-delta);
            return rect;
        }
    }
}