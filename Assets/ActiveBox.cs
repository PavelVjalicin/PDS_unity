using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ActiveBox : MonoBehaviour {
    public ActiveBox ActiveBox1;
    public ActiveBox ActiveBox2;
    public ActiveBox ActiveBox3;
    public ActiveBox ActiveBox4;
    public int Num;
    public string Type = "Empty";
    public GameObject HomingLaser;
    public GameObject PiercingCannon;
    public GameObject Shield;
    public GameObject EnergyPrison;
    public GameObject FlakCannon;
    public GameObject ChargingLaser;
    public GameObject RocketLauncher;
    public GameObject AOECannon;
    public GameObject HomingCannon;
    public GameObject LaserCannon;
    public GameObject SpreadCannon;
    public GameObject SniperCannon;
    private GameVar gameVar;
    private GameObject[] WepArray;
    private GameObject remove;
    // Use this for initialization
    void Start () {
        remove = transform.FindChild("Remove").gameObject;
        GameObject gVar = GameObject.Find("GameVar");
        gameVar = gVar.GetComponent<GameVar>();
        WepArray = new GameObject[12] { HomingLaser, PiercingCannon, Shield, EnergyPrison, FlakCannon, ChargingLaser, RocketLauncher, AOECannon, HomingCannon, LaserCannon, SpreadCannon, SniperCannon };
        if (SceneManager.GetActiveScene().name == "Game")
        {
            
            if (Num == 1)
                Type = gameVar.Weapon1;
            if (Num == 2)
                Type = gameVar.Weapon2;
            if (Num == 3)
                Type = gameVar.Weapon3;
            if (Num == 4)
                Type = gameVar.Weapon4;
            ChangeIcon(Type);
        }
        else {

            
            if (Num == 1)
                gameVar.Weapon1 = Type;
            if (Num == 2)
                gameVar.Weapon2 = Type;
            if (Num == 3)
                gameVar.Weapon3 = Type;
            if (Num == 4)
                gameVar.Weapon4 = Type;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Type == "Empty")
            remove.SetActive(false);
        else
            remove.SetActive(true);
	}
    public void ChangeIcon(string t)
    {
        MakeEmpty();
        if(t=="HomingLaser")
        {
            HomingLaser.SetActive(true);
            Type = "HomingLaser";
        }
        else if (t == "PiercingCannon")
        {
            PiercingCannon.SetActive(true);
            Type = "PiercingCannon";
        }
        else if (t == "Shield")
        {
            Shield.SetActive(true);
            Type = "Shield";
        }
        else if (t == "EnergyPrison")
        {
            EnergyPrison.SetActive(true);
            Type = "EnergyPrison";
        }
        else if (t == "FlakCannon")
        {
            FlakCannon.SetActive(true);
            Type = "FlakCannon";
        }
    else if (t == "ChargingLaser")
        {
            ChargingLaser.SetActive(true);
            Type = "ChargingLaser";
        }
    else if (t == "RocketLauncher")
        {
            RocketLauncher.SetActive(true);
            Type = "RocketLauncher";
        }
    else if (t == "AOECannon")
        {
            AOECannon.SetActive(true);
            Type = "AOECannon";
        }
    else if (t == "HomingCannon")
        {
            HomingCannon.SetActive(true);
            Type = "HomingCannon";
        }
    else if (t == "LaserCannon")
        {
            LaserCannon.SetActive(true);
            Type = "LaserCannon";
        }
    else if (t == "SpreadCannon")
        {
            SpreadCannon.SetActive(true);
            Type = "SpreadCannon";
        }
    else if (t == "SniperCannon")
        {
            SniperCannon.SetActive(true);
            Type = "SniperCannon";
        }
        else if (t == "Empty")
        {
            MakeEmpty();
        }
        if (Num == 1)
            gameVar.Weapon1 = Type;
        if (Num == 2)
            gameVar.Weapon2 = Type;
        if (Num == 3)
            gameVar.Weapon3 = Type;
        if (Num == 4)
            gameVar.Weapon4 = Type;
    }
    public void ResetIcons()
    {
        
        if (Num == 1)
        {
            
            ChangeIcon(ActiveBox2.Type);
            ActiveBox2.ChangeIcon(ActiveBox3.Type);
            ActiveBox3.ChangeIcon(ActiveBox4.Type);
            ActiveBox4.MakeEmpty();
        }
        else if(Num == 2)
        {
            ActiveBox2.ChangeIcon(ActiveBox3.Type);
            ActiveBox3.ChangeIcon(ActiveBox4.Type);
            ActiveBox4.MakeEmpty();
        }
        else if(Num == 3)
        {
            ActiveBox3.ChangeIcon(ActiveBox4.Type);
            ActiveBox4.MakeEmpty();
        }
        else if (Num == 4)
        {
            MakeEmpty();
        }
    }
    private void MakeEmpty()
    {
        for (int i = 0; i < WepArray.Length;i++)
        {
            WepArray[i].SetActive(false);
        }
        Type = "Empty";
        if (Num == 4)
            gameVar.Weapon4 = Type;
    }
}
