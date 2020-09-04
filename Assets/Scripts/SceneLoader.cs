using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    
    public Animator transitionAnimator;

    public void LoadNextScene() {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadPreviousScene() {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex - 1));
    }

    public void LoadSceneByID(int id) {
        StartCoroutine(LoadScene(id));
    }

    IEnumerator LoadScene(int sceneIndex) {

        transitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(sceneIndex);

        
    }
}
