using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RogueSharp;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RogueSharp.Random;

namespace Roguelike
{
    class Entity
    {
        Point pos;
        protected readonly IMap map;
        public int X { get { return pos.x; } protected set { pos.x = value; } }
        public int Y { get { return pos.y; } protected set { pos.y = value; } }
        public float Scale { get; private set; }
        public Texture2D Sprite { get; private set; }

        public Entity(float scale, Texture2D sprite, IMap Map)
        {
            map = Map;
            var temp = GetRandomEmptyCell();
            X = temp.X;
            Y = temp.Y;
            Scale = scale;
            Sprite = sprite;
        }

        private Cell GetRandomEmptyCell()
        {

            while (true)
            {
                var x = Statics.random.Next(49);
                var y = Statics.random.Next(29);
                if (map.IsWalkable(x, y))
                {
                    return map.GetCell(x, y);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Game1.drawTexture(spriteBatch, Sprite, map.GetCell(X, Y), renderLayer.spriteLayer);
        }

#pragma warning disable
        public virtual bool Update(InputState inputState)
        {
            return false;
        }
#pragma warning restore
    }
}
