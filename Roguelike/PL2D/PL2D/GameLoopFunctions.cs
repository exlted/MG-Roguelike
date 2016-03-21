using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL2D
{
    static class GameLoopFunctions
    {
        public static List<Entity> Updatable = new List<Entity>();
        public static List<Cell> Renderable = new List<Cell>();
        public static InputState inputState = new InputState();

        public static void Update()
        {
            inputState.Update();
            foreach (Entity E in Updatable)
                E.Update();
        }

        public static void Render(SpriteBatch spriteBatch)
        {
            foreach (Cell C in Renderable)
                C.Draw(spriteBatch);
        }
    }
}
