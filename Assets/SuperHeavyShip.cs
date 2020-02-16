using UnityEngine;
using System.Collections;

public class SuperHeavyShip : Enemy {
    private float time;
	// Use this for initialization

    // Update is called once per frame
    void Update()
    {

        if (!paused)
        {
            time += Time.deltaTime;
            if (time > 0.4f)
            {
                Game.AddParticle("BlueBall", this.transform.position.x+0.15f, this.transform.position.y + 0.25f);
                Game.AddParticle("BlueBall", this.transform.position.x - 0.15f, this.transform.position.y + 0.25f);
                time = 0;
            }
            FreezeTime -= Time.deltaTime;

            xSpeed = Mathf.Sin(angle / 180 * Mathf.PI) * -speed;
            ySpeed = Mathf.Cos(angle / 180 * Mathf.PI) * -speed;

            Move();
            if (transform.position.y < -5.2)
            {

                outOfBounds();


            }
            if (impact == true)
            {
                impactTime -= Time.deltaTime;
                if (impactTime < 0)
                {
                    Impact();
                }
            }
        }
    }
}
