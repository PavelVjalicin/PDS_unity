using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
    public int damage;
    // Use this for initialization
    void Start()
    {

        Vector2 thisPos = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(thisPos, Vector2.up, 10f);
        for (int i = 0; i < hitInfo.Length; i++)
        {
            if (hitInfo[i].collider.gameObject.tag == "Enemy")
            {
                Enemy enemy = hitInfo[i].collider.gameObject.GetComponent<Enemy>();
                enemy.Damage(damage);
            }
        }
    }
	// Update is called once per frame
}
