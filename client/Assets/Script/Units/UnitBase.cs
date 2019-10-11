using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour {
    public Animator animator;
    
    public virtual void OnDestroy() {
        Destroy(this);
    }
}
