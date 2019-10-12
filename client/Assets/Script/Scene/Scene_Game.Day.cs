using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

// for Direct
public partial class Scene_Game : Scene_Base {
    [Header("Doing Game - Day")]
    public Transform trans_ParentCustomer;
    public GameObject layout_Day;

    private List<UnitCustomer> unitCustomers = new List<UnitCustomer>();       // Max 3

    private int _currentRecipeOrder;
    private int _remainColorTouch = 2;

    private double _lastCreateCustomer = -1;
    private double _createCustomerTime = 10;

    private int _selectRecipe_A;        // cup
    private int _selectRecipe_B;        // color hex
    private int _selectRecipe_C;        // straw

    private int _score = 0;

    [NonSerialized] public static int hasColor = 0;

    public void SpawnCustomer() {
        if (GameData.instance.prefab_UnitCustomer == null)
            return;

        if (unitCustomers.Count < Contents.MAX_CUSTOMER_COUNT) {
            DateTime currentDate = DateTime.Now;
            TimeSpan span = new TimeSpan(currentDate.Ticks);
            var currentSecond = span.TotalSeconds;
            if (_lastCreateCustomer < 0 || _lastCreateCustomer + _createCustomerTime <= currentSecond) {
                _lastCreateCustomer = currentSecond;

                var useIDs = unitCustomers.Select(x => x.id);
                var random = new System.Random();
                int randomID = random.Next(Contents.MAX_CUSTOMER_COUNT);
                for (int i = 0; i < 100; ++i) {
                    if (useIDs.Contains(randomID))
                        randomID = random.Next(Contents.MAX_CUSTOMER_COUNT);
                    else
                        break;
                }

                if (GameData.instance.lstCustomerPosition.Length <= randomID)
                    return;

                if (!useIDs.Contains(randomID)) {
                    var unit = Instantiate(GameData.instance.prefab_UnitCustomer);
                    unit.transform.SetParent(trans_ParentCustomer);
                    unit.transform.localPosition = GameData.instance.lstCustomerPosition[randomID];

                    var unitCustomer = unit.GetComponent<UnitCustomer>();
                    unitCustomer.Initialize(randomID);

                    unitCustomers.Add(unitCustomer);
                }
            }
        }
    }

    public void RemoveUnitCustomer(UnitCustomer obj) {
        unitCustomers.Remove(obj);
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
                    _selectRecipe_B = ColorCode.TotalColor(_selectRecipe_B, select);
                --_remainColorTouch;
                break;
            case 2:
                _selectRecipe_C = select;
                break;
        }

        if (order != 1 || _remainColorTouch <= 0) {
            if (_currentRecipeOrder + 1 >= Contents.MAX_RECIPE_OREDER)
                CompleteRecipe();
            else
                ++_currentRecipeOrder;
            MoveRecipeUI();
        }
    }

    public void ResetCurrentRecipe() {
        _currentRecipeOrder = 0;
        _remainColorTouch = 2;
        _selectRecipe_A = -1;
        _selectRecipe_B = -1;
        _selectRecipe_C = -1;
    }

    public void CompleteRecipe() {
        unitCustomers.Sort((x, y) => x.remainTime.CompareTo(y.remainTime));
        foreach (var unit in unitCustomers) {
            if (unit.CheckObjective(_selectRecipe_A, _selectRecipe_B, _selectRecipe_C)) {
                AddScore(Contents.GetScoreByTime(unit.GetRemainTimeRatio()));
                unit.SetUnitDestroy(false);
                break;
            }
        }
        ResetCurrentRecipe();
    }

}
