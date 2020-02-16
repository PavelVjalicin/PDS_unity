using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Info : MonoBehaviour {
    public GameObject[] Cannons;
    public Text txt;
    private string[] infoString = { "Name: Tiny Laser\nEnergyCost: 40\n\nBasic and Cheap weapon\n\nEffective against weak fast targets",
"Name: P. Cannon\nEnergy Cost: 90\n\nStreight Shooting Cannon\n\nEffective against strong single targets",
"Name: Machine Gun\nEnergy Cost: 75\n\nShoots bullets towards your finger\n\nA very flexible weapon",
"Name: Annihilator\nEnergy Cost: 80\n\nLow range burst weapon\n\nQuickly destroys any slow moving target",
"Name: P. Laser\nEnergy Cost: 75\n\nShoots a laser beam that goes through enemies\n\n",
"Name: Destroyer\nEnergy Cost: 80\n\nLow range burst weapon\n\nGood for killing immobile targets",
"Name: Sonic pitch\nEnergy Cost: 150\n\nDeals area damage around itself\n\nWorks great with platforms",
"Name: Hornet\nEnergy Cost: 150\n\nSeeks out and destroys biggest threat\n\nEffective against huge enemies",
"Name: Capacitor X\nEnergy Cost: 130\n\nSlowly gathers charge to destroy any enemy that gets close enough\n\n",
"Name: Flak\nEnergy Cost: 100\n\nFires slow moving projectiles towards your finget explodes dealing damage in a small area\n\n",
"Name: Energy prison\nEnergy Cost: 45\n\nCreates an electric field around target stoping any movement\n\n",
"Name: Ravelin\nEnergy Cost: 90\n\nGenerates electro-magnetic field around planet protecting it from foreign objects\n\n",
"Name: Battery\nEnergy Cost: 50\n\nDoubles your energy capacity\n\nProvides you with advanced construction options",
"Name: Generator\nEnergy Cost: 50\n\nIncreases your energy gain\n\nEssencial in beating long levels",
"Name: Platform\nEnergy Cost: 25\n\nUsed to place weapons in space\n\n" };
   
	// Use this for initialization
    void Start()
    {
        txt = transform.FindChild("MainText").GetComponent<Text>();
        transform.FindChild("MainText").gameObject.SetActive(false);
    }
    public void ChangeInfo(byte num)
    {
        transform.FindChild("InfoText").gameObject.SetActive(false);
        txt.gameObject.SetActive(true);
        txt.gameObject.SetActive(true);
        txt.text = infoString[num-1];
        for(byte i = 0;i < Cannons.Length;i++)
        {
            Cannons[i].SetActive(false);
        }
        Cannons[num - 1].SetActive(true);
    }
}
