using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Energy : MonoBehaviour {
    public float energy = 50;
    public float maxEnergy = 100;
    public int energyAmount = 1;
    private float delay =  0;
    private Text energyText;
    private Text percent;
    private Transform green;
    private Transform redBox;
    private bool paused;

	// Use this for initialization
	void Start () {
        Canvas myCanvasObject = GetComponent<Canvas>();
        myCanvasObject.sortingLayerName = "ui";
        int height = Camera.main.pixelHeight;
        int width = Camera.main.pixelWidth;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3((float)width, (float)height, 0));

        this.transform.position = new Vector3(worldPos.x -.43f, worldPos.y-.55f, worldPos.z);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);

        Transform go = transform.Find("EnergyAmount");
        energyText = go.GetComponent<Text>();
        go = transform.Find("EnergyPercent");
        percent = go.GetComponent<Text>();
        go = transform.Find("Green");
        green = go.GetComponent<Transform>();
        redBox = transform.Find("EnergyBoxRed");
        redBox.gameObject.SetActive(false);
        UpdateUI();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!paused)
        {
            delay += Time.deltaTime;
            if (delay > 0.66f)
            {

                delay -= 0.66f;
                energy += energyAmount;

                UpdateUI();





            }

        }

	}
    public void UpdateUI()
    {
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
            redBox.gameObject.SetActive(true);
            energyText.color = Color.red;
        }
        else
        {
            redBox.gameObject.SetActive(false);
            energyText.color = Color.green;
        }
        energyText.text = energy.ToString();
        percent.text = ((int)(energy / maxEnergy * 100)).ToString() + "%";
        green.localScale = new Vector3(green.localScale.x, energy / maxEnergy, 0f);
    }
    void OnPauseGame()
    {
        paused = true;
    }
    void Resume()
    {
        paused = false;
    }
}
