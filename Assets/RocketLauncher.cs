using UnityEngine;
using System.Collections;

public class RocketLauncher : Cannon {
    public Animator anim;
    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
        delay = realDelay;
        bulletOfSet = new Vector3(0f, 0.5f, 0f);
    }
	// Update is called once per frame
	void Update () {
        if (!paused)
        {
            MoveToMouse();
            delay -= Time.deltaTime;
            anim.SetFloat("Delay", delay);
            if (delay < 0)
            {
                ShootBullet();


            }
        }
    }
}
