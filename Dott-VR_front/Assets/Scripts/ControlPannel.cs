using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPannel : MonoBehaviour
{
    public string selectedEra;
    
    public WcButton button1;
    public WcButton button2;

    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        button1.SetUnPressed();
        selectedEra = button2.eraName;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SendObject(GameObject obj)
    {
        Era targetEra = gameManager.activeGame.eras.Find(e=> e.name == selectedEra);
        gameManager.SendObjectTo(obj, targetEra);
    }

    internal void ToogleButtons()
    {
        button1.inverseState();
        button2.inverseState();
    }
}
