using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    public GameObject menu;

    public void toogleMenu()
    {
        Debug.Log("Menu button pressed!");
        if(menu.activeSelf)
            menu.SetActive(false);
        else
            menu.SetActive(true);
        
    }

    
    
}
