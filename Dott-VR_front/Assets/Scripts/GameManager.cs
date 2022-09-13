using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

   
    public Game activeGame = null;
    public NetworkManager networkManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        Debug.Log("fonction save lancée dans le GameManager");
        
        var gos = GameObject.FindGameObjectsWithTag("GrapableObject");
        var grapableObjects = new List<GrapableObject>();
        
        Debug.Log("GameObjects trouvés: " + gos.Length);

        foreach (var go in gos)
        {
            Debug.Log(go.name);
            var grapableObject = new GrapableObject();
            grapableObject.name = go.name;
            grapableObject.position = go.transform.position;
            grapableObject.rotation = go.transform.rotation;
            grapableObjects.Add(grapableObject);
        }


        Era era = new Era(6,"Present",grapableObjects);
        
        networkManager.SaveEra(era);
    }

    
}
