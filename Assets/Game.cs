using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{

    public static GameObject impact;
    public static float stageLeft;
    public static float stageRight;
    public static float stageTop;
    public static float stageBottom;
    public float time = 0f;
    public float secTime = 0f;
    int[][] levelArray;
    public byte level;
    public int levelEnd;
    private byte WeaponUnlock = 0;
    private byte[] LevelUnlocks = new byte[0];
    public bool levelComplete;
    public Energy energy;
    public Image impactParticles;
    public Sprite[] planetMasks;
    public Sprite[] bigPlanetImage;
    public GameObject[] planetColliders;
    public SpriteRenderer bigPlanet;
    public byte planetType;
    private bool paused = false;
    private static bool graphics = GameVar.graphics;
    void Tutorial1()
    {
        GameObject instancet = Instantiate(Resources.Load("Tutorial1", typeof(GameObject))) as GameObject;

    }
    void Start()
    {
        TutorialClass.tutorial = false;
        TutorialClass.tutorial2 = false;
        int width = Screen.width;
        int height = Screen.height;
        Vector3 vec3 = Camera.main.ScreenToWorldPoint(new Vector3((float)width, (float)height, 0f));
        stageRight = vec3.x;
        stageTop = vec3.y;
        vec3 = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        stageLeft = vec3.x;
        stageBottom = vec3.y;
        impact = GameObject.Find("ImpactParticles");
        GameVar gv = GameObject.Find("GameVar").GetComponent<GameVar>();
        planetType = gv.planetType;
        if (planetType < 5)
        {
            impactParticles.sprite = planetMasks[planetType - 1];

            bigPlanet.sprite = bigPlanetImage[planetType - 1];
            for(int i=0;i< planetColliders.Length; i++)
            {
                if(i != planetType-1)
                {
                    Destroy(planetColliders[i]);
                }
            }
        }
        else if (planetType == 5)
        {
            bigPlanet.sprite = null;
            impactParticles.sprite = null;
            for (int i = 0; i < planetColliders.Length; i++)
            {
                Destroy(planetColliders[i]);
            }
        }
        else if (planetType > 5)
        {
            impactParticles.sprite = planetMasks[planetType - 2];

            bigPlanet.sprite = bigPlanetImage[planetType - 2];
            for (int i = 0; i < planetColliders.Length; i++)
            {
                if (i != planetType - 2)
                {
                    Destroy(planetColliders[i]);
                }
            }
        }
        level = (byte)gv.Level;
        LoadLevel(level);

    }
    void LoadLevel(int level)
    {
        byte from = 0;
        byte till = 1;
        byte interval = 2;
        byte enemy = 3;
        byte enemyX = 4;
        byte timer = 5;
        byte LightShip = 0;
        byte MediumShip = 1;
        byte HeavyShip = 2;
        byte SuperHeavy = 3;
        byte Bomber = 4;
        byte Trickster = 5;
        byte Teleporter = 6;
        byte InvisibleShip = 7;
        byte Cluster = 8;
        byte Carrier = 9;
        byte Asteroid1 = 10;
        byte Asteroid2 = 11;
        byte Asteroid3 = 12;
        byte TricksterLeft = 13;
        byte TricksterRight = 14;

        levelArray = new int[6][];

        if (level == 1)
        {
            levelArray[from] = new int[] { 60, 800, 1600 };
            levelArray[till] = new int[] { 2200, 2200, 2200 };
            levelArray[interval] = new int[] { 90, 60, 90 };
            levelArray[enemy] = new int[] { LightShip, LightShip, LightShip };
            levelArray[enemyX] = new int[] { 0, 0, 0 };

            LevelAddUnit(2400, MediumShip, 50);
            energy.energyAmount = 2;
            levelEnd = 2401;
            WeaponUnlock = 2;
            LevelUnlocks= new byte[] { 8, 3 };
            Tutorial1();
        }
        if (level == 2)//Weapons:Homing+Pierce. Energy = 50;
        {
            levelArray[from] = new int[] { 0, 1200, 2800};
            levelArray[till] = new int[] { 3800, 3800, 3800 };
            levelArray[interval] = new int[] { 80, 60, 30 };
            levelArray[enemy] = new int[] { LightShip, LightShip, LightShip };
            levelArray[enemyX] = new int[] { 0, 0, 50 };
            WeaponUnlock = 3;
            levelEnd = 3801;
            LevelUnlocks = new byte[] { 8, 5 };
        }
        if (level == 3)
        {
            levelArray[from] = new int[] { 0, 70, 840, 1610, 1610, 2500 };
            levelArray[till] = new int[] { 1610, 1610, 2500, 2500, 2500, 3800 };
            levelArray[interval] = new int[] { 140, 140, 70, 70, 70, 110 };
            levelArray[enemy] = new int[] { LightShip, LightShip, LightShip, LightShip, LightShip, MediumShip };
            levelArray[enemyX] = new int[] { 20, 80, 50, 20, 80, 50 };
            LevelAddUnit(70, LightShip, 80);
            levelEnd = 3801;
            WeaponUnlock = 13;
            LevelUnlocks = new byte[] { 1, 4 };
        }
        if (level == 4)//Weapons:3 cannons. Energy = 70;
        {
            levelArray[from] = new int[] { 0, 1300 };
            levelArray[till] = new int[] { 2000, 2000 };
            levelArray[interval] = new int[] { 30, 80 };
            levelArray[enemy] = new int[] { Asteroid1, Asteroid2 };
            levelArray[enemyX] = new int[] { 0, 0 };
            LevelAddUnit(2100, 0, 250);
            LevelAddUnit(2300, 0, 250);
            for(var i = 0;i<=10;i++)
            LevelAddUnit(2500,Asteroid1,0);
            for(var i = 0;i<=10;i++)
            LevelAddUnit(2800,Asteroid1,0);
            for(var i = 0;i<=10;i++)
            LevelAddUnit(3100,Asteroid1,0);

            energy.energy = 70;
            levelEnd = 3101;
            WeaponUnlock = 4;

            LevelUnlocks = new byte[] { 7, 3 };
        }
        if (level == 5)//4first cannons
        {
            levelArray[from] = new int[] { 0, 1400, 1900, 2600 };
            levelArray[till] = new int[] { 1900, 1900, 4800, 3200 };
            levelArray[interval] = new int[] { 60, 120, 80, 60 };
            levelArray[enemy] = new int[] { LightShip, LightShip, MediumShip, Trickster };
            levelArray[enemyX] = new int[] { 0, 0, 0, 0 };
            multipleUnits(3200, 4800, 100, TricksterLeft/*TricksterLeft*/, 20, 0, 1);
            multipleUnits(3200, 4800, 100, TricksterRight/*TricksterRight*/, 40, 0, 1);
            multipleUnits(3200, 4800, 100, TricksterLeft/*TricksterLeft*/, 60, 0, 1);
            multipleUnits(3200, 4800, 100, TricksterRight/*TricksterRight*/, 80, 0, 1);
            LevelAddUnit(4800, HeavyShip, 50);
            LevelAddUnit(4800, HeavyShip, 25);
            LevelAddUnit(4800, HeavyShip, 75);
            energy.energyAmount = 2;
            energy.energy = 100;
            levelEnd = 4801;
            LevelUnlocks = new byte[] { 7, 2, 6 };
            WeaponUnlock = 14;

        }
        if (level == 6)//4first cannons, solar panel unlocked
        {
            levelArray[from] = new int[] { 0, 0, 2500, 3500, 5000 };
            levelArray[till] = new int[] { 4500, 4500, 3500, 5000, 5400 };
            levelArray[interval] = new int[] { 90, 90, 90, 130, 60 };
            levelArray[enemy] = new int[] { LightShip, LightShip, MediumShip, HeavyShip, Bomber };
            levelArray[enemyX] = new int[] { 0, 0, 0, 0, 0 };
            energy.energy = 100;
            levelEnd = 5401;
            LevelUnlocks = new byte[] { 11, 5, 14 };

        }
        if (level == 7)
        {
            levelArray[from] = new int[] { 30, 250, 1300 };
            levelArray[till] = new int[] { 3000, 3000, 3000 };
            levelArray[interval] = new int[] { 120, 40, 40 };
            levelArray[enemy] = new int[] { MediumShip, LightShip, Trickster };
            levelArray[enemyX] = new int[] { 0, 0, 0 };
            multipleUnits(2500, 3000, 150, HeavyShip, 30, 0, 1);
            multipleUnits(2500, 3000, 150, HeavyShip, 70, 0, 1);
            multipleUnits(2650, 3150, 150, TricksterRight, 30, 0, 1);/*TricksterRight*/
            multipleUnits(2650, 3150, 150, TricksterLeft, 50, 0, 1);/*TricksterLeft*/
            multipleUnits(2650, 3150, 150, TricksterRight, 50, 0, 1);/*TricksterRight*/
            multipleUnits(2650, 3150, 150, TricksterLeft, 70, 0, 1);/*TricksterLeft*/
            energy.energyAmount = 5;
            energy.energy = 100;
            levelEnd = 3150;
            LevelAddUnit(30, MediumShip, 0);
            LevelUnlocks = new byte[] { 5, 4, 9, 12 };
        }
        if (level == 8)
        {
            levelArray[from] = new int[] { 30, 630, 850, 1150, 1400, 1400, 1800, 1800 };
            levelArray[till] = new int[] { 600, 900, 1250, 1350, 2600, 1800, 2600, 2600 };
            levelArray[interval] = new int[] { 22, 22, 22, 22, 22, 22, 32, 32 };
            levelArray[enemy] = new int[] { LightShip, LightShip, LightShip, LightShip, LightShip, LightShip, LightShip, LightShip };
            levelArray[enemyX] = new int[] { 50, 20, 80, 50, 20, 80, 80, 50 };
            energy.energy = 100;
            energy.energyAmount = 2;
            LevelUnlocks = new byte[] { 2 };
            LevelAddUnit(2800, MediumShip, 0);
            levelEnd = 2801;
            //Game.tutorialTextField.text= 'You can press "p" to pause the game';
        }
        if (level == 9)
        {
            levelArray[from] = new int[] { 30, 400, 2000 };
            levelArray[till] = new int[] { 400, 3000, 3000 };
            levelArray[interval] = new int[] { 50, 40, 75 };
            levelArray[enemy] = new int[] { LightShip, LightShip, InvisibleShip };
            levelArray[enemyX] = new int[] { 0, 0, 0 };
            multipleUnits(3000, 4000, 75, InvisibleShip, 0, 0, 2);

            LevelAddUnit(4000, HeavyShip, 50);
            LevelAddUnit(4000, HeavyShip, 35);
            LevelAddUnit(4000, HeavyShip, 65);
            LevelAddUnit(4200, HeavyShip, 50);
            LevelAddUnit(4200, HeavyShip, 35);
            LevelAddUnit(4200, HeavyShip, 65);
            LevelAddUnit(4300, HeavyShip, 50);
            LevelAddUnit(4300, HeavyShip, 35);
            LevelAddUnit(4300, HeavyShip, 65);

            energy.energy = 100;
            levelEnd = 4301;
            LevelUnlocks = new byte[] { 10, 7 };
            WeaponUnlock = 5;
            //multipleUnits(from,till,interval,unit,_x,_y,amount)
        }
        if (level == 10)
        {
            levelArray[from] = new int[] { 0, 1300, 2500, 3800, 4800 };
            levelArray[till] = new int[] { 7000, 7000, 7000, 7000, 7000 };
            levelArray[interval] = new int[] { 28, 70, 70, 70, 100 };
            levelArray[enemy] = new int[] { Asteroid1, Asteroid2, Asteroid2, Asteroid2, Asteroid3 };
            levelArray[enemyX] = new int[] { 0, 0, 0, 0, 0 };

            energy.energy = 100;
            levelEnd = 7001;
            LevelUnlocks = new byte[] { 19, 15, 9 };

        }
        if (level == 11)
        {
            levelArray[from] = new int[] { 30 };
            levelArray[till] = new int[] { 1200 };
            levelArray[interval] = new int[] { 40 };
            levelArray[enemy] = new int[] { Bomber };
            levelArray[enemyX] = new int[] { 0 };
            energy.energyAmount = 0;
            energy.maxEnergy = 210;
            energy.energy = 210;
            levelEnd = 1201;
            LevelUnlocks = new byte[] { 18 };
        }
        if (level == 12)
        {
            levelArray[from] = new int[] { 300 };
            levelArray[till] = new int[] { 1300 };
            levelArray[interval] = new int[] { 70 };
            levelArray[enemy] = new int[] { Cluster };
            levelArray[enemyX] = new int[] { 0 };
            LevelAddUnit(100, HeavyShip, 50);
            LevelAddUnit(100, HeavyShip, 20);
            LevelAddUnit(100, HeavyShip, 80);

            energy.energyAmount = 0;
            energy.maxEnergy = 400;
            energy.energy = 400;
            levelEnd = 1301;
            LevelUnlocks = new byte[] { 13, 15, 7 };

        }
        if (level == 13)
        {
            levelArray[from] = new int[] { 600, 1600, 2000, 2600 };
            levelArray[till] = new int[] { 1500, 2500, 2500, 3000 };
            levelArray[interval] = new int[] { 45, 10, 20, 18 };
            levelArray[enemy] = new int[] { LightShip, LightShip, LightShip, Teleporter };
            levelArray[enemyX] = new int[] { 0, 0, 0, 0 };

            multipleUnits(100, 1500, 100, TricksterLeft/*TricksterLeft*/, 30, 0, 1);
            multipleUnits(50, 1500, 100, TricksterRight/*TricksterRight*/, 70, 0, 1);

            energy.energyAmount = 2;
            energy.maxEnergy = 100;
            energy.energy = 100;
            levelEnd = 3001;
            WeaponUnlock = 6;

        }
        if (level == 14)
        {

            levelArray[from] = new int[] { 0, 900 };
            levelArray[till] = new int[] { 4500, 4500 };
            levelArray[interval] = new int[] { 50, 30 };
            levelArray[enemy] = new int[] { Trickster, LightShip };
            levelArray[enemyX] = new int[] { 50, 0 };

            multipleUnits(4500, 4700, 50, Asteroid3, 0, 0, 2);

            energy.energyAmount = 2;
            energy.maxEnergy = 100;
            energy.energy = 100;
            levelEnd = 4701;
            WeaponUnlock = 9;
            LevelUnlocks = new byte[] { 6, 20 };
        }
        if (level == 15)
        {
            //Enemies Asteroid1-3, LightShip, MediumShip, HeavyShip, SuperHeavy, Bomber, Trickster, Teleporter
            levelArray[from] = new int[] { 0, 800, 2200, 3700 };
            levelArray[till] = new int[] { 800, 2200, 3700, 5000 };
            levelArray[interval] = new int[] { 30, 15, 45, 15 };
            levelArray[enemy] = new int[] {LightShip, LightShip, MediumShip, Teleporter};
            levelArray[enemyX] = new int[] { 0, 0, 0, 0 };
            energy.energyAmount = 2;
            energy.maxEnergy = 100;
            energy.energy = 90;
            levelEnd = 5001;
            LevelUnlocks = new byte[] { 16, 12, 10 };
        }
        if (level == 16)
        {
            levelArray[from] =    new int[] {0, 500, 1200, 2400};
            levelArray[till] =     new int[] {3000, 1300, 3000, 3000};
            levelArray[interval] = new int[] {70, 45, 25, 45};
            levelArray[enemy] =    new int[] {MediumShip, LightShip, LightShip, Teleporter};
            levelArray[enemyX] =   new int[] {50, 0, 0, 0};

            multipleUnits(3200, 4300, 40, TricksterLeft, 0, 0, 2);
            multipleUnits(3200, 4300, 40, TricksterRight, 0, 0, 2);

            energy.energyAmount = 2;
            energy.maxEnergy = 200;
            energy.energy = 200;
            levelEnd = 4301;
            WeaponUnlock = 15;
            LevelUnlocks = new byte[] {17};
        }
        if (level == 17)
        {
            levelArray[from] =     new int[] {0, 1500, 3100, 4000};
            levelArray[till] =     new int[] {1500, 3000, 5000, 5000};
            levelArray[interval] = new int[] {45, 90, 120, 90};
            levelArray[enemy] =    new int[] {LightShip, Cluster, HeavyShip, InvisibleShip};
            levelArray[enemyX] =   new int[] {0, 0, 0, 0};

            energy.energyAmount = 2;
            energy.maxEnergy = 200;
            energy.energy = 200;
            levelEnd = 5001;
            WeaponUnlock = 7;
        }
        if(level == 18)
			{
				levelArray[from] =      new int[] {0          ,500            ,500             ,1000        };
				levelArray[till] =      new int[] {3000       ,3000           ,3000            ,2600        };
				levelArray[interval] =  new int[] {30         ,60             ,60              ,30          };
				levelArray[enemy] =     new int[] {LightShip,TricksterLeft,TricksterRight,Teleporter};
				levelArray[enemyX] =    new int[] {0  ,0      ,0       ,0};

            LevelAddUnit(1500,Carrier,20);
            LevelAddUnit(1500,Carrier,35);
            LevelAddUnit(1500,Carrier,50);
            LevelAddUnit(1500,Carrier,65);
            LevelAddUnit(1500,Carrier,80);

            LevelAddUnit(2600,SuperHeavy,50);
            LevelAddUnit(2600,SuperHeavy,25);
            LevelAddUnit(2600,SuperHeavy,75);
            LevelAddUnit(2700,SuperHeavy,50);
            LevelAddUnit(2700,SuperHeavy,25);
            LevelAddUnit(2700,SuperHeavy,75);
				
				multipleUnits(3000,3200,50,Bomber,0,0,4);
				
				energy.energyAmount = 10;
				energy.maxEnergy = 100;
				energy.energy = 50;
				levelEnd = 3201;
				WeaponUnlock = 11;
			}
        if (level == 19)
        {
            levelArray[from] =      new int[] {60, 500, 2000, 3500};
            levelArray[till] =      new int[] {500, 5500, 5500, 5500};
            levelArray[interval] =  new int[] {100, 90, 40, 100};
            levelArray[enemy] =     new int[] {MediumShip, InvisibleShip, LightShip, HeavyShip};
            levelArray[enemyX] =    new int[] {0, 0, 0, 0};

            energy.energyAmount = 1;
            energy.maxEnergy = 200;
            energy.energy = 200;
            levelEnd = 5501;
            LevelUnlocks = new byte[] {10,22};
            WeaponUnlock = 8;
        }
        if (level == 20)
        {
            levelArray[from] =   new int[] {};
            levelArray[till] =     new int[] {};
            levelArray[interval] = new int[] {};
            levelArray[enemy] =    new int[] {};
            levelArray[enemyX] =   new int[] {};

            LevelAddUnit(100, TricksterLeft, 50);
            LevelAddUnit(150, TricksterRight, 50);
            LevelAddUnit(200, TricksterLeft, 50);
            LevelAddUnit(300, MediumShip, 30);
            LevelAddUnit(300, MediumShip, 50);
            LevelAddUnit(300, MediumShip, 70);
            LevelAddUnit(475, MediumShip, 30);
            LevelAddUnit(475, MediumShip, 50);
            LevelAddUnit(475, MediumShip, 70);
            LevelAddUnit(750, Bomber, 40);
            LevelAddUnit(750, Bomber, 60);
            LevelAddUnit(800, Bomber, 40);
            LevelAddUnit(800, Bomber, 60);
            LevelAddUnit(900, HeavyShip, 50);
            LevelAddUnit(1000, LightShip, 50);
            LevelAddUnit(1000, LightShip, 40);
            LevelAddUnit(1000, LightShip, 60);
            LevelAddUnit(1100, LightShip, 50);
            LevelAddUnit(1100,LightShip, 40);
            LevelAddUnit(1100, LightShip, 60);
            LevelAddUnit(1200, LightShip, 50);
            LevelAddUnit(1200, LightShip, 40);
            LevelAddUnit(1200, LightShip, 60);


            energy.energyAmount = 0;
            energy.maxEnergy = 295;
            energy.energy = 295;
            LevelUnlocks = new byte[] {21, 14};
            levelEnd = 1200;

        }
        if (level == 21)
        {
            levelArray[from] =     new int[] {0, 1200, 2000, 5500};
            levelArray[till] =     new int[] {7000, 7000, 7000, 7000};
            levelArray[interval] = new int[] {130, 30, 45, 100};
            levelArray[enemy] =    new int[] {MediumShip, LightShip, Asteroid2, SuperHeavy};
            levelArray[enemyX] =   new int[] {0, 0, 0, 0};
            LevelUnlocks = new byte[] {20, 23, 25};
            energy.energyAmount = 2;
            energy.energy = 100;
            levelEnd = 7001;
            multipleUnits(2800, 6000, 300, Cluster, 50, 0, 1);
            multipleUnits(2800, 6000, 300, Cluster, 25, 0, 1);
            multipleUnits(2800, 6000, 300, Cluster, 75, 0, 1);
            LevelAddUnit(4600, Carrier, 50);
            LevelAddUnit(4600, Carrier, 25);
            LevelAddUnit(4600, Carrier, 75);
            LevelAddUnit(5500, Carrier, 25);
            LevelAddUnit(5500, Carrier, 40);
            LevelAddUnit(5500, Carrier, 60);
            LevelAddUnit(5500, Carrier, 75);
            WeaponUnlock = 10;
        }
        if(level == 22)
			{
				levelArray[from] =    new int[] {0          ,0           ,1300       ,3000       ,3500};
				levelArray[till] =     new int[] {3500       ,5500        ,2500       ,5500       ,5500};
				levelArray[interval] = new int[] {100        ,200         ,50         ,150        ,30};
				levelArray[enemy] =    new int[] {LightShip,MediumShip,LightShip,HeavyShip,LightShip};
				levelArray[enemyX] =   new int[] {0  ,0   ,0  ,0  ,0};
				LevelUnlocks = new byte[] {25,26,19};
				energy.energyAmount = 2;
				energy.energy = 150;
				energy.maxEnergy = 150;
				levelEnd = 6500;
				multipleUnits(2500,5500,200,TricksterLeft,50,0,1);
				multipleUnits(2500,5500,200,TricksterRight,50,0,1);
				multipleUnits(5500,6500,175,HeavyShip,50,0,1);
				multipleUnits(5500,6500,175,HeavyShip,30,0,1);
				multipleUnits(5500,6500,175,HeavyShip,70,0,1);
				multipleUnits(5550,6500,715,Teleporter,70,0,5);
			}
        if (level == 23)
        {
            levelArray[from] =     new int[] { 0, 1600, 4500, 4750 };
            levelArray[till] =     new int[] { 4500, 4500, 5500, 5500 };
            levelArray[interval] = new int[] { 60, 30, 10, 50 };
            levelArray[enemy] = new int[] { Teleporter, LightShip, InvisibleShip, Cluster };
            levelArray[enemyX] = new int[] { 0, 0, 0, 0 };
            LevelUnlocks = new byte[] {24, 21};
            energy.energy = 300;
            energy.maxEnergy = 300;
            energy.energyAmount = 0;
            levelEnd = 5500;
            LevelAddUnit(2000, Carrier, 50);
            LevelAddUnit(2500, Carrier, 60);
            LevelAddUnit(3000, Carrier, 30);
            LevelAddUnit(3500, Carrier, 50);
            LevelAddUnit(4000, Carrier, 70);
            LevelAddUnit(4500, Carrier, 50);
            LevelAddUnit(4750, Carrier, 60);
        }
        if (level == 24)
        {
            levelArray[from] =     new int[] {0, 1200, 2500, 3000, 3500, 4500};
            levelArray[till] =     new int[] {1200, 3000, 3000, 4500, 4500, 5500};
            levelArray[interval] = new int[] {130, 130, 30, 30, 30, 50};
            levelArray[enemy] =    new int[] {MediumShip, HeavyShip, LightShip, Cluster, Teleporter, SuperHeavy};
            levelArray[enemyX] =   new int[] {0, 0, 0, 0, 0, 0};
            energy.energy = 100;
            energy.maxEnergy = 100;
            energy.energyAmount = 2;
            WeaponUnlock = 12;
            levelEnd = 5500;
        }
        /*if (level == 3)
        {
            levelArray[from] = new int[] {};
            levelArray[till] = new int[] {};
            levelArray[interval] = new int[] {};
            levelArray[enemy] = new int[] {};
            levelArray[enemyX] = new int[] {};
            LevelUnlocks = new byte[] {};
        }*/
        if (level == 25)
        {
            levelArray[from] = new int[] { 0, 1200, 3500, 4000 };
            levelArray[till] = new int[] { 6000, 6000, 4000, 6000 };
            levelArray[interval] = new int[] { 30, 19, 100, 100 };
            levelArray[enemy] = new int[] { LightShip, LightShip, Bomber, HeavyShip };
            levelArray[enemyX] = new int[] { 0, 0, 0, 0 };
            multipleUnits(2500, 5000, 100, TricksterLeft, 50, 0, 1);
            multipleUnits(2500, 5000, 100, TricksterRight, 50, 0, 1);
            multipleUnits(2500, 5000, 100, TricksterLeft, 25, 0, 1);
            multipleUnits(2500, 5000, 100, TricksterRight, 25, 0, 1);
            multipleUnits(2500, 5000, 100, TricksterLeft, 75, 0, 1);
            multipleUnits(2500, 5000, 100, TricksterRight, 75, 0, 1);
            LevelAddUnit(5000, Carrier, 50);
            LevelAddUnit(5000, Carrier, 70);
            LevelAddUnit(5000, Carrier, 30);
            LevelUnlocks = new byte[] { 21, 22, 27 };
            energy.energy = 100;
            energy.maxEnergy = 100;
            energy.energyAmount = 2;
            levelEnd = 5500;
        }
        if (level == 26)
        {
            levelArray[from] = new int[] { 0, 600, 1200, 3000 };
            levelArray[till] = new int[] { 1300, 1300, 2000, 4000 };
            levelArray[interval] = new int[] { 25, 90, 140, 70 };
            levelArray[enemy] = new int[] { Asteroid1, Asteroid2, Asteroid3, Asteroid3 };
            levelArray[enemyX] = new int[] { 0, 0, 0, 0 };
            LevelUnlocks = new byte[] { 22, 27 };
            energy.energy = 100;
            energy.maxEnergy = 100;
            energy.energyAmount = 2;
            levelEnd = 4000;
            multipleUnits(2000, 4000, 30, Asteroid1, 0, 0, 7);
        }
        if (level == 27)
        {
            levelArray[from] = new int[] { 0, 3500, 5000 };
            levelArray[till] = new int[] { 1500, 6000, 6000 };
            levelArray[interval] = new int[] { 270, 30, 50 };
            levelArray[enemy] = new int[] { HeavyShip, Teleporter, HeavyShip };
            levelArray[enemyX] = new int[] { 0, 0, 0 };
            LevelUnlocks = new byte[] { 25, 26, 28 };
            energy.energy = 100;
            energy.maxEnergy = 100;
            energy.energyAmount = 2;
            levelEnd = 6001;
            multipleUnits(1500, 6000, 80, LightShip, 0, 0, 7);
            LevelAddUnit(4000, Carrier, 75);
            LevelAddUnit(4000, Carrier, 25);
            LevelAddUnit(4000, Carrier, 40);
        }
        if (level == 28)
        {
            levelArray[from] = new int[] { 0, 1300, 1800, 2800, 3500, 4000 };
            levelArray[till] = new int[] { 1300, 2800, 2800, 4000, 5000, 5000 };
            levelArray[interval] = new int[] { 80, 80, 100, 80, 100, 50 };
            levelArray[enemy] = new int[] { LightShip, MediumShip, InvisibleShip, HeavyShip, Bomber, SuperHeavy };
            levelArray[enemyX] = new int[] { 0, 0, 0, 0, 0, 0 };
            LevelAddUnit(1, Carrier, 50);
            LevelAddUnit(1100, Carrier, 75);
            LevelAddUnit(2200, Carrier, 25);
            LevelAddUnit(3300, Carrier, 50);
            LevelAddUnit(4500, Carrier, 75);
            LevelAddUnit(4500, Carrier, 25);
            energy.energy = 100;
            energy.maxEnergy = 100;
            energy.energyAmount = 2;
            levelEnd = 5001;
        }
        levelArray[timer] = new int[levelArray[0].Length];

        for (int i = 0; i < levelArray[timer].Length; i++)
        {
            levelArray[timer][i] = -1;
        }
    }
    void multipleUnits(int _from, int _till, int _interval, int _enemy, int _x, int _y, int amount)
    {
        byte from = 0;
        byte till = 1;
        byte interval = 2;
        byte enemy = 3;
        byte enemyX = 4;
        int[] array;

        for (var i = 0; i < amount; i++)
        {
            array = newArray(levelArray[from]);
            addToLevelArray(from, array, _from);
            array = newArray(levelArray[till]);
            addToLevelArray(till, array, _till);
            array = newArray(levelArray[interval]);
            addToLevelArray(interval, array, _interval);
            array = newArray(levelArray[enemy]);
            addToLevelArray(enemy, array, _enemy);
            array = newArray(levelArray[enemyX]);
            addToLevelArray(enemyX, array, _x);
        }
    }
        void LevelAddUnit(int _from, int _enemy, int _x)
    {

        byte from = 0;
        byte till = 1;
        byte interval = 2;
        byte enemy = 3;
        byte enemyX = 4;
        int[] array;


        array = newArray(levelArray[from]);
        addToLevelArray(from, array, _from);
        array = newArray(levelArray[till]);
        addToLevelArray(till, array, _from + 1);
        array = newArray(levelArray[interval]);
        addToLevelArray(interval, array, 0);
        array = newArray(levelArray[enemy]);
        addToLevelArray(enemy, array, _enemy);
        array = newArray(levelArray[enemyX]);
        addToLevelArray(enemyX, array, _x);




    }
    int[] newArray(int[] array)
    {
        int[] newArray = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            newArray[i] = array[i];
        }
        return newArray;
    }

    void addToLevelArray(int oldArray, int[] newArray, int value)
    {
        levelArray[oldArray] = new int[levelArray[oldArray].Length + 1];
        for (int i = 0; i < newArray.Length; i++)
        {
            levelArray[oldArray][i] = newArray[i];
        }
        levelArray[oldArray][levelArray[oldArray].Length - 1] = value;
    }
    void EndLevel()
    {
        if (levelComplete == false)
        {
            if(GameVar.Score[level - 1]< (int)PlanetHealth.planetHealth)
                GameVar.Score[level - 1] = (int)PlanetHealth.planetHealth;
            GameObject instance = Instantiate(Resources.Load("LevelComplete", typeof(GameObject))) as GameObject;
            instance.transform.FindChild("TextScore").GetComponent<Text>().text = "Score: "+Mathf.Round(PlanetHealth.planetHealth).ToString();
            levelComplete = true;
            GameVar.LevelsComplete[level - 1] = true;
            if(LevelUnlocks.Length!=0)
                for (var i = 0; i < LevelUnlocks.Length; i++)
                {
                    GameVar.LevelsUnlocked[LevelUnlocks[i]-1] = true;
                }
            if(WeaponUnlock!= 0)
                GameVar.WeaponsUnlocked[WeaponUnlock-1] = true;
            GameVar.Save();
        }
    }
    void Update()
    {
        if (!paused)
        {
            if (time > levelEnd * 0.033f + 2)
            {
                if (RocketLauncherSystem.e.Count == 0)
                {
                    EndLevel();
                }
            }
            if (levelComplete == false)
            {
                time += Time.deltaTime;
                secTime += Time.deltaTime;
            }
            if (secTime >= 0.033f)
            {
                for (int i = 0; i < levelArray[5].Length; i++)
                {

                    if (levelArray[5][i] != -1)
                    {

                        levelArray[5][i] += 1;
                        if (levelArray[5][i] >= levelArray[2][i])
                        {
                            AddEnemy(levelArray[3][i], levelArray[4][i]);
                            levelArray[5][i] = 0;
                        }
                    }
                }
                secTime -= 0.033f;
            }
            for (int i = 0; i < levelArray[0].Length; i++)
                if (levelArray[0][i] != -1)
                {
                    if (time > levelArray[0][i] * 0.033f)
                    {

                        addTimer(i);
                    }
                }
            for (int i = 0; i < levelArray[1].Length; i++)
                if (levelArray[1][i] != -1)
                    if (time > levelArray[1][i] * 0.033f)
                    {
                        levelArray[5][i] = -1;
                        levelArray[1][i] = -1;
                    }
        }
    }
    void addTimer(int i)
    {
        levelArray[0][i] = -1;
        levelArray[5][i] = 0;
    }
    public static void AddParticle(string particle, float _x, float _y)
    {
        if(graphics == true)
            if (particle != "")
            {
                GameObject instance = Instantiate(Resources.Load(particle, typeof(GameObject))) as GameObject;
                instance.transform.position = new Vector2(_x, _y);
                if (particle == "Impact")
                {
                    instance.transform.SetParent(Game.impact.transform);
                    instance.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));

                }
            }
    }
    public void AddEnemy(int enemyint, int _x, float _y = 0f)
    {

        float x = 0f;
        if (_x == 0)
            x = Random.Range(-0.95f, 0.95f);
        else
            x = ((float)_x/100 - 0.5f) * 2;
        string enemy;
        if (enemyint == 0)
            enemy = "LightShip";
        else if (enemyint == 1)
            enemy = "MediumShip";
        else if (enemyint == 2)
            enemy = "HeavyShip";
        else if (enemyint == 3)
            enemy = "SuperHeavyShip";
        else if (enemyint == 4)
            enemy = "Bomber";
        else if (enemyint == 5)
            enemy = "Trickster";
        else if (enemyint == 6)
            enemy = "Teleporter";
        else if (enemyint == 7)
            enemy = "InvisibleShip";
        else if (enemyint == 8)
            enemy = "Cluster";
        else if (enemyint == 9)
            enemy = "Carrier";
        else if (enemyint == 10)
            enemy = "Asteroid1";
        else if (enemyint == 11)
            enemy = "Asteroid2";
        else if (enemyint == 12)
            enemy = "Asteroid3";
        else if (enemyint == 13)
        {
            enemy = "Trickster";
            GameObject instancet = Instantiate(Resources.Load(enemy, typeof(GameObject))) as GameObject;
            instancet.transform.position = new Vector2(x * stageRight, stageTop);
            Trickster e = instancet.GetComponent<Trickster>();
            e.Left = true;
            return;
        }
        else if (enemyint == 14)
        {
            enemy = "Trickster";
            GameObject instancet = Instantiate(Resources.Load(enemy, typeof(GameObject))) as GameObject;
            instancet.transform.position = new Vector2(x * stageRight, stageTop);
            Trickster e = instancet.GetComponent<Trickster>();
            e.Left = false;
            return;
        }
        else enemy = null;
        GameObject instance = Instantiate(Resources.Load(enemy, typeof(GameObject))) as GameObject;
        instance.transform.position = new Vector2(x* stageRight, stageTop);

        /*enemy = new Asteroid(_x, _y, 1);
        enemy.cacheAsBitmap = true;
        enemiesMc.addChild(enemy);
        enemy = null;*/
    }
    void OnPauseGame()
    {
        paused = true;
    }
    void Resume()
    {
        paused = false;
    }
    /* public void AddEnemy(string enemy, string _x, float _y = 0f)
     {
         _y = stageTop;
         float newX = Random.Range(stageLeft, stageRight);
         AddEnemy(enemy,newX,_y);
     }*/
     
}