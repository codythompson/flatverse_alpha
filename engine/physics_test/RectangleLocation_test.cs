using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using flatverse.physics;
using Microsoft.Xna.Framework;

namespace flatverse.physics.test
{
    [TestClass]
    public class RectangleLocation_Test
    {
        [TestMethod]
        public void getPath_test()
        {
            Vector2 pos = new Vector2(224.5f, 442.25f);
            Vector2 delt = new Vector2(300, 600);
            Vector2 dims = new Vector2(200, 155);
            Dictionary<string, object>[] testRects = starTestRects(pos, delt, dims.X, dims.Y);

            // moved down
            FVRectangle orig = testRects[0]["rect"] as FVRectangle;
            FVRectangle moved = testRects[0]["movedRect"] as FVRectangle;
            Polygon expectedPath = new Polygon(new Vector2[] {
                orig.topLeft(),
                orig.topRight(),
                moved.bottomRight(),
                moved.bottomLeft()
            });
            Polygon actualPath = (testRects[0]["loc"] as RectangleLocation).getPath(1);
            Utils.AssertPolygonsAreEqual(expectedPath, actualPath);

            // moved down and right
            orig = testRects[1]["rect"] as FVRectangle;
            moved = testRects[1]["movedRect"] as FVRectangle;
            expectedPath = new Polygon(new Vector2[] {
                orig.topLeft(),
                orig.topRight(),
                moved.topRight(),
                moved.bottomRight(),
                moved.bottomLeft(),
                orig.bottomLeft()
            });
            actualPath = (testRects[1]["loc"] as RectangleLocation).getPath(1);
            Utils.AssertPolygonsAreEqual(expectedPath, actualPath);

            // moved right
            orig = testRects[2]["rect"] as FVRectangle;
            moved = testRects[2]["movedRect"] as FVRectangle;
            expectedPath = new Polygon(new Vector2[] {
                orig.topLeft(),
                moved.topRight(),
                moved.bottomRight(),
                orig.bottomLeft()
            });
            actualPath = (testRects[2]["loc"] as RectangleLocation).getPath(1);
            Utils.AssertPolygonsAreEqual(expectedPath, actualPath);

            // moved up and right
            orig = testRects[3]["rect"] as FVRectangle;
            moved = testRects[3]["movedRect"] as FVRectangle;
            expectedPath = new Polygon(new Vector2[] {
                orig.topLeft(),
                moved.topLeft(),
                moved.topRight(),
                moved.bottomRight(),
                orig.bottomRight(),
                orig.bottomLeft()
            });
            actualPath = (testRects[3]["loc"] as RectangleLocation).getPath(1);
            Utils.AssertPolygonsAreEqual(expectedPath, actualPath);

            // moved up
            orig = testRects[4]["rect"] as FVRectangle;
            moved = testRects[4]["movedRect"] as FVRectangle;
            expectedPath = new Polygon(new Vector2[] {
                moved.topLeft(),
                moved.topRight(),
                orig.bottomRight(),
                orig.bottomLeft()
            });
            actualPath = (testRects[4]["loc"] as RectangleLocation).getPath(1);
            Utils.AssertPolygonsAreEqual(expectedPath, actualPath);

            // moved up and left
            orig = testRects[5]["rect"] as FVRectangle;
            moved = testRects[5]["movedRect"] as FVRectangle;
            expectedPath = new Polygon(new Vector2[] {
                moved.topLeft(),
                moved.topRight(),
                orig.topRight(),
                orig.bottomRight(),
                orig.bottomLeft(),
                moved.bottomLeft()
            });
            actualPath = (testRects[5]["loc"] as RectangleLocation).getPath(1);
            Utils.AssertPolygonsAreEqual(expectedPath, actualPath);
            
            // moved left
            orig = testRects[6]["rect"] as FVRectangle;
            moved = testRects[6]["movedRect"] as FVRectangle;
            expectedPath = new Polygon(new Vector2[] {
                moved.topLeft(),
                orig.topRight(),
                orig.bottomRight(),
                moved.bottomLeft()
            });
            actualPath = (testRects[6]["loc"] as RectangleLocation).getPath(1);
            Utils.AssertPolygonsAreEqual(expectedPath, actualPath);

            // moved down and left
            orig = testRects[7]["rect"] as FVRectangle;
            moved = testRects[7]["movedRect"] as FVRectangle;
            expectedPath = new Polygon(new Vector2[] {
                orig.topLeft(),
                orig.topRight(),
                orig.bottomRight(),
                moved.bottomRight(),
                moved.bottomLeft(),
                moved.topLeft()
            });
            actualPath = (testRects[7]["loc"] as RectangleLocation).getPath(1);
            Utils.AssertPolygonsAreEqual(expectedPath, actualPath);
        }

        /*
         * Helpers
         */
        public Dictionary<string, object> makeTestRect(Vector2 pos, Vector2 delt, float w, float h)
        {
            Dictionary<string, object> rectStuff = new Dictionary<string, object>();

            FVRectangle rect = new FVRectangle(pos, new Vector2(w, h));
            FVRectangle movedRect = rect + delt;

            RectangleLocation loc = new RectangleLocation(pos, w, h);
            loc.moveTo(pos + delt);

            rectStuff.Add("loc", loc);
            rectStuff.Add("rect", rect);
            rectStuff.Add("movedRect", movedRect);
            rectStuff.Add("path", null);

            return rectStuff;
        }

        public Dictionary<string, object>[] starTestRects(Vector2 center, Vector2 delt, float w, float h)
        {
            Dictionary<string, object>[] star = new Dictionary<string, object>[8];
            star[0] = makeTestRect(center, new Vector2(0, delt.Y), w, h);
            star[1] = makeTestRect(center, new Vector2(delt.X, delt.Y), w, h);

            star[2] = makeTestRect(center, new Vector2(delt.X, 0), w, h);
            star[3] = makeTestRect(center, new Vector2(delt.X, -delt.Y), w, h);

            star[4] = makeTestRect(center, new Vector2(0, -delt.Y), w, h);
            star[5] = makeTestRect(center, new Vector2(-delt.X, -delt.Y), w, h);

            star[6] = makeTestRect(center, new Vector2(-delt.X, 0), w, h);
            star[7] = makeTestRect(center, new Vector2(-delt.X, delt.Y), w, h);

            return star;
        }
    }
}