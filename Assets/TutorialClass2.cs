using UnityEngine;
using System.Collections;

public class TutorialClass2 : MonoBehaviour {
    public Vector3 point1;
    Vector3 point2;
    float wait;
    int phase = 0;
    SpriteRenderer s;
    GameObject sprite;
    // Use this for initialization
    void Start () {
        this.transform.position = point1;
        TutorialClass.tutorial2 = true;
        sprite = transform.FindChild("Sprite").gameObject;
        sprite.SetActive(false);
        s = GetComponent<SpriteRenderer>();
        s.color = new Color(1f, 1f, 1f, 1f);
        point2 = new Vector3(point1.x+0.75f, point1.y, point1.z);
    }
	public void Next()
    {
        TutorialClass.tutorial2 = false;
        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update ()
    {
        if (phase == -1)
        {
            s.color = new Color(1f, 1f, 1f, s.color.a + 0.5f * Time.deltaTime);
            if (s.color.a >= 1f)
            {
                phase = 0;
            }
        }
        else if (phase == 0)
        {
            GameObject instancet = Instantiate(Resources.Load("Click", typeof(GameObject))) as GameObject;
            instancet.transform.position = this.transform.position;
            sprite.SetActive(true);
            phase = 1;
        }
        else if (phase == 1)
        {
            wait -= Time.deltaTime;
            if (wait < -1f)
            {
                wait = 0;
                phase = 2;
                
            }
        }
        else if (phase == 2)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, point2, 1.5f * Time.deltaTime);
            if (this.transform.position == point2)
                phase = 3;
        }
        else if (phase == 3)
        {
            GameObject instancet = Instantiate(Resources.Load("Click", typeof(GameObject))) as GameObject;
            instancet.transform.position = this.transform.position;
            phase = 4;
        }
        else if (phase == 4)
        {
            s.color = new Color(1f, 1f, 1f, s.color.a - 0.5f * Time.deltaTime);
            if (s.color.a <= 0)
            {
                sprite.SetActive(false);
                this.transform.position = point1;
                phase = -1;
                wait = 0;
            }
        }
    }
}
