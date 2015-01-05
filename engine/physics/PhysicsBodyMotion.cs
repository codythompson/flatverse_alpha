using System;
using Microsoft.Xna.Framework;

namespace flatverse.physics
{
    public interface PhysicsBodyMotion
    {
        Vector2 updatePos(PlatformInfo platformInfo, Vector2 newPos);
    }
}