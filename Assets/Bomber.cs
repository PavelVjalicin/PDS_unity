using UnityEngine;
using System.Collections;

public class Bomber : Enemy {
    public float acceleration;
    public float acceleration2;
    private float dustTime;
	// Use this for initialization
	void Start () {
        dustTime = Random.Range(0.1f, .5f);
        sound = gameObject.GetComponent<AudioSource>();
        RocketLauncherSystem.e.Insert(RocketLauncherSystem.priority[priority], this);
        RocketLauncherSystem.addPriority(priority);

    }
	
	// Update is called once per frame
	void Update () {
        if (!paused)
        {
            FreezeTime -= Time.deltaTime;
            if (FreezeTime <= 0)
            {
                dustTime -= Time.deltaTime;
                if (dustTime < 0)
                {
                    Game.AddParticle(particle, this.transform.position.x, this.transform.position.y + 0.1f);
                    dustTime = Random.Range(0.05f, .25f);
                }
                acceleration += acceleration2 * Time.deltaTime;
                speed += acceleration * Time.deltaTime;
                

                xSpeed = Mathf.Sin(angle / 180 * Mathf.PI) * -speed;
                ySpeed = Mathf.Cos(angle / 180 * Mathf.PI) * -speed;

                Move();
                if (transform.position.y < -5.2)
                {

                    Impact();
                }
                if (impact == true)
                {
                    impactTime -= Time.deltaTime;
                    if (impactTime < 0)
                    {
                        Impact();
                    }
                }
            }
        }
    }
}
