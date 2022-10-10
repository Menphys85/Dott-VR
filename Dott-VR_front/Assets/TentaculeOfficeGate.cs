using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaculeOfficeGate : MonoBehaviour
{
    public GameObject accessController;
    public OfficeLeftDoor leftDoor;
    public OfficeRightDoor rightDoor;


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Carte d'accès")
        {
            accessController.GetComponent<AudioSource>().Play();
            if (leftDoor.open == false)
            {
                OpenDoors();
            }
            else
                CloseDoors();
            
            
        }
        
    }

    private void OpenDoors()
    {
        leftDoor.open = true;
        rightDoor.open = true;
        accessController.GetComponent<Renderer>().material.SetColor("_EmissionColor",Color.green);
    }

    private void CloseDoors()
    {
        leftDoor.open = false;
        rightDoor.open = false;
        accessController.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
    }

}
