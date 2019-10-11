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

    public GameObject blocker;
    public Image img_fade;
    public void Awake() {
        img_fade.gameObject.SetActive(true);
        Sequence sequence = DOTween.Sequence()
            .OnStart(delegate () {
                img_fade.gameObject.SetActive(true);
            })
            .Append(img_fade.DOFade(0f, 0.5f))
            .OnComplete(delegate () {
                img_fade.gameObject.SetActive(false);
            });
    }

    public void ChangeScene(int index) {
        Sequence sequence = DOTween.Sequence()
            .OnStart(delegate () {
                img_fade.gameObject.SetActive(true);
            })
            .Append(img_fade.DOFade(1f, 0.5f))
            .OnComplete(delegate () {
                SceneManager.LoadScene(index);
            });
    }
}
