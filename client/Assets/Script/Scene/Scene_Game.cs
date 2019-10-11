using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject contents;

    public GameObject panel_RecipeUI;
    public GameObject panel_WeaponUI;

    [Header("UI_Recipe Panel")]
    public GameObject[] lstRecipePanels;
    public Button[] lstColors;

    // private
    private int _currentState;
    private int _currentWorld;

    private int _currentRecipeOrder;
    private float _doingTime = -1f;

    private int _selectRecipe_A;        // cup
    private int _selectRecipe_B;        // color
    private int _selectRecipe_C;        // straw

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
                _selectRecipe_B = select;
                break;
            case 2:
                _selectRecipe_C = select;
                break;
        }
        ++_currentRecipeOrder;


    }
}
