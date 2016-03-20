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
    class Statics
    {
        public static readonly IRandom random = new DotNetRandom();
        public static GameStates GameState { get; set; }
        public const float backGroundLayer = 0.8f;
        public const float pathLayer = 0.6f;
        public const float spriteLayer = 0.5f;
        public const int mapWidth = 50;
        public const int mapHeight = 30;
        public const int spriteWidth = 64;
        public const int spriteHeight = 64;
    }
}
