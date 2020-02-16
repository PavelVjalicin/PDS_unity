using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RocketLauncherSystem : MonoBehaviour {
    // Use this for initialization
    float timer;
    Vector2 TopRight;
    Vector2 BottomLeft;
    public Enemy Target;
    public static int[] priority; //{ "Carrier","SuperHeavy","HeavyShip","InvisibleShip","Trickster","Teleporter","Bomber","MediumShip","LightShip","ClusterUnit"};
    public static List<Enemy> e;
	void Start () {
        priority = new int[15];
        e = new List<Enemy>();
        int width = Screen.width;
        int height = Screen.height;
        Vector3 vec3 = Camera.main.ScreenToWorldPoint(new Vector3((float)width, (float)height, 0f));
        TopRight = new Vector2(vec3.x,vec3.y);
        vec3 = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        BottomLeft = new Vector2(vec3.x, vec3.y);
    }
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            
            getTarget();
            timer = 0;
        }
	}
    void getTarget()
    {
        if (e.Count > 0)
            Target = e[0];
        else
            Target = null;
    }
    public static void addPriority(int num)
    {
        if (num >= 0)
        {
            for (int i = num; i < 15; i++)
            {
                priority[i]++;
                
            }
            return;
        }
    }
    public static void removePriority(int num)
    {
        if (num >= 0)
        {
            for (int i = num; i < 15; i++)
            {
                priority[i]--;

            }
            return;
        }
    }
}
