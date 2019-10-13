using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

// for Direct
public partial class Scene_Game : Scene_Base {
    [Header("Graphic")]
    public RectTransform rt_Curtain;
    public Image img_Curtain;

    [Header("MainUI")]
    public Text txt_Money;
    public Text txt_Score;

    [Header("Ended")]
    public Text txt_End_BestScore;
    public Text txt_End_Score;


    public void ChangeStateDirect(int value) {
        panel_StartUI.SetActive(value == State.NONE_START);
        panel_StartDoingUI.SetActive(value == State.WAIT_START);
        panel_DoingUI.SetActive(value == State.PAUSE || value == State.DOING);

        obj_EndedContents.SetActive(value == State.ENDED);
        panel_GameOverUI.SetActive(value == State.WAIT_ENDED || value == State.ENDED);

        InitializeState();
    }

    public void ChangeWorldDirect(bool isChange) {
        // animator 실행 (wait은 coroutine으로 할 예정)
        StartCoroutine(_ChangeWorldDirect(isChange));
        //Sequence sequence1 = DOTween.Sequence()
        //    .OnStart(delegate() {
        //        rt_ChangeLayoutEffect.pivot = new Vector2(currentWorld == World.NIGHT ? 0 : 1f, 0.5f);
        //        rt_ChangeLayoutEffect.anchorMin = new Vector2(currentWorld == World.NIGHT ? 0 : 1f, 1f);
        //        rt_ChangeLayoutEffect.anchorMax = new Vector2(currentWorld == World.NIGHT ? 0 : 1f, 1f);
        //
        //        rt_ChangeLayoutEffect.localPosition = Vector3.zero;
        //        rt_ChangeLayoutEffect.gameObject.SetActive(true);
        //    })
        //    .Append(rt_ChangeLayoutEffect.DOMoveX(currentWorld == World.NIGHT ? -2160 : 2160, 0.5f))
        //    .OnComplete(delegate () {
        //        rt_ChangeLayoutEffect.gameObject.SetActive(false);
        //        rt_DoingLayout.localPosition = new Vector2(currentWorld == World.DAY ? 0 : -1080, 0);
        //
        //        bool isDayTime = currentState != State.ENDED && currentWorld == World.DAY;
        //        bool isNightTime = currentState != State.ENDED && currentWorld == World.NIGHT;
        //
        //        panel_RecipeUI.SetActive(isDayTime);
        //        panel_WeaponUI.SetActive(isNightTime);
        //    });
        ////Sequence sequence2 = DOTween.Sequence()
        //    .Append(rt_DoingLayout.DOMoveX(currentWorld == World.DAY ? 0 : -1080, 0.3f))
        //    .OnComplete(delegate () {
        //        bool isDayTime = currentState != State.ENDED && currentWorld == World.DAY;
        //        bool isNightTime = currentState != State.ENDED && currentWorld == World.NIGHT;
        //
        //        panel_RecipeUI.SetActive(isDayTime);
        //        panel_WeaponUI.SetActive(isNightTime);
        //    });
    }

    private IEnumerator _ChangeWorldDirect(bool isChange) {
        if (isChange) {
            rt_Curtain.DOLocalMoveY(0, 0.5f);
            yield return new WaitForSeconds(0.6f);
        }
        rt_DoingLayout.localPosition = new Vector2(currentWorld == World.DAY ? -540 : -1620, 0);

        bool isDayTime = currentState != State.ENDED && currentWorld == World.DAY;
        bool isNightTime = currentState != State.ENDED && currentWorld == World.NIGHT;

        panel_RecipeUI.SetActive(isDayTime);
        panel_WeaponUI.SetActive(isNightTime);

        if (isChange) {
            rt_Curtain.DOLocalMoveY(1920, 0.3f);
            yield return new WaitForSeconds(0.4f);
            img_Curtain.sprite = GameData.instance.sprite_Curtain[currentWorld == World.DAY ? 0 : 1];
            yield return new WaitForSeconds(0.4f);
            rt_Curtain.DOLocalMoveY(1600, 0.4f);
        } else
           yield return new WaitForSeconds(0.35f);
    }

    public void MoveRecipeUI() {
        rt_RecipeUI.DOMoveX(-(_currentRecipeOrder * 1080), 0.5f);
    }

    public void UpdateMyData() {
        txt_Money.text = MyData.Instance.money.ToString();
        txt_Score.text = score.ToString();
    }

    public void RefreshWeaponCells(bool isInit = false) {
        int index = 0;
        foreach (var cell in lstWeaponCells) {
            if (isInit) {
                cell.Initialize();
                cell.bt_Current.onClick.RemoveAllListeners();
                cell.bt_Current.onClick.AddListener(delegate () {
                    selectWeapon.ShowEffect(false);
                    selectWeapon = cell;
                    selectWeapon.ShowEffect(true);
                });

                if (index == 0) {
                    selectWeapon = cell;
                    cell.ShowEffect(true);
                }
            }

            cell.gameObject.SetActive(index < showColorCount);
            ++index;
        }
    }

    public void RefreshMaterialCells(bool isInit = false) {
        foreach (var data in lstRecipeButtons) {
            var tempList = new List<MaterialCell>(data.cells);
            foreach (var cell in tempList) {
                var index = tempList.IndexOf(cell);
                if (isInit) {
                    cell.bt_main.onClick.RemoveAllListeners();
                    cell.bt_main.onClick.AddListener(delegate () { OnTouchedRecipe(data.key, data.key == 1 ? ColorCode.IndexOf(index) : index); });
                }

                if (data.key == 0) {        // Cup
                    cell.img_Icon.GetComponent<Image>().sprite = GameData.instance.lstCupMainSprites[_lstCupTypes[index]];
                    cell.gameObject.SetActive(index < showCupCount);
                }
                else if (data.key == 1) {     // Color
                    cell.img_Icon.GetComponent<Image>().sprite = GameData.instance.lstWeaponSprites[index];
                    var count = haveColorDatas[ColorCode.IndexOf(index)];
                    cell.txt_Count.text = count.ToString();
                    cell.bt_main.interactable = count > 0;
                    cell.gameObject.SetActive(index < showColorCount);
                }
                else {
                    cell.img_Icon.GetComponent<Image>().sprite = GameData.instance.lstDecoSprites[_lstDecoTypes[index]];
                    cell.gameObject.SetActive(index < showDecoCount);
                }

            }
        }
    }

    public void PlayFX(GameObject prefab, Transform parent, float time) {
        var clone = Instantiate(prefab);
        clone.transform.SetParent(parent);
        clone.transform.localPosition = Vector2.zero;

        StartCoroutine(_PlayFX(clone, time));
    }

    private IEnumerator _PlayFX(GameObject clone, float time) {
        yield return new WaitForSeconds(time);
        Destroy(clone);
    }
}
