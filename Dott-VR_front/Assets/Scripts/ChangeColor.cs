using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private Color initialColor;
    private Color HightLightColor;

    // Start is called before the first frame update
    void Start()
    {
        initialColor = gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toogleColor()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
