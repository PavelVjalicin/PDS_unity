using UnityEngine;
using System.Collections;
using System;

public class ChargingLaser : Cannon {
    public ArrayList enemies = new ArrayList();
    public GameObject turret;
    public Collider2D range;
    public GameObject target;
    public float Charge;
    public float MaxCharge;
    public int damage;
    public GameObject GreenBar;
    GameObject rangeSprite;
    public float dmgTimer = 0;
    public bool Charging = true;
    public float isShooting;
    // Use this for initialization
    void Start () {
        sound = GetComponent<AudioSource>();
        rangeSprite = transform.FindChild("RangeSprite").gameObject;
        bulletOfSet = new Vector3(0f, 0.0f, 0f);
    }

    // Update is called once per frame
    public static bool IsNull(System.Object aObj)
    {
        return aObj == null || aObj.Equals(null);
    }
    void Update()
    {
        if (!paused)
        {
            MoveToMouse();
            isShooting -= Time.deltaTime;
            if (isShooting < -sound.clip.length)
            {
                sound.Stop();
            }
            if (Charge < 0)
                Charging = true;
            if (Charge > 50)
                Charging = false;
            if (Charge < MaxCharge)
                Charge += Time.deltaTime * 30;
            else
            {
                Charge = MaxCharge;
            }

            GreenBar.transform.localScale = new Vector3(Charge / MaxCharge, 1, 0);
            if (drag == true)
            {
                if(emptyCannon!= null)
                    emptyCannon.GetComponent<ChargingLaser>().Charge = Charge;
                if (enemies.Count > 0)
                    enemies = new ArrayList();
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
                
                getTarget();
                if (target != null)
                {
                    if (Charging == false)
                        ShootBullet();
                }
            }
            
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            for (var i = 0; i < enemies.Count; i++)
            {
                int e = enemies[i].GetHashCode();
                if (e == col.gameObject.GetHashCode())
                {
                    return;
                }
            }
            enemies.Add(col.gameObject);

        }


    }
    public new void ShootBullet()
    {
        dmgTimer += Time.deltaTime;
        isShooting = 0;

        if (GameVar.sound == true)
            if (sound.isPlaying == false)
            {
                sound.Play();
            }
       
        if (target != null )
        {
            rotateTurret();
            Enemy e = target.GetComponent<Enemy>();
            if (dmgTimer > 0.034f)
            {
                e.Damage(damage);
                dmgTimer -= 0.034f;
            }
            GameObject instance = Instantiate(Resources.Load("ChargingLaserBullet", typeof(GameObject))) as GameObject;

            instance.transform.eulerAngles = new Vector3(0f, 0f, this.turret.transform.eulerAngles.z);
            instance.transform.localScale = new Vector3(1f, Vector2.Distance(this.transform.position, target.transform.position)*3.2f, 1f);

            instance.transform.position = new Vector3(this.transform.position.x,this.transform.position.y+0.052f,0f);
            Destroy(instance, 0.1f);

        }
        Charge-= Time.deltaTime*30*6;
        target = null;
        getTarget();
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
            /*if (dist2 > 2.5f)
            {
                enemies.RemoveAt(i);
                i--;
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
