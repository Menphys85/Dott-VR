using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Firesplash.UnityAssets.SocketIO;
using Models;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    public GameManager gameManager;
    private SocketIOCommunicator sioCom;
    public GameObject connectionScreen;
    public GameObject connectionStatus;
    public GameObject gameLists;
    public Transform listContent;
    public GameObject gameBox;

    private GameList gameList =null;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);       
        sioCom = GetComponent<SocketIOCommunicator>();
        //connectionStatus = connectionScreen.GetComponentInChildren(typeof(TextMeshPro)) as TextMeshPro;
        
        sioCom.Instance.On("connect", (payload) =>
        {
            Debug.Log("Connected! Socket ID: " + sioCom.Instance.SocketID);
            sioCom.Instance.Emit("getGames");
            sioCom.Instance.Emit("saveEra");
        });
        
        sioCom.Instance.On("disconnect", (payload) =>
        {
            Debug.LogWarning("Disconnected: " + payload);
        });
        
        sioCom.Instance.On("gamesListReceived", (payload) =>
        {
            Debug.LogWarning("Games list received from the server: " + payload);
            
            gameList = JsonUtility.FromJson(payload, typeof(GameList)) as GameList;

            gameList!.games.ForEach((game) =>
            {
                GameObject box = GameObject.Instantiate(gameBox, listContent);
                
                //Debug.Log(box);
                var GBScript = box.GetComponent("GameBox") as GameBox;
                //Debug.Log("script Game: " + GBScript.game);
                GBScript.game = game;
                
                var tmp = box.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
                tmp.text = game.name;
            });
                
        });
        //Debug.Log("Etat du socket avant la coroutine de connection:" + sioCom.Instance.IsConnected());
        
        
        
        //sioCom.Instance.Emit("saveEra" /*, eraJson*/);
        StartCoroutine(ConnectSocket());
        
        //Debug.Log("Etat du socket après la coroutine de connection:" + sioCom.Instance.IsConnected());
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ConnectSocket()
    {
        connectionScreen.SetActive(true);

        sioCom.Instance.Connect();
        
        yield return new WaitUntil(() => sioCom.Instance.IsConnected());
        
        TextMeshProUGUI Texte = connectionStatus.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        Texte.text = "Connected!";

        yield return new WaitForSeconds(2);
        
        gameLists.SetActive(true);
        connectionScreen.SetActive(false);
    }

    public void SaveEra(Era era)
    {
        //Debug.Log("nombre d'objets dans era: "+ era.grapableObjects.Count);
        //era.grapableObjects.ForEach( g=> Debug.Log(g.name));
        string eraJson = JsonUtility.ToJson(era);
        //Debug.Log("Emit area to the server : "+ era);
        sioCom.Instance.Emit("saveEra", eraJson,false);
        
    }
}
