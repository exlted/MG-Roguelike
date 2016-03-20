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

    class Player : Entity
    {


        public Player(float scale, Texture2D sprite, IMap Map) : base(scale, sprite, Map)
        {
            Statics.Camera.CenterOn(map.GetCell(X, Y));
        }

        public override bool Update(InputState inputState)
        {
            var moved = false;
            map.ComputeFov(X, Y, 30, true);
            foreach (Cell cell in map.GetAllCells())
            {
                if (map.IsInFov(cell.X, cell.Y))
                {
                    map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
            if (inputState.IsUp(PlayerIndex.One) && map.IsWalkable(X, Y-1))
            {
                Y -= 1;
                moved = true;
            }
            else if (inputState.IsDown(PlayerIndex.One) && map.IsWalkable(X, Y+1))
            {
                Y += 1;
                moved = true;
            }
            if (inputState.IsLeft(PlayerIndex.One) && map.IsWalkable(X-1, Y))
            {
                X -= 1;
                moved = true;
            }
            else if (inputState.IsRight(PlayerIndex.One) && map.IsWalkable(X+1, Y))
            {
                X += 1;
                moved = true;
            }
            if (moved) Statics.Camera.CenterOn(map.GetCell(X, Y));
            return moved;
        }

    }
}
