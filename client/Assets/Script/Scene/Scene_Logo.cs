using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Scene_Logo : Scene_Base
{
    public Image img_Logo;
    
    public override void Awake() {
        base.Awake();
        StartCoroutine(_CoroutineEfffect());
    }

    IEnumerator _CoroutineEfffect() {
        yield return new WaitForSeconds(0.5f);
        img_Logo.gameObject.SetActive(true);
        img_Logo.DOFade(1, 1f).OnComplete(delegate() {
            img_Logo.color = new Color(255, 255, 255, 255);
        });
        yield return new WaitForSeconds(2f);

        ChangeScene((int)Scene_Index.INGAME);
    }
}
