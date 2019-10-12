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
    private int _currentState;
    private int _currentWorld;

    private int _currentRecipeOrder;
    private int _remainColorTouch = 2;
    private float _doingTime = -1f;

    private int _selectRecipe_A;        // cup
    private int _selectRecipe_B;        // color hex
    private int _selectRecipe_C;        // straw

    [NonSerialized] public static int hasColor = 0;

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

        foreach (var data in lstRecipeButtons) {
            int index = 0;
            foreach (var bt in data.buttons) {
                bt.onClick.RemoveAllListeners();
                bt.onClick.AddListener(delegate () { OnTouchedRecipe(data.key, index); });
                ++index;
            }
        }

        UpdateMyData();
    }

    public void OnChangeWorld() {
        _currentWorld = _currentWorld == World.NIGHT ? World.DAY : World.NIGHT;
        ChangeWorldDirect();

        panel_RecipeUI.SetActive(_currentWorld == World.DAY);
        panel_WeaponUI.SetActive(_currentWorld == World.NIGHT);
    }

    public void OnTouchedRecipe(int order, int select) {
        if (order != _currentRecipeOrder) {
            return;
        }

        switch (order) {
            case 0:
                _selectRecipe_A = select;
                break;
            case 1:
                if (_remainColorTouch == 2)
                    _selectRecipe_B = select;
                else if (_remainColorTouch == 1)
                    _selectRecipe_B = RecipeColor.TotalColor(_selectRecipe_B, select);
                --_remainColorTouch;
                break;
            case 2:
                _selectRecipe_C = select;
                break;
        }

        if (order != 1 || _remainColorTouch <= 0) {
            if (_currentRecipeOrder + 1 >= Contents.MAX_RECIPE_OREDER)
                _currentRecipeOrder = 0;
            else
                ++_currentRecipeOrder;
            MoveRecipeUI();
        }
    }

    public void DropCurrentRecipe() {
        _currentRecipeOrder = -1;
        _remainColorTouch = 2;
        _selectRecipe_A = -1;
        _selectRecipe_B = -1;
        _selectRecipe_C = -1;
    }
}
