using UnityEngine;
using System.Collections;

public class Carrier : Enemy {
    private float SpawnTime;
    public float RealSpawnTime;
    public float stageTop;
    private GameObject glow;
    private float glowTime = 0.2f;
    public Animator anim;
	// Use this for initialization
	void Start () {

        glow = transform.FindChild("Glow").gameObject;
        sound = gameObject.GetComponent<AudioSource>();
        RocketLauncherSystem.e.Insert(RocketLauncherSystem.priority[priority], this);
        RocketLauncherSystem.addPriority(priority);
        SpawnTime = RealSpawnTime;
        //stageTop = Game.stageTop*0.7f;
	}

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Delay", SpawnTime);
        glowTime -= Time.deltaTime;
        if (glowTime < 0)
        {
            glowTime = Random.Range(0.05f, 0.2f);
            if (glow.activeSelf)
            {
                glow.SetActive(false);
            }
            else
            {
                glow.SetActive(true);
            }
        }

        if (!paused)
        {
            SpawnTime -= Time.deltaTime;
            if (SpawnTime < 0)
                Spawn();
            FreezeTime -= Time.deltaTime;
            xSpeed = Mathf.Sin(angle / 180 * Mathf.PI) * -speed;
            ySpeed = Mathf.Cos(angle / 180 * Mathf.PI) * -speed;
            if (transform.position.y > stageTop)
                Move();
        }
    }
    void Spawn()
    {
        GameObject instance = Instantiate(Resources.Load("LightShip", typeof(GameObject))) as GameObject;
        instance.transform.position = new Vector2(this.transform.position.x, this.transform.position.y-0.55f);
        SpawnTime = RealSpawnTime;
    }
}
