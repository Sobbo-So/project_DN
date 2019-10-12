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

    public void ChangeStateDirect(int prev, int value) {
        switch (value) {
            case State.NONE_START:
                break;
            case State.DOING:
                break;
            case State.PAUSE:
                break;
            case State.ENDED:
                if (prev == State.DOING) {
                    // Coroutine Effect
                }
                break;
        }
    }

    public void ChangeWorldDirect() {
        // animator 실행 (wait은 coroutine으로 할 예정)
    }

    public void MoveRecipeUI() {
        rt_RecipeUI.DOMoveX(-(_currentRecipeOrder * 980) + 50, 0.5f);
    }

    public void UpdateMyData() {
        txt_Money.text = MyData.Instance.money.ToString();
        txt_Score.text = _score.ToString();
    }
}
