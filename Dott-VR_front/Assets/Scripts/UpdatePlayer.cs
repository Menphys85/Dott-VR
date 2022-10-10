using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayer : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        player.transform.position = new Vector3(-4.34f, 0, -4.049f);
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
