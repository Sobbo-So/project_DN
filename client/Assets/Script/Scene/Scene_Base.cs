using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class Scene_Base : MonoBehaviour {
    // set for build setting
    public enum Scene_Index {
        LOGO = 0,
        INGAME,
    }

    public static bool changeScene = false;

    public GameObject blocker;
    public Image img_fade;
    public virtual void Awake() {
        if (changeScene) {
            img_fade.gameObject.SetActive(true);
            Sequence sequence = DOTween.Sequence()
                .OnStart(delegate () {
                    img_fade.gameObject.SetActive(true);
                })
                .Append(img_fade.DOFade(0f, 1f))
                .OnComplete(delegate () {
                    img_fade.color = Color.clear;
                    img_fade.gameObject.SetActive(false);
                });
            changeScene = false;
        }
    }

    public virtual void ChangeScene(int index) {
        Sequence sequence = DOTween.Sequence()
            .OnStart(delegate () {
                img_fade.color = Color.clear;
                img_fade.gameObject.SetActive(true);
            })
            .Append(img_fade.DOFade(1f, 1f))
            .OnComplete(delegate () {
                SceneManager.LoadScene(index);
            });
        changeScene = true;
    }
}
