using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateNpc : MonoBehaviour
{
    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        var gm = gameManager.GetComponent<GameManager>();
        gm.getNpcsData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
