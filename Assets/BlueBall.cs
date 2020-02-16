using UnityEngine;
using System.Collections;

public class BlueBall : MonoBehaviour {

    // Use this for initialization
    private float xSpeed = 0f;
    private float ySpeed = 0f;
    private float alpha = 1f;
    void Start()
    {
        xSpeed = Random.Range(-0.5f, .5f);
        ySpeed = Random.Range(0f, 0.5f);

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
        alpha -= 0.4f * Time.deltaTime;
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        rend.color = new Color(1f, 1f, 1f, alpha);

    }
}
