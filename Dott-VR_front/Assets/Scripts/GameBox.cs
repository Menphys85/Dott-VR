using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBox : MonoBehaviour
{

    public Game game;
    private GameObject gameManager;
    
    private GameObject gameList;
    private GameObject characterSelection;
    private GameObject deleteScreen;
    private TextMeshProUGUI deleteScreentitle;



    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameList = GameObject.Find("GameList");
        characterSelection = GameObject.Find("CharacterSelection");
        deleteScreen = GameObject.Find("DeleteScreen");

        deleteScreentitle = GameObject.Find("DeteleTitle").GetComponent<TextMeshProUGUI>();
    }


    public void loadGame()
    {
        var txt = gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        gameManager.GetComponent<GameManager>().activeGame = game;
        gameManager.GetComponent<GameManager>().GetAreasOfTheActiveGame();

        gameList.SetActive(false);
        characterSelection.SetActive(true);
        deleteScreen.SetActive(false);
    }

    public void DeleteGame()
    {
        deleteScreentitle.text = "Suprimer " + game.name;
        deleteScreen.SetActive(true);
        deleteScreen.GetComponent<DeleteScreen>().gameBox = gameObject;
        deleteScreen.GetComponent<DeleteScreen>().game = game;
        gameList.SetActive(false);
    }
    
}
