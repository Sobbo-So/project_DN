using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPrefabData : MonoBehaviour {
    public static GlobalPrefabData instance = null;

    [Header("Popup")]
    public Popup_Pause pref_popup_pause;

    [Header("Units")]
    public GameObject[] lstCustomerPrefab;
    public GameObject[] lstEnemyPrefab;

    public GameObject prefab_UnitCustomer;
    public GameObject prefab_UnitEnemy;

    public Vector2[] lstCustomerPosition;

    public void Awake() {
        if (instance == null)
            instance = this;
    }
}
