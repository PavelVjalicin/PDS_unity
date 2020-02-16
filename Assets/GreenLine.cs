using UnityEngine;
using System.Collections;

public class GreenLine : MonoBehaviour {

    // Use this for initialization
    private float xSpeed = 0f;
    private float ySpeed = 1f;
    private float alpha = 1f;
    void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x+Random.Range(-0.25f,0.25f), this.transform.position.y + Random.Range(-0.25f, 0.25f), this.transform.position.z);
        

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (alpha < 0f)
            Destroy(this.gameObject);
    }


    void Move()
    {
        this.transform.position = new Vector2(this.transform.position.x + xSpeed * Time.deltaTime, this.transform.position.y + ySpeed * Time.deltaTime);
        alpha -= 0.9f * Time.deltaTime;
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        rend.color = new Color(1f, 1f, 1f, alpha);

    }
}
