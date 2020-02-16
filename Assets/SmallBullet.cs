using UnityEngine;
using System.Collections;

public class SmallBullet : Bullet {

    public void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Enemy")
        {
            Game.AddParticle("SmallBulletParticle", this.gameObject.transform.position.x, this.gameObject.transform.position.y);
            Enemy enemy = col.GetComponent<Enemy>();

            enemy.Damage(damage);
            Destroy(this.gameObject);

        }
    }
}
