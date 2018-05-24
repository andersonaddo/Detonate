using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bombDefuser : MonoBehaviour {

    public string bombLayer;

    public GameObject defuseInstructions, sliderPanel;
    public Slider defuzeSlider;

    public bomb currentBomb; //All logic involving this was implemented to prevent bugs if the player ends up in two bomb colliders at the same tine
    

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer != LayerMask.NameToLayer(bombLayer)) return;
        defuseInstructions.SetActive(false);
        if (currentBomb != null)
        {
            currentBomb.isBeingDefused = false;
            currentBomb = null;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer != LayerMask.NameToLayer(bombLayer)) return;
        defuseInstructions.SetActive(true);

        if (Input.GetAxisRaw("Defuse") == 1)
        {
            if (currentBomb == null)
            {
                currentBomb = collider.GetComponent<bomb>();
                sliderPanel.SetActive(true);
                defuzeSlider.maxValue = currentBomb.maxDefusePoints;
            }
            if (collider.GetComponent<bomb>() != currentBomb) return;
            currentBomb.isBeingDefused = true;
        }
        else
        {
            if (collider.GetComponent<bomb>() != currentBomb) return;
            currentBomb.isBeingDefused = false;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.layer != LayerMask.NameToLayer(bombLayer)) return;
        defuseInstructions.SetActive(true);

        if (Input.GetAxisRaw("Defuse") == 1)
        {
            if (currentBomb == null)
            {
                currentBomb = collider.GetComponent<bomb>();
                sliderPanel.SetActive(true);
                defuzeSlider.maxValue = currentBomb.maxDefusePoints;
            }
            if (collider.GetComponent<bomb>() != currentBomb) return;
            currentBomb.isBeingDefused = true;
        }
        else
        {
            if (collider.GetComponent<bomb>() != currentBomb) return;
            currentBomb.isBeingDefused = false;
        }
    }

    void Update()
    {
        if (currentBomb == null && sliderPanel.activeSelf && defuzeSlider.value == 0) sliderPanel.SetActive(false);
    }

}
