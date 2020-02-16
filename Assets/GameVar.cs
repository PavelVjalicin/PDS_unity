using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameVar : MonoBehaviour {

    public string Weapon1;
    public string Weapon2;
    public string Weapon3;
    public string Weapon4;
    public static bool[] WeaponsUnlocked = new bool[15];
    public static bool[] LevelsUnlocked = new bool[28] ;
    public static bool[] LevelsComplete = new bool[28];
    public static int[] Score = new int[28];
    public byte planetType;
    public int Level;
    public static bool sound = true;
    public static bool music = true;
    public static bool graphics = true;
    private static bool gv = false;
    public static string data;
    // Update is called once per frame
    void Start()
    {
       
       
    }
    void Awake()
    {
        if (gv == true)
            Destroy(gameObject);
        else
            LoadOptions();
        gv = true;
        Load();
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Text ScoreGo = GameObject.Find("Score").GetComponent<Text>();
            if (ScoreGo != null)
            {
                int totalScore = 0;
                for (int i = 0; i < Score.Length; i++)
                {
                    totalScore += Score[i];
                }
                ScoreGo.text = "Score: " + totalScore;
            }
        }
        //UnlockAll();
        LevelsUnlocked[0] = true;
        WeaponsUnlocked[0] = true;
        //Save();
        DontDestroyOnLoad(transform.gameObject);
    }
    public void UnlockAll()
    {
        for(var i =0; i < LevelsUnlocked.Length;i++)
        {
            LevelsUnlocked[i] = true;
        }
        for(var o = 0; o < WeaponsUnlocked.Length;o++)
        {
            WeaponsUnlocked[o] = true;
        }
    }
    public static void Save()
    {
        
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + data); //you can call it anything you want

        saveFile sf = new saveFile();
        sf.LevelsComplete = LevelsComplete;
        sf.LevelsUnlocked = LevelsUnlocked;
        sf.WeaponsUnlocked = WeaponsUnlocked;
        sf.Score = Score;
        bf.Serialize(file, sf);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + data))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + data, FileMode.Open);
            saveFile sf = (saveFile)bf.Deserialize(file);
            file.Close();
            LevelsUnlocked = sf.LevelsUnlocked;
            LevelsComplete = sf.LevelsComplete;
            WeaponsUnlocked = sf.WeaponsUnlocked;
            if (sf.Score != null)
                Score = sf.Score;
            else
                Score = new int[28];
        }
    }
    public static void SaveOptions()
    {

        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/Options.pds"); //you can call it anything you want

        optionsFile sf = new optionsFile();
        sf.sound = sound;
        sf.music = music;
        bf.Serialize(file, sf);
        file.Close();
    }
    public static void LoadOptions()
    {
        if (File.Exists(Application.persistentDataPath + "/Options.pds"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Options.pds", FileMode.Open);
            optionsFile sf = (optionsFile)bf.Deserialize(file);
            file.Close();
            music = sf.music;
            sound = sf.sound;
        }
    }
}
[System.Serializable]
class saveFile
{
    public int[] Score;
    public bool[] WeaponsUnlocked;
    public bool[] LevelsUnlocked;
    public bool[] LevelsComplete;
}
[System.Serializable]
class optionsFile
{
    public bool sound;
    public bool music;
}