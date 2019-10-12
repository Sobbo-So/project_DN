using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


// for Game
public partial class Scene_Game : Scene_Base {
    void FixedUpdate() {
        if (_currentState == State.DOING)
            doingTime += Time.deltaTime;

        SpawnCustomer();
    }

    void Update() {
    }
}
