using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    Animator sceneTransition;
    public string sceneName;

    private void Awake()
    {
        sceneTransition = GetComponent<Animator>();
    }

    public void LoadNextScene()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        sceneTransition.SetTrigger("end");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
    }
}
