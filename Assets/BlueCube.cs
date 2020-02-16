using UnityEngine;
using System.Collections;

public class BlueCube : MonoBehaviour {
    private float xSpeed = 0f;
    private float ySpeed = 0f;
    private float alpha = 1f;
    // Use this for initialization
    void Start()
    {
        xSpeed = Random.Range(-1f, 1f);
        ySpeed = Random.Range(-1f, 1f);

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
        alpha -= 0.8f * Time.deltaTime;
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        rend.color = new Color(1f, 1f, 1f, alpha);

    }
}
