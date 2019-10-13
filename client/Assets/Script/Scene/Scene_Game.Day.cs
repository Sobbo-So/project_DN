using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

// for Direct
public partial class Scene_Game : Scene_Base {
    [Header("Doing Game - Day")]
    public Transform trans_ParentCustomer;

    public GameObject panel_RecipeUI;
    public RectTransform rt_RecipeUI;

    public Image img_CurrentCup;
    public Image img_CurrentColor;
    public Image img_CurrentDeco;

    private List<UnitCustomer> unitCustomers = new List<UnitCustomer>();       // Max 3

    private int _currentRecipeOrder;
    private int _remainColorTouch = 2;

    private int _maxCustomerCount = 1;

    private double _lastCreateCustomer = -1;
    private double _createCustomerTime = 10;

    private int _selectRecipe_A;        // cup
    private int _selectRecipe_B;        // color hex
    private int _selectRecipe_C;        // deco

    private int _completedCustomerCount = 0;
    public int completedCustomerCount {
        get {
            return _completedCustomerCount;
        }
        set {
            _completedCustomerCount = value;
            RefreshDayLevel();
        }
    }
    public void SpawnCustomer() {
        if (GameData.instance.prefab_UnitCustomer == null)
            return;

        if (unitCustomers.Count < _maxCustomerCount) {
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
                    if (randomID == 2)
                        unitCustomer.image.rectTransform.localScale = new Vector3(-1, 1, 1);
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
                img_CurrentCup.gameObject.SetActive(true);
                img_CurrentCup.sprite = GameData.instance.lstCupMainSprites[_lstCupTypes[_selectRecipe_A]];
                break;
            case 1:
                if (_remainColorTouch == 2)
                    _selectRecipe_B = select;
                else if (_remainColorTouch == 1)
                    _selectRecipe_B = ColorCode.TotalColor(_selectRecipe_B, select);
                --_remainColorTouch;
                img_CurrentColor.gameObject.SetActive(true);
                img_CurrentColor.sprite = GameData.instance.lstCupLayerSprites.Where(x => x.cupIndex == _lstCupTypes[_selectRecipe_A] && x.colorID == _selectRecipe_B).First().sprite;
                haveColorDatas[select] -= 1;
                RefreshMaterialCells();
                break;
            case 2:
                _selectRecipe_C = select;
                img_CurrentDeco.gameObject.SetActive(true);
                img_CurrentDeco.sprite = GameData.instance.lstDecoSprites[_lstDecoTypes[_selectRecipe_C]];
                break;
        }

        if (order != 1 || _remainColorTouch <= 0) {
            ++_currentRecipeOrder;
            if (_currentRecipeOrder >= Contents.MAX_RECIPE_OREDER)
                StartCoroutine(_CompleteRecipe());
            MoveRecipeUI();
        }
    }

    private IEnumerator _CompleteRecipe() {
        yield return new WaitForSeconds(1f);
        CompleteRecipe();
    }

    public void ResetCurrentRecipe() {
        _currentRecipeOrder = 0;
        _remainColorTouch = 2;
        _selectRecipe_A = -1;
        _selectRecipe_B = -1;
        _selectRecipe_C = -1;

        img_CurrentCup.gameObject.SetActive(false);
        img_CurrentColor.gameObject.SetActive(false);
        img_CurrentDeco.gameObject.SetActive(false);

        MoveRecipeUI();
    }

    public void CompleteRecipe() {
        unitCustomers.Sort((x, y) => x.remainTime.CompareTo(y.remainTime));
        foreach (var unit in unitCustomers) {
            if (unit.CheckObjective(_selectRecipe_A, _selectRecipe_B, _selectRecipe_C)) {
                float ratio = unit.GetRemainTimeRatio();
                AddScore(Contents.GetScoreByTime(ratio));
                AddMoney(Contents.GetMoneyByTime(ratio));

                ++completedCustomerCount;
                unit.SetUnitDestroy(false);
                break;
            }
        }
        ResetCurrentRecipe();
    }

    public void RefreshDayLevel() {
        showCupCount = LevelValue.GetMaxCupAndDecoCount(_completedCustomerCount);
        showColorCount = LevelValue.GetMaxColorCount(_completedCustomerCount);
        showDecoCount = LevelValue.GetMaxCupAndDecoCount(_completedCustomerCount);
        _maxCustomerCount = LevelValue.GetMaxCustomerCount(doingTime);
        enemyAddHP = _completedCustomerCount / 5;
        RefreshWeaponCells();
    }

    public void ResetDayTime() {
        unitCustomers.Clear();
        foreach (Transform child in trans_ParentCustomer) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
