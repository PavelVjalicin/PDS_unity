using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour {
    SpriteRenderer s;
    float a = 1;
	// Use this for initialization
	void Start () {
        s= GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = transform.localScale + new Vector3(0.1f, 0.1f, 0.1f)*Time.deltaTime*60;
        a -= 0.03f * Time.deltaTime*60;
        s.color = new Color(1f, 1f, 1f, a);
        if(a <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
