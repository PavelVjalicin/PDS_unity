using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponSelect : MonoBehaviour {
    public bool map = true;
	// Use this for initialization
	void Start () {
        Canvas myCanvasObject = GetComponent<Canvas>();
        myCanvasObject.sortingLayerName = "Cannon";
        myCanvasObject.sortingOrder = 10;
        float ScreenAspect = (float)Screen.width / (float)Screen.height;
        if (map)
        {
            
            transform.localScale = new Vector3(ScreenAspect * 22.4f, ScreenAspect * 28.8f, 1);
            float localAspect = transform.localScale.x / transform.localScale.y;
            transform.position = new Vector3(0, 0.625f * 8 - ScreenAspect * 8, 0f);
        }
        else
        {
            transform.localScale = new Vector3(ScreenAspect * 22.4f, ScreenAspect * 28.8f, 1)*0.75f;
            float localAspect = transform.localScale.x / transform.localScale.y;
            transform.position = new Vector3(0, 0.475f * 8 - ScreenAspect * 8, 0f);
            transform.parent.FindChild("Go").localScale /= 1.4f;
            transform.parent.FindChild("ReturnToMap").localScale /= 1.4f;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
