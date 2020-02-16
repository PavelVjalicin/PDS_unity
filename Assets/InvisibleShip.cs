using UnityEngine;
using System.Collections;

public class InvisibleShip : Enemy {
    SpriteRenderer sp;
    public float t;
    private bool tTrig = true;
    public float tSpeed;
    public float maxT;
    public float minT;
    // Use this for initialization
    void Start () {
        sound = gameObject.GetComponent<AudioSource>();
        RocketLauncherSystem.e.Insert(RocketLauncherSystem.priority[priority], this);
        RocketLauncherSystem.addPriority(priority);
        sp = this.GetComponent<SpriteRenderer>();
        t = maxT;

    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            if (tTrig == true)
                t -= tSpeed * Time.deltaTime;
            else
                t += tSpeed * Time.deltaTime;
            if (t > maxT)
                tTrig = true;
            if (t < minT)
                tTrig = false;
            sp.color = new Color(1f, 1f, 1f, t);
            FreezeTime -= Time.deltaTime;

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
