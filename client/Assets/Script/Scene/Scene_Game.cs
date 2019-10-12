using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// for Game
public partial class Scene_Game : Scene_Base
{
    public static class State {
        public const int NONE_START = 0;
        public const int DOING = 1;
        public const int PAUSE = 2;

        public const int ENDED = 100;
    }

    public static class World {
        public const int DAY = 0;
        public const int NIGHT = 1;
    }

    [NonSerialized] public static Scene_Game instance = null;

    [Serializable]
    public struct RecipeButton {
        public int key;
        public Button[] buttons;
    }

    public GameObject contents;
    public Transform trans_popups;

    public GameObject panel_RecipeUI;
    public RectTransform rt_RecipeUI;
    public GameObject panel_WeaponUI;

    [Header("UI_Recipe Panel")]
    public RecipeButton[] lstRecipeButtons;

    // private
    [NonSerialized] public static float doingTime = 0f;

    private int _currentState;
    private int _currentWorld;

    public int currentState {
        get {
            return _currentState;
        }
        set {
            if (_currentState == value)
                return;

            int prev = _currentState;
            _currentState = value;
            ChangeStateDirect(prev, _currentState);
        }
    }

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

        UpdateMyData();
    }

    public void OnChangeWorld() {
        _currentWorld = _currentWorld == World.NIGHT ? World.DAY : World.NIGHT;
        ChangeWorldDirect();
    }

    public void OnPause() {
        currentState = State.PAUSE;
        var popup = Instantiate(GameData.instance.pref_popup_pause);
        popup.transform.SetParent(trans_popups);
    }

    public void AddScore(int add) {
        _score += add;
        UpdateMyData();
    }

    public void AddMoney(int add) {
        MyData.Instance.money += add;
        UpdateMyData();
    }

    public void SetGameOver() {

    }

    public IEnumerator _SetGameOver() {
        if (_currentWorld == World.DAY) {
            _currentWorld = World.NIGHT;
            ChangeWorldDirect();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
