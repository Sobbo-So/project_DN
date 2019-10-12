using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UnitCustomer : UnitBase {
    [NonSerialized] public int id;

    private GameObject _prefab;

    private int _objective_A = 0;     // cup
    private int _objective_B = 0;     // color
    private int _objective_C = 0;     // straw

    [Header("Timer UI")]
    public Image img_timerGauge;
    public RectTransform rt_timerEffect;

    private float _timer = 0f;
    private float _totalTimer= 10f;
    private void Update() {
        _timer -= Time.deltaTime;
        TimerUpdate();
    }

    private void TimerUpdate() {
        float amount = 1f - (_timer / _totalTimer);
        img_timerGauge.fillAmount = amount;
        rt_timerEffect.localEulerAngles = new Vector3(0, 0, amount * 360);
    }

    public void Initialize(int id) {
        if (GlobalPrefabData.instance == null)
            return;

        this.id = id;

        var random = new System.Random();
        _objective_A = random.Next(Contents.MAX_RECIPE_CUP - 1);
        _objective_B = random.Next(RecipeColor.MaxColor(Scene_Game.hasColor));
        _objective_C = random.Next(Contents.MAX_RECIPE_STRAW - 1);
        _prefab = Instantiate(GlobalPrefabData.instance.lstCustomerPrefab[random.Next(GlobalPrefabData.instance.lstCustomerPrefab.Length)]);
        _totalTimer = random.Next(10, 15);
        _prefab.transform.SetParent(transform);
    }

    public bool CheckObjective(int a, int b, int c) {
        return _objective_A == a && _objective_B == b && _objective_C == c;
    }
}
