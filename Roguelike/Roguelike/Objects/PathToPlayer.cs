using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueSharp;

namespace Roguelike
{
    internal class PathToPlayer
    {
        private readonly Player player;
        private readonly IMap map;
        private readonly Texture2D sprite;
        private readonly PathFinder pathFinder;
        private Path cells;

        public PathToPlayer(Player player, IMap map, Texture2D sprite)
        {
            this.player = player;
            this.map = map;
            this.sprite = sprite;
            pathFinder = new PathFinder(map);
        }

        public Cell FirstCell => cells.Start;

        public void CreateFrom(int x, int y)
        {
            if (x != player.X || y != player.Y)
                cells = pathFinder.ShortestPath(map.GetCell(x, y), map.GetCell(player.X, player.Y));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (cells != null && Statics.GameState == GameStates.Debugging)
            {
                for (var _cell = cells.Start; cells.CurrentStep != cells.End; _cell = cells.StepForward())
                {
                    var _position = new Vector2(_cell.X * Statics.SpriteWidth, _cell.Y * Statics.SpriteHeight);
                    spriteBatch.Draw(sprite, _position, null, null, null, 0.0f, Vector2.One, Color.White, SpriteEffects.None, Statics.Layers[(int)RenderLayer.PathLayer]);
                }
            }
        }
    }
}