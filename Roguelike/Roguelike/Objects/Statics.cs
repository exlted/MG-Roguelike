using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    public enum renderLayer
    {
        backGroundLayer = 0,
        pathLayer = 1,
        spriteLayer = 2,
        LENGTH_OF_ARRAY
    }
    class Statics
    {
        public static readonly IRandom random = new DotNetRandom();
        public static GameStates GameState { get; set; }
        public static readonly float[] Layers = { 0.8f, 0.6f, 0.5f };
        public const int mapWidth = 50;
        public const int mapHeight = 30;
        public const int spriteWidth = 64;
        public const int spriteHeight = 64;
        public static readonly Camera Camera = new Camera();
    }
}
