using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Remote : MonoBehaviour
{

    public PlayVideo vidéoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        vidéoPlayer = GameObject.Find("Screen").GetComponent<PlayVideo>();
    }

    public void TooglePlayStop()
    {
        vidéoPlayer.TogglePlayStop();
    }
}
