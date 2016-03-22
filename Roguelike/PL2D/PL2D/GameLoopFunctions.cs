using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL2D
{
    delegate void addUpdate();
    delegate void addRender(SpriteBatch spriteBatch);

    static class GameLoopFunctions
    {
        public static List<Entity> Updatable = new List<Entity>();
        public static List<Cell> Renderable = new List<Cell>();
        public static InputState inputState = new InputState();
        public static addUpdate earlyUpdate;
        public static addUpdate lateUpdate;
        public static addRender earlyRender;
        public static addRender lateRender;


        public static void Update()
        {
            if(earlyUpdate !=  null)
                earlyUpdate();
            inputState.Update();
            foreach (Entity E in Updatable)
                E.Update();
            if (lateUpdate != null)
                lateUpdate();
        }

        public static void Render(SpriteBatch spriteBatch)
        {
            if(earlyRender != null)
                earlyRender(spriteBatch);
            foreach (Cell C in Renderable)
                C.Draw(spriteBatch);
            if (lateRender != null)
                lateRender(spriteBatch);
        }
    }
}
