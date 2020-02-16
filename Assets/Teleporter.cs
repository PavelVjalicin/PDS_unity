using UnityEngine;
using System.Collections;

public class Teleporter : Enemy
{
    public float TeleportTime;
    public float realTeleportTime;
    public float screenLeft;
    public float screenRight;
    private float time;
    private Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = transform.FindChild("Anim").GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
        RocketLauncherSystem.e.Insert(RocketLauncherSystem.priority[priority], this);
        RocketLauncherSystem.addPriority(priority);
        TeleportTime = realTeleportTime;
        //screenLeft = Game.stageLeft;
        //screenRight = Game.stageRight;


    }

    // Update is called once per frame
    void Update()
    {

        if (!paused)
        {
            anim.SetFloat("Delay", TeleportTime);
            time += Time.deltaTime;
            if (time > 0.02f)
            {
                Game.AddParticle("GreenLine", this.transform.position.x, this.transform.position.y);
                time = 0;
            }
            TeleportTime -= Time.deltaTime;
            if(FreezeTime<0)
                if (TeleportTime < 0)
                    Teleport();
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
    private void Teleport()
    {
        Vector3 newPos = new Vector3(Random.Range(screenLeft, screenRight), this.transform.position.y, 0f);
        this.transform.position = newPos;
        TeleportTime = realTeleportTime;
    }
}
