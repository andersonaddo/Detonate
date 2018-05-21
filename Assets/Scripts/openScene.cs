using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openScene : MonoBehaviour {

		public void goToScene (int index) {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
	    }
}
