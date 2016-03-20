using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RogueSharp;
using RogueSharp.Random;

namespace Roguelike
{
    public struct Point { public int x; public int y; }

    class Player
    {
        Point pos;
        readonly IMap map;
        public int X { get { return pos.x; } private set { pos.x = value; } }
        public int Y { get { return pos.y; } private set { pos.y = value; } }
        public float Scale { get; private set; }
        public Texture2D Sprite { get; private set; }

        public Player(float scale, Texture2D sprite, IMap Map)
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
            IRandom random = new DotNetRandom();

            while (true)
            {
                var x = random.Next(49);
                var y = random.Next(29);
                if (map.IsWalkable(x, y))
                {
                    return map.GetCell(x, y);
                }
            }
        }

        public void Draw( SpriteBatch spriteBatch)
        {
            var multiplier = Scale * Sprite.Width;
            spriteBatch.Draw(Sprite, new Vector2(X * multiplier, Y * multiplier),
                null, null, null, 0.0f, new Vector2(Scale, Scale),
                Color.White, SpriteEffects.None, Statics.spriteLayer);
        }

        public void Update()
        {
            map.ComputeFov(X, Y, 30, true);
            if (Keyboard.GetState().IsKeyDown(Keys.W) && map.GetCell(X, Y - 1).IsWalkable)
                Y -= 1;
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && map.GetCell(X, Y + 1).IsWalkable)
                Y += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.A) && map.GetCell(X - 1, Y).IsWalkable)
                X -= 1;
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && map.GetCell(X + 1, Y).IsWalkable)
                X += 1;
        }

    }
}
