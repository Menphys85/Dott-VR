using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PercistentsDestructor : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject ui;
    private GameObject netManager;
    private GameObject player;

    // Start is called before the first frame update
    public void DestroyPersistentsAndExit()
    {
        gameManager = GameObject.Find("GameManager");
        ui = GameObject.Find("UserInterface");
        netManager = GameObject.Find("NetworkManager");
        player = GameObject.Find("Player");

        
        GameObject.Destroy(gameManager);
        GameObject.Destroy(player);
        GameObject.Destroy(ui);
        GameObject.Destroy(netManager);

        SceneManager.LoadScene("Connection");
    }
}
