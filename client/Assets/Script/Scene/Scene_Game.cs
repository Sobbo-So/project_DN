using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// for Game
public partial class Scene_Game : Scene_Base
{
    [Serializable]
    public struct RecipeButton {
        public int key;
        public MaterialCell[] cells;
    }

    public GameObject contents;
    public Transform trans_popups;

    public GameObject panel_StartUI;
    public GameObject panel_StartDoingUI;
    public GameObject panel_DoingUI;
    public GameObject panel_GameOverUI;
    public GameObject obj_EndedContents;

    public RectTransform rt_DoingLayout;
    public RectTransform rt_ChangeLayoutEffect;

    [Header("Panel_WaitStart")]
    public Text txt_Counter;

    [Header("Panel_GameOver")]
    public GameObject obj_GameOverLogo;

    [Header("UI_Recipe Panel")]
    public RecipeButton[] lstRecipeButtons;

    // private
    public override void Awake() {
        base.Awake();

        instance = this;

        for (int i = 0; i < GameData.instance.lstCupMainSprites.Length; ++i) {
            _lstCupTypes.Add(i);
        }

        for (int i = 0; i < GameData.instance.lstDecoSprites.Length; ++i) {
            _lstDecoTypes.Add(i);
        }

        for (int i = 0; i < 5; ++i) {
            AddMyItem(ColorCode.IndexOf(i), 0);
        }

        RefreshMaterialCells(true);
        ChangeState(State.NONE_START);
        UpdateMyData();
    }

    public void InitializeState() {
        if (currentState == State.NONE_START)
            txtBestScore.text = MyData.Instance.best_score.ToString();
        else if (currentState == State.WAIT_START) {
            Contents.ShuffleList(_lstCupTypes);
            Contents.ShuffleList(_lstDecoTypes);
            ResetMyItem();
            doingTime = 0;
            completedCustomerCount = 0;

            DateTime currentDate = DateTime.Now;
            TimeSpan span = new TimeSpan(currentDate.Ticks);
            var currentSecond = span.TotalSeconds;

            _lastCreateCustomer = _lastCreateCustomer = currentSecond;

            ResetCurrentRecipe();

            currentWorld = World.DAY;
            ChangeWorldDirect(false);
            RefreshWeaponCells(true);

            StartCoroutine(_SetStartGame());
        } else if (currentState == State.WAIT_ENDED) {
            StartCoroutine(_SetGameOver());
        } else if (currentState == State.ENDED) {
            obj_EndedContents.SetActive(true);
            txt_End_BestScore.text = MyData.Instance.best_score.ToString();
            txt_End_Score.text = score.ToString();
        }
    }

    public void OnChangeWorld() {
        currentWorld = currentWorld == World.NIGHT ? World.DAY : World.NIGHT;
        ChangeWorldDirect(true);
    }

    public void OnPause() {
        currentState = State.PAUSE;
        var popup = Instantiate(GameData.instance.pref_popup_pause);
        popup.transform.SetParent(trans_popups);
        popup.transform.localPosition = Vector3.zero;
    }

    public void AddScore(int add) {
        score += add;
        UpdateMyData();
    }
    public void SetScore(int value) {
        score = value;
        UpdateMyData();
    }

    public void AddMoney(int add) {
        MyData.Instance.money += add;
        UpdateMyData();
    }
    public void PayMoney(int pay) {
        MyData.Instance.money -= pay;
        UpdateMyData();
    }

    public void SetGameOver() {
        ChangeState(State.WAIT_ENDED);
    }

    private IEnumerator _SetGameOver() {
        if (currentWorld == World.DAY) {
            currentWorld = World.NIGHT;
            ChangeWorldDirect(true);
            yield return new WaitForSeconds(0.35f);
        }
        obj_GameOverLogo.SetActive(true);
        yield return new WaitForSeconds(3f);

        if (MyData.Instance.best_score < score) {
            MyData.Instance.best_score = score;
            MyData.Instance.Save();
        }

        ChangeState(State.ENDED);
        obj_GameOverLogo.SetActive(false);
    }
    
    private IEnumerator _SetStartGame() {
        ResetDayTime();
        ResetNightTime();
        SetScore(0);

        int count = 3;
        while (count > 0) {
            txt_Counter.text = count.ToString();
            yield return new WaitForSeconds(1f);
            --count; 
        }

        ChangeState(State.DOING);
    }

    public void AddMyItem(int key, int add) {
        if (!haveColorDatas.ContainsKey(key)) {
            haveColorDatas.Add(key, add);
            return;
        }

        haveColorDatas[key] += add;
        RefreshMaterialCells();
    }

    public void ResetMyItem() {
        foreach (var key in new List<int>(haveColorDatas.Keys)) {
            haveColorDatas[key] = 0;
        }
        RefreshMaterialCells();
    }
}
