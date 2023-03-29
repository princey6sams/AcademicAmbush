using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS
{
    public enum GameStatus
    {
        IN_PROGRESS, IS_PAUSED, GAME_OVER
    }

    public static class globalGameStatus
    {
        public static GameStatus Status { get; set; }
    }

}

