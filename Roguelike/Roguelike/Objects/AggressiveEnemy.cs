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
        private readonly PathToPlayer path;
        public AggressiveEnemy(float scale, Texture2D sprite, IMap map, PathToPlayer Path) : base(scale, sprite, map)
        {
            path = Path;
            path.CreateFrom(X, Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(map.IsInFov(X, Y) || Statics.GameState == GameStates.Debugging)
                base.Draw(spriteBatch);
            path.Draw(spriteBatch);
        }

        public override bool Update(InputState inputState)
        {
            path.CreateFrom(X, Y);
            X = path.FirstCell.X;
            Y = path.FirstCell.Y;
            return base.Update(inputState);
        }
    }
}
