using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OptionsMenu : MonoBehaviour {
    public GameObject mouseCheck;
    public bool map = false;
    GameObject[] obj;
    // Use this for initialization
    void Start () {
        ChangeOpacity();
        obj = new GameObject[] { GameObject.Find("ReturnToMap"), GameObject.Find("ResumeGame"), GameObject.Find("RestartLevel") };


        if (map == false)
        {
            RectTransform rt = this.GetComponent<RectTransform>();
            rt.localScale = rt.localScale * 0.7f;
            GameObject cam = GameObject.Find("Main Camera");
            this.transform.position = new Vector3(this.transform.position.x, cam.transform.position.y);
            SpriteRenderer[] sr = transform.GetComponentsInChildren<SpriteRenderer>();
            for(var i =0; i < sr.Length;i++)
            {
                sr[i].sortingLayerName = "Cannon";
                sr[i].sortingOrder = 10;
            }
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 9;
            Canvas myCanvasObject = GetComponent<Canvas>();
            myCanvasObject.sortingLayerName = "Cannon";
            myCanvasObject.sortingOrder = 10;
           
            for(var i =0;i<obj.Length;i++)
            {
                obj[i].SetActive(true);
            }
            cam = GameObject.Find("Back");
            cam.SetActive(false);
        }
        else
        {
            
            for (var i = 0; i < obj.Length; i++)
            {

                
                obj[i].SetActive(false);
                
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit2D[] hitInfo = Physics2D.RaycastAll(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 1f);
            for (var r = 0; r < hitInfo.Length; r++)
            {

                if (hitInfo[r].collider != null)
                {
                    if (hitInfo[r].collider.gameObject.name == "ResumeGame")
                    {
                        goBack();
                    }
                    if (hitInfo[r].collider.gameObject.name == "RestartLevel")
                    {
                        GameObject instance = Instantiate(Resources.Load("WeaponSelectPrefab", typeof(GameObject))) as GameObject;
                        WeaponSelect ws = instance.gameObject.GetComponentInChildren<WeaponSelect>();
                        ws.map = false;
                        Destroy(gameObject);
                        //SceneManager.LoadScene(1);
                    }
                    if (hitInfo[r].collider.gameObject.name == "ReturnToMap")
                    {
                        SceneManager.LoadScene(2);
                        MusicPlayer mp = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
                        mp.ChangeSong = true;
                        mp.nextSong = 0;
                    }
                    if (hitInfo[r].collider.gameObject.name == "gOn")

                    {
                        GameVar.graphics = true;
                    }
                    else if (hitInfo[r].collider.gameObject.name == "gOff")

                    {
                        GameVar.graphics = false;
                        
                    }
                    if (hitInfo[r].collider.gameObject.name == "sOn")

                    {
                        GameVar.sound = true;
                        GameVar.SaveOptions();
                    }
                    if (hitInfo[r].collider.gameObject.name == "sOff")

                    {
                        GameVar.sound = false;
                        GameVar.SaveOptions();
                    }
                    if (hitInfo[r].collider.gameObject.name == "mOn")

                    {
                        MusicPlayer mp = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
                        mp.MusicOn();
                        GameVar.music = true;
                        GameVar.SaveOptions();
                    }
                    if (hitInfo[r].collider.gameObject.name == "mOff")

                    {
                        MusicPlayer mp = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
                        mp.MusicOff();
                        GameVar.music = false;
                        GameVar.SaveOptions();
                    }
                    if (hitInfo[r].collider.gameObject.name == "Back")

                    {
                        goBack();
                        
                    }
                }
                ChangeOpacity();
            }
        }

    }
    void goBack()
    {
        Destroy(this.gameObject);
        if (map)
        {
            MouseCheckMap mc = mouseCheck.GetComponent<MouseCheckMap>();
            mc.options = false;
            Map mapScript = mc.map.GetComponent<Map>();
            mapScript.active = true;
        }
        else
        {
            UnityEngine.Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                go.SendMessage("Resume", SendMessageOptions.DontRequireReceiver);
            }
            MouseCheck mc = mouseCheck.GetComponent<MouseCheck>();
            mc.paused = false;
        }
    }
    void ChangeOpacity()
    {
        GameObject o;
        if (GameVar.graphics == false)
        {
            o = transform.FindChild("gOff").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
            o = transform.FindChild("gOn").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            o = transform.FindChild("gOff").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, .5f);
            o = transform.FindChild("gOn").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
        }
        if (GameVar.music == false)
        {
            o = transform.FindChild("mOff").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
            o = transform.FindChild("mOn").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            o = transform.FindChild("mOff").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, .5f);
            o = transform.FindChild("mOn").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
        }
        if (GameVar.sound == false)
        {
            o = transform.FindChild("sOff").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
            o = transform.FindChild("sOn").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            o = transform.FindChild("sOff").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, .5f);
            o = transform.FindChild("sOn").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
