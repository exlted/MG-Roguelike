using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace PL2D
{
    internal static class Assets
    {
        public static readonly Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public static readonly Dictionary<string, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();
        public static readonly Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();

        public static void ImportTextures(Game game)
        {
            var _import  = Directory.GetFiles(Path.GetFullPath(@"GameContent/Textures/"));
            foreach (var _i in _import)
            {//Allows for importing ALL of the different types of Texture files that Monogame can import by default
                if (_i.Contains(".png") || _i.Contains(".jpg") || _i.Contains(".bmp") || _i.Contains(".dds")
                    || _i.Contains(".dib") || _i.Contains(".hdr") || _i.Contains(".pfm") || _i.Contains("ppm")
                    || _i.Contains(".tga"))
                    Textures.Add(Path.GetFileNameWithoutExtension(_i), game.Content.Load<Texture2D>("Textures/" + Path.GetFileNameWithoutExtension(_i)));
            }
        }

        public static void ImportSounds(Game game)
        {
            var _import = Directory.GetFiles(Path.GetFullPath(@"GameContent/Sounds/"));
            foreach (var _i in _import)
            {//Allows for importing ALL of the different types of the audio files that Monogame can import by default
                if (_i.Contains(".xap") || _i.Contains(".wma") || _i.Contains(".mp3") || _i.Contains(".wav"))
                    Sounds.Add(Path.GetFileNameWithoutExtension(_i), game.Content.Load<SoundEffect>("Sounds/" + Path.GetFileNameWithoutExtension(_i)));
            }
        }

        public static void ImportFonts(Game game)
        {
            var _import = Directory.GetFiles(Path.GetFullPath(@"GameContent/Fonts/"));
            foreach (var _i in _import)
            {
                if (_i.Contains(".spritefont"))
                    Fonts.Add(Path.GetFileNameWithoutExtension(_i), game.Content.Load<SpriteFont>("Fonts/" + Path.GetFileNameWithoutExtension(_i)));
            }
        }

        public static void ImportContent(Game game)
        {
            ImportFonts(game);
            ImportSounds(game);
            ImportTextures(game);
        }
    }
}