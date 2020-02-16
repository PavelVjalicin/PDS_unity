using UnityEngine;
using System.Collections;

public class HomingBullet : Bullet {

    public float delay = 0.8f;
    // Use this for initialization
    void Update()
    {
        Move();
        delay -= Time.deltaTime;
        if (delay < 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Enemy")
        {

            Enemy enemy = col.GetComponent<Enemy>();

            enemy.Damage(damage);
            Destroy(this.gameObject);

        }
    }

}
