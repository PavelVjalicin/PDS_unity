using UnityEngine;
using System.Collections;

public class TrashCannon : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int height = 0;
        int width = Camera.main.pixelWidth;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3((float)width, (float)height, 0));

        this.transform.position = new Vector3(worldPos.x - .35f, worldPos.y + 1.5f, worldPos.z);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
    }
	
}
