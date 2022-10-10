using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Remote : MonoBehaviour
{

    public PlayVideo vid�oPlayer;

    // Start is called before the first frame update
    void Start()
    {
        vid�oPlayer = GameObject.Find("Screen").GetComponent<PlayVideo>();
    }

    public void TooglePlayStop()
    {
        vid�oPlayer.TogglePlayStop();
    }
}
