using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamekazi : MonoBehaviour {

    public string layerToDamage;
    public int damage;
    public float knockbackForce, shakeIntensity, shakeDuration;
    public GameObject explosion;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer(layerToDamage))
        {
            health victimHealth = collider.GetComponent<health>();
            if (victimHealth == null) victimHealth = collider.transform.parent.GetComponent<health>();
            victimHealth.incrementHealthBy(-damage);

            //Also adding knockback
            Instantiate(explosion, transform.position, Quaternion.identity);
            Vector2 force = GetComponent<Rigidbody2D>().velocity.normalized * knockbackForce;
            collider.GetComponent<Rigidbody2D>().AddForce(force);
            Camera.main.GetComponent<cameraShake>().shake(shakeIntensity, shakeDuration);
            Destroy(gameObject);
        }
    }
}
