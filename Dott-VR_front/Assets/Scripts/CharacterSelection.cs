using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager _gameManagerScript;

    public GameObject gameList;

    // Start is called before the first frame update
    void Start()
    {
        _gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    public void LoadHuagui()
    {
        SceneManager.LoadScene("PastScene");
        var gameManagerScript = gameManager.GetComponent<GameManager>();
        var disponiblesEras = gameManagerScript.activeGame.eras;
        gameManagerScript.EnterInEra(disponiblesEras.Find(e => e.name == "Past"));
        
    }
    
    public void LoadBernard()
    {
        SceneManager.LoadScene("PresentScene");
        var gameManagerScript = gameManager.GetComponent<GameManager>();
        var disponiblesEras = gameManagerScript.activeGame.eras;
        gameManagerScript.EnterInEra(disponiblesEras.Find(e => e.name == "Present"));
    }
    
    public void LoadLaverne()
    {
        SceneManager.LoadScene("FuturScene");
        var gameManagerScript = gameManager.GetComponent<GameManager>();
        var disponiblesEras = gameManagerScript.activeGame.eras;
        gameManagerScript.EnterInEra(disponiblesEras.Find(e => e.name == "Futur"));
    }

    public void BackToGamelist()
    {
        gameList.SetActive(true);
    }
   
}
