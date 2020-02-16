using UnityEngine;
using System.Collections;

public class PlayUntilFinished : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioSource sound = gameObject.GetComponent<AudioSource>();
        Destroy(gameObject, sound.clip.length);
        Destroy(this);
	}
}
