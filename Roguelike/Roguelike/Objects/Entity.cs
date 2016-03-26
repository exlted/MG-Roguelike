using Microsoft.Xna.Framework.Graphics;
using RogueSharp;

namespace Roguelike
{
    internal class Entity
    {
        private Point pos;
        protected readonly IMap Map;
        public int X { get { return pos.X; } protected set { pos.X = value; } }
        public int Y { get { return pos.Y; } protected set { pos.Y = value; } }
        public float Scale { get; private set; }
        public Texture2D Sprite { get; }

        public Entity(float scale, Texture2D sprite, IMap map)
        {
            Map = map;
            var _randomEmptyCell = GetRandomEmptyCell();
            X = _randomEmptyCell.X;
            Y = _randomEmptyCell.Y;
            Scale = scale;
            Sprite = sprite;
        }

        private Cell GetRandomEmptyCell()
        {
            while (true)
            {
                var _x = Statics.Random.Next(49);
                var _y = Statics.Random.Next(29);
                if (Map.IsWalkable(_x, _y))
                {
                    return Map.GetCell(_x, _y);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Game1.DrawTexture(spriteBatch, Sprite, Map.GetCell(X, Y), RenderLayer.SpriteLayer);
        }

#pragma warning disable

        public virtual bool Update(InputState inputState)
        {
            return false;
        }

#pragma warning restore
    }
}