using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    public GameObject menu;

    public GameObject dialAndDescription;
    public TextMeshProUGUI objectNameTMP;
    public TextMeshProUGUI objectDescriptionTMP;

    public void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }

    public void DisplayDescription(string objectName, string objectdescription)
    {
        StartCoroutine(StartDisplayDescription(objectName, objectdescription));
    }

    private IEnumerator StartDisplayDescription(string objectName, string objectDescription)
    {
        objectNameTMP.text = objectName;
        objectDescriptionTMP.text = objectDescription;
        dialAndDescription.SetActive(true);
        yield return new WaitForSeconds(10);
        dialAndDescription.SetActive(false);
    }  
    
    public void toogleMenu()
    {
        Debug.Log("Menu button pressed!");
        
        if(SceneManager.GetActiveScene().name != "Connection")
        {
            if (menu.activeSelf)
                menu.SetActive(false);
            else
                menu.SetActive(true);
        }
    }
}
