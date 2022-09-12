using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager _gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        _gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    public void LoadHuagui()
    {
        SceneManager.LoadScene("PastScene");
    }
    
    public void LoadBernard()
    {
        SceneManager.LoadScene("PresentScene");
    }
    
    public void LoadLaverne()
    {
        SceneManager.LoadScene("FuturScene");
    }
   
}
