using UnityEngine;
using System.Collections;

public class Dust : MonoBehaviour {

    // Use this for initialization
    private float angle = 0f;
    private float xSpeed = 0f;
    private float ySpeed = 0f;
    private float rotationSpeed = 0f;
    private float alpha = 1f;
	void Start () {
        angle = Random.Range(0f, 360f);
        xSpeed = Random.Range(-1f, 1f);
        ySpeed = Random.Range(-1f, 1f);
        rotationSpeed = Random.Range(-90f, 90f);
        
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        if (alpha < 0f)
            Destroy(this.gameObject);
    }
   

    void Move()
    {
        this.transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z+rotationSpeed*Time.deltaTime);
        this.transform.position = new Vector2(this.transform.position.x + xSpeed * Time.deltaTime, this.transform.position.y + ySpeed * Time.deltaTime);
        alpha -= 0.5f * Time.deltaTime;
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        rend.color = new Color(1f, 1f, 1f, alpha);
        
    }
}
