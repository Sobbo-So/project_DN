using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBase : MonoBehaviour {
    public Image image;
    protected Animator animator;

    public void Awake() {
        if (image == null) {
            if (gameObject.GetComponent<Image>())
                image = gameObject.GetComponent<Image>();
            else
                image = gameObject.AddComponent<Image>();
        }
    }

    public virtual void SetUnitDestroy(bool dead) {
        Destroy(gameObject);
    }

    public void SetAnimatorBool(int key, bool value) {
        if (animator == null)
            return;

        animator.SetBool(key, value);
    }
}
