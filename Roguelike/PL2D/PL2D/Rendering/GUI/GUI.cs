using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL2D.Rendering.GUI.GUI_Elements;

namespace PL2D.Rendering.GUI
{
    class GUI
    {

    }

    interface IGUIElement
    {
        void Render(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch);
    }
}
