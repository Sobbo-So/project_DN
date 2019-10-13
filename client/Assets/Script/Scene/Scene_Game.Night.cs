using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

// for Direct
public partial class Scene_Game : Scene_Base {


    [Header("Doing Game - Night")]
    public Transform trans_Parent_NormalEnemy;
    public Transform trans_Parent_PenaltyEnemy;
    public GameObject panel_WeaponUI;

    public GameObject panel_Penalty;

    [SerializeField]
    public List<WeaponCell> lstWeaponCells = new List<WeaponCell>();

    private List<UnitEnemy> normalEnemies = new List<UnitEnemy>();
    private List<UnitEnemy> penaltyEnemies = new List<UnitEnemy>();

    private double _lastCreateEnemy = -1;
    private Stack<int> _lstSpawnPositionX;

    public void SpawnEnemy() {
        if (currentState != State.DOING)
            return;

        DateTime currentDate = DateTime.Now;
        TimeSpan span = new TimeSpan(currentDate.Ticks);
        var currentSecond = span.TotalSeconds;
        if (_lastCreateEnemy < 0 || _lastCreateEnemy + 3 <= currentSecond) {
            _lastCreateEnemy = currentSecond;

            for (int i = 0; i < UnityEngine.Random.Range(1, Contents.MAX_SPAWN_ENEMY_COUNT); ++i) {
                var unit = Instantiate(GameData.instance.prefab_UnitEnemy);
                unit.transform.SetParent(trans_Parent_NormalEnemy);

                if (_lstSpawnPositionX == null || _lstSpawnPositionX.Count <= 0) {
                    List<int> tempList = new List<int>(GameData.instance.lstEnemyPositionX);
                    Contents.ShuffleList(tempList);
                    _lstSpawnPositionX = new Stack<int>(tempList);
                }

                unit.transform.localPosition = new Vector2(_lstSpawnPositionX.Pop(), GameData.instance.lstEnemyPositionY[_lstSpawnPositionX.Count % 2 == 0 ? 0 : 1]);

                var unitEnemy = unit.GetComponent<UnitEnemy>();
                unitEnemy.Initialize(UnitEnemy.Type.NORMAL, ColorCode.IndexOf(UnityEngine.Random.Range(0, showColorCount)));

                normalEnemies.Add(unitEnemy);
            }
        }
    }

    public void RemoveEnemy(UnitEnemy obj) {
        if (obj.type == UnitEnemy.Type.NORMAL)
            normalEnemies.Remove(obj);
        if (obj.type == UnitEnemy.Type.PENALTY) {
            penaltyEnemies.Remove(obj);
            if (penaltyEnemies.Count <= 0)
                panel_Penalty.SetActive(false);
        }
    }

    public void SpawnPenaltyEnemy() {
        if (!panel_Penalty.activeSelf) {
            panel_Penalty.SetActive(true);
        }

        var unit = Instantiate(GameData.instance.prefab_UnitPenaltyEnemy);
        unit.transform.SetParent(trans_Parent_PenaltyEnemy);
        unit.transform.localPosition = new Vector2(UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(-100, 100));

        var unitEnemy = unit.GetComponent<UnitEnemy>();
        unitEnemy.Initialize(UnitEnemy.Type.PENALTY);

        penaltyEnemies.Add(unitEnemy);
    }

    public void ResetNightTime() {
        normalEnemies.Clear();
        penaltyEnemies.Clear();
        foreach (Transform child in trans_Parent_NormalEnemy) {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in trans_Parent_PenaltyEnemy) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
   