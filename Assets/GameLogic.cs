using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    // GameObjects
    public GameObject playButton;
    public GameObject gameName;
    public GameObject noPlayers;
    public GameObject tutorialButton;
    public GameObject tutorial;
    public GameObject backButton;

    // Other files
    public InputHandling inputHandling;
    public PlayerSpawner spawner;

    private void Start()
    {
        inputHandling = GameObject.FindGameObjectWithTag("Player Logic").GetComponent<InputHandling>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<PlayerSpawner>();
    }

    public void StartGame()
    {
        playButton.SetActive(false);
        gameName.SetActive(false);
        noPlayers.SetActive(false);
        tutorialButton.SetActive(false);
        spawner.Spawner();
        StartRound(null);
    }

    public void StartRound(string killingPlatform)
    {
        inputHandling.checkWhoKilled(killingPlatform);
        inputHandling.TeleportObjects();
        inputHandling.gameStart = true;
        Debug.Log("Round started!");
    }

    public void Tutorial()
    {
        playButton.SetActive(false);
        gameName.SetActive(false);
        noPlayers.SetActive(false);
        tutorialButton.SetActive(false);

        tutorial.SetActive(true);
        backButton.SetActive(true);
    }

    public void BackButton()
    {
        tutorial.SetActive(false);
        backButton.SetActive(false);

        playButton.SetActive(true);
        gameName.SetActive(true);
        noPlayers.SetActive(true);
        tutorialButton.SetActive(true);
    }
}
