using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL2D.Rendering.GUI.GUI_Elements;
using NLua;

namespace PL2D.Rendering.GUI
{
    class GUI
    {

    }

    class luaGUI : IDisposable
    {
        public Lua Lua { get; set; }

        public luaGUI()
        {
            Lua = new Lua();
        }

        public void Dispose()
        {
            Lua.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
