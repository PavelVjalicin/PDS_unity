using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PositionToScreen : MonoBehaviour {
    public bool left;
    public bool right;
    public bool top;
    public bool bottom;
    public float offsetX;
    public float offsetY;
    // Use this for initialization
    void Start() {
        int width = Camera.main.pixelWidth / 2;
        if (left == true)
        {
            width = 0;
        }
        else if (right == true)
        {
            width = Camera.main.pixelWidth;
        }
        int height = Camera.main.pixelHeight / 2;
        if (bottom == true)
        {
            height = 0;
        }
        else if (top == true)
        {
            height = Camera.main.pixelHeight;
        }
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3((float)width, (float)height, 0));
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            this.transform.position = new Vector3(worldPos.x + offsetX, worldPos.y + offsetY, worldPos.z);
            
        }
        else
        {
            this.transform.position = new Vector3(worldPos.x + offsetX/1.5f, worldPos.y + offsetY/1.5f, worldPos.z);
        }
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
