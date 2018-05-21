using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public bool inRange; //Will be manipulated by exrernal scripts
    List<Node> pathToPlayer;
    int currentNodeIndex;
    public float speed;
	
	// Calls for newer paths every once in a while.
    // Can't call this too often...could lag the game
	IEnumerator periodicallyRequest() {
        while (true)
        {
            if (!inRange)
            {
                yield return new WaitForSeconds(Random.Range(0.3f, 1));
                continue;
            }                
            pathToPlayer = gameManager.Instance.pathFinder.FindPathToPlayer(transform.position);
            currentNodeIndex = 0;
            yield return new WaitForSeconds(Random.Range(0.3f, 1));
        }
	}

    IEnumerator follow()
    {
        while (true)
        {
            if (pathToPlayer == null || pathToPlayer.Count == 0)
            {
                yield return new WaitForSeconds(0.01f);
                continue;
            }
            Vector2 relativeVector = (Vector3)pathToPlayer[currentNodeIndex].worldPosition - transform.position;
            relativeVector = relativeVector.normalized;
            transform.Translate(relativeVector * speed * 0.1f);
            flipIfNeeded(relativeVector);

            if (Vector3.Distance(transform.position, (Vector3)pathToPlayer[currentNodeIndex].worldPosition) < 0.1f)
            {
                currentNodeIndex++;
                if (currentNodeIndex == pathToPlayer.Count)
                {
                    pathToPlayer = null;
                    currentNodeIndex = 0;
                }
            }

            yield return new WaitForSeconds(0.01f);

        }
    }

    void Start()
    {
        StartCoroutine("periodicallyRequest");
        StartCoroutine("follow");
    }

    void flipIfNeeded(Vector2 vector)
    {
        if (vector.x > 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
}
