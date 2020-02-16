using UnityEngine;
using System.Collections;

public class SniperBullet : Bullet {

	// Use this for initialization

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Enemy")
        {
            Game.AddParticle("PiercingBulletParticle", this.gameObject.transform.position.x, this.gameObject.transform.position.y);
            Enemy enemy = col.GetComponent<Enemy>();

            enemy.Damage(damage);
            Destroy(this.gameObject);

        }
    }
}
