﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class UnitEnemy : UnitBase {
    public static class Type {
        public const int NORMAL = 0;
        public const int PENALTY = 1;
    }

    public Collider2D _collider;

    // for Penalty
    public Rigidbody2D _rigidbody;

    [NonSerialized] public int type = Type.NORMAL; 
    private int _color = 0;
    private int _hp = 0;
    private int _speed = 1;

    public void Initialize(int type, int colorCode = 0) {
        this.type = type;

        if (_collider == null)
            _collider = gameObject.GetComponent<Collider2D>();

        if (type == Type.NORMAL) {
            _color = colorCode;
            _hp = UnityEngine.Random.Range(Contents.MIN_ENEMY_HP + Scene_Game.enemyAddHP, Contents.MAX_ENEMY_HP + Scene_Game.enemyAddHP);

            var spriteData = GameData.instance.lstNormalEnemySpriteData[ColorCode.GetIndex(colorCode)];
            image.sprite = spriteData.sprite;
            animator = spriteData.animator == null ? gameObject.GetComponent<Animator>() : spriteData.animator;
        } else {
            _hp = Contents.PENALTY_ENEMY_HP;

            var spriteData = GameData.instance.lstPenaltyEnemySpriteData[UnityEngine.Random.Range(0, Mathf.Max(0, GameData.instance.lstPenaltyEnemySpriteData.Length - 1))];
            image.sprite = spriteData.sprite;
            animator = spriteData.animator == null ? gameObject.GetComponent<Animator>() : spriteData.animator;
        }
    }

    public override void SetUnitDestroy(bool dead) {
        if (Scene_Game.instance != null)
            Scene_Game.instance.RemoveEnemy(this);

        base.SetUnitDestroy(dead);
    }

    public bool Attack(int color, int damage) {
        if (_color > 0 && _color != color)
            return false;     // miss 처리

        _hp -= type == Type.PENALTY ? 1 : damage;
        if (_hp <= 0) {
            SetUnitDestroy(false);
        }

        return true;
    }

    private void Update() {
        if (Scene_Game.currentState != Scene_Game.State.DOING)
            return;

        if (type == Type.NORMAL) {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - _speed);

            if (transform.localPosition.y <= -467) {
                SetUnitDestroy(true);
                Scene_Game.instance.SetGameOver();
            }
        }
    }

    public void OnTouch() {
        Attack(Scene_Game.selectWeapon.colorCode, Scene_Game.selectWeapon.data.damage);
    }
}
