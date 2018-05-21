using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    public int damage;

    void OnTriggerEnter2D(Collider2D collider)
    {

            health victimHealth = collider.GetComponent<health>();
            if (victimHealth == null) victimHealth = collider.transform.parent.GetComponent<health>();
            if (victimHealth != null) victimHealth.incrementHealthBy(-damage);
            Destroy(gameObject);
    }
}
