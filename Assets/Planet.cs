using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    public int level;
    public int planetType;
    public GameObject planetLocked;
    public bool unlocked = false;
    public GameObject complete;
    public bool completeLevel;
    public Text text;
    // Use this for initialization
    void Start()
    {
        completeLevel = true;
        text.text = "";
        
        if(GameVar.LevelsComplete[level-1] == false)
        {
            GameObject go = gameObject.transform.FindChild("Complete").gameObject;
            go.SetActive(false);
            completeLevel = false;
        }
        else
        {
            //Debug.Log(GameVar.Score.Length);
            text.text = "Score: "+GameVar.Score[level-1].ToString();
        }
        if(GameVar.LevelsUnlocked[level-1] == true)
        {
            GameObject go = gameObject.transform.FindChild("Planet" + planetType).gameObject;
            go.SetActive(true);
            planetLocked.SetActive(false);
            unlocked = true;
        }
        if (completeLevel == false)
            if (level == 3 || level == 1 || level == 2 || level == 4 || level == 5 || level == 13 || level == 14 || level == 18 || level == 9 || level == 16 || level == 17 || level ==19 || level == 21 || level == 18 || level == 24)
            {
                text.text = "New Weapon Unlock!";
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
