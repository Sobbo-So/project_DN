using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// for Direct
public partial class Scene_Game : Scene_Base {
    public Animator animator_curtain;
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

}
