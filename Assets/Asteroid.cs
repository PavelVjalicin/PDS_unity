using UnityEngine;
using System.Collections;

public class Asteroid : Enemy
{

    // Use this for initialization
    public byte size;
    void Start()
    {

        if (size > 2)
        {
            canBeFrozen = false;
        }
        sound = GetComponent<AudioSource>();
        RocketLauncherSystem.e.Insert(RocketLauncherSystem.priority[priority], this);
        RocketLauncherSystem.addPriority(priority);
        angle = Random.Range(-15, 15);
        speed = Random.Range(0.7f, 2f);
        transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));
        impactTime = UnityEngine.Random.Range(speed / 10, (speed / 10) * 4);
    }
    public override void Damage(int damage)
    {
        if (health - damage < 1)
            damage = health;
        for (var i = 0; i < damage/2; i++)
        {
            Game.AddParticle(particle, this.transform.position.x, this.transform.position.y);
            Game.AddParticle("AsteroidBit", this.transform.position.x, this.transform.position.y);
        }

        health -= damage;
        if (health < 1)
        {
            if (size > 1)
            {
                for (var i = 0; i < 2; i++)
                {
                    string p = "Asteroid" + (size - 1);
                    GameObject instance = Instantiate(Resources.Load(p, typeof(GameObject))) as GameObject;
                    instance.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
                }
            }
            Kill();
        }

        
        // Update is called once per frame
    }
    public override void Impact()
    {
        if (!killed)
        {
            if (transform.position.x < Game.stageLeft)
            {
                if (transform.position.x > Game.stageRight)
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
                    if (GameVar.sound == true)
                    {
                        GameObject instance = Instantiate(Resources.Load("ImpactSound", typeof(GameObject))) as GameObject;
                    }
                    Kill();
                }
                else Kill();
            }
            else Kill();
        }

    }
    public override void outOfBounds()
    {
        if (transform.position.x < Game.stageLeft)
        {
            if (transform.position.x > Game.stageRight)
            {
                if (!killed)
                {
                    PlanetHealth.shield -= damage;
                    if (PlanetHealth.shield < 0)
                    {
                        PlanetHealth.planetHealth += PlanetHealth.shield;
                        PlanetHealth.shield = 0;
                        Kill();

                    }
                }
            }
            else
                Kill();
        }
        else
            Kill();

    }
}
