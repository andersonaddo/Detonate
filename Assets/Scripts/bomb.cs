using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour {

    public float timeToDefuse, refuseRate, listeningRate, refuseStep, shakeIntensity, shakeDuration;
    public int maxDefusePoints;

    float currentDefuseLevel;

    float incrementPerListen;

    public bool isBeingDefused = false;

    bombDefuser player;

    public GameObject bombPS;
	
	void Start()
    {
        incrementPerListen = (float)maxDefusePoints / (timeToDefuse / listeningRate);
        StartCoroutine("ListenForDefusing");
        StartCoroutine("RefuseOverTime");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        player = collider.GetComponent<bombDefuser>();
    }

    IEnumerator ListenForDefusing()
    {
        while (true)
        {
            if (!isBeingDefused)
            {
                yield return new WaitForSeconds(listeningRate);
                continue;
            }
            currentDefuseLevel += incrementPerListen;
            player.defuzeSlider.value = currentDefuseLevel;
            if (currentDefuseLevel >= maxDefusePoints) defuse();
            yield return new WaitForSeconds(listeningRate);
        }
    }

    IEnumerator RefuseOverTime()
    {
        while (true)
        {
            if (isBeingDefused || currentDefuseLevel == 0)
            {
                yield return new WaitForSeconds(refuseRate);
                continue;
            }
            currentDefuseLevel -= refuseStep;
            if (0 >= currentDefuseLevel) currentDefuseLevel = 0;
            player.defuzeSlider.value = currentDefuseLevel;
            yield return new WaitForSeconds(refuseRate);
        }
    }

    void defuse()
    {
        player.currentBomb = null; //Freeing up the active bomb set for other bombs. 
                                   //That is useful if the player is by two bombs at the same time
        player.defuzeSlider.value = 0;
        Destroy(gameObject);
        gameManager.Instance.updateBombsLeft(-1);

    }

    public void detonate()
    {
        Camera.main.GetComponent<cameraShake>().shake(shakeIntensity, shakeDuration);
        Instantiate(bombPS, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}
