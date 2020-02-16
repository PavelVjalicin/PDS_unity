using UnityEngine;
using System.Collections;

public class Trickster : Enemy {
    public bool Left = false;
    public float rotationSpeed;
    private float time;
	// Use this for initialization
	void Start () {
        sound = gameObject.GetComponent<AudioSource>();
        RocketLauncherSystem.e.Insert(RocketLauncherSystem.priority[priority], this);
        RocketLauncherSystem.addPriority(priority);
        if (Left == true)
        {
            angle = 90;
        }
        else
        {
            angle = -90;
        }
        transform.eulerAngles = new Vector3(0f, 0f, angle);
    }
	
	// Update is called once per frame
	void Update () {
        if (!paused)
        {
            time += Time.deltaTime;
            if (time > 0.2f)
            {
                Game.AddParticle("BlueBall", this.transform.position.x, this.transform.position.y + 0.1f);
                time = 0;
            }
            if(FreezeTime<0)
                if (Left == true)
                {
                    rotateLeft();
                }
                else
                {
                    rotateRight();
                }
            FreezeTime -= Time.deltaTime;

            xSpeed = Mathf.Sin(-angle / 180 * Mathf.PI) * -speed;
            ySpeed = Mathf.Cos(-angle / 180 * Mathf.PI) * -speed;

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
    private void rotateLeft()
    {
        angle += rotationSpeed*Time.deltaTime;
        transform.eulerAngles = new Vector3(0f, 0f, angle);
        if (angle > 90)
        {
            Left = false;
        }
    }
    private void rotateRight()
    {
        angle -= rotationSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0f, 0f, angle);
        if (angle < -90)
        {
            Left = true;
        }
    }
}
