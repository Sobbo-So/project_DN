using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

// for Direct
public partial class Scene_Game : Scene_Base {
    [Header("Doing Game")]
    public Transform trans_ParentCustomer;

    private Dictionary<int, UnitCustomer> unitCustomers;       // Max 3

    private int _lastCreateCustomer = -1;
    private int _createCustomerTime = 10;

    private int _score = 0;

    public void FixedUpdate() {
        if (GlobalPrefabData.instance == null)
            return;

        if (unitCustomers.Count < Contents.MAX_CUSTOMER_COUNT) {
            if (_lastCreateCustomer + _createCustomerTime <= DateTime.Now.Millisecond) {
                _lastCreateCustomer = DateTime.Now.Millisecond;

                var random = new System.Random();
                int randomID = random.Next(Contents.MAX_CUSTOMER_COUNT);
                for (int i = 0; i < 100; ++i) {
                    if (unitCustomers.ContainsKey(randomID))
                        randomID = random.Next(Contents.MAX_CUSTOMER_COUNT);
                    else
                        break;
                }

                if (!unitCustomers.ContainsKey(randomID)) {
                    var unit = Instantiate(GlobalPrefabData.instance.prefab_UnitCustomer);
                    unit.transform.SetParent(trans_ParentCustomer);
                    unit.transform.localPosition = GlobalPrefabData.instance.lstCustomerPosition[randomID];

                    var unitCustomer = unit.GetComponent<UnitCustomer>();
                    unitCustomer.Initialize(randomID);

                    unitCustomers.Add(randomID, unitCustomer);
                }
            }
        }
    }

    public void OnPause() {
        currentState = State.PAUSE;

    }
}
