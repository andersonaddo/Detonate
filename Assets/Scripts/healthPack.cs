using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPack : MonoBehaviour {

    public int healthIncrease;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        collider.GetComponent<health>().incrementHealthBy(healthIncrease);
        Destroy(gameObject);
    }
}
