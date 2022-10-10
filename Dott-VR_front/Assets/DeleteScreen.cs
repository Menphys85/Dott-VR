using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteScreen : MonoBehaviour
{
    public GameObject gameBox;
    public Game game;
    public GameObject gameList;
    private NetworkManager netMan;

    private void Start()
    {
        netMan = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        //gameObject.SetActive(false);
    }

    public void DeleteTheGame()
    {
        netMan.DeleteGame(game);
        GameObject.Destroy(gameBox);
        gameList.SetActive(true);
        gameObject.SetActive(false);
    }
}
