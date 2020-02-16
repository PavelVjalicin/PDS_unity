using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
public class SpreadCannon : Cannon
{



    [NonSerialized]
    public Animator anim;
    [NonSerialized]
    public Animator anim2;
    GameObject rangeSprite;
    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
        rangeSprite = transform.FindChild("RangeSprite").gameObject;
        Transform Anim = gameObject.transform.Find("Anim");
        Transform glow = gameObject.transform.Find("Glow");
        bulletOfSet = new Vector3(0f, 0.2f, 0f);
        anim = Anim.GetComponent<Animator>();
        anim2 = glow.GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {

            if (drag == true)
                rangeSprite.SetActive(true);
            else
                rangeSprite.SetActive(false);
            anim.SetFloat("Delay", delay);
            anim2.SetFloat("Delay", delay);
            if (drag == false && active)
            {
                delay -= Time.deltaTime;
                if (delay < 0) ShootBullet();
            }
            MoveToMouse();
        }
    }

}