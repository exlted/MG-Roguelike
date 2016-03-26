using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PL2D.Rendering.GUI.GUI_Elements
{
    internal class Button : IGuiElement
    {
        public enum ButtonState { Disabled = 0, Shown = 1, Clicked = 2 };
        public ButtonState State { get; set; }
        private readonly Texture2D[] textures = new Texture2D[3];
        public Texture2D this[int index]
        {
            set
            {
                textures[index] = value;
            }
        }

        public Rectangle Position { get; set; }
        public RenderLayers Layer { get; set; }
        public Color Tint { get; set; }
#pragma warning disable CC0074 // Make field readonly
        public EventHandler Click;
#pragma warning restore CC0074 // Make field readonly
        public Button()
        {
            textures[0] = textures[1] = textures[2] = null;
            Position = new Rectangle(0, 0, 0, 0);
            Layer = RenderLayers.FarBackgroundLayer;
            Tint = Color.Black;
        }

        public Button(Texture2D disabled, Texture2D shown, Texture2D clicked, Rectangle position, Color? tint, RenderLayers layer = RenderLayers.FrontSpriteLayer)
        {
            textures[0] = disabled;
            textures[1] = shown;
            textures[2] = clicked;
            Position = position;
            Tint = tint.GetValueOrDefault();
            Layer = layer;
        }

        public void Render(SpriteBatch spriteBatch)
        {
            if(textures[(int)State] != null)
            {
                PL2D.Render.DrawTexture(spriteBatch, textures[(int)State], Position, Layer, Tint);
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            if (GameLoopFunctions.InputState.IsNewLeftMouseClick(out GameLoopFunctions.MouseState) && Position.Contains(GameLoopFunctions.MouseState.Position))
            {
                Click?.Invoke(this, new EventArgs());
            }
        }
    }
}
