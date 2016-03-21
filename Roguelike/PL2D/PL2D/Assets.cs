using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL2D
{
    static class Assets
    {
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();
        public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public static void importTextures(Game game)
        {
            string[] import;
            import = Directory.GetFiles(Path.GetFullPath(@"GameContent/Textures/"));
            for (int i = 0; i < import.Length; i++)
            {//Allows for importing ALL of the different types of texture files that Monogame can import by default
                if (import[i].Contains(".png") || import[i].Contains(".jpg") || import[i].Contains(".bmp") || import[i].Contains(".dds")
                    || import[i].Contains(".dib") || import[i].Contains(".hdr") || import[i].Contains(".pfm") || import[i].Contains("ppm")
                    || import[i].Contains(".tga"))
                    textures.Add(Path.GetFileNameWithoutExtension(import[i]), game.Content.Load<Texture2D>("Textures/" + Path.GetFileNameWithoutExtension(import[i])));
            }
        }

        public static void importSounds(Game game)
        {
            string[] import;
            import = Directory.GetFiles(Path.GetFullPath(@"GameContent/Sounds/"));
            for (int i = 0; i < import.Length; i++)
            {//Allows for importing ALL of the different types of the audio files that Monogame can import by default
                if (import[i].Contains(".xap") || import[i].Contains(".wma") || import[i].Contains(".mp3") || import[i].Contains(".wav"))
                    sounds.Add(Path.GetFileNameWithoutExtension(import[i]), game.Content.Load<SoundEffect>("Sounds/" + Path.GetFileNameWithoutExtension(import[i])));
            }
        }

        public static void importFonts(Game game)
        {
            string[] import;
            import = Directory.GetFiles(Path.GetFullPath(@"GameContent/Fonts/"));
            for (int i = 0; i < import.Length; i++)
            {
                if (import[i].Contains(".spritefont"))
                    fonts.Add(Path.GetFileNameWithoutExtension(import[i]), game.Content.Load<SpriteFont>("Fonts/" + Path.GetFileNameWithoutExtension(import[i])));
            }
        }

        public static void importContent(Game game)
        {
            importFonts(game);
            importSounds(game);
            importTextures(game);
        }
    }
}
