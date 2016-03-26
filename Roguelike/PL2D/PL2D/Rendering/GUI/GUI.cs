using System.Collections.Generic;

namespace PL2D.Rendering.GUI
{
    internal class Gui
    {
        public List<IGuiElement> Elements { get; set; }

        public Gui()
        {
            Elements = new List<IGuiElement>();
        }

        public void Render(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            foreach (var _element in Elements)
                _element.Render(spriteBatch);
        }
    }

    interface IGuiElement
    {
        void Render(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch);
    }
}
