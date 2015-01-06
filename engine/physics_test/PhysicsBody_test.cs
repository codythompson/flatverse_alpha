using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using flatverse.physics;
using Microsoft.Xna.Framework;

namespace physics_test
{
    [TestClass]
    public class PhysicsBody_test
    {
        [TestMethod]
        public void constructor_test()
        {
            int bodyCount = 10;
            PhysicsBody[] bodies = new PhysicsBody[bodyCount];
            for (int i = 0; i < bodyCount; i++)
            {
                bodies[i] = new PhysicsBody(null, null);
            }

            for (int i = 0; i < bodies.Length; i++)
            {
                int curId = bodies[i].ID;
                for (int j = i + 1; j < bodies.Length; j++)
                {
                    Assert.AreNotEqual(curId, bodies[j].ID);
                }
            }
        }
    }
}