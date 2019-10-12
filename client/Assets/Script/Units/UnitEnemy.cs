using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class UnitEnemy : UnitBase {
    public static class EnemyType {
        public const int NORMAL = 0;
        public const int PENALTY = 1;
    }

    public Collider2D _collider;
    public Rigidbody2D _rigidbody;

    [NonSerialized] public int type = EnemyType.NORMAL; 
    private int _color = 0;
    private int _hp = 0;
    private int _speed = 0;

    public void Initialize(int type, int colorCode = 0) {
        this.type = type;

        if (_collider == null)
            _collider = gameObject.GetComponent<Collider2D>();

        if (type == EnemyType.NORMAL) {
            _color = colorCode;

            var spriteData = GameData.instance.lstNormalEnemySpriteData[ColorCode.GetIndex(colorCode)];
            sprite = spriteData.sprite;
            animator = spriteData.animator == null ? gameObject.GetComponent<Animator>() : spriteData.animator;
        } else {
            var spriteData = GameData.instance.lstPenaltyEnemySpriteData[UnityEngine.Random.Range(0, Mathf.Max(0, GameData.instance.lstPenaltyEnemySpriteData.Length - 1))];
            sprite = spriteData.sprite;
            animator = spriteData.animator == null ? gameObject.GetComponent<Animator>() : spriteData.animator;
        }
    }

    public override void SetUnitDestroy(bool dead) {
        if (Scene_Game.instance != null)
            Scene_Game.instance.RemoveEnemy(this);

        base.SetUnitDestroy(dead);
    }

    public bool Attack(Vector2 pos, int color, int damage) {
        if (!_collider.bounds.Contains(pos))
            return false;

        if (_color > 0 && _color != color)
            return false;     // miss 처리

        _hp -= type == EnemyType.PENALTY ? 1 : damage;
        if (_hp <= 0) {
            SetUnitDestroy(false);
        }

        return true;
    }

    private void Update() {
        if (Scene_Game.instance == null || Scene_Game.instance.currentState != Scene_Game.State.DOING)
            return;

        if (type == EnemyType.NORMAL) {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - _speed);

            if (transform.localPosition.y <= 0)
                Scene_Game.instance.SetGameOver();
        } else {

        }
    }
}
