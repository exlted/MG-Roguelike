using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PL2D
{
    internal class Entity : Cell
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