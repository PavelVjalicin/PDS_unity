using UnityEngine;
using System.Collections;

public class BulletParticle : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));

        Animator anim = GetComponent<Animator>();
        Destroy(this.gameObject, anim.runtimeAnimatorController.animationClips[0].length/anim.speed);
    }

    // Update is called once per frame

}
