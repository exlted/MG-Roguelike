using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PL2D
{
    public enum RenderLayers
    {
        FarBackgroundLayer = 0,
        SpriteBackgroundLayer = 1,
        SpriteClippingLayer = 2,
        BackSpriteLayer = 3,
        FrontSpriteLayer = 4,
        SpriteForegroundLayer = 5
    }

    internal static class Render
    {
        public static readonly float[] Layers = { 0.9f, 0.8f, 0.7f, 0.6f, 0.5f, 0.4f };

#pragma warning disable CC0074 // Make field readonly
        /// <summary>
        /// The main gameplay Camera - INITIALIZE TO OVERIDEN CAMERA TYPE BEFORE USING
        /// </summary>
        public static Camera Camera;
#pragma warning restore CC0074 // Make field readonly

        public static void DrawTexture(SpriteBatch spriteBatch, Texture2D texture, Vector2 cell, RenderLayers layer, Color tint)
        {
            spriteBatch.Draw(texture, cell, null, null, null, 0.0f, Vector2.One, tint, SpriteEffects.None, Layers[(int)layer]);
        }

        public static void DrawTexture(SpriteBatch spriteBatch, Texture2D texture, Rectangle cell, RenderLayers layer, Color tint)
        {
            spriteBatch.Draw(texture, destinationRectangle: cell, layerDepth: Layers[(int)layer], color: tint);
        }
    }
}