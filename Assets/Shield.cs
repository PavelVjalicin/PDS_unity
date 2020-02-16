using UnityEngine;
using System.Collections;

public class Shield : Cannon {
    private Transform buttom;
    private SpriteRenderer middle;
    private bool glow = true;
    // Use this for initialization
    void Start()
    {
        buttom = transform.FindChild("Bottom");
        middle = transform.FindChild("Middle").GetComponent < SpriteRenderer>() ;
        middle.color = new Color(1f, 1f, 1f, 0f);
    }
	// Update is called once per frame
	void Update () {
        Vector3 ea = buttom.eulerAngles;
        if(glow)
        {
            if (middle.color.a < 1)
                middle.color = new Color(1f, 1f, 1f, middle.color.a + 1f * Time.deltaTime);
            else
                glow = false;
        }
        else
        {
            if(middle.color.a > 0)
                middle.color = new Color(1f, 1f, 1f, middle.color.a - 1f * Time.deltaTime);
            else
                glow = true;
        }
        buttom.eulerAngles = new Vector3(0f, 0f, ea.z - 180 * Time.deltaTime);
        
        if (!paused)
        {
            MoveToMouse();
            PlanetHealth.shield += Time.deltaTime / 300 * 1000;
        }
	}
}
