using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL2D.Rendering.GUI.GUI_Elements
{
    class Button : IGUIElement
    {
        public enum buttonState { disabled = 0, shown = 1, clicked = 2 };
        public buttonState state { get; set; }
        Texture2D[] textures = new Texture2D[3];
        public Texture2D this[int index]
        {
            set
            {
                textures[index] = value;
            }
        }

        public Rectangle position { get; set; }
        public RenderLayers layer { get; set; }
        public Color tint { get; set; }
        public EventHandler Click;
        public Button()
        {
            textures[0] = textures[1] = textures[2] = null;
            position = new Rectangle(0, 0, 0, 0);
            layer = RenderLayers.FarBackgroundLayer;
            tint = Color.Black;
        }

        public Button(Texture2D disabled, Texture2D shown, Texture2D clicked, Rectangle Position, Color? Tint, RenderLayers Layer = RenderLayers.FrontSpriteLayer)
        {
            textures[0] = disabled;
            textures[1] = shown;
            textures[2] = clicked;
            position = Position;
            tint = Tint.GetValueOrDefault();
            layer = Layer;
        }

        public void Render(SpriteBatch spriteBatch)
        {
            if(textures[(int)state] != null)
            {
                PL2D.Render.drawTexture(spriteBatch, textures[(int)state], position, layer, tint);
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            if (GameLoopFunctions.inputState.IsNewLeftMouseClick(out GameLoopFunctions.mouseState))
            {
                if(position.Contains(GameLoopFunctions.mouseState.Position))
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
