using UnityEngine;
using System.Collections;

public class ImpactExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Animator anim = transform.GetComponentInChildren<Animator>();
        Destroy(this.gameObject, anim.runtimeAnimatorController.animationClips[0].length);
    }
	
}
