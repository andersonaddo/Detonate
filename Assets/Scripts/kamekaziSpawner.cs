using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamekaziSpawner : MonoBehaviour {

    public GameObject nazi;
    public float fireRate, launchSpeed;
    float nextShootingTime = 0;

    public FollowPlayer followScript;

    void OnTriggerStay2D(Collider2D collider)
    {
        followScript.inRange = true;

        if (nextShootingTime < Time.time)
            {
                nextShootingTime = Time.time + fireRate;
                GameObject recentNazi = Instantiate(nazi, transform.position, Quaternion.identity);
                recentNazi.GetComponent<Rigidbody2D>().velocity = (collider.transform.position - transform.position).normalized * launchSpeed;

                nextShootingTime = Time.time + fireRate;
            }
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        followScript.inRange = false;
    }
}
