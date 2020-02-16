using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Cannon : MonoBehaviour {

    // Use this for initialization
    public float delay = 0.66f;
    public float realDelay = 0.66f;
    public bool drag = false;
    
    [NonSerialized]public int cost;
    public bool active = true;
    public GameObject emptyCannon;
    public bool onPlanet;
    public bool onCannon;
    public bool onPlatform;
    public bool collision;
    public string type;
    public string bulletType;
    [NonSerialized]public Vector3 bulletOfSet;
    [NonSerialized]public float angle = 0f;
    public BoxCollider2D col2D;
    public AudioSource sound;
    public bool paused = false;
     void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    

    public void MoveToMouse()
    {
        if(active)
            if (drag == true)
            {
                this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);

            }
    }
    public bool CheckCollision()
    {
        if (onCannon) return false;
        else if (onPlatform) return true;
        else if (!onPlanet) return false;
        return true;
    }
    public void ShootBullet()
    {
        if(GameVar.sound == true)
            sound.Play();
        GameObject instance = Instantiate(Resources.Load(bulletType, typeof(GameObject))) as GameObject;
        Bullet instanceScript = instance.GetComponent<Bullet>();
        instanceScript.transform.position = this.transform.position + bulletOfSet ;
        instanceScript.angle = angle;
        delay = realDelay;
    }
    public void Drag(bool eC = true)
    {
        if (active)
        {
            col2D.gameObject.SetActive(false);
            if (eC == true)
                CreateEmptyCannon();
            drag = true;
            
            //renderer.material.color.a = 0.5;
            this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            onCannon = false;
            collision = CheckCollision();
        }
    }
    Collider2D[] SpawnBoxCollision(Vector2 pos, BoxCollider2D col)
    {
        Vector2 a = new Vector2(pos.x - col.size.x/2, pos.y + col.size.y/2);
        Vector2 b = new Vector2(pos.x + col.size.x/2, pos.y - col.size.y/2);
        Collider2D[] Box = Physics2D.OverlapAreaAll(a, b);
        return Box;
    }
    public void Drop(Energy energy = null)
    {
        
        if (active)
        {
            col2D.gameObject.SetActive(true);
            if (collision == true)
            {
                Destroy(emptyCannon);
                drag = false;
                if (emptyCannon == null)
                {
                    if (TutorialClass.tutorial == true)
                    {
                        GameObject.Find("Tutorial1(Clone)").GetComponent<TutorialClass>().Next(this.transform.position);
                    }
                    energy.energy -= cost;
                    energy.UpdateUI();
                    if (type == "Battery")
                    {
                        energy.maxEnergy *= 2;
                    }
                    if (type == "Generator")
                    {
                        energy.energyAmount += 1;
                    }
                }
                else
                {
                    if (TutorialClass.tutorial2 == true)
                    {
                        GameObject.Find("Tutorial1(Clone)").GetComponent<TutorialClass2>().Next();
                    }
                }
            }
            else
            {
                Vector2 boxVec = new Vector2() ;
                Vector2 newVec = transform.position;
                int steps = 0;
                bool fail = false;
                BoxCollider2D col = transform.Find("hitTestCannon").GetComponent<BoxCollider2D>();
                Collider2D[] Box = SpawnBoxCollision(transform.position,col);
                bool isOnPlanet = false;
                for (var i = 0; i < Box.Length; i++)
                    if (Box[i].tag == "Cannon")
                    {
                        GameObject o = Box[i].gameObject.transform.parent.gameObject;
                        if (o != this.gameObject && o != emptyCannon)
                        {
                            boxVec = o.transform.position;
                        }
                    }

                while (collision == false)
                {
                    fail = true;
                    newVec = Vector2.MoveTowards(newVec, boxVec, -0.01f);
                    Collider2D[] newBox = SpawnBoxCollision(newVec,col);
                    
                    for (var i = 0; i < newBox.Length; i++)
                    {
                        if (newBox[i].tag == "Cannon")
                        {
                           
                            GameObject o = newBox[i].gameObject.transform.parent.gameObject;
                            if (o != this.gameObject && o != emptyCannon)
                            {
                                fail = true;
                                break;
                            }
                        }
                        if (newBox[i].tag == "BigPlanet")
                        {
                            fail = false;
                        }
                        if (newBox[i].tag == "Platform")
                        {
                            fail = false;
                            
                            goto End;
                        }
                    }
                    float stageLeft = Game.stageLeft;
                    float stageRight = Game.stageRight;
                    float stageTop = Game.stageTop;
                    float stageBottom = Game.stageBottom;
                    if (newVec.x > stageRight)
                        break;
                    if (newVec.x < stageLeft)
                        break;
                    if (newVec.y < stageBottom)
                    {
                        break;
                    }
                    if (fail == false)
                    {
                        onCannon = false;
                        this.transform.position = newVec;
                        Destroy(emptyCannon);
                        drag = false;
                        collision = true;
                        if (emptyCannon == null)
                        {
                            if (TutorialClass.tutorial == true)
                            {
                                GameObject.Find("Tutorial1(Clone)").GetComponent<TutorialClass>().Next(this.transform.position);
                            }
                            energy.energy -= cost;
                            energy.UpdateUI();
                            if (type == "Battery")
                            {
                                energy.maxEnergy *= 2;
                            }
                            if (type == "Generator")
                            {
                                energy.energyAmount += 1;
                            }
                        }
                        else
                        {
                            if (TutorialClass.tutorial2 == true)
                            {
                                GameObject.Find("Tutorial1(Clone)").GetComponent<TutorialClass2>().Next();
                            }
                        }
                        return;
                    }
                    
                    steps++;
                    //Debug.Log(steps);
                    if(steps > 100)
                    {
                        break;
                    }
                }
                End:
                if (emptyCannon != null)
                {
                    Cannon cannon = emptyCannon.GetComponent<Cannon>();
                    cannon.Active();
                    /*if (isOnPlanet == true)
                    {
                        cannon.onPlanet = true;
                    }
                    else cannon.onPlatform = true;*/
                    cannon.onCannon = false;

                }
                Destroy(this.gameObject);
            }
        }
    }
    void CreateEmptyCannon()
    {
        emptyCannon = Instantiate(Resources.Load(type, typeof(GameObject))) as GameObject;
        Cannon instanceScript = emptyCannon.GetComponent<Cannon>();
        instanceScript.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
        instanceScript.Passive();
    }
    public void Passive()
    {
        Transperant(0.5f);
        active = false;
    }
    public void Active()
    {
        
        Transperant(1f);
        active = true;
    }
    public virtual void Transperant(float alpha)
    {
        SpriteRenderer[] rend = GetComponentsInChildren<SpriteRenderer>();
        for(int i = 0; i < rend.Length;i++)
            rend[i].color = new Color(1f, 1f, 1f, alpha);
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (drag == true)
        {
            if (col.tag == "Cannon")
            {

                GameObject go = col.gameObject.transform.parent.gameObject;

                if (go != emptyCannon)
                {
                    onCannon = true;
                }

            }
            else if (col.tag == "Platform") onPlatform = true;
            else if (col.tag == "BigPlanet") onPlanet = true;
        }
        collision = CheckCollision();
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (drag == true)
        {
            if (col.tag == "Cannon")
            {
                GameObject go = col.gameObject.transform.parent.gameObject;
                if (go != emptyCannon)
                {
                    onCannon = false;

                }

            }
            else if (col.tag == "Platform") onPlatform = false;
            else if (col.tag == "BigPlanet") onPlanet = false;
        }
        collision = CheckCollision();
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
