using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

// for Direct
public partial class Scene_Game : Scene_Base {
    [Header("Doing Game - Night")]
    public Transform trans_ParentEnemy;
    public GameObject layout_Night;

    private List<UnitEnemy> unitEnemies = new List<UnitEnemy>();

    private int _lastCreateEnemy = -1;

    public void SpawnEnemy() {

    }


}
