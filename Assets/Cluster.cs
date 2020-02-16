using UnityEngine;
using System.Collections;

public class Cluster : MonoBehaviour {
    public float speed;
    public float rotSpeed;
    private float spawnTime;
    public float realSpawnTime;
    public Vector2[] pos;
    public Enemy[] posEmpty;
    private float switchTime;
    public float realSwitchTime;
    bool paused;

	// Use this for initialization
	void Start () {
        Animator anim = transform.FindChild("Wing1").GetComponent<Animator>();
        anim.speed = Random.Range(0.8f,1.2f);
        anim = transform.FindChild("Wing2").GetComponent<Animator>();
        anim.speed = Random.Range(0.8f, 1.2f);
        anim = transform.FindChild("Wing3").GetComponent<Animator>();
        anim.speed = Random.Range(0.8f, 1.2f);
        pos = new Vector2[7];
        posEmpty = new Enemy[7];
        spawnTime = realSpawnTime;
        switchTime = realSwitchTime;
        for (int i = 0; i < 7; i++)
        {
            GameObject instance = Instantiate(Resources.Load("ClusterUnit", typeof(GameObject))) as GameObject;
            instance.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            ClusterUnit e = instance.GetComponent<ClusterUnit>();
            e.speed = this.speed;
            e.cluster = this;
            GivePosition(e);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!paused)
        {
            switchTime -= Time.deltaTime;
            if (switchTime < 0)
                SwitchAllPositions();
            ChangePosVectors();
            transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + rotSpeed);

            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            spawnTime -= Time.deltaTime;
            if (spawnTime < 0)
            {
                spawnTime = realSpawnTime;
                Spawn();
            }
        }
    }
    void Spawn()
    {
        if (posEmpty[0] == null)
        { 
            GameObject instance = Instantiate(Resources.Load("ClusterUnit", typeof(GameObject))) as GameObject;
            instance.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            ClusterUnit e = instance.GetComponent<ClusterUnit>();
            e.speed = this.speed;
            e.cluster = this;
            GivePosition(e);
        }
    }
    void ChangePosVectors()
    {
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y);
        pos[0] = new Vector2(newPos.x,newPos.y);

        pos[1] = new Vector2(newPos.x - 0.22f, newPos.y - 0.4f);

        pos[2] = new Vector2(newPos.x + 0.22f, newPos.y - 0.4f);

        pos[3] = new Vector2(newPos.x + 0.45f, newPos.y);

        pos[4] = new Vector2(newPos.x + 0.22f, newPos.y + 0.4f);

        pos[5] = new Vector2(newPos.x - 0.22f, newPos.y + 0.4f);

        pos[6] = new Vector2(newPos.x - 0.45f, newPos.y);

    }
    void GivePosition(ClusterUnit Unit)
    {
        for(var i = 1; i<7;i++)
        {
            if(posEmpty[i]==null)
            {
                posEmpty[0] = null;
                posEmpty[i] = Unit;
                Unit.pos = i;
                return;
            }
        }
        posEmpty[0] = Unit;
        Unit.pos = 0;
    }
    void SwitchAllPositions()
    {
        posEmpty = new Enemy[7] { posEmpty[0], posEmpty[6], posEmpty[1], posEmpty[2], posEmpty[3], posEmpty[4], posEmpty[5] };
        for (var i = 0; i < 7; i++)
        {
            if (posEmpty[i] != null)
            {
                ClusterUnit e = posEmpty[i].GetComponent<ClusterUnit>();
                e.pos = i;
            }
        }
        switchTime = realSwitchTime;
    }
    public void CheckIfDead(ClusterUnit unit)
    {
        posEmpty[unit.pos] = null;
        if (posEmpty[0] != null)
        {
            ClusterUnit e = posEmpty[0].GetComponent<ClusterUnit>();
            GivePosition(e);
        }
        for (var i=0;i<7;i++)
        {
            if (posEmpty[i] != null)
                return;
        }
        for (int i = 0; i < 7; i++)
        {
            Game.AddParticle("LightningParticle", transform.position.x + Random.Range(-0.5f, .5f), transform.position.y + Random.Range(-.5f, .5f));
        }
        Destroy(this.gameObject);
        
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
