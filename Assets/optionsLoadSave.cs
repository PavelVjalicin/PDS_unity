using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class optionsLoadSave : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ChangeOpacity();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D[] hitInfo = Physics2D.RaycastAll(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 1f);
            for (var r = 0; r < hitInfo.Length; r++)
            {

                if (hitInfo[r].collider != null)
                {
                    if (hitInfo[r].collider.gameObject.name == "sOn")

                    {
                        GameVar.sound = true;
                        GameVar.SaveOptions();
                    }
                    if (hitInfo[r].collider.gameObject.name == "sOff")

                    {
                        GameVar.sound = false;
                        GameVar.SaveOptions();
                    }
                    if (hitInfo[r].collider.gameObject.name == "mOn")

                    {
                        GameVar.music = true;
                        GameVar.SaveOptions();
                    }
                    if (hitInfo[r].collider.gameObject.name == "mOff")

                    {
                        GameVar.music = false;
                        GameVar.SaveOptions();
                    }
                }

                ChangeOpacity();
            }
        }
    }
    void ChangeOpacity()
    {
        GameObject o;
        if (GameVar.music == false)
        {
            o = transform.FindChild("mOff").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
            o = transform.FindChild("mOn").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            o = transform.FindChild("mOff").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, .5f);
            o = transform.FindChild("mOn").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
        }
        if (GameVar.sound == false)
        {
            o = transform.FindChild("sOff").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
            o = transform.FindChild("sOn").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            o = transform.FindChild("sOff").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, .5f);
            o = transform.FindChild("sOn").gameObject;
            o.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
