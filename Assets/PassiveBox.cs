using UnityEngine;
using System.Collections;


public class PassiveBox : MonoBehaviour {
    public string Type;
    public ActiveBox ActiveBox1;
    public ActiveBox ActiveBox2;
    public ActiveBox ActiveBox3;
    public ActiveBox ActiveBox4;
    public bool Locked;
    public int num;
    public bool unavailable = false;
    private GameObject add;
    private SpriteRenderer sp;
    // Use this for initialization
    void Start () {
        sp = transform.FindChild("Sprite").gameObject.GetComponent<SpriteRenderer>();
        add = transform.FindChild("Add").gameObject;
        int[] BlockedWeapons = new int[15];
        GameObject g = GameObject.Find("GameVar");
        GameVar gv = g.GetComponent<GameVar>();
        if(gv.Level == 11)
        {
            BlockedWeapons = new int[] {1,2,4,5,6,7,8,9,10,11,12};
        }
        else if(gv.Level == 15)
        {
            BlockedWeapons = new int[] {1 };
        }
        else if (gv.Level == 16)
        {
            BlockedWeapons = new int[] { 3 };
        }
        else if (gv.Level == 20)
        {
            BlockedWeapons = new int[] { 3,1,2,4,6,8,9,10,5,12 };
        }
        else if (gv.Level == 21)
        {
            BlockedWeapons = new int[] {2,4,8,9 };
        }
        else if (gv.Level == 22)
        {
            BlockedWeapons = new int[] {1,6,9,7};
        }
        else if (gv.Level == 23)
        {
            BlockedWeapons = new int[] {8};
        }
        else if (gv.Level == 24)
        {
            BlockedWeapons = new int[] {1,2,4,11,6,8,7,5};
        }
        else if (gv.Level == 28)
        {
            BlockedWeapons = new int[] { 10};
        }
        for (int i = 0; i < BlockedWeapons.Length; i++)
        {
            if (BlockedWeapons[i] == num)
            {
                unavailable = true;
            }
        }
        if (unavailable == false)
        {
            if (GameVar.WeaponsUnlocked[num - 1] == false)
            {
                Locked = true;
                transform.FindChild("Sprite").gameObject.SetActive(false);
                transform.FindChild("Locked").gameObject.SetActive(true);
            }
        }
        else
        {
            Locked = true;
            transform.FindChild("Sprite").gameObject.SetActive(false);
            transform.FindChild("Locked").gameObject.SetActive(false);
            transform.FindChild("UnAvailable").gameObject.SetActive(true);
        }

    }
    void Update()
    {
        if (ActiveBox1.Type != this.Type && ActiveBox2.Type != this.Type && ActiveBox3.Type != this.Type && ActiveBox4.Type != this.Type && Locked == false && unavailable == false)
        {

            add.SetActive(true);
            sp.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            sp.color = new Color(1f, 1f, 1f, 0.5f);
            add.SetActive(false);
        }
    }
    public void AddWeapon()
    {
        if (Locked == false)
        {
            ActiveBox curAB;
            for (int i = 1; i < 5; i++)
            {
                if (i == 1)
                    curAB = ActiveBox1;
                else if (i == 2)
                    curAB = ActiveBox2;
                else if (i == 3)
                    curAB = ActiveBox3;
                else
                    curAB = ActiveBox4;
                if (curAB.Type == Type)
                    return;
            }
            for (int i = 1; i < 5; i++)
            {
                if (i == 1)
                    curAB = ActiveBox1;
                else if (i == 2)
                    curAB = ActiveBox2;
                else if (i == 3)
                    curAB = ActiveBox3;
                else
                    curAB = ActiveBox4;
                if (curAB.Type == "Empty")
                {
                    curAB.ChangeIcon(Type);
                    return;
                }
            }
        }
    }
	// Update is called once per frame
}
