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
            if (_cells != null && Statics.GameState == GameStates.Debugging)
            {
                for (int i = 0; i < _cells.Length; i++)
                {
                    if (_cells.CurrentStep != _cells.End)
                        break;
                    const float scale = .25f;
                    var multiplier = scale * _sprite.Width;
                    spriteBatch.Draw(_sprite, new Vector2(_cells.CurrentStep.X * multiplier, _cells.CurrentStep.Y * multiplier),
                      null, null, null, 0.0f, new Vector2(scale, scale), Color.Blue * .2f,
                      SpriteEffects.None, 0.6f);
                    _cells.StepForward();
                }
            }
        }
    }
}
