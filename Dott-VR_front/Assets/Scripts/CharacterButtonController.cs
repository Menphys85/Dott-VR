using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonController : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager gm;
    public string eraName;
    public Button button;
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gm = gameManager.GetComponent<GameManager>();

        eraName = gameObject.name switch
        {
            "BernardButton" => "Present",
            "HuaguiButton" => "Past",
            "LaverneButton" => "Futur",
            _ => null
        };

        button = gameObject.GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gm.activeGame != null)
        {
            Era era = gm.activeGame.eras.Find(era =>
            {
                return era.name == eraName;
            });

            if(era !=null)
                button.interactable = era.isFree;
        }
        
    }
}
