using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorObject : MonoBehaviour
{
    public UserInterface userInterface;
    public string description;


    // Start is called before the first frame update
    void Start()
    {
        userInterface = GameObject.Find("UserInterface").GetComponent<UserInterface>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnObjectActive()
    {
        userInterface.DisplayDescription(gameObject.name, description);

    }
}
