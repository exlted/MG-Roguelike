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
        /// The main gameplay camera - INITIALIZE TO OVERIDEN CAMERA TYPE BEFORE USING
        /// </summary>
        public static Camera camera;
#pragma warning restore CC0074 // Make field readonly

        private static readonly Color White = Color.White;

        public static void drawTexture(SpriteBatch spriteBatch, Texture2D texture, Vector2 cell, RenderLayers Layer, Color tint)
        {
            var position = new Vector2(cell.X, cell.Y);
            spriteBatch.Draw(texture, position, null, null, null, 0.0f, Vector2.One, tint, SpriteEffects.None, Layers[(int)Layer]);
        }
    }
}