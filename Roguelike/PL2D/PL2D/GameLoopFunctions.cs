using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PL2D
{
    internal delegate void addUpdate();

    internal delegate void addRender(SpriteBatch spriteBatch);

    internal static class GameLoopFunctions
    {
        /// <summary>
        /// The list that holds all Entities (and children of Entities) automatically
        /// </summary>
        public static List<Entity> Updatable = new List<Entity>();

        /// <summary>
        /// The list that holds all Cells (and children of Cells) automatically
        /// </summary>
        public static List<Cell> Renderable = new List<Cell>();

        /// <summary>
        /// The input state for any input to be taken
        /// </summary>
        public static InputState inputState = new InputState();

        /// <summary>
        /// Adds a function to the update chain BEFORE the Entity-based update happens
        /// </summary>
        public static addUpdate earlyUpdate;

        /// <summary>
        /// Adds a function to the update chain AFTER the Entity-based update happens
        /// </summary>
        public static addUpdate lateUpdate;

        /// <summary>
        /// Adds a function to the render chain BEFORE the Cell-based render happens
        /// </summary>
        public static addRender earlyRender;

        /// <summary>
        /// Adds a function to the render chain AFTER the Cell-based render happens
        /// </summary>
        public static addRender lateRender;

        public static void Update()
        {
            if (earlyUpdate != null)
                earlyUpdate();
            inputState.Update();
            foreach (Entity E in Updatable)
                E.Update();
            if (lateUpdate != null)
                lateUpdate();
        }

        public static void Render(SpriteBatch spriteBatch)
        {
            if (earlyRender != null)
                earlyRender(spriteBatch);
            foreach (Cell C in Renderable)
                C.Draw(spriteBatch);
            if (lateRender != null)
                lateRender(spriteBatch);
        }
    }
}