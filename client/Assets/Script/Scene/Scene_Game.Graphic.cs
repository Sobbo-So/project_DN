using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

// for Direct
public partial class Scene_Game : Scene_Base {
    [Header("Graphic")]
    public Animator animator_curtain;

    [Header("MainUI")]
    public Text txt_Money;
    public Text txt_Score;

    public void ChangeStateDirect(int value) {
        panel_StartUI.SetActive(value == State.NONE_START);
        panel_StartDoingUI.SetActive(value == State.WAIT_START);
        panel_DoingUI.SetActive(value == State.DOING);
        panel_GameOverUI.SetActive(value == State.WAIT_ENDED || value == State.ENDED);

        InitializeState();
        switch (value) {
            case State.NONE_START:
                break;
            case State.WAIT_START:
                StartCoroutine(_SetStartGame());
                break;
            case State.DOING:
                RefreshWeaponCells(true);
                break;
            case State.PAUSE:
                break;
            case State.WAIT_ENDED:
                StartCoroutine(_SetGameOver());
                break;
            case State.ENDED:
                break;
        }
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
            rt_ChangeLayoutEffect.pivot = new Vector2(currentWorld == World.NIGHT ? 0 : 1f, 0.5f);
            rt_ChangeLayoutEffect.anchorMin = new Vector2(currentWorld == World.NIGHT ? 0 : 1f, 1f);
            rt_ChangeLayoutEffect.anchorMax = new Vector2(currentWorld == World.NIGHT ? 0 : 1f, 1f);

            rt_ChangeLayoutEffect.localPosition = Vector3.zero;
            rt_ChangeLayoutEffect.gameObject.SetActive(true);

            rt_ChangeLayoutEffect.DOMoveX(currentWorld == World.NIGHT ? -2160 : 2160, 0.5f);

            yield return new WaitForSeconds(0.08f);

            rt_DoingLayout.localPosition = new Vector2(currentWorld == World.DAY ? -540 : -1620, 0);
        }

        bool isDayTime = currentState != State.ENDED && currentWorld == World.DAY;
        bool isNightTime = currentState != State.ENDED && currentWorld == World.NIGHT;

        panel_RecipeUI.SetActive(isDayTime);
        panel_WeaponUI.SetActive(isNightTime);

        yield return new WaitForSeconds(0.35f);
    }

    public void MoveRecipeUI() {
        rt_RecipeUI.DOMoveX(-(_currentRecipeOrder * 980) + 50, 0.5f);
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
            }

            cell.gameObject.SetActive(index < showColorCount);
            ++index;
        }
    }
}
