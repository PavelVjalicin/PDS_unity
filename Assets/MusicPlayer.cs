using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
    public AudioSource sound;
    public AudioClip[] audioClip;
    public int nextSong;
    public bool ChangeSong = false;
    private static bool s = false;
	// Use this for initialization
	void Start () {
        
        if(s == true)
        {
            Destroy(gameObject);
        }
        s = true;
        sound = gameObject.GetComponent<AudioSource>();
        if (GameVar.music == false)
        {
            sound.mute = true;
        }
        DontDestroyOnLoad(transform.gameObject);
    }
	void Update()
    {
        if(ChangeSong == true)
        {
            
            sound.volume -= 0.01f;
            if(sound.volume <= 0)
            {
                sound.clip = audioClip[nextSong];
                sound.Play();
                ChangeSong = false;
            }

        }
        else
        {
            if (sound.volume < 1)
                sound.volume += 0.01f;

        }
    }
	// Update is called once per frame
	public void MusicOff()
    {
        this.sound.mute = true;
    }
    public void MusicOn()
    {
        this.sound.mute = false;
    }
}
