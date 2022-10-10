using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSreen : MonoBehaviour
{
    public void BeginGame()
    {
        SceneManager.LoadSceneAsync("Connection");
    }
}
