using UnityEngine;
using System.Collections;

public class Rocket : Bullet
{
    private float timer;
    public GameObject Target;
    public float rSpeed;


    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                if (RocketLauncherSystem.e.Count > 0)
                    Target = RocketLauncherSystem.e[0].gameObject;
            }
            xSpeed = -Mathf.Sin(angle / 180 * Mathf.PI) * speed;
            ySpeed = Mathf.Cos(angle / 180 * Mathf.PI) * speed;
            transform.position = new Vector2(transform.position.x + xSpeed * Time.deltaTime, transform.position.y + ySpeed * Time.deltaTime);
            if (Target != null)
            {
                Vector3 vectorToTarget = Target.transform.position - transform.position;
                float Angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
                Quaternion q = Quaternion.AngleAxis(Angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, rSpeed * Time.deltaTime);
                angle = transform.eulerAngles.z;
            }
        }
    }
    public void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject == Target)
        {
            Game.AddParticle("RocketExplosion", this.transform.position.x, this.transform.position.y);
            Enemy e = Target.GetComponent<Enemy>();
            e.Damage(damage);
            Destroy(this.gameObject);
        }
    }
}
