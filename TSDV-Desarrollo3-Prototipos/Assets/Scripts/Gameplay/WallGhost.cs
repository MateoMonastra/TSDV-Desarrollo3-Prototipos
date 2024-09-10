using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGhost : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform trappingPos;

    [SerializeField] private GameObject ADMinigameObj;


    private bool playerCatched;

    void Start()
    {
        if(ADMinigameObj)
        {
            ADMinigameObj.GetComponentInChildren<ADMinigame>().OnWin += WallGhost_OnWin;
            ADMinigameObj.GetComponentInChildren<ADMinigame>().OnLose += WallGhost_OnLose;
        }
    }

    private void WallGhost_OnLose()
    {
        ADMinigameObj.SetActive(false);
        Debug.Log("Perdio");
        player.GetComponent<Running>().enabled = true;
        player.GetComponentInChildren<VacuumCleaner>().enabled = true;
        gameObject.SetActive(false);
        playerCatched = false;
    }

    private void WallGhost_OnWin()
    {
        ADMinigameObj.SetActive(false);
        Debug.Log("Gano");
        player.GetComponent<Running>().enabled = true;
        player.GetComponentInChildren<VacuumCleaner>().enabled = true;
        gameObject.SetActive(false);
        playerCatched = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CapturePlayer(other);
    }

    private void Update()
    {
        if (playerCatched)
        {
            player.transform.position = trappingPos.position;
        }
    }

    private void CapturePlayer(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer($"Player"))
        {
            player.transform.position = trappingPos.position;
            player.GetComponent<Running>().enabled = false;
            player.GetComponentInChildren<VacuumCleaner>().enabled = false;
            Debug.Log("Entro en el trigger");
            ADMinigameObj.SetActive(true);
            playerCatched = true;
        }
    }
}
