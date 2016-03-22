using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL2D
{
    /// <summary>
    /// The base part of any rendered item
    /// </summary>
    class Cell
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position of the Cell.
        /// </value>
        protected Vector2 position { get; set; }
        /// <summary>
        /// </summary>
        /// <value>
        /// The x position of the Cell.
        /// </value>
        public float X { get; protected set; }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y position of the Cell.
        /// </value>
        public float Y { get; protected set; }
        /// <summary>
        /// Gets the texture.
        /// </summary>
        /// <value>
        /// The texture of the Cell.
        /// </value>
        public Texture2D texture { get; private set; }
        /// <summary>
        /// Gets or sets the layer.
        /// </summary>
        /// <value>
        /// The layer the Cell will be rendered on.
        /// </value>
        public RenderLayers layer { get; protected set; }
        public Color tint { get; protected set; }

        public Cell(float x, float y, Texture2D Texture, RenderLayers Layer, Color? Tint)
        {
            position = new Vector2(x, y);
            texture = Texture;
            layer = Layer;
            if (Tint.HasValue)
                tint = Tint.GetValueOrDefault();
            else tint = Color.White;
            GameLoopFunctions.Renderable.Add(this);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Render.drawTexture(spriteBatch, texture, position, layer, tint);
        }
    }
}
