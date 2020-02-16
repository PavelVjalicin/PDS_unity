using UnityEngine;
using System.Collections;

public class TutorialClass : MonoBehaviour {

    // Use this for initialization
    Vector3 point1;
    Vector3 point2;
    float wait;
    int phase = -1;
    SpriteRenderer s;
    GameObject sprite;
    public static bool tutorial;
    public static bool tutorial2;
	void Start () {
        tutorial = true;
        sprite = transform.FindChild("Sprite").gameObject;
        sprite.SetActive(false);
        s = GetComponent<SpriteRenderer>();
        transform.position = new Vector3();
        point1 = GameObject.Find("WeaponIcon1").transform.position;
        point2 = new Vector3(0,-3.7f,0f);
	}

    // Update is called once per frame
    public void Next(Vector3 pos)
    {
        TutorialClass2 c2 = gameObject.AddComponent<TutorialClass2>();
        c2.point1 = pos;
        tutorial = false;
        Destroy(this);
    }
    void Update()
    {
        if(phase == -1)
        {
            s.color = new Color(1f, 1f, 1f, s.color.a + 0.5f * Time.deltaTime);
            if (s.color.a >= 1f)
            {
                phase = 0;
            }
        }
        else if (phase == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, point1, 3f * Time.deltaTime);
            if (this.transform.position == point1)
                phase = 1;
        }
        else if (phase == 1)
        {
            GameObject instancet = Instantiate(Resources.Load("Click", typeof(GameObject))) as GameObject;
            instancet.transform.position = this.transform.position;
            sprite.SetActive(true);
            phase = 2;
        }
        else if (phase == 2)
        {
            wait -= Time.deltaTime;
            if (wait < -1f)
            {
                phase = 3;
                wait = 0;
            }
        }
        else if (phase == 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, point2, 3f * Time.deltaTime);
            if (this.transform.position == point2)
                phase = 4;
        }
        else if (phase == 4)
        {
            GameObject instancet = Instantiate(Resources.Load("Click", typeof(GameObject))) as GameObject;
            instancet.transform.position = this.transform.position;
            phase = 5;
        }
        else if (phase == 5)
        {
            s.color = new Color(1f, 1f, 1f, s.color.a - 0.5f * Time.deltaTime);
            if(s.color.a <= 0)
            {
                sprite.SetActive(false);
                this.transform.position = new Vector3();
                phase = -1;
                wait = 0;
            }
        }
    }
}
