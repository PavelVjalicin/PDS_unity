using UnityEngine;
using System.Collections;
using System;

public class HomingCannon : Cannon
{
    GameObject rangeSprite;
    public ArrayList enemies = new ArrayList();
    public GameObject turret;
    public Collider2D range;
    public GameObject target;
    public GameObject glow;
    public float coolDown = 0.36f;
    public float bulletRecharge = 0.33f;
    private float trueBulletRecharge;
    private float trueCoolDown;
    public int bulletNumber = 6;
    private float maxBulletNumber;
    private float bulletSpeed = 4.5f;
    public bool shooting = false;
    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
        rangeSprite = transform.FindChild("RangeSprite").gameObject;
        maxBulletNumber = bulletNumber;
        trueBulletRecharge = bulletRecharge;
        bulletOfSet = new Vector3(0f, 0f, 0f);
        trueCoolDown = coolDown;


    }

    public static bool IsNull(System.Object aObj)
    {
        return aObj == null || aObj.Equals(null);
    }
    void Update()
    {
        if (!paused)
        {
            SpriteRenderer rend = glow.GetComponent<SpriteRenderer>();
            float alpha = bulletNumber / maxBulletNumber;
            rend.color = new Color(1f, 1f, 1f, alpha);

            if (bulletNumber < maxBulletNumber)
            {
                bulletRecharge -= Time.deltaTime;
                if (bulletRecharge < 0)
                {
                    bulletRecharge = trueBulletRecharge;
                    bulletNumber += 1;
                }
            }
            coolDown -= Time.deltaTime;
            if (coolDown < 0)
            {
                if (bulletNumber > 3)
                {
                    getTarget();
                }
                if (target != null)
                {
                    coolDown = trueCoolDown;
                }
            }
            if (drag == true)
            {
                if (enemies.Count > 0)
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
            if (bulletNumber == 0)
                shooting = false;
            if (target != null)
            {
                if (drag == false && active)
                {
                    delay -= Time.deltaTime;
                    if (delay < 0)
                    {

                        if (target != null)
                        {
                            if (shooting == false)
                            {
                                if (bulletNumber > 3)
                                {
                                    shooting = true;
                                    bulletNumber -= 1;
                                    rotateTurret();
                                    angle = angle + UnityEngine.Random.Range(-10f, 10f);
                                    ShootBullet();
                                    SpawnShell();
                                }
                            }
                            else
                            {
                                bulletNumber -= 1;
                                rotateTurret();
                                angle = angle + UnityEngine.Random.Range(-10f, 10f);
                                ShootBullet();
                                SpawnShell();
                            }
                        }
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
    private void rotateTurret()
    {
        float dx = target.transform.position.x - this.transform.position.x;
        float dy = target.transform.position.y - this.transform.position.y;
        Enemy e = target.GetComponent<Enemy>();
        float xSpeed = 0;
        float ySpeed = 0;
        if (e.FreezeTime <= 0)
        {
            xSpeed = e.xSpeed * Time.deltaTime;
            ySpeed = e.ySpeed * Time.deltaTime;
        }
        float bulletS = bulletSpeed * Time.deltaTime;
        float a = xSpeed*xSpeed+ySpeed*ySpeed- bulletS * bulletS;
        float b = 2 * (xSpeed * dx + ySpeed * dy);
        float c = dx * dx + dy * dy;
        float q = b * b - 4 * a * c;
        if (q < 0) return;
        float t = ((a<0 ? -1 : 1)*Mathf.Sqrt(q) - b) / (2 * a);
        dx += t * xSpeed;
        dy += t * ySpeed;
        float rotZ = Mathf.Atan2(dy,dx) * Mathf.Rad2Deg;
        
        turret.transform.rotation = Quaternion.Euler(0f, 0f, rotZ-90);
        
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
    public void SpawnShell()
    {
        GameObject instance = Instantiate(Resources.Load("Shell", typeof(GameObject))) as GameObject;
        instance.transform.position = new Vector2(transform.position.x, transform.position.y);
        instance.GetComponent<Shell>().angle = angle;

    }
}