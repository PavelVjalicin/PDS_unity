using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 lastPos;
    private bool pressed;
    private GameObject backGround;
    public bool active = true;
    // Use this for initialization
    void Start()
    {
        backGround = GameObject.Find("BackGround");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (active == true)
        {
            backGround.transform.position = -this.transform.position / 8;
            if (Input.GetMouseButtonDown(0))
            {
                if (pressed == false) mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pressed = true;



            }
            if (Input.GetMouseButtonUp(0)) pressed = false;
            if (pressed == true)
            {
                lastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 newPos = this.transform.position;
                newPos -= mousePos - lastPos;
                mousePos = lastPos;
                this.transform.position = new Vector3(newPos.x, newPos.y, 0f);
                if (transform.position.x > 7)
                {
                    transform.position = new Vector3(7, transform.position.y, transform.position.z);
                }
                if (transform.position.x < -7)
                {
                    transform.position = new Vector3(-7, transform.position.y, transform.position.z);
                }
                if (transform.position.y < -5)
                {
                    transform.position = new Vector3(transform.position.x, -5, transform.position.z);
                }
                if (transform.position.y >4)
                {
                    transform.position = new Vector3(transform.position.x, 4, transform.position.z);
                }
            }
        }
        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
