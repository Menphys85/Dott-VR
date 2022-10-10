using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfficeDoor : MonoBehaviour
{
    public bool open = false;
    public AudioSource winVoice;
    public AudioSource winMusic;

    void Update()
    {
        if (open && transform.localRotation.eulerAngles.y <= 260.0f ) {
            Debug.Log(transform.localRotation.eulerAngles.y);
            transform.Rotate(0, 2, 0);  
        }
        if (!open && transform.localRotation.eulerAngles.y > 180)
        {
            Debug.Log(transform.localRotation.eulerAngles.y);
            transform.Rotate(0, -2, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Npc")
        {
            StartCoroutine("OpenOneShot");
        }
    }

    private IEnumerator OpenOneShot()
    {
        openDoor();
        yield return new WaitForSeconds(1);
        closeDoor();
    }

    public void openDoor()
    {
        open = true;
    }

    public void openDoorandWin()
    {
        open = true;
        winMusic.Play();
        winVoice.Play();
        StartCoroutine("loadEndScene");
    }

    private IEnumerator loadEndScene()
    {
        yield return new WaitForSeconds(2);

        var UI = GameObject.Find("UserInterface");
        var gm = GameObject.Find("GameManager");
        var player = GameObject.Find("Player");
        var networkManager = GameObject.Find("NetworkManager");

        GameObject.Destroy(UI);
        GameObject.Destroy(gm);
        GameObject.Destroy(player);
        GameObject.Destroy(networkManager);

        
        SceneManager.LoadSceneAsync("End");
    }

    public void closeDoor()
    {
        open = false;
    }

}
