using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimingScript : MonoBehaviour {

    public Vector3 targetPosition { get; private set; }
    public Transform crossHairs;

    // Update is called once per frame
    void Update () {
        Vector3 pointTouched = Input.mousePosition;
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(pointTouched.x, pointTouched.y, 11));
        crossHairs.position = targetPosition;
    }
}
