using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PL2D.Rendering.Textured_Objects;

namespace PL2D
{
    internal class Entity : Cell
    {
        public Entity(float x, float y, Texture2D texture, RenderLayers layer, Color? tint) : base(x, y, texture, layer, tint)
        {
            GameLoopFunctions.Updatable.Add(this);
        }

        public virtual bool Update()
        {
            return false;
        }
    }
}