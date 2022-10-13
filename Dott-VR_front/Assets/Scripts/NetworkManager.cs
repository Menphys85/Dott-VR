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
using UnityEngine.SceneManagement;
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

        sioCom.Instance.On("gameCreated", (payload)=> {
            Debug.Log("Nouvelle partie reçue");
            var game = JsonUtility.FromJson<Game>(payload);
            GameObject box = GameObject.Instantiate(gameBox, listContent);

            var GBScript = box.GetComponent("GameBox") as GameBox;

            GBScript.game = game;

            var tmp = box.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
            tmp.text = game.name;
        });
        
        sioCom.Instance.On("erasReceived", payload =>
        {
            
            Debug.Log("Era received from the server");
            var eraList = JsonUtility.FromJson<EraList>(payload);
            gameManager.activeGame.eras = eraList.eras;

        });
        
        sioCom.Instance.On("GrapableObjectList", payload =>
        {
            Debug.Log("Grapable objects received from the server: " + payload);
            var objList = JsonUtility.FromJson<GrapableObjectList>(payload);
            //gameManager.UpdateObjects(objList);
            gameManager.GenerateObjects(objList);
        } );

        sioCom.Instance.On("NpcsList", payload =>
        {
            Debug.Log("Npcs received from the server: " + payload);
            var npcList = JsonUtility.FromJson<NpcList>(payload);
            gameManager.GenerateNpcs(npcList);
        });

        sioCom.Instance.On("PlayerJoin", payload => {
            Debug.Log(payload);
            var era = JsonUtility.FromJson<Era>(payload);
            Debug.Log("Player joined " + era.name);
            gameManager.PlayerJoin(era);
        });

        sioCom.Instance.On("PlayerLeave", payload => {

            var era = JsonUtility.FromJson<Era>(payload);
            Debug.Log("Player Leaved " + era.name);
            gameManager.PlayerLeft(era);
        });

        sioCom.Instance.On("reconnect", (payload) =>
        {
            if(gameManager.GetComponent<GameManager>().activeEra != null)
            {
                ConnectToEra(gameManager.GetComponent<GameManager>().activeEra);
            }
        });

        sioCom.Instance.On("GrapableObjectReceived", payload => {
            var obj = JsonUtility.FromJson<GrapableObject>(payload);
            gameManager.SpawnObjectFromWC(obj);
        });

        StartCoroutine(ConnectSocket());
    }

    public void UnsetIsNewForGame(Game game)
    {
        var gameJson = JsonUtility.ToJson(game);
        sioCom.Instance.Emit("UnsetIsNewForGame", gameJson, false);
    }

    internal void DeleteGame(Game gameToDelete)
    {
        var gameJson = JsonUtility.ToJson(gameToDelete);
        sioCom.Instance.Emit("DeleteGame", gameJson, false);
    }

    public void CreateNewGame()
    {
        sioCom.Instance.Emit("createGameRequired");
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

    public void sendObjectTo(GrapableObject obj)
    {
        
        var objJson = JsonUtility.ToJson(obj);

        sioCom.Instance.Emit("objectToSend", objJson, false);
        
    }

    public void GetErasOf(Game game)
    {
        var gameJson = JsonUtility.ToJson(game);
        Debug.Log("GameJson sended: " + gameJson);
        var result = new List<Era>();

        sioCom.Instance.Emit("getErasOf", gameJson, false);
    }

    public void GetGrapableObjects(Era eraOfTheObject)
    {
        var data = "{\"eraId\":" + eraOfTheObject.id + "}";
        Debug.Log("Sended to the server to get object: " + data);
        sioCom.Instance.Emit("getGrapableObjects", data , false);         
    }

    public void GetNpcs(Era eraOfTheObject)
    {
        var data = "{\"eraId\":" + eraOfTheObject.id + "}";
        Debug.Log("Sended to the server to get Npc: " + data);
        sioCom.Instance.Emit("getNpcs", data, false);
    }

    public void ConnectToEra(Era era)
    {
        Debug.LogWarning("Methode connect to Era lancées dans le netmanager pour '" + era.name + "'" );
        var eraJson = JsonUtility.ToJson(era);
        sioCom.Instance.Emit("connectToEra", eraJson, false);
        
    }

    public void ExitGame()
    {
        sioCom.Instance.Emit("ExitGame");
    }

}
