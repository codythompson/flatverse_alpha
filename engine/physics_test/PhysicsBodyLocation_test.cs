using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using flatverse.physics;
using Microsoft.Xna.Framework;

namespace physics_test
{
    [TestClass]
    public class PhysicsBodyLocation_test
    {
        [TestMethod]
        public void getPos_test()
        {
            Dictionary<string, Vector2> posVals = getPosValues(125, 200);
            Dictionary<string, PhysicsBodyLocation> bodyLocs = getBodyLocs(posVals);

            Utils.AssertVectorsAreEqual(posVals["negneg"], bodyLocs["negneg"].getPos());
            Utils.AssertVectorsAreEqual(posVals["negpos"], bodyLocs["negpos"].getPos());
            Utils.AssertVectorsAreEqual(posVals["posneg"], bodyLocs["posneg"].getPos());
            Utils.AssertVectorsAreEqual(posVals["pospos"], bodyLocs["pospos"].getPos());
        }

        [TestMethod]
        public void getPrevPos_test()
        {
            Dictionary<string, Vector2> posVals = getPosValues(125, 200);
            Dictionary<string, PhysicsBodyLocation> bodyLocs = getBodyLocs(posVals);

            Utils.AssertVectorsAreEqual(posVals["negneg"], bodyLocs["negneg"].getPrevPos());
            Utils.AssertVectorsAreEqual(posVals["negpos"], bodyLocs["negpos"].getPrevPos());
            Utils.AssertVectorsAreEqual(posVals["posneg"], bodyLocs["posneg"].getPrevPos());
            Utils.AssertVectorsAreEqual(posVals["pospos"], bodyLocs["pospos"].getPrevPos());
        }

        [TestMethod]
        public void moveTo_test()
        {
            Dictionary<string, Vector2> posVals = getPosValues(125, 200);
            Dictionary<string, PhysicsBodyLocation> bodyLocs = getBodyLocs(posVals);

            foreach (KeyValuePair<string, Vector2> posVal in posVals)
            {
                string key = posVal.Key;
                Vector2 pos = posVal.Value;
                PhysicsBodyLocation body = bodyLocs[key];

                float newX = pos.X + 1001.5f;
                float newY = pos.Y - 2002.25f;

                body.moveTo(new Vector2(newX, newY));

                Utils.AssertVectorsAreEqual(pos, body.getPrevPos());
                Utils.AssertVectorsAreEqual(new Vector2(newX, newY), body.getPos());
            }
        }

        [TestMethod]
        public void getIntermediate_test()
        {
            Dictionary<string, Vector2> posVals = getPosValues(125, 200);
            Dictionary<string, PhysicsBodyLocation> bodyLocs = getBodyLocs(posVals);

            foreach (KeyValuePair<string, Vector2> posVal in posVals)
            {
                string key = posVal.Key;
                Vector2 pos = posVal.Value;
                PhysicsBodyLocation body = bodyLocs[key];

                float newX = pos.X + 1001.5f;
                float newY = pos.Y - 2002.25f;

                body.moveTo(new Vector2(newX, newY));

                Vector2 intAt0 = getIntermediatePoint(body.getPos(), body.getPrevPos(), 0.0f);
                Utils.AssertVectorsAreEqual(intAt0, body.getIntermediate(0.0f));

                Vector2 intAt25 = getIntermediatePoint(body.getPos(), body.getPrevPos(), 0.25f);
                Utils.AssertVectorsAreEqual(intAt25, body.getIntermediate(0.25f));

                Vector2 intAt33 = getIntermediatePoint(body.getPos(), body.getPrevPos(), 0.33f);
                Utils.AssertVectorsAreEqual(intAt33, body.getIntermediate(0.33f));

                Vector2 intAt5 = getIntermediatePoint(body.getPos(), body.getPrevPos(), 0.5f);
                Utils.AssertVectorsAreEqual(intAt5, body.getIntermediate(0.5f));

                Vector2 intAt66 = getIntermediatePoint(body.getPos(), body.getPrevPos(), 0.66f);
                Utils.AssertVectorsAreEqual(intAt66, body.getIntermediate(0.66f));

                Vector2 intAt75 = getIntermediatePoint(body.getPos(), body.getPrevPos(), 0.75f);
                Utils.AssertVectorsAreEqual(intAt75, body.getIntermediate(0.75f));

                Vector2 intAt1 = getIntermediatePoint(body.getPos(), body.getPrevPos(), 1);
                Utils.AssertVectorsAreEqual(intAt1, body.getIntermediate(1));
            }
        }

        [TestMethod]
        public void getDistance_test()
        {
            // start 1122.2121, -5678.8989
            // end -998.7879, 7799.8989
            PhysicsBodyLocation bodyLoc = new PhysicsBodyLocation_stub(new Vector2(1122.2121f, -5678.8989f));
            bodyLoc.moveTo(new Vector2(-998.7879f, 7799.8989f));
            Assert.AreEqual(13644.65577188683935910095287193f, bodyLoc.getDistance(0, 1));
        }

        /*
         * helpers 
         */
        public Dictionary<string, Vector2> getPosValues(float x, float y)
        {
            Dictionary<string, Vector2> posVals = new Dictionary<string,Vector2>();

            posVals.Add("negneg", new Vector2(-x, -y));
            posVals.Add("negpos", new Vector2(-x, y));
            posVals.Add("posneg", new Vector2(x, -y));
            posVals.Add("pospos", new Vector2(x, y));

            return posVals;
        }

        public Dictionary<string, PhysicsBodyLocation> getBodyLocs(Dictionary<string, Vector2> posVals)
        {
            Dictionary<string, PhysicsBodyLocation> bodyLocs = new Dictionary<string, PhysicsBodyLocation>();
            
            foreach (KeyValuePair<string, Vector2> posVal in posVals)
            {
                bodyLocs.Add(posVal.Key, new PhysicsBodyLocation_stub(posVal.Value));
            }

            return bodyLocs;
        }

        public Vector2 getIntermediatePoint(Vector2 pos, Vector2 prevPos, float t)
        {
            float intX = pos.X + ((pos.X - prevPos.X) * t);
            float intY = pos.Y + ((pos.Y - prevPos.Y) * t);

            return new Vector2(intX, intY);
        }
    }
}
