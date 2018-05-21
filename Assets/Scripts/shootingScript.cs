using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingScript : MonoBehaviour {

    public AudioClip pewpew;
    public GameObject bullet;
    public float fireRate;
    float nextFireTime;
    public Transform gunTip;
    aimingScript aimer;

    void Start()
    {
        aimer = GetComponent<aimingScript>();
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("Fire1") == 1 && Time.time > nextFireTime){
            fireBullet();
            nextFireTime = Time.time + fireRate;
        }
	}

    void fireBullet()
    {
        Instantiate(bullet, gunTip.position, rotateToPosition());
        GetComponent<AudioSource>().PlayOneShot(pewpew);
    }

    Quaternion rotateToPosition()
    {
        Vector2 targetPos = aimer.targetPosition;
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
}
