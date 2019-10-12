using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCell : MonoBehaviour
{
    public Image img_Icon;
    public GameObject selectEffect;
    public Text txt_Level;
    public Text txt_Price;

    public void Initialize() {

    }

    public void ShowEffect(bool show) {
        selectEffect.SetActive(show);
    }
}
