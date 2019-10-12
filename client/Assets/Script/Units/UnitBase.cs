using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour {
    protected Sprite sprite;
    protected Animator animator;

    public virtual void SetUnitDestroy(bool dead) {
        Destroy(gameObject);
    }

    public void SetAnimatorBool(int key, bool value) {
        if (animator == null)
            return;

        animator.SetBool(key, value);
    }
}
