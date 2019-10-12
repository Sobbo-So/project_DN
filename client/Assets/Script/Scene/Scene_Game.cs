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
        public Button[] buttons;
    }

    public GameObject contents;
    public Transform trans_popups;

    public GameObject panel_StartUI;
    public GameObject panel_StartDoingUI;
    public GameObject panel_DoingUI;
    public GameObject panel_GameOverUI;

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
        foreach (var data in lstRecipeButtons) {
            var tempList = new List<Button>(data.buttons);
            foreach (var bt in tempList) {
                var indexOf = tempList.IndexOf(bt);
                bt.onClick.RemoveAllListeners();
                bt.onClick.AddListener(delegate () { OnTouchedRecipe(data.key, data.key == 1 ? ColorCode.IndexOf(indexOf) : indexOf); });
            }
        }

        ChangeState(State.NONE_START);
        selectWeapon = lstWeaponCells[0];
        lstWeaponCells[0].ShowEffect(true);
        UpdateMyData();
    }

    public void InitializeState() {
        if (currentState == State.NONE_START)
            txtBestScore.text = MyData.Instance.best_score.ToString();
        else if (currentState == State.DOING) {
            DateTime currentDate = DateTime.Now;
            TimeSpan span = new TimeSpan(currentDate.Ticks);
            var currentSecond = span.TotalSeconds;

            _lastCreateCustomer = _lastCreateCustomer = currentSecond;

            currentWorld = World.DAY;
            ChangeWorldDirect(false);
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
    }

    public void AddScore(int add) {
        score += add;
        UpdateMyData();
    }

    public void AddMoney(int add) {
        MyData.Instance.money += add;
        UpdateMyData();
    }

    public void SetGameOver() {
        ChangeState(State.WAIT_ENDED);
    }

    private IEnumerator _SetGameOver() {
        if (currentWorld == World.DAY) {
            currentWorld = World.NIGHT;
            ChangeWorldDirect(true);
            yield return new WaitForSeconds(0.5f);
        }
        obj_GameOverLogo.SetActive(true);
        yield return new WaitForSeconds(3f);
        ChangeState(State.ENDED);
        obj_GameOverLogo.SetActive(false);
    }
    
    private IEnumerator _SetStartGame() {
        int count = 3;
        while (count > 0) {
            txt_Counter.text = count.ToString();
            yield return new WaitForSeconds(1f);
            --count; 
        }

        ChangeState(State.DOING);
    }
}
