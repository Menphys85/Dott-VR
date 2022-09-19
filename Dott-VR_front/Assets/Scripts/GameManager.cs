using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Game activeGame = null;
    public Era activeEra = null;

    public NetworkManager networkManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void SaveGame()
    {
        Debug.Log("fonction save lancée dans le GameManager");
        
        var gos = GameObject.FindGameObjectsWithTag("GrapableObject");
        var grapableObjects = new List<GrapableObject>();
        
        Debug.Log("GameObjects trouvés: " + gos.Length);

        foreach (var go in gos)
        {
            
            Debug.Log("position en x de " + go.name +": " + go.transform.position.x);
            var grapableObject = new GrapableObject();
            grapableObject.name = go.name;
            grapableObject.position = go.transform.position;
            grapableObject.rotation = go.transform.rotation;
            grapableObjects.Add(grapableObject);
        }
        
        this.activeEra.grapableObjects = grapableObjects;
        
        networkManager.SaveEra(activeEra);
    }

    public void GetAreasOfTheActiveGame()
    {
        networkManager.GetErasOf(activeGame);
    }

    public void getObjectsData()
    {
        networkManager.GetGrapableObjects(activeEra);
    }

    public void UpdateObjects(GrapableObjectList objectList)
    {

        if (objectList.objects.Count == 0 )
        {
            return;
        }

        var gos = GameObject.FindGameObjectsWithTag("GrapableObject");

        foreach (var go in gos)
        {
            bool objectNotInTheObjectsList = objectList.objects.Find((obj) => obj.name == go.name) ==null ;
            if (objectNotInTheObjectsList)
            {
                GameObject.Destroy(go);
            }
            else
            {
                var objReceived = objectList.objects.Find((obj) => obj.name == go.name);
                Debug.Log("object: " + objReceived.name + " pos: X:" + objReceived.position.x + " y:" + objReceived.position.y + " z:" + objReceived.position.z);
                go.transform.position = objReceived.position;
                go.transform.rotation = objReceived.rotation;
            }
        }
    }
    


}
