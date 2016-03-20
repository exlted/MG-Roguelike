using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using RogueSharp;

namespace Roguelike
{
    class AggressiveEnemy : Entity
    {
        public AggressiveEnemy(float scale, Texture2D sprite, IMap map) : base(scale, sprite, map)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(map.IsInFov(X, Y))
                base.Draw(spriteBatch);
        }

        public override bool Update(InputState inputState)
        {
            return base.Update(inputState);
        }
    }
}
