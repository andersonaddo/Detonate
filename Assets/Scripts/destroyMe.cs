using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyMe : MonoBehaviour {

    public float aliveTime;

	void Start () {
        Destroy(gameObject, aliveTime);
	}
}
