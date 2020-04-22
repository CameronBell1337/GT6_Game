using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class swordCutSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector playableDirector;
    public Animator animator;

    public PlayerStats sword;
    public cameraManager cameraManager;

    public GameObject player;
    public GameObject cutscenePlayer;
    public GameObject swordOBJ;
    public PlayerInput input;

    public Vector3 newPos;
    void Start()
    {
        cutscenePlayer.SetActive(false);
        input = FindObjectOfType<PlayerInput>();
        cameraManager = FindObjectOfType<cameraManager>();
        sword = FindObjectOfType<PlayerStats>();
    }

    

    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            input.canInput = false;
            input.KillInput();
            player.SetActive(false);
            cameraManager.cutScene01Active = true;
            cutscenePlayer.SetActive(true);
            StartCoroutine(Delay());
        }
    }
    void spawnPlayer()
    {
        player.transform.position = newPos;
    }

    IEnumerator Delay()
    {
        spawnPlayer();
        playableDirector.Play();
        yield return new WaitForSeconds(10);
        sword.hasSword = true;
        cutscenePlayer.SetActive(false);
        player.SetActive(true);
        cameraManager.cutScene01Active = false;
        input.canInput = true;
        playableDirector.Stop();
        Destroy(cutscenePlayer);
        Destroy(swordOBJ);
        Destroy(gameObject);
    }

    

}
