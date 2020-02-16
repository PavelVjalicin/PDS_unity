using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class NoWeaponsSelected : MonoBehaviour {
    private Text sp;
	// Use this for initialization
	void Start () {
        sp = gameObject.GetComponent<Text>();
        sp.color = new Color(1f,1f,1f,0f);
	}
	void Update()
    {
        if(sp.color.a > 0)
        {
            sp.color = new Color(1f,0f,0f, sp.color.a-0.01f);
        }
    }
	// Update is called once per frame

}
