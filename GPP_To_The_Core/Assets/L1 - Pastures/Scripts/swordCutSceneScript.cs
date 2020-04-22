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
            input.KillInput();
            input.canInput = false;
            player.SetActive(false);
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
        yield return new WaitForSeconds(12);
        sword.hasSword = true;
        //playableDirector.Stop();
        player.SetActive(true);
        cutscenePlayer.SetActive(false);
        cameraManager.cutScene01Active = false;
        input.canInput = true;
        Destroy(cutscenePlayer);
        Destroy(swordOBJ);
        Destroy(gameObject);
    }

    

}
