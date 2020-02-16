using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlanetHealth : MonoBehaviour {
    public static float planetHealth = 250;
    public static float shield;
    private Transform green;
    private Transform blue;
    private bool levelComplete;
    public Game game;
	// Use this for initialization
	void Start () {
        planetHealth = 1000;
        shield = 0;
        Canvas myCanvasObject = GetComponent<Canvas>();
        myCanvasObject.sortingLayerName = "ui";
        Transform go = transform.Find("Green");
        green = go.GetComponent<Transform>();
        go = transform.Find("Blue");
        blue = go.GetComponent<Transform>();
        int height = Camera.main.pixelHeight/2;
        int width = Camera.main.pixelWidth;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3((float)width, (float)height, 0));

        this.transform.position = new Vector3(worldPos.x-1.1f , worldPos.y+0.2f , worldPos.z);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
    }
	
	// Update is called once per frame
    
	void Update () {
        if (shield < 1000)
        {
            blue.localScale = new Vector3(shield / 1000f, 1f, 0f);
        }
        else
        {
            shield = 1000;
            blue.localScale = new Vector3(shield / 1000f, 1f, 0f);
        }
        if(planetHealth < 0)
        {
            planetHealth = 0;
            if (levelComplete == false)
            {
                game.levelComplete = true;
                GameObject instance = Instantiate(Resources.Load("LevelComplete", typeof(GameObject))) as GameObject;
                instance.transform.FindChild("TextScore").GetComponent<Text>().text = "Score: 0";
                instance.transform.FindChild("TextMain").GetComponent<Text>().text = "Mission Failed";
                levelComplete = true;
            }
        }
        green.localScale = new Vector3(planetHealth / 1000f, 1f, 0f);
    }
}
