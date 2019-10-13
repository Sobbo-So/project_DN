using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameData : MonoBehaviour {
    public static GameData instance = null;

    [Serializable]
    public struct SpriteData {
        public Sprite sprite;
        public Animator animator;
    }

    [Serializable]
    public struct ColorCupData {
        public int cupIndex;
        public int colorID;
        public Sprite sprite;
    }


    [Header("Popup")]
    public Popup_Pause pref_popup_pause;

    [Header("Recipe - CUP")]
    public Sprite[] lstCupMainSprites;
    public ColorCupData[] lstCupLayerSprites;

    [Header("Recipe - DECO")]
    public Sprite[] lstDecoSprites;

    [Header("Recipe - COLOR")]
    public Sprite[] lstColorSprites;

    [Header("Units")]
    public SpriteData[] lstCustomerSpriteData;
    public SpriteData[] lstNormalEnemySpriteData;
    public SpriteData[] lstPenaltyEnemySpriteData;

    public GameObject prefab_UnitCustomer;
    public GameObject prefab_UnitEnemy;
    public GameObject prefab_UnitPenaltyEnemy;

    public Vector2[] lstCustomerPosition;
    public int[] lstEnemyPositionX;
    public int[] lstEnemyPositionY = new int[2] { 1100, 1220 };

    public Sprite[] lstWeaponSprites;

    [Header("MainUI")]
    public Sprite[] sprite_Curtain;
    public Sprite[] sprite_Button1;
    public Sprite[] sprite_Button2;
    public Sprite[] sprite_Button3;
    public Sprite[] sprite_Button4;


    public void Awake() {
        if (instance == null)
            instance = this;
    }
}
