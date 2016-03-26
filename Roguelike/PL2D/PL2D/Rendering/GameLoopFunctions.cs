using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using PL2D.Rendering.Textured_Objects;

namespace PL2D
{
    internal delegate void AddUpdate();

    internal delegate void AddRender(SpriteBatch spriteBatch);

    internal static class GameLoopFunctions
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
        /// The input State for any input to be taken
        /// </summary>
        public static readonly InputState InputState = new InputState();

        public static MouseState MouseState;

#pragma warning disable CC0074
        /// <summary>
        /// Adds a function to the update chain BEFORE the Entity-based update happens
        /// </summary>
        public static AddUpdate EarlyUpdate;

        /// <summary>
        /// Adds a function to the update chain AFTER the Entity-based update happens
        /// </summary>
        public static AddUpdate LateUpdate;

        /// <summary>
        /// Adds a function to the render chain BEFORE the Cell-based render happens
        /// </summary>
        public static AddRender EarlyRender;

        /// <summary>
        /// Adds a function to the render chain AFTER the Cell-based render happens
        /// </summary>
        public static AddRender LateRender;
#pragma warning restore CC0074
        /// <summary>
        /// Add to the Initialize() portion of Monogame's default class.
        /// Automatically deals with all generic PL2D initializations.
        /// Due to how varied initialization code is there are no delegates to add initializations to Init.
        /// </summary>
        /// <exception cref="Exceptions.CameraNotImplementedException">Please initialize the Camera before starting the game</exception>
        public static void Init()
        {
            if (PL2D.Render.Camera == null)
                throw new Exceptions.CameraNotImplementedException("Please initialize the Camera before starting the game");
        }
        /// <summary>
        /// Add to the Update(GameTime) portion of Monogame's default class.
        /// Automatically updates all classes inheriting from Entity.
        /// Add a void(void) to either EarlyUpdate or LateUpdate to add other objects to be updated.
        /// </summary>
        public static void Update()
        {
            InputState.Update();
            EarlyUpdate?.Invoke();
            foreach (var _entity in Updatable)
                _entity.Update();
            LateUpdate?.Invoke();
        }

        /// <summary>
        /// Add to the Draw(GameTime) portion of Monogame's default class.
        /// Automatically renders all classes inheriting from Cell.
        /// Add a void(SpriteBatch) to either EarlyRender or LateRender to add other objects to be rendered.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public static void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, PL2D.Render.Camera.TranslationMatrix);
            EarlyRender?.Invoke(spriteBatch);
            foreach (var _cell in Renderable)
                _cell.Draw(spriteBatch);
            LateRender?.Invoke(spriteBatch);
            spriteBatch.End();
        }
    }
}