using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PL2D
{
    internal delegate void addUpdate();

    internal delegate void addRender(SpriteBatch spriteBatch);

    static class GameLoopFunctions
    {
        /// <summary>
        /// The list that holds all Entities (and children of Entities) automatically
        /// </summary>
        public static readonly List<Entity> Updatable = new List<Entity>();

        /// <summary>
        /// The list that holds all Cells (and children of Cells) automatically
        /// </summary>
        public static readonly List<Cell> Renderable = new List<Cell>();

        /// <summary>
        /// The input state for any input to be taken
        /// </summary>
        public static readonly InputState inputState = new InputState();
        public static MouseState mouseState;

#pragma warning disable CC0074
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
#pragma warning restore CC0074
        public static void Init()
        {
            if (PL2D.Render.camera == null)
                throw new Exceptions.CameraNotImplementedException("Please initialize the camera before starting the game");
        }
        /// <summary>
        /// Add to the Update(GameTime) portion of Monogame's default class.
        /// Automatically updates all classes inheriting from Entity.
        /// Add a void(void) to either earlyUpdate or lateUpdate to add other objects to be updated.
        /// </summary>
        /// <exception cref="Exceptions.CameraNotImplementedException">Please initialize the camera before starting the game</exception>
        public static void Update()
        {
            inputState.Update();
            earlyUpdate?.Invoke();
            foreach (Entity E in Updatable)
                E.Update();
            lateUpdate?.Invoke();
        }

        /// <summary>
        /// Add to the Draw(GameTime) portion of Monogame's default class.
        /// Automatically renders all classes inheriting from Cell.
        /// Add a void(SpriteBatch) to either earlyRender or lateRender to add other objects to be rendered.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public static void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, PL2D.Render.camera.TranslationMatrix);
            earlyRender?.Invoke(spriteBatch);
            foreach (Cell C in Renderable)
                C.Draw(spriteBatch);
            lateRender?.Invoke(spriteBatch);
            spriteBatch.End();
        }
    }
}