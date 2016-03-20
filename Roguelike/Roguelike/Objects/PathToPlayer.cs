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
            _cells = _pathFinder.ShortestPath(_map.GetCell(x, y), _map.GetCell(_player.X, _player.Y));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            /*
            if (_cells != null && Statics.GameState == GameStates.Debugging)
            {
                for (int i = 0; i < _cells.Length; i++)
                {
                    const float scale = .25f;
                    var multiplier = scale * _sprite.Width;
                    Game1.drawTexture(spriteBatch, _sprite, _cells.CurrentStep);
                    if (_cells.CurrentStep != _cells.End)
                        break;
                    _cells.StepForward();
                }
            }*/
        }
    }
}
