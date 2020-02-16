using UnityEngine;
using System.Collections;
using System;

public class SniperCannon : Cannon {

    [NonSerialized]
    public Animator anim;
    public GameObject turret;
    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
        bulletOfSet = new Vector3(0f, 0f,0f);
        Transform turret = gameObject.transform.Find("Turret");
        col2D = gameObject.transform.Find("hitTest").GetComponent<BoxCollider2D>();
        anim = turret.GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            if (active == true && drag == false)
                CheckMouseAngle();
            anim.SetFloat("Delay", delay);
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
        turret.transform.rotation = Quaternion.Euler(0f, 0f, rotZ-90);
        angle = turret.transform.eulerAngles.z;
    }
}
