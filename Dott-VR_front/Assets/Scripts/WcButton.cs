using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WcButton : MonoBehaviour
{
    public bool isPressed;
    public GameObject controlPannel;
    public string eraName;

  
    private void Awake()
    {
        isPressed = true;
        Debug.Log("installation du boolean a true");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed) {
            var cp = controlPannel.GetComponent<ControlPannel>();
            Debug.Log("triger déclenché");
            cp.ToogleButtons();
            cp.selectedEra = eraName;
        }
        
    }

    public void inverseState()
    {

        Debug.Log("InverseStateDéclenché pour le " + eraName);

        if (isPressed == true)
        {
            SetUnPressed();
        }
            
        else
            SetPressed();
    }

    public void SetPressed()
    {
        Debug.Log("bouton " + eraName + " setpressed");
        transform.localPosition = new Vector3(0,0.23f,0);
        GetComponent<Renderer>().material.color = Color.green;
        GetComponent<AudioSource>().Play();
        isPressed = true;
    }

    public void SetUnPressed()
    {

        Debug.Log("bouton " + eraName + " setUNpressed");
        transform.localPosition = new Vector3(0, 0.8f, 0);
        GetComponent<Renderer>().material.color = Color.red;
        isPressed = false;
    }

    
}
