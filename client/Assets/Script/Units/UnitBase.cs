using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour {
    protected GameObject prefab;
    protected Animator animator_prefab;

    public virtual void SetUnitDestroy(bool dead) {
        Destroy(gameObject);
    }

    public void SetAnimatorBool(int key, bool value) {
        if (animator_prefab == null)
            return;

        animator_prefab.SetBool(key, value);
    }
}
