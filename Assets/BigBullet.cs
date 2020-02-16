using UnityEngine;
using System.Collections;

public class BigBullet : Bullet {
    public float delay = 2f;
    // Use this for initialization
    AudioSource sound;
    void Start()
    {
        sound = GetComponent<AudioSource>();
        transform.eulerAngles = new Vector3(0f, 0f, angle);
        xSpeed = -Mathf.Sin(angle / 180 * Mathf.PI) * speed;
        ySpeed = Mathf.Cos(angle / 180 * Mathf.PI) * speed;
    }
    void Update()
    {
        if (!paused)
        { 
            Move();
            delay -= Time.deltaTime;
            if (delay < 0)
            {
                SpawnSmallBullets();
                Destroy(this.gameObject, sound.clip.length);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Enemy")
        {
            
            Enemy enemy = col.GetComponent<Enemy>();

            if (enemy.health > damage)
            {
                Game.AddParticle("BigBulletParticle", this.gameObject.transform.position.x, this.gameObject.transform.position.y);
                enemy.Damage(damage);
                hide();
                Destroy(this.gameObject,sound.clip.length);
            }
            else
            {
                Game.AddParticle("BigBulletParticle", this.gameObject.transform.position.x, this.gameObject.transform.position.y);
                int h = enemy.health;
                enemy.Damage(damage);
                damage -= h;
                SpawnSmallBullets();
                hide();
                Destroy(this.gameObject, sound.clip.length);
            }

        }
    }
    private void SpawnSmallBullets()
    {
        if (GameVar.sound)
            sound.Play();
        hide();
        Game.AddParticle("BigBulletParticle", this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        damage = Mathf.FloorToInt(damage / 2);
        if (damage > 0)
            NewSmallBullet(90);
        if (damage > 1)
            NewSmallBullet(270);
        if (damage > 2)
            NewSmallBullet(0);
        if (damage > 3)
            NewSmallBullet(180);
        if (damage > 4)
            NewSmallBullet(45);
        if (damage > 5)
            NewSmallBullet(135);
        if (damage > 6)

            NewSmallBullet(225);
        if (damage > 7)
            NewSmallBullet(315);

    }
    private void NewSmallBullet(float angle)
    {
        GameObject instance = Instantiate(Resources.Load("SmallBullet", typeof(GameObject))) as GameObject;
        Bullet instanceScript = instance.GetComponent<Bullet>();
        instanceScript.transform.position = this.transform.position;
        instanceScript.angle = angle;
    }
    new void OnBecameInvisible()

        {
        return;
        }
    void hide()
    {
        delay = 9999;
        xSpeed = 0;
        ySpeed = 0;
        
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        Destroy(sp);
        Destroy(GetComponent<Collider2D>());
    }
}
