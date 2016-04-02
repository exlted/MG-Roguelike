using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PL2D.Rendering.GUI.GUI_Elements
{
    class RadioButtonGruop : IGuiElement
    {
        public List<RadioButton> Buttons { get; set; }


        public void Render(SpriteBatch spriteBatch)
        {
            foreach(var _radioButton in Buttons)
                _radioButton.Render(spriteBatch);
        }

        public void Update()
        {
            var _temp = false;
            EventHandler _tempHandler = SelectionChanged;
            foreach (var _radioButton in Buttons)
            {
                _radioButton.Update();
                foreach (var _r in _radioButton.SelectionChanged.GetInvocationList().Where(r => r.Method == _tempHandler.Method))
                {
                    _temp = true;
                }
                if (!_temp)
                    _radioButton.SelectionChanged += SelectionChanged;
            }
        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            foreach (var _radioButton in Buttons.Where(radioButton => radioButton != sender))
            {
                _radioButton.State = RadioButton.RadioButtonState.Deselected;
            }
        }
    }
    class RadioButton : IGuiElement
    {
        public enum RadioButtonState { Disabled = 0, Selected = 1, Deselected = 2 };

        public EventHandler SelectionChanged;

        public RadioButtonState State
        {
            get { return state; }
            set
            {
                state = value;
                if (value == RadioButtonState.Selected)
                {
                    SelectionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private readonly Texture2D[] textures = new Texture2D[3];
        private RadioButtonState state;

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
            switch (State)
            {
                case RadioButtonState.Disabled:
                    break;
                case RadioButtonState.Selected:
                    break;
                case RadioButtonState.Deselected:
                    State = RadioButtonState.Selected;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
