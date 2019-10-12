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

    [Header("Popup")]
    public Popup_Pause pref_popup_pause;

    [Header("Units")]
    public SpriteData[] lstCustomerSpriteData;
    public SpriteData[] lstNormalEnemySpriteData;
    public SpriteData[] lstPenaltyEnemySpriteData;

    public GameObject prefab_UnitCustomer;
    public GameObject prefab_UnitEnemy;

    public Vector2[] lstCustomerPosition;
    public int[] lstEnemyPositionX;
    public int[] lstEnemyPositionY = new int[2] { 1100, 1220 };

    public Sprite[] lstWeaponSprites;

    public void Awake() {
        if (instance == null)
            instance = this;
    }
}
