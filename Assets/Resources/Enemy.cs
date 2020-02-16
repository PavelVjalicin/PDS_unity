using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    public float xSpeed = 0f;
    public float ySpeed = 0f;
    public float speed = 3f;
    public int health;
    public int damage;
    public bool impact = false;
    public float impactTime;
    public float angle = 0f;
    public string particle;
    public bool canBeFrozen = true;
    public float RealFreezeTime = 2;
    public float FreezeTime = 0;
    public int priority = 0;
    public bool killed = false;
    public AudioSource sound;
    public bool paused = false;
    // Use this for initialization
    void Awake()
    {
        impactTime = UnityEngine.Random.Range(0.1f, speed*0.7f);
    }
    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
        RocketLauncherSystem.e.Insert(RocketLauncherSystem.priority[priority], this);
        RocketLauncherSystem.addPriority(priority);
        
        
    }
   /* void addPriority()
    {
        if (RocketLauncherSystem.e.Count >= priority)
        {
            
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            FreezeTime -= Time.deltaTime;

            xSpeed = Mathf.Sin(angle / 180 * Mathf.PI) * -speed;
            ySpeed = Mathf.Cos(angle / 180 * Mathf.PI) * -speed;

            Move();
            if (transform.position.y < -5.2)
            {

                outOfBounds();


            }
            if (impact == true)
            {
                impactTime -= Time.deltaTime;
                if (impactTime < 0)
                {
                    Impact();
                }
            }
        }
    }
    public virtual void Impact()
    {
        if (!killed)
        {
            for (int i = 0; i < Mathf.Round(damage / 10); i++)
                Game.AddParticle("Dust", this.transform.position.x, this.transform.position.y);
            Game.AddParticle("ImpactExplosion", this.transform.position.x, this.transform.position.y);
            PlanetHealth.shield -= damage;
            if (PlanetHealth.shield < 0)
            {
                PlanetHealth.planetHealth += PlanetHealth.shield;
                PlanetHealth.shield = 0;
                Game.AddParticle("Impact", this.transform.position.x, this.transform.position.y);

            }
            bool s = GameVar.sound;
            if (GameVar.sound== true)
            {
                GameObject instance = Instantiate(Resources.Load("ImpactSound", typeof(GameObject))) as GameObject;
            }
            Kill();
        }
        
    }
    public void Move()
    {
        if (FreezeTime < 0)
        {
            transform.position = new Vector2(transform.position.x + xSpeed * Time.deltaTime, transform.position.y + ySpeed * Time.deltaTime);
            if (transform.position.y > 6) Kill();
        }
    }
    public virtual void Damage(int damage)
    {
        
        if (GameVar.sound == true)
        {
            sound.Play();
        }
        if (health - damage < 1)
            damage = health;
        for (var i = 0; i < damage/2+1; i++)
            Game.AddParticle(particle, this.transform.position.x, this.transform.position.y);
        health -= damage;
        if (health < 1) Kill();

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!impact)
        { 
            if (col.tag == "BigPlanet")
            {
                impact = true;
            }
        }
    }
    public void Freeze()
    {
        
        FreezeTime = RealFreezeTime;

    }
    public virtual void Kill()
    {
        if (killed == false)
        {
            Vector3 p = transform.position;
            if (priority == 9)
            {
                
                Game.AddParticle("SmallExplosion", p.x+0.1f, p.y);
                Game.AddParticle("SmallExplosion", p.x - 0.1f, p.y-0.04f);
                Game.AddParticle("SmallExplosion", p.x + 0.06f, p.y+0.04f);
            }
            else if(priority == 8)
            {
                Game.AddParticle("SmallExplosion", p.x + 0.1f, p.y);
                Game.AddParticle("SmallExplosion", p.x - 0.1f, p.y - 0.04f);
                Game.AddParticle("SmallExplosion", p.x + 0.06f, p.y + 0.04f);
                Game.AddParticle("SmallExplosion", p.x + 0.15f, p.y + 0.1f);
                Game.AddParticle("SmallExplosion", p.x - 0.15f, p.y - 0.10f);
            }
            else if (priority == 2)
            {
                Game.AddParticle("SmallExplosion", p.x + 0.1f, p.y);
                Game.AddParticle("SmallExplosion", p.x - 0.1f, p.y - 0.04f);
                Game.AddParticle("SmallExplosion", p.x + 0.06f, p.y + 0.04f);
                Game.AddParticle("SmallExplosion", p.x + 0.15f, p.y + 0.1f);
                Game.AddParticle("SmallExplosion", p.x - 0.15f, p.y - 0.10f);
                Game.AddParticle("SmallExplosion", p.x - 0.25f, p.y + 0.1f);
                Game.AddParticle("SmallExplosion", p.x, p.y-0.25f);
            }
            else if (priority == 1)
            {
                Game.AddParticle("SmallExplosion", p.x + 0.1f, p.y);
                Game.AddParticle("SmallExplosion", p.x - 0.1f, p.y - 0.04f);
                Game.AddParticle("SmallExplosion", p.x + 0.06f, p.y + 0.04f);
                Game.AddParticle("SmallExplosion", p.x + 0.15f, p.y + 0.1f);
                Game.AddParticle("SmallExplosion", p.x - 0.15f, p.y - 0.10f);
                Game.AddParticle("SmallExplosion", p.x - 0.25f, p.y + 0.1f);
                Game.AddParticle("SmallExplosion", p.x, p.y-0.25f);
                Game.AddParticle("SmallExplosion", p.x + 0.5f, p.y + 0.05f);
                Game.AddParticle("SmallExplosion", p.x - 0.5f, p.y - 0.05f);
            }
            RocketLauncherSystem.e.Remove(this);
            RocketLauncherSystem.removePriority(priority);
            Destroy(gameObject, sound.clip.length);
            Lock();
            killed = true;
        }
    }
    public void Lock()
    {
        speed = 0;
        canBeFrozen = false;
        impactTime = 9999;
        impact = false;
        SpriteRenderer[] sp = GetComponentsInChildren<SpriteRenderer>();
        for(int i = 0; i < sp.Length; i ++)
            Destroy(sp[i]);
        Destroy(GetComponent<Collider2D>());
    }
    public virtual void outOfBounds()
    {
        if (!killed)
        {
            PlanetHealth.shield -= damage;
            if (PlanetHealth.shield < 0)
            {
                PlanetHealth.planetHealth += PlanetHealth.shield;
                PlanetHealth.shield = 0;
                RocketLauncherSystem.e.Remove(this);
                RocketLauncherSystem.removePriority(priority);
                Destroy(gameObject);

            }
        }
    }
    void OnPauseGame()
    {
        paused = true;
    }
    void Resume()
    {
        paused = false;
    }
}
