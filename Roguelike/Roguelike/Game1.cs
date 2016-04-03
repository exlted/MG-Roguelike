using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueSharp;
using RogueSharp.MapCreation;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Roguelike
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// <seealso cref="Microsoft.Xna.Framework.Game" />
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private IMap map;
        private readonly InputState inputState = new InputState();
        private readonly Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private readonly Entity[] entities = new Entity[2];

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IMapCreationStrategy<Map> _mapCreationStrategy = new RandomRoomsMapCreationStrategy<Map>(Statics.MapWidth, Statics.MapHeight, 100, 7, 3);
            map = Map.Create(_mapCreationStrategy);
            Statics.GameState = GameStates.PlayerTurn;
            Statics.Camera.ViewportHeight = graphics.GraphicsDevice.Viewport.Height;
            Statics.Camera.ViewportWidth = graphics.GraphicsDevice.Viewport.Width;
            Window.AllowUserResizing = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            var _import  = Directory.GetFiles(System.IO.Path.GetFullPath(@"Content/Textures/"));
            foreach (var _i in _import.Where(s => s.Contains(".png")))
            {
                textures.Add(System.IO.Path.GetFileNameWithoutExtension(_i), Content.Load<Texture2D>("Textures/" + System.IO.Path.GetFileNameWithoutExtension(_i)));
            }
            entities[0] = new Player(0.25f, textures["player"], map);
            textures.Remove("player");
            entities[1] = new AggressiveEnemy(0.25f, textures["hound"], map, new PathToPlayer((Player)entities[0], map, textures["white"]));
            textures.Remove("hound");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            inputState.Update();
            if (inputState.IsExitGame(PlayerIndex.One))
                Exit();
            else if (inputState.IsSpace(PlayerIndex.One))
            {
                switch (Statics.GameState)
                {
                    case GameStates.PlayerTurn:
                        Statics.GameState = GameStates.Debugging;
                        break;
                    case GameStates.Debugging:
                        Statics.GameState = GameStates.PlayerTurn;
                        break;
                }
            }
            else if (entities[0].Update(inputState))
                for (var _i = 1; _i < entities.Length; _i++)
                {
                    entities[_i].Update(inputState);
                }
            Statics.Camera.Update(inputState, PlayerIndex.One);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Statics.Camera.TranslationMatrix);

            foreach (var _cell in map.GetAllCells().Where(cell => cell.IsExplored || Statics.GameState == GameStates.Debugging))
            {
                DrawTexture(spriteBatch, _cell.IsWalkable? textures["floor"] : textures["wall"], _cell, RenderLayer.BackGroundLayer);
            }
            foreach (var _i in entities)
            {
                _i.Draw(spriteBatch);
            }
            spriteBatch.End();

            Statics.Camera.ViewportHeight = graphics.GraphicsDevice.Viewport.Height;
            Statics.Camera.ViewportWidth = graphics.GraphicsDevice.Viewport.Width;

            base.Draw(gameTime);
        }

        public static void DrawTexture(SpriteBatch spriteBatch, Texture2D texture, Cell cell, RenderLayer layer)
        {
            var _white = Color.White;
            var _position = new Vector2(cell.X * Statics.SpriteWidth, cell.Y * Statics.SpriteHeight);
            if (!cell.IsInFov && Statics.GameState != GameStates.Debugging)
                _white = Color.Gray;
            spriteBatch.Draw(texture, _position, null, null, null, 0.0f, Vector2.One, _white, SpriteEffects.None, Statics.Layers[(int)layer]);
        }
    }
}