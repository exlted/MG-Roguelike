using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PL2D.Rendering.GUI.GUI_Elements
{
    class ToggleBox : IGuiElement
    {
        public enum ToggleBoxState { Disabled = 0, On = 1,Off = 2}


        public EventHandler StateChanged;

        private ToggleBoxState state;
        public ToggleBoxState State
        {
            get { return state; }
            set
            {
                if (value != state)
                {
                    state = value;
                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

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


        public void Render(SpriteBatch spriteBatch)
        {
            if (textures[(int)State] != null)
            {
                PL2D.Render.DrawTexture(spriteBatch, textures[(int)State], Position, Layer, Tint);
            }
        }

        public void Update()
        {
            if (!GameLoopFunctions.InputState.IsNewLeftMouseClick(out GameLoopFunctions.MouseState) ||
                !Position.Contains(GameLoopFunctions.MouseState.Position)) return;
            switch(State)
            {
                case ToggleBoxState.Disabled:
                    break;
                case ToggleBoxState.On:
                    State = ToggleBoxState.Off;
                    break;
                case ToggleBoxState.Off:
                    State = ToggleBoxState.On;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
