using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{
    public Animator transition;
    public PlayerInput inputs;

    public float delayTime;

    void Start()
    {
        inputs = FindObjectOfType<PlayerInput>();
        
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        inputs.canInput = false;
        inputs.KillInput();
        transition.SetTrigger("startTransition");

        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(levelIndex);
        inputs.canInput = true;

    }
}
