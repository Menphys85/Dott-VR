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
    
    // Start is called before the first frame update
    void Start()
    {
        
        gameManager = GameObject.Find("GameManager");
        gameList = GameObject.Find("GameList");
        characterSelection = GameObject.Find("CharactereSelection");
        
        Debug.Log(characterSelection);
        Debug.Log(gameList);
    }

   

    // Update is called once per frame
    void Update()
    {
      
    }

    public void loadGame()
    {
        var txt = gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        gameManager.GetComponent<GameManager>().activeGame = game;
        gameManager.GetComponent<GameManager>().GetAreasOfTheActiveGame();
        
        gameList.SetActive(false);
        //characterSelection.SetActive(true);
    }
    
}
