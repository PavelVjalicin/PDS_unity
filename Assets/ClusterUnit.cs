using UnityEngine;
using System.Collections;

public class ClusterUnit : Enemy {
    public Cluster cluster;
    public int pos;
	// Use this for initialization
	void Start()
    {
        Animator anim = transform.FindChild("Anim").GetComponent<Animator>();
        anim.speed = Random.Range(0.6f, 1f);
        sound = gameObject.GetComponent<AudioSource>();
        RocketLauncherSystem.e.Insert(RocketLauncherSystem.priority[priority], this);
        RocketLauncherSystem.addPriority(priority);
    }
	// Update is called once per frame
	void Update () {
        if (!paused)
        {
            FreezeTime -= Time.deltaTime;

            xSpeed = Mathf.Sin(angle / 180 * Mathf.PI) * -speed;
            ySpeed = Mathf.Cos(angle / 180 * Mathf.PI) * -speed;

            Move();
            MoveTo(pos);
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
    void MoveTo(int pos)
    {
        if (pos != 0)
        {
            Vector2 newPos = cluster.pos[pos];
            newPos = Vector2.MoveTowards(this.transform.position, newPos, speed * Time.deltaTime);
            this.transform.position = newPos;
        }
    }
    public override void Kill()
    {
        if (killed == false)
        {
            RocketLauncherSystem.e.Remove(this);
            RocketLauncherSystem.removePriority(priority);
            cluster.CheckIfDead(this);
            Destroy(gameObject, sound.clip.length);
            Lock();
            killed = true;
        }
    }
}
