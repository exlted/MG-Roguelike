using RogueSharp.Random;

namespace Roguelike
{
    public enum GameStates
    {
        None = 0,
        PlayerTurn = 1,
        EnemyTurn = 2,
        Debugging = 3
    }

    public enum RenderLayer
    {
        BackGroundLayer = 0,
        PathLayer = 1,
        SpriteLayer = 2,
        LengthOfArray
    }

    internal static class Statics
    {
        public static readonly IRandom Random = new DotNetRandom();
        public static GameStates GameState { get; set; }
        public static readonly float[] Layers = { 0.8f, 0.6f, 0.5f };
        public const int MapWidth = 50;
        public const int MapHeight = 30;
        public const int SpriteWidth = 64;
        public const int SpriteHeight = 64;
        public static readonly Camera Camera = new Camera();
    }
}