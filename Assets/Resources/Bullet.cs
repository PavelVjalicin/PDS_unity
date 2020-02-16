using UnityEngine;
using System.Collections;
using System;

public class Bullet : MonoBehaviour
{


    public float xSpeed  =0f;
    public float ySpeed = 0f;
    public float speed  =3f;
    [NonSerialized]public float angle =0f;
    public int damage;
    public bool paused = false;
    
    // Use this for initialization
    void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, angle);
        xSpeed =- Mathf.Sin(angle/180*Mathf.PI) * speed;
        ySpeed = Mathf.Cos(angle / 180 * Mathf.PI) * speed;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            Move();
        } 

    }
    public void Move()
    {
        transform.position = new Vector2(transform.position.x + xSpeed*Time.deltaTime, transform.position.y + ySpeed*Time.deltaTime);
    }
    public void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    void OnPauseGame()
    {
        paused = true;
    }
    void Resume()
    {
        paused = false;
    }
}
