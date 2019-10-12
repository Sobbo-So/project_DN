using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class UnitCustomer : UnitBase {
    [NonSerialized] public int id;

    private int _objective_A = 0;     // cup
    private int _objective_B = 0;     // color
    private int _objective_C = 0;     // straw

    [Header("Timer UI")]
    public Image img_timerGauge;
    public RectTransform rt_timerEffect;

    private float _timer = 0f;
    public float remainTime {
        get {
            return Mathf.Max(0, _timer);
        }
    }
    private float _totalTimer= 10f;
    private void Update() {
        if (_timer > 0) {
            _timer -= Time.deltaTime;
            TimerUpdate();

            if (_timer <= 0) {
                SetUnitDestroy(true);
            }
        }
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
        _objective_B = RecipeColor.RandomColor(Scene_Game.hasColor);
        _objective_C = random.Next(Contents.MAX_RECIPE_STRAW - 1);

        prefab = Instantiate(GlobalPrefabData.instance.lstCustomerPrefab[random.Next(GlobalPrefabData.instance.lstCustomerPrefab.Length)]);
        prefab.transform.SetParent(transform);
        prefab.transform.localPosition = Vector3.zero;

        animator_prefab = prefab.GetComponent<Animator>() != null ? prefab.GetComponent<Animator>() : prefab.AddComponent<Animator>();

        _totalTimer = random.Next(150, 160);
        _timer = _totalTimer;
    }

    public bool CheckObjective(int a, int b, int c) {
        return _objective_A == a && _objective_B == b && _objective_C == c;
    }

    public override void SetUnitDestroy(bool dead) {
        if (Scene_Game.instance != null)
            Scene_Game.instance.RemoveUnitCustomer(this);

        //연출 후 Destory
        base.SetUnitDestroy(dead);
    }

    public float GetRemainTimeRatio() {
        return _timer / _totalTimer;
    }
}
