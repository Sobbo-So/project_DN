using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

// for Direct
public partial class Scene_Game : Scene_Base {
    public class WeaponData {
        public int colorCode;

        public int level;
        public int damage;
        public int price;
        
        public WeaponData() {
            colorCode = 0;
            level = 1;
            damage = 0;
            price = 100;
        }
    }

    [Header("Doing Game - Night")]
    public Transform trans_Parent_NormalEnemy;
    public Transform trans_Parnet_PenaltyEnemy;
    public GameObject layout_Night;

    public GameObject panel_Penalty;

    private List<UnitEnemy> normalEnemies = new List<UnitEnemy>();
    private List<UnitEnemy> penaltyEnemies = new List<UnitEnemy>();

    private int _lastCreateEnemy = -1;
    private WeaponData _selectWeapon = null;

    public void SpawnEnemy() {

    }

    public void RemoveEnemy(UnitEnemy obj) {
        if (obj.type == UnitEnemy.EnemyType.NORMAL)
            normalEnemies.Remove(obj);
        if (obj.type == UnitEnemy.EnemyType.PENALTY)
            penaltyEnemies.Remove(obj);
    }

}
