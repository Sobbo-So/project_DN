using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// for Game
public partial class Scene_Game : Scene_Base {
    public static class State {
        public const int NONE_START = 0;
        public const int WAIT_START = 1;
        public const int DOING = 2;
        public const int PAUSE = 3;

        public const int WAIT_ENDED = 100;
        public const int ENDED = 101;
    }

    public static class World {
        public const int DAY = 0;
        public const int NIGHT = 1;
    }

    [NonSerialized] public static Scene_Game instance = null;

    // Level
    [NonSerialized] public static float doingTime = 0f;
    [NonSerialized] public static int showCupCount = 2;
    [NonSerialized] public static int showColorCount = 2;
    [NonSerialized] public static int showDecoCount = 2;
    [NonSerialized] public static int enemyAddHP = 0;
    
    public static int currentState = -1;
    public static void ChangeState(int value) {
        currentState = value;
        if (instance != null)
            instance.ChangeStateDirect( currentState);
    }

    public static int currentWorld;

    public static int score = 0;

    public static WeaponCell selectWeapon = null;
}
