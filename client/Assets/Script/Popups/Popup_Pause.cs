using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Pause : MonoBehaviour
{
    public Image bt_Lobby;
    public Image bt_Resume;
    public Image bt_Retry;
    public Image bt_Help;

    public void Awake() {
        int index = Scene_Game.currentState == Scene_Game.World.DAY ? 0 : 1;
        bt_Lobby.sprite = GameData.instance.sprite_Button1[index];
        bt_Resume.sprite = GameData.instance.sprite_Button2[index];
        bt_Retry.sprite = GameData.instance.sprite_Button3[index];
        bt_Help.sprite = GameData.instance.sprite_Button4[index];
    }

    public void OnLobby() {
        Scene_Game.ChangeState(Scene_Game.State.NONE_START);
        Destroy(gameObject);
    }
    public void OnResume() {
        Scene_Game.ChangeState(Scene_Game.State.DOING);
        Destroy(gameObject);
    }

    public void OnRetry() {
        Scene_Game.ChangeState(Scene_Game.State.WAIT_START);
        Destroy(gameObject);
    }

    public void OnHelp() {

    }
}
