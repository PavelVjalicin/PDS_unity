using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SaveLoad : MonoBehaviour {
    private bool[] LevelsComplete;
    private int[] Score;
    public byte slot;
    private string data;
    private bool ng = false;
    private Collider2D reset;
    private Collider2D LoadCol;
    private Collider2D NewGame;
    public static bool resetScreen = false;
    public static byte resetSlot;
    // Use this for initialization
    void Start () {
        if(slot == 1)
        {
            data = "/Data.pds";
        }
        if (slot == 2)
        {
            data = "/Data2.pds";
        }
        if (slot == 3)
        {
            data = "/Data3.pds";
        }
        Load();
        if (ng == false)
        {
            Collider2D[] col = GetComponentsInChildren<Collider2D>();
            for(int i = 0;i<col.Length;i++)
            {
                if(col[i].gameObject.name == "Load")
                {
                    LoadCol = col[i];
                }
                if (col[i].gameObject.name == "Reset")
                {
                    reset = col[i];
                }
            }
            int lc = 0;
            for (byte i = 0; i < LevelsComplete.Length; i++)
            {
                if (LevelsComplete[i] == true)
                    lc++;
            }
            int s = 0;
            for (byte i = 0; i < Score.Length; i++)
            {
                s += Score[i];
            }
            GetComponentInChildren<Text>().text = "Slot " + slot.ToString() + "\n\n" + "Levels: " + lc.ToString() + "/28\n\n" + "Score: " + s.ToString();
        }
        else
        {
            
            transform.FindChild("SaveLoadSprite").gameObject.SetActive(false);
            transform.FindChild("NewGameSprite").gameObject.SetActive(true);
            transform.FindChild("NewGameSprite").FindChild("Slot").GetComponent<Text>().text = "Slot " + slot;
            Collider2D[] col = GetComponentsInChildren<Collider2D>();
            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].gameObject.name == "NewGame")
                {
                    NewGame = col[i];
                }
            }
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hitInfo = Physics2D.RaycastAll(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 1f);
            if (resetScreen == false)
                for (int i = 0; i < hitInfo.Length; i++)
                {
                    if (hitInfo[i].collider == LoadCol)
                    {
                        GameVar.data = data;
                        SceneManager.LoadScene(2);
                        return;
                    }
                    if (hitInfo[i].collider == NewGame)
                    {

                        GameVar.data = data;
                        SceneManager.LoadScene(2);
                        return;
                    }
                    if (hitInfo[i].collider == reset)
                    {
                        GameObject.Find("ResetScreen").transform.position = new Vector3();
                        resetSlot = this.slot;
                        resetScreen = true;
                    }
                }
            else
            {
                if(resetSlot == slot)
                for (int i = 0; i < hitInfo.Length; i++)
                {
                    if (hitInfo[i].collider.gameObject.name == "Yes")
                    {
                            GameObject.Find("ResetScreen").transform.position = new Vector3(10f, 0, 0);
                            resetScreen = false;
                        ResetSave();
                    }
                    if (hitInfo[i].collider.gameObject.name == "No")
                    {
                        GameObject.Find("ResetScreen").transform.position = new Vector3(10f, 0, 0);
                        resetScreen = false;
                    }
                }
            }
        }

    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + data))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + data, FileMode.Open);
            saveFile sf = (saveFile)bf.Deserialize(file);
            file.Close();
            LevelsComplete = sf.LevelsComplete;
            if (sf.Score != null)
                Score = sf.Score;
            else
                Score = new int[28];
        }
        else
        {
            ng = true;
        }
    }
    public void ResetSave()
    {
        transform.FindChild("SaveLoadSprite").gameObject.SetActive(false);
        transform.FindChild("NewGameSprite").gameObject.SetActive(true);
        transform.FindChild("NewGameSprite").FindChild("Slot").GetComponent<Text>().text = "Slot " + slot;
        Collider2D[] col = GetComponentsInChildren<Collider2D>();
        for (int z = 0; z < col.Length; z++)
        {
            if (col[z].gameObject.name == "NewGame")
            {
                NewGame = col[z];
            }
        }
        File.Delete(Application.persistentDataPath + data);
        LoadCol = null;
        reset = null;
    }
}
