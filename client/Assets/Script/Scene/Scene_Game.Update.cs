using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public partial class Scene_Game : Scene_Base {
    void FixedUpdate() {
        if (currentState == State.DOING) {
            doingTime += Time.deltaTime;
            if (_maxCustomerCount < Contents.MAX_CUSTOMER_COUNT)
                RefreshDayLevel();
        }
    }

    void Update() {
        if (currentState == State.DOING) {
            SpawnCustomer();
            SpawnEnemy();
        }

        if (currentState == State.NONE_START) {
        }
    }
}
