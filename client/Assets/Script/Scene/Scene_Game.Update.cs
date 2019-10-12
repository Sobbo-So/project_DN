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
        if (Input.touchCount > 0) {    //터치가 1개 이상이면.
            for (int i = 0; i < Input.touchCount; i++) {
                var tempTouchs = Input.GetTouch(i);
                if (tempTouchs.phase == TouchPhase.Began) {
                    if (CheckAttackEnemy(tempTouchs.position))
                        break;
                }
            }
        }

        if (currentState == State.DOING) {
            SpawnCustomer();
            SpawnEnemy();
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
