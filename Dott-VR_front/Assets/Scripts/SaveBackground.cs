using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBackground : MonoBehaviour
{
    public GameObject gameManager;
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gm = gameManager.GetComponent<GameManager>();
    }

    public void saveGame()
    {
        Debug.Log("Button pressed");
        gm.SaveGame();
    }
}
