using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class WeaponIcon : MonoBehaviour {
    public int position = 1;
    public string type;
    public int cost;
    private Text t;
    private Transform box;
    private GameObject green;
    private Energy energy;
	// Use this for initialization
	void Start () {
        GameObject gameVar = GameObject.Find("GameVar");
        if (gameVar != null)
        {
            GameVar gVar = gameVar.GetComponent<GameVar>();
            if (position == 1)
            {
                type = gVar.Weapon1;
            }
            if (position == 2)
            {
                type = gVar.Weapon2;
            }
            if (position == 3)
            {
                type = gVar.Weapon3;
            }
            if (position == 4)
            {
                type = gVar.Weapon4;
            }
            if (position == 5)
            {
                if (GameVar.WeaponsUnlocked[12] == false)
                    Destroy(this.gameObject);
            }
            if (position == 6)
            {
                if (GameVar.WeaponsUnlocked[13] == false)
                    Destroy(this.gameObject);
            }
            if (position == 7)
            {
                if (GameVar.WeaponsUnlocked[14] == false)
                    Destroy(this.gameObject);
            }
            if (type == "Empty")
                Destroy(this.gameObject);
        }
        Canvas myCanvasObject = GetComponent<Canvas>();
        myCanvasObject.sortingLayerName = "ui";
        cost = GetCost(type);
        int height = Camera.main.pixelHeight;
        int width = 0;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3((float)width, (float)height, 0));

        if (position < 5)
        {
            this.transform.position = new Vector3(worldPos.x + 0.35f + .7f * position - .7f, worldPos.y - 0.35f, worldPos.z);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        }
        else{
            this.transform.position = new Vector3(worldPos.x + 0.35f + .7f * (position-4) - .7f, worldPos.y - 0.35f-.7f, worldPos.z);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        }

        Transform text = transform.Find("Cost");
        t = text.GetComponent<Text>();
        
        t.text = cost.ToString();


        Transform go = transform.Find("Box");
        box = go.GetComponent<Transform>();

        go = transform.Find("SpriteGreen");
        green = go.gameObject;
        GameObject xo = GameObject.Find("EnergyBox");
        energy = xo.GetComponent<Energy>();
        go = transform.Find(type);
        if (type != "Empty")
        {
            go.gameObject.SetActive(true);
        }
    }
	

    int GetCost(string t)
    {
        if (t == "HomingLaser") return 40;
        if (t == "PiercingCannon") return 90;
        if (t == "SpreadCannon") return 120;
        if (t == "SniperCannon") return 70;
        if (t == "LaserCannon") return 75;
        if (t == "HomingCannon") return 80;
        if (t == "AOECannon") return 150;
        if (t == "RocketLauncher") return 150;
        if (t == "ChargingLaser") return 130;
        if (t == "EnergyPrison") return 45;
        if (t == "FlakCannon") return 100;
        if (t == "Shield") return 90;
        if (t == "Battery") return 50;
        if (t == "Generator") return 50;
        if (t == "Platform") return 25;
        return 0;
    }
    void Update()
    {


        if (energy.energy < cost)
        {
            green.SetActive(false);
            box.transform.localScale = new Vector3(energy.energy / cost, box.localScale.y, 0f);
        }
        else
        {
            green.SetActive(true);
            box.transform.localScale = new Vector3(1f, box.localScale.y, 0f);
        }
    }
}
