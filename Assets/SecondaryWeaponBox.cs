using UnityEngine;
using System.Collections;

public class SecondaryWeaponBox : MonoBehaviour {
    public int num;
    public GameObject locked;
    public GameObject unavailable;
    public bool unav;
    // Use this for initialization
    void Start()
    {
        if (unav == false)
        {
            if (GameVar.WeaponsUnlocked[num - 1] == false)
            {
                locked.SetActive(true);
                gameObject.transform.FindChild("Sprite").gameObject.SetActive(false);
            }
        }

    }
	// Update is called once per frame
}
