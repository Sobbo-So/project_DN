using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


// for Game
public partial class Scene_Game : Scene_Base {

    private Vector3 touchedPos;
    private bool touchOn;

    void FixedUpdate() {
        if (_currentState == State.DOING)
            doingTime += Time.deltaTime;

        SpawnCustomer();
    }

    void Update() {
        if (Input.touchCount > 0) {    //터치가 1개 이상이면.
            for (int i = 0; i < Input.touchCount; i++) {
                var tempTouchs = Input.GetTouch(i);
                if (tempTouchs.phase == TouchPhase.Began) {
                    if (CheckAttackEnemy(tempTouchs.position))
                        break;
                }
            }
        }
    }

    public bool CheckAttackEnemy(Vector2 pos) {
        if (!panel_Penalty.activeSelf) {
            foreach (UnitEnemy enemy in normalEnemies) {
                if (enemy.Attack(pos, _selectWeapon == null ? 0 : _selectWeapon.colorCode, _selectWeapon.damage))
                    return true;
            }
        } else {
            foreach (UnitEnemy enemy in penaltyEnemies) {
                if (enemy.Attack(pos, _selectWeapon == null ? 0 : _selectWeapon.colorCode, _selectWeapon.damage))
                    return true;
            }
        }
        return false;
    }
}
