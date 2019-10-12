using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCell : MonoBehaviour {
    public class WeaponData {

        public int level;
        public int damage;
        public int price;

        public WeaponData() {
            level = 1;
            damage = 1;
            price = 100;
        }
    }

    public int colorCode;
    public WeaponData data;

    public Image img_Icon;
    public GameObject selectEffect;
    public Text txt_Level;
    public Text txt_Price;

    public Button bt_Current;

    public void Initialize() {
        data = new WeaponData();
        img_Icon.sprite = GameData.instance.lstWeaponSprites[ColorCode.GetIndex(colorCode)];
        selectEffect.SetActive(false);
    }

    public void Refresh() {
        txt_Level.text = data.level.ToString();
        txt_Price.text = data.price.ToString();
    }

    public void ShowEffect(bool show) {
        selectEffect.SetActive(show);
    }
    public void Upgrade() {
        if (MyData.Instance.money < data.price)
            return;

        ++data.level;
        data.price = (int)(data.price * 1.2f);
        data.damage = 1 * data.level;

        Refresh();
    }
}
