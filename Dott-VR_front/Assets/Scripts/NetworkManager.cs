using System;
using System.Collections;
using System.Collections.Generic;
using Firesplash.UnityAssets.SocketIO;
using Models;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    private SocketIOCommunicator sioCom;
    public GameObject connectionScreen;
    public GameObject connectionStatus;
    public GameObject gameLists;
    public Transform listContent;
    public GameObject gameBox;
    
    // Start is called before the first frame update
    void Start()
    {
               
        sioCom = GetComponent<SocketIOCommunicator>();
        //connectionStatus = connectionScreen.GetComponentInChildren(typeof(TextMeshPro)) as TextMeshPro;
        
        sioCom.Instance.On("connect", (payload) =>
        {
            Debug.Log("Connected! Socket ID: " + sioCom.Instance.SocketID);
            sioCom.Instance.Emit("getGames");
        });
        
        sioCom.Instance.On("disconnect", (payload) =>
        {
            Debug.LogWarning("Disconnected: " + payload);
        });
        
        sioCom.Instance.On("gamesListReceived", (payload) =>
        {
            Debug.LogWarning("Games list received from the server: " + payload);
            
            GameList gameList = JsonUtility.FromJson(payload, typeof(GameList)) as GameList;
            
            gameList.games.ForEach((e) =>
            {
                GameObject.Instantiate(gameBox, listContent);
            });
                
        });
        
        StartCoroutine(ConnectSocket());
        
        
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
}
