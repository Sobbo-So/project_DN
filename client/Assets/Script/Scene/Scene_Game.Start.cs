using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// for Start
public partial class Scene_Game : Scene_Base
{
    [Header("Panel_Start")]
    public Text txtBestScore;
    public RectTransform rt_HeartPattern;
    public RectTransform rt_HeartPattern2;

    public void OnStart() {
        ChangeState(State.WAIT_START);
    }

    public void OnHelp() {
        // Popup
    }

    public void OnLobby() {
        ChangeState(State.NONE_START);
    }
}
