using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pnj : MonoBehaviour
{
    public UserInterface userInterface;
    public string description;


    // Start is called before the first frame update
    void Start()
    {
        userInterface = GameObject.Find("UserInterface").GetComponent<UserInterface>();

    }

 
    public void OnObjectActive()
    {
        userInterface.DisplayDescription(gameObject.name, description);

    }
}
