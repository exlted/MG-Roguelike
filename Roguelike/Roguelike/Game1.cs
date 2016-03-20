using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using RogueSharp;
using RogueSharp.MapCreation;

namespace Roguelike
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// <seealso cref="Microsoft.Xna.Framework.Game" />
    public class Game1 : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private IMap map;
        private readonly InputState inputState = new InputState();
        readonly Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        readonly Entity[] entities = new Entity[2];

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
            IMapCreationStrategy<Map> mapCreationStrategy = new RandomRoomsMapCreationStrategy<Map>(50, 30, 100, 7, 3);
            map = Map.Create(mapCreationStrategy);
            Statics.GameState = GameStates.PlayerTurn;
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
            string[] import;
            import = Directory.GetFiles(System.IO.Path.GetFullPath(@"Content/Textures"));
            for (int i = 0; i < import.Length; i++)
            {
                textures.Add(System.IO.Path.GetFileNameWithoutExtension(import[i]), Content.Load<Texture2D>("Textures/" + System.IO.Path.GetFileNameWithoutExtension(import[i])));
            }
            entities[0] = new Player(0.25f, textures["player"], map);
            textures.Remove("player");
            entities[1] = new AggressiveEnemy(0.25f, textures["hound"], map, new PathToPlayer(entities[0] as Player, map, textures["white"]));
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
                if (Statics.GameState == GameStates.PlayerTurn)
                    Statics.GameState = GameStates.Debugging;
                else if (Statics.GameState == GameStates.Debugging)
                    Statics.GameState = GameStates.PlayerTurn;
            }
            else if (entities[0].Update(inputState))
                for (int i = 1; i < entities.Length; i++)
                {
                    entities[i].Update(inputState);
                }
            { }
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
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            foreach(Cell cell in map.GetAllCells())
            {
                if (!cell.IsExplored && Statics.GameState != GameStates.Debugging)
                    continue;

                if (cell.IsWalkable)
                    drawTexture(spriteBatch, textures["floor"], cell);
                else
                    drawTexture(spriteBatch, textures["wall"], cell);
            }
            for (int i = 0; i < entities.Length; i++)
            {
                entities[i].Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        static void drawTexture(SpriteBatch spriteBatch, Texture2D texture, Cell cell)
        {
            const int sizeOfSprites = 64;
            const float scale = .25f;
            var tint = Color.White;
            var position = new Vector2(cell.X * sizeOfSprites * scale, cell.Y * sizeOfSprites * scale);
            if (!cell.IsInFov && Statics.GameState != GameStates.Debugging)
                tint = Color.Gray;
            spriteBatch.Draw(texture, position, null, null, null, 0.0f, new Vector2(scale, scale), tint, SpriteEffects.None, Statics.backGroundLayer);
        }
    }
}
