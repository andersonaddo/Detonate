using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    Animator myAnim;
    SpriteRenderer myRenderer;
    aimingScript aimer;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        aimer = GetComponent<aimingScript>();
    }

    // Update is called once per frame
    void Update () {

        float xDimension = Input.GetAxisRaw("Horizontal");
        float yDimension = Input.GetAxisRaw("Vertical");


        float x = xDimension * speed * Time.deltaTime;
        float y = yDimension * speed * Time.deltaTime;
        transform.Translate(new Vector3(x, y, 0));

        if (xDimension == 0 && yDimension == 0) myAnim.SetBool("isWalking", false);
        else myAnim.SetBool("isWalking", true);

        //Flipping the character if his aim is behind him
        if (transform.position.x - aimer.targetPosition.x < 0 && transform.localScale.x < 0)
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

        if (transform.position.x - aimer.targetPosition.x > 0 && transform.localScale.x > 0)
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
