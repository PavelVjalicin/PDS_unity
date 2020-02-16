using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
public class AOECannon : Cannon {
    GameObject rangeSprite;
    public Animator anim;
    public Collider2D range;
    public int damage;
    private Transform spin;
    private SpriteRenderer glow;
    private float rotationSpeed = 0;
	// Use this for initialization
    void Start()
    {
        glow = transform.FindChild("Glow").GetComponent<SpriteRenderer>();
        spin = transform.FindChild("Spin");
        sound = GetComponent<AudioSource>();
        rangeSprite = transform.FindChild("RangeSprite").gameObject;
    }
	// Update is called once per frame
	void Update () {
        if (!paused)
        {
            glow.color = new Color(1f, 1f, 1f,(realDelay-delay)/realDelay) ;
            if (rotationSpeed < 7)
                rotationSpeed += 0.1f;
            spin.eulerAngles = new Vector3(0f, 0f, spin.eulerAngles.z-rotationSpeed);
            if (active)
                anim.SetFloat("Delay", delay);
            if (drag == true)
            {
                rangeSprite.SetActive(true);
                if (range.gameObject.activeSelf == true)
                {
                    onCannon = false;
                }
                range.gameObject.SetActive(false);

            }
            else {
                range.gameObject.SetActive(true);
                rangeSprite.SetActive(false);
            }
            if (drag == false && active)
            {
                delay -= Time.deltaTime;
                if (delay < 0)
                    ShootBullet();
            }
            MoveToMouse();
        }
    }
    new void ShootBullet()
    {
        if (GameVar.sound == true)
            sound.Play();
        Vector2 vec = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Collider2D[] Box = Physics2D.OverlapCircleAll(vec,1.5f);
        for(int i = 0; i <  Box.Length;i++)
        {
            if(Box[i].tag == "Enemy")
            {
                Enemy enemy = Box[i].GetComponent<Enemy>();
                enemy.Damage(damage);
            }
        }
        delay = realDelay;
    }
}
