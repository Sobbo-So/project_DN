using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyData {
    private static MyData _instance = null;
    public static MyData Instance {
        get {
            if (_instance == null) {
                _instance = new MyData();
                _instance.Load();
            }
            return _instance;
        }
    }

    public int money;
    public int best_score;

    public void Load() {
        money = PlayerPrefs.GetInt("Data_Money", 0);
        best_score = PlayerPrefs.GetInt("Data_BestScore", 0);
    }

    public void Save() {
        PlayerPrefs.SetInt("Data_Money", money);
        PlayerPrefs.SetInt("Data_BestScore", best_score);

        PlayerPrefs.Save();
    }

}
