using UnityEngine;
using System.Collections;
using System;

public class HomingLaser : Cannon
{
    public ArrayList enemies = new ArrayList();
    [NonSerialized]
    
    public Animator anim;
    public GameObject turret;
    public Collider2D range;
    public GameObject target;
    public int damage;
    public GameObject rangeSprite;
    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
        rangeSprite = transform.FindChild("RangeSprite").gameObject;
        bulletOfSet = new Vector3(0f, 0f, 0f);
        Transform turret = gameObject.transform.Find("Turret");
        col2D = gameObject.transform.Find("hitTest").GetComponent<BoxCollider2D>();
        anim = turret.GetComponent<Animator>();

        
    }

    public static bool IsNull(System.Object aObj)
    {
        return aObj == null || aObj.Equals(null);
    }
    void Update()
    {
        if (!paused)
        {
            
            if (drag == true)
            {
                if(enemies.Count > 0)
                 enemies = new ArrayList();
                rangeSprite.SetActive(true);
                if (range.gameObject.activeSelf == true)
                {
                    onCannon = false;
                }
                range.gameObject.SetActive(false);

            }
            else
            {
                range.gameObject.SetActive(true);
                rangeSprite.SetActive(false);
            }
            anim.SetFloat("Delay", delay);
            if (drag == false && active)
            {
                delay -= Time.deltaTime;
                if (delay < 0)
                {
                    getTarget();
                    
                    if (target != null)
                    {
                        ShootBullet();
                    }
                }
            }
            MoveToMouse();
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
                for (var i=0; i<enemies.Count;i++)
            {
                int e = enemies[i].GetHashCode();
                if(e == col.gameObject.GetHashCode())
                {
                    return;
                }
            }
            enemies.Add(col.gameObject);
        }
        
       
    }
    public new void ShootBullet()
    {
        if(GameVar.sound)
            sound.Play();
        if (target != null)
        {
            Game.AddParticle("LaserParticle", target.transform.position.x, target.transform.position.y);
            rotateTurret();
            Enemy e = target.GetComponent<Enemy>();
            e.Damage(damage);
            GameObject instance = Instantiate(Resources.Load("HomingLaserBullet", typeof(GameObject))) as GameObject;

            instance.transform.eulerAngles = new Vector3(0f, 0f, this.turret.transform.eulerAngles.z);
            instance.transform.localScale = new Vector3(1f, Vector2.Distance(this.transform.position, target.transform.position), 1f);
            
            instance.transform.position = this.transform.position;
            Destroy(instance, 0.2f);
           
        }
        delay = realDelay;
        target = null;
    }
    private void rotateTurret()
    {
        Vector3 a = target.transform.position - transform.position;
        a.Normalize();
        float rotZ = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg;
        turret.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
        angle = turret.transform.eulerAngles.z;
    }
    private void getTarget()
    {
        for (var i = 0; i < enemies.Count; i++)
        {
            if (IsNull(enemies[i]))
            {
                enemies.RemoveAt(i);
                i--;
            }
        }
        float dist = 100f;

        for (var i = 0; i < enemies.Count; i++)
        {
            GameObject o = enemies[i] as GameObject;
            
            float dist2 = Vector3.Distance(this.transform.position, o.transform.position);
            /*if (dist2 > 2f)
            {
                enemies.RemoveAt(i);
                continue;
            }*/
            if (dist2 < dist)
            {
                Enemy e = o.GetComponent<Enemy>();
                if (e.killed == false)
                {
                    dist = dist2;
                    target = o;
                }
            }
        }
    }
}
