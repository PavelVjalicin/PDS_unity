using UnityEngine;
using System.Collections;

public class randomAnimDuration : MonoBehaviour {
    public float minSpeed = 0.8f;
    public float maxSpeed = 1.2f;
	// Use this for initialization
	void Start () {
        Animator anim = GetComponent<Animator>();
        anim.speed = Random.Range(minSpeed, maxSpeed);
        Destroy(this);
	}
	
	// Update is called once per frame

}