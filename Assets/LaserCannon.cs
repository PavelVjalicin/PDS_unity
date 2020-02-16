using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
public class LaserCannon : Cannon
{


    GameObject rangeSprite;
    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
        rangeSprite = transform.FindChild("RangeSprite").gameObject;
        bulletOfSet = new Vector3(0f, 0f, 0f);


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
            if (drag == false && active)
            {
                delay -= Time.deltaTime;
                if (delay < 0) ShootBullet();
            }
            MoveToMouse();
        }
    }
    new void ShootBullet()
    {
        if (GameVar.sound)
            sound.Play();
        GameObject instance = Instantiate(Resources.Load(bulletType, typeof(GameObject))) as GameObject;
        instance.transform.position = this.transform.position + bulletOfSet;

        delay = realDelay;
        Destroy(instance, 0.2f);
    }
}