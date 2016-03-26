using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PL2D.Rendering.Textured_Objects
{
    /// <summary>
    /// The base part of any rendered item
    /// </summary>
    internal class Cell
    {
        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        /// <value>
        /// The Position of the Cell.
        /// </value>
        protected Vector2 Position { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The x Position of the Cell.
        /// </value>
        public float X { get; protected set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y Position of the Cell.
        /// </value>
        public float Y { get; protected set; }

        /// <summary>
        /// Gets the texture.
        /// </summary>
        /// <value>
        /// The texture of the Cell.
        /// </value>
        public Texture2D Texture { get; }

        /// <summary>
        /// Gets or sets the Layer.
        /// </summary>
        /// <value>
        /// The Layer the Cell will be rendered on.
        /// </value>
        public RenderLayers Layer { get; protected set; }

        public Color Tint { get; protected set; }

        public Cell(float x, float y, Texture2D texture, RenderLayers layer, Color? tint)
        {
            Position = new Vector2(x, y);
            Texture = texture;
            Layer = layer;
            Tint = tint.HasValue ? tint.GetValueOrDefault() : Color.White;
            GameLoopFunctions.Renderable.Add(this);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Render.DrawTexture(spriteBatch, Texture, Position, Layer, Tint);
        }
    }
}