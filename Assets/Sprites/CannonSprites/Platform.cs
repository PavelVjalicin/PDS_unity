using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    public bool drag = true;
    public bool collision = true;
    public bool onPlanet = false;
    public bool onPlatform = false;
    public string type = "Platform";
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        
        MoveToMouse();
	}
    public void Drag(bool eC = true)
    { 
        collision = CheckCollision();
    }
    public void Drop(Energy e)
    {
        drag = false;
        collision = CheckCollision();
        if (!collision)
        {
            Destroy(this.gameObject);

        }
        else
        {
            e.energy -= 25;
        }
    }
    public void MoveToMouse()
    {
            if (drag == true)
            {
                this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            collision = CheckCollision();

            }
    }
    public bool CheckCollision()
    {
        if (transform.position.y > 1) return false;
        if (onPlanet == true) return false;
        if (onPlatform == true) return false;
        return true;
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (drag == true)
        {
            if (col.tag == "PlatformHitTest")
            {

                onPlatform = true;

            }
            else if (col.tag == "BigPlanet") onPlanet = true;
        }
        collision = CheckCollision();
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (drag == true)
        {
            if (col.tag == "PlatformHitTest")
            {
                onPlatform = false;

            }
            else if (col.tag == "BigPlanet") onPlanet = false;
        }
        collision = CheckCollision();
    }
}
