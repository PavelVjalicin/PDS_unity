using UnityEngine;
using System.Collections;

public class EnergyPrison : Cannon {
    public ArrayList enemies = new ArrayList();
    public GameObject turret;
    public Collider2D range;
    public GameObject target;
    GameObject rangeSprite;
    private SpriteRenderer sp;
    // Use this for initialization
    void Start () {
        sp = transform.FindChild("Anim").GetComponent<SpriteRenderer>() ;
        sound = GetComponent<AudioSource>();
        rangeSprite = transform.FindChild("RangeSprite").gameObject;
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
            sp.color = new Color(1f, 1f, 1f, (realDelay-delay)/realDelay);
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
            else {
                range.gameObject.SetActive(true);
                rangeSprite.SetActive(false);
            }
            if (drag == false && active)
            {
                delay -= Time.deltaTime;
                if (delay < 0)
                {
                    getTarget();
                    if (target != null)
                    {
                        Enemy en = target.GetComponent<Enemy>();
                        if (en.FreezeTime < 0 && en.canBeFrozen == true)
                        {
                            ShootBullet();
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
    public new void ShootBullet()
    {
        if (GameVar.sound == true)
            sound.Play();
        if (target != null)
        {
            Enemy e = target.GetComponent<Enemy>();
            GameObject instance = Instantiate(Resources.Load("EnergyPrisonBullet", typeof(GameObject))) as GameObject;

            Vector3 a = target.transform.position - transform.position;
            a.Normalize();
            float rotZ = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg;

            instance.transform.eulerAngles = new Vector3(0f, 0f, rotZ - 90);
            instance.transform.localScale = new Vector3(1f, Vector2.Distance(this.transform.position, target.transform.position)*1.2f, 1f);

            instance.transform.position = this.transform.position;
            Destroy(instance, 0.2f);
            Enemy en = target.GetComponent<Enemy>();
            en.Freeze();
            if (GameVar.graphics)
            {
                GameObject ep = Instantiate(Resources.Load("PrisonParticle", typeof(GameObject))) as GameObject;
                ep.transform.position = new Vector3(en.transform.position.x, en.transform.position.y, 0f);
                ep.GetComponent<PrisonParticle>().time = en.RealFreezeTime;
            }
            
        }
        delay = realDelay;
        target = null;
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
}
