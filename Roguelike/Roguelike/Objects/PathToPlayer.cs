using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike
{
    class PathToPlayer
    {
        private readonly Player _player;
        private readonly IMap _map;
        private readonly Texture2D _sprite;
        private readonly PathFinder _pathFinder;
        private Path _cells;

        public PathToPlayer(Player player, IMap map, Texture2D sprite)
        {
            _player = player;
            _map = map;
            _sprite = sprite;
            _pathFinder = new PathFinder(map);
        }
        public Cell FirstCell
        {
            get
            {
                return _cells.Start;
            }
        }
        public void CreateFrom(int x, int y)
        {
            if (x != _player.X || y != _player.Y)
                _cells = _pathFinder.ShortestPath(_map.GetCell(x, y), _map.GetCell(_player.X, _player.Y));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_cells != null && Statics.GameState == GameStates.Debugging)
            {
                for (Cell cell = _cells.Start; _cells.CurrentStep != _cells.End; cell = _cells.StepForward())
                {
                    var position = new Vector2(cell.X * Statics.spriteWidth, cell.Y * Statics.spriteHeight);
                    spriteBatch.Draw(_sprite, position, null, null, null, 0.0f, Vector2.One, Color.White, SpriteEffects.None, Statics.Layers[(int)renderLayer.pathLayer]);
                }
            }
        }
    }
}
