using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueSharp;

namespace Roguelike
{
    public struct Point { public int X; public int Y; }

    internal class Player : Entity
    {
        public Player(float scale, Texture2D sprite, IMap map) : base(scale, sprite, map)
        {
            Statics.Camera.CenterOn(Map.GetCell(X, Y));
        }

        public override bool Update(InputState inputState)
        {
            var _moved = false;
            Map.ComputeFov(X, Y, 30, true);
            foreach (var _cell in Map.GetAllCells().Where(cell => Map.IsInFov(cell.X, cell.Y)))
            {
                Map.SetCellProperties(_cell.X, _cell.Y, _cell.IsTransparent, _cell.IsWalkable, true);
            }
            if (inputState.IsUp(PlayerIndex.One) && Map.IsWalkable(X, Y - 1))
            {
                Y -= 1;
                _moved = true;
            }
            else if (inputState.IsDown(PlayerIndex.One) && Map.IsWalkable(X, Y + 1))
            {
                Y += 1;
                _moved = true;
            }
            if (inputState.IsLeft(PlayerIndex.One) && Map.IsWalkable(X - 1, Y))
            {
                X -= 1;
                _moved = true;
            }
            else if (inputState.IsRight(PlayerIndex.One) && Map.IsWalkable(X + 1, Y))
            {
                X += 1;
                _moved = true;
            }
            if (_moved) Statics.Camera.CenterOn(Map.GetCell(X, Y));
            return _moved;
        }
    }
}