using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
public class PiercingCannon : Cannon
{


    public GameObject rangeSprite;
    [NonSerialized]    public Animator anim;
    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
        rangeSprite = transform.FindChild("RangeSprite").gameObject;
        bulletOfSet = new Vector3(0f, 0.2f,0f);
        col2D = gameObject.transform.Find("hitTest").GetComponent<BoxCollider2D>();
        bulletType = "PiercingBullet";
        type = "PiercingCannon";
        anim = GetComponent<Animator>();
        
        
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
            if (drag == false && active)
            {
                delay -= Time.deltaTime;
                if (delay < 0) ShootBullet();
            }
            MoveToMouse();
        }
    }
    
}

