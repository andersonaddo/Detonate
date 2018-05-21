using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSpewer : MonoBehaviour {

    public AudioClip pewpew;


    public GameObject lava;
    public float fireRate, burstRate;
    float nextShootingTime = 0;
    public int bulletsPerBurst;

    bool isShooting = false;

    Transform player;

    public FollowPlayer followScript;

    void OnTriggerStay2D(Collider2D collider)
    {
        followScript.inRange = true;
            if (player == null) player = collider.transform;
            if (isShooting) return;
            if (nextShootingTime < Time.time)
            {
                StartCoroutine("fireBursts");
            }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        followScript.inRange = false;
    }

    Quaternion rotateToPosition()
    {
        Vector2 targetPos = player.position;
        float deltaY = targetPos.y - transform.position.y;
        float deltaX = targetPos.x - transform.position.x;

        float angleToRotate = Mathf.Atan2(Mathf.Abs(deltaY), Mathf.Abs(deltaX));

        angleToRotate *= Mathf.Rad2Deg; //Converting to degrees

        //Adjusting the angle 
        if (deltaX < 0 && deltaY > 0) angleToRotate = 90 - angleToRotate;
        if (deltaX < 0 && deltaY < 0) angleToRotate = 90 + angleToRotate;
        if (deltaX > 0 && deltaY > 0) angleToRotate = 270 + angleToRotate;
        if (deltaX > 0 && deltaY < 0) angleToRotate = 270 - angleToRotate;
        if (deltaX == 0) angleToRotate = 0;

        return Quaternion.Euler(0, 0, angleToRotate);
    }

    IEnumerator fireBursts()
    {
        isShooting = true;
        for (int i = 0; i < bulletsPerBurst; i++)
        {
            Instantiate(lava, transform.position, rotateToPosition());
            GetComponent<AudioSource>().PlayOneShot(pewpew);

            yield return new WaitForSeconds(burstRate);
        }
        nextShootingTime = Time.time + fireRate;
        isShooting = false;

    }
}
