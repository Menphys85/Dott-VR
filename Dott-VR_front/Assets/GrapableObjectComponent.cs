using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrapableObjectComponent : MonoBehaviour
{
    public UserInterface userInterface;
    public string description;

    void Start()
    {
        userInterface = GameObject.Find("UserInterface").GetComponent<UserInterface>();
        
    }

    public void OnObjectActive()
    {
        userInterface.DisplayDescription(gameObject.name, description);

    } 
}
