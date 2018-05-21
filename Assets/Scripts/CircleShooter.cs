using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShooter : MonoBehaviour {

    public AudioClip pewpew;

    public GameObject circleOfBullets;
    public float fireRate;
    float nextShootingTime = 0;

    public FollowPlayer followScript;

    void OnTriggerStay2D(Collider2D collider)
    {
        followScript.inRange = true;

        if (nextShootingTime < Time.time)
            {
                nextShootingTime = Time.time + fireRate;
            GetComponent<AudioSource>().PlayOneShot(pewpew);

            Instantiate(circleOfBullets, transform.position, Quaternion.identity);
            }
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        followScript.inRange = false;
    }
}
