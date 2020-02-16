using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MouseCheckMap : MonoBehaviour {
    public GameObject map;
    bool weaponSelect = false;
    public bool options = false;
    private Info info;
	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hitInfo = Physics2D.RaycastAll(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 1f);
            for (var r = 0; r < hitInfo.Length; r++)
            {
                if (hitInfo[r].collider.gameObject.name == "OptionButton")

                {
                    Map mapScript = map.GetComponent<Map>();
                    if (options == false && mapScript.active == true)
                    {
                        
                        options = true;
                        
                        mapScript.active = false;
                        GameObject instance = Instantiate(Resources.Load("OptionsMenu", typeof(GameObject))) as GameObject;

                        OptionsMenu om = instance.GetComponent<OptionsMenu>();
                        om.map = true;
                        om.mouseCheck = this.gameObject;
                        return;
                    }
                }
            }
            for (var r = 0; r < hitInfo.Length; r++)
            {
                if (hitInfo[r].collider != null)
                {
                    
                        if (hitInfo[r].collider.gameObject.name == "HitTest")
                    {
                        Map mapScript = map.GetComponent<Map>();
                        if (mapScript.active == true)
                        {
                            Planet planet = hitInfo[r].collider.gameObject.GetComponentInParent<Planet>();
                            

                            if (planet.unlocked == true)
                            {
                                GameObject instance = Instantiate(Resources.Load("WeaponSelectPrefab", typeof(GameObject))) as GameObject;
                                mapScript.active = false;
                                GameVar gv = GameObject.Find("GameVar").GetComponent<GameVar>();
                                gv.Level = planet.level;
                                gv.planetType = (byte)planet.planetType;
                            }
                        }

                    }
                    else if (hitInfo[r].collider.gameObject.name == "Go")
                    {
                        if (hitInfo[r].collider.transform.parent.FindChild("WeaponSelect").FindChild("ActiveBox1").GetComponent<ActiveBox>().Type != "Empty")
                        {
                            SceneManager.LoadScene(1);
                            MusicPlayer mp = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
                            mp.ChangeSong = true;
                            mp.nextSong = Random.Range(1, 4);
                        }
                        else
                        {
                            hitInfo[r].collider.gameObject.transform.parent.FindChild("WeaponSelect").FindChild("NoweaponsSelected").GetComponent<Text>().color = new Color(1f, 0f, 0f, 1f);
                        }
                    }
                    else if (hitInfo[r].collider.gameObject.name == "ReturnToMap")
                    {

                        Destroy(hitInfo[r].collider.gameObject.transform.parent.gameObject);
                        Map mapScript = map.GetComponent<Map>();
                        mapScript.active = true;
                        return;
                    }
                    else if (hitInfo[r].collider.gameObject.name == "UnLockAll")
                    {
                        Map mapScript = map.GetComponent<Map>();
                        if (mapScript.active == true)
                        {
                            GameObject.Find("GameVar").GetComponent<GameVar>().UnlockAll();
                            SceneManager.LoadScene(2);
                        }
                    }
                    else
                    {
                        for(int i = 1;i<4;i++)
                        {
                            if (hitInfo[r].collider.gameObject.name == i.ToString())
                            {
                                if (GameVar.WeaponsUnlocked[11 + i] == true)
                                {
                                    info = GameObject.Find("Info").GetComponent<Info>();
                                    info.ChangeInfo((byte)(12 + i));
                                }
                            }
                        }
                        string PassiveSlotName;
                        for (int i = 1; i < 13; i++)
                        {
                            PassiveSlotName = "PassiveBox" + i;
                            if (hitInfo[r].collider.gameObject.name == PassiveSlotName)
                            {
                                PassiveBox passiveBox = hitInfo[r].collider.gameObject.GetComponent<PassiveBox>();
                                passiveBox.AddWeapon();
                                if (passiveBox.Locked == false)
                                {
                                    info = GameObject.Find("Info").GetComponent<Info>();
                                    info.ChangeInfo((byte)passiveBox.num);
                                }
                            }
                        }
                        for (int i = 1; i < 5; i++)
                        {
                            PassiveSlotName = "ActiveBox" + i;
                            if (hitInfo[r].collider.gameObject.name == PassiveSlotName)
                            {
                                ActiveBox activeBox = hitInfo[r].collider.gameObject.GetComponent<ActiveBox>();
                                activeBox.ResetIcons();
                            }
                        }
                    }

                }
            }
        }
    }
}
