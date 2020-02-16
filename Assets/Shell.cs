using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {
    public float angle;
    public float speed;
    public float rSpeed;
    public float xSpeed;
    public float ySpeed;
    public float time;
    private GameObject impact;
	// Use this for initialization
	void Start () {
        impact = Game.impact;
        transform.eulerAngles = new Vector3(0f, 0f, angle);
        speed = Random.Range(0.5f, 1f);
        angle = angle + Random.Range(5f, 10f);
        rSpeed = Random.Range(5f, 10f);
        xSpeed = Mathf.Sin(angle / 180 * Mathf.PI) * speed;
        ySpeed = -Mathf.Cos(angle / 180 * Mathf.PI) * speed;
        this.transform.position = new Vector3(this.transform.position.x+Mathf.Sin((angle -40)*Mathf.PI/180)*0.2f, this.transform.position.y - Mathf.Cos((angle - 40) * Mathf.PI / 180) * 0.2f,0f);
        /*angle = Angle + Math.random() * 10 - 5;
        rSpeed = Math.random() * 10 - 5;
        addEventListener(Event.ENTER_FRAME, enterFrame);
        xSpeed = Math.sin(angle * Math.PI / 180) * speed;
        ySpeed = Math.cos(angle * Math.PI / 180) * speed;
        x = _x + Math.sin((Angle - 40) * Math.PI / 180) * 15;
        y = _y + Math.cos((Angle - 40) * Math.PI / 180) * 15;*/
    }
	
	// Update is called once per frame
	void Update () {
	    if(time < 0.67f)
        {
            time+= Time.deltaTime;
            this.transform.position = new Vector3(transform.position.x + xSpeed * Time.deltaTime, transform.position.y + ySpeed * Time.deltaTime, 0f);
            angle += angle + rSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0f, 0f, angle);
        }
        else
        {
            if(this.transform.position.y < -3.4f)
            {
                GameObject instance = Instantiate(Resources.Load("ShellTexture", typeof(GameObject))) as GameObject;
                instance.transform.position = this.transform.position;
                instance.transform.eulerAngles = this.transform.eulerAngles;
                instance.transform.SetParent(impact.transform);
                instance.isStatic = true;
            }
            Destroy(gameObject);
        }
	}
}
