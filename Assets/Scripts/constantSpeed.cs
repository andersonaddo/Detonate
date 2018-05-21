using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constantSpeed : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
	}
}
