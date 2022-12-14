using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject networkManager;
    public GameManager gm;
    public GameObject player;
    public GameObject interFace ;

    private PercistentsDestructor destructor;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        networkManager = GameObject.Find("NetworkManager");
        gm = gameManager.GetComponent<GameManager>();
        player = GameObject.Find("Player");
    }

    public void SaveGame()
    {
        Debug.Log("Save pressed");
        gm.SaveGame();
        
    }
    
    public void ExitGame() 
    {
        Debug.Log("Exit pressed");
        gm.ExitGame();

        destructor = GameObject.Find("PercistentsDestructor").GetComponent<PercistentsDestructor>();
        destructor.DestroyPersistentsAndExit();

    }

  
}
