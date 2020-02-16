using UnityEngine;
using System.Collections;
using System;

public class FlakCannon : Cannon {
    public GameObject turret;
    public int damage;
    // Use this for initialization
	
	// Update is called once per frame
	void Update () {
        if (!paused)
        {
            if (active == true && drag == false)
                CheckMouseAngle();
            if (drag == false && active)
            {
                delay -= Time.deltaTime;
                if (delay < 0) ShootBullet();
            }
            MoveToMouse();
        }
    }
    void CheckMouseAngle()
    {
        Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        a.Normalize();
        float rotZ = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg;
        turret.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
        angle = turret.transform.eulerAngles.z;
    }
    new void ShootBullet()
    {
        if (GameVar.sound)
            sound.Play();
        GameObject instance = Instantiate(Resources.Load(bulletType, typeof(GameObject))) as GameObject;
        instance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        instance.transform.position = new Vector3(instance.transform.position.x+ UnityEngine.Random.Range(-0.3f, 0.3f), instance.transform.position.y+ UnityEngine.Random.Range(-0.3f, 0.3f), 0f);
        //Bullet instanceScript = instance.GetComponent<Bullet>();
        Destroy(instance, 0.31f);
        Collider2D[] col = Physics2D.OverlapCircleAll(new Vector2(instance.transform.position.x, instance.transform.position.y), 0.4f);
        for (var i = 0; i < col.Length; i++)
        {
            if(col[i].tag == "Enemy")
            {
                Enemy e  = col[i].GetComponent<Enemy>();
                e.Damage(damage);
            }
        }
        //instanceScript.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        delay = realDelay+UnityEngine.Random.Range(-0.1f,0.1f);
    }
}
