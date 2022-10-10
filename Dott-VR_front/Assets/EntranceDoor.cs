using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceDoor : MonoBehaviour
{
    
    public EntranceLeftDoor leftDoor;
    public EntranceRightDoor rightDoor;


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Hervé Concombre")
        {
            StartCoroutine("OpenDoors");            
        }
        
    }

    private IEnumerator OpenDoors()
    {
        leftDoor.open = true;
        rightDoor.open = true;
        yield return new WaitForSeconds(1);
        leftDoor.open = false;
        rightDoor.open = false;

    }

    private void CloseDoors()
    {
        leftDoor.open = false;
        rightDoor.open = false;
        
    }

}
