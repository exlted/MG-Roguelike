using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL2D
{
    class Entity : Cell
    {
        public Entity(float x, float y, Texture2D Texture, RenderLayers Layer, Color? tint) : base(x, y, Texture, Layer, tint)
        {
            GameLoopFunctions.Updatable.Add(this);
        }

        public virtual bool Update()
        {
            return false;
        }
    }
}
