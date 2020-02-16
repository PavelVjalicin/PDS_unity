using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MouseCheck : MonoBehaviour {
    Cannon cannon;
    private Energy energy;
    private Transform marker;
    private Platform platform;
    public bool options = false;
    public bool paused = false;
    public Text infoText;
	// Use this for initialization
	void Start () {
        marker = transform.Find("Marker");
        infoText = GameObject.Find("InfoText").GetComponent<Text>();
        infoText.color = new Color(infoText.color.r, infoText.color.g, infoText.color.b, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (infoText.color.a > 0)
        {
            infoText.color = new Color(infoText.color.r, infoText.color.g, infoText.color.b, infoText.color.a - 0.50f * Time.deltaTime);
        }
        if (cannon!=null)
        {
            Renderer mS = marker.GetComponent<Renderer>();
            marker.gameObject.SetActive(true);
            if (cannon.collision == false)
            {
                
                mS.material.color = Color.red;

            }else
            {
                mS.material.color = Color.white;
            }
            marker.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            marker.transform.position = new Vector3(marker.transform.position.x-0.01f, marker.transform.position.y+0.01f, 0);
        }
        
        if (platform != null)
        {
            Renderer mS = marker.GetComponent<Renderer>();
            marker.gameObject.SetActive(true);
            if (platform.collision == false)
            {

                mS.material.color = Color.red;

            }
            else
            {
                mS.material.color = Color.white;
            }
            marker.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            marker.transform.position = new Vector3(marker.transform.position.x - 0.01f, marker.transform.position.y + 0.01f, 0);
        }

        if(cannon == null && platform == null)
        {
            marker.gameObject.SetActive(false);
        }
        CheckMouseDown();
        GameObject go = GameObject.Find("EnergyBox");
        energy = go.GetComponent<Energy>();
       
    }
    void CheckMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cannon = null;
            float dis;
            float dis2 = 10;
            RaycastHit2D[] hitInfo = Physics2D.RaycastAll(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 1f);
            for (int i = 0; i < hitInfo.Length; i++)
            {
                if (paused == false)
                {
                    if (hitInfo[i].collider.gameObject.name == "OptionsButton")
                    {
                        paused = true;
                        UnityEngine.Object[] objects = FindObjectsOfType(typeof(GameObject));
                        foreach (GameObject go in objects)
                        {
                            go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
                        }
                        options = true;
                        GameObject instance = Instantiate(Resources.Load("OptionsMenu", typeof(GameObject))) as GameObject;

                        OptionsMenu om = instance.GetComponent<OptionsMenu>();
                        om.mouseCheck = this.gameObject;
                        return;
                    }
                
                    if (hitInfo[i].collider.gameObject.tag == "hitTest")
                    {
                        dis = Vector2.Distance(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), new Vector2(hitInfo[i].collider.gameObject.transform.parent.transform.position.x, hitInfo[i].collider.gameObject.transform.parent.transform.position.y));
                        if (dis < dis2)
                        {
                            dis2 = dis;
                            cannon = hitInfo[i].collider.gameObject.GetComponentInParent<Cannon>();
                        }


                    }
                    else if (hitInfo[i].collider.gameObject.tag == "WeaponIcon")
                    {
                        WeaponIcon icon = hitInfo[i].collider.gameObject.GetComponent<WeaponIcon>();
                        if (icon.type != "Platform")
                        {
                            if (energy.energy >= icon.cost)
                            {
                                GameObject instance = Instantiate(Resources.Load(icon.type, typeof(GameObject))) as GameObject;
                                cannon = instance.GetComponent<Cannon>();
                                cannon.type = icon.type;
                                cannon.Drag(false);
                                cannon.cost = icon.cost;
                                return;
                            }
                            else
                            {
                                infoText.color = new Color(infoText.color.r, infoText.color.g, infoText.color.b, 1f);
                            }
                        }
                        else
                        {
                            if (energy.energy >= icon.cost)
                            {
                                GameObject instance = Instantiate(Resources.Load(icon.type, typeof(GameObject))) as GameObject;
                                platform = instance.GetComponent<Platform>();
                            }
                        }
                    }
                    else if (hitInfo[i].collider.gameObject.name == "GoBack")
                    {
                        SceneManager.LoadScene(2);
                        MusicPlayer mp = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
                        mp.ChangeSong = true;
                        mp.nextSong = 0;
                    }
                    else if (hitInfo[i].collider.gameObject.name == "RestartLevel")
                    {
                        GameObject instance = Instantiate(Resources.Load("WeaponSelectPrefab", typeof(GameObject))) as GameObject;
                        WeaponSelect ws = instance.gameObject.GetComponentInChildren<WeaponSelect>();
                        ws.map = false;
                        paused = true;
                    }

                }else
                {
                    if (hitInfo[i].collider.gameObject.name == "Go")
                    {
                        if(hitInfo[i].collider.transform.parent.FindChild("WeaponSelect").FindChild("ActiveBox1").GetComponent<ActiveBox>().Type!="Empty")
                            SceneManager.LoadScene(1);
                        else
                        {
                            hitInfo[i].collider.gameObject.transform.parent.FindChild("WeaponSelect").FindChild("NoweaponsSelected").GetComponent<Text>().color = new Color(1f, 0f, 0f, 1f);
                        }

                    }
                    else if (hitInfo[i].collider.gameObject.name == "ReturnToMap")
                    {

                        SceneManager.LoadScene(2);
                        MusicPlayer mp = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
                        mp.ChangeSong = true;
                        mp.nextSong = 0;
                    }
                    else
                    {
                        for (int r = 1; r < 4; r++)
                        {
                            if (hitInfo[i].collider.gameObject.name == r.ToString())
                            {
                                if (GameVar.WeaponsUnlocked[11 + r] == true)
                                {
                                    Info info = GameObject.Find("Info").GetComponent<Info>();
                                    info.ChangeInfo((byte)(12 + r));
                                }
                            }
                        }
                        string PassiveSlotName;
                        for (int r = 1; r < 13; r++)
                        {
                            PassiveSlotName = "PassiveBox" + r;
                            if (hitInfo[i].collider.gameObject.name == PassiveSlotName)
                            {
                                PassiveBox passiveBox = hitInfo[i].collider.gameObject.GetComponent<PassiveBox>();
                                passiveBox.AddWeapon();
                                if (passiveBox.Locked == false)
                                {
                                    Info info = GameObject.Find("Info").GetComponent<Info>();
                                    info.ChangeInfo((byte)passiveBox.num);
                                }
                            }
                        }
                        for (int r = 1; r < 5; r++)
                        {
                            PassiveSlotName = "ActiveBox" + r;
                            if (hitInfo[i].collider.gameObject.name == PassiveSlotName)
                            {
                                ActiveBox activeBox = hitInfo[i].collider.gameObject.GetComponent<ActiveBox>();
                                activeBox.ResetIcons();
                            }
                        }
                    }
                }

            }
            if (cannon != null) 
                cannon.Drag();

        }
        if (Input.GetMouseButtonUp(0))
        {
            if (platform == null)
            {
                if (cannon != null)
                {
                    RaycastHit2D[] hitInfo = Physics2D.RaycastAll(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 1f);
                    for (int i = 0; i < hitInfo.Length; i++)
                    {
                        if (hitInfo[i].collider.gameObject.tag == "TrashCan")
                        {
                            if (cannon.emptyCannon != null)
                            {
                                energy.energy += Mathf.Round(cannon.cost * 0.66f);
                                if (cannon.type == "Battery")
                                {
                                    energy.maxEnergy *= 0.5f;
                                }
                                if (cannon.type == "Generator")
                                {
                                    energy.energyAmount -= 1;
                                }
                                energy.UpdateUI();
                            }
                            Destroy(cannon.emptyCannon, 0f);
                            Destroy(cannon.gameObject, 0f); ;
                            return;
                        }
                    }


                    StartCoroutine(MouseRelease(cannon));
                }


            }
            else
            {
                platform.Drop(energy);
                platform = null;
            }
                
            

        }
    }
    IEnumerator MouseRelease(Cannon c)
    {
        cannon = null;
        c.active = false;
        yield return new WaitForSeconds(0.1f);
        if (c != null)
            c.active=true;
            c.Drop(energy);
        c = null;
        
    }
    IEnumerator MousePress(Cannon c)
    {
        cannon = null;
        c.active = false;
        yield return new WaitForSeconds(0.1f);
        if (c != null)
            c.active = true;
        c.Drag(false);
        c = null;

    }
    void notEmpty()
    {
        Debug.Log("Yes");
    }
}
