using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WcObjectSpawner : MonoBehaviour
{
    public WcWater wcWater;

    void Start()
    {
        wcWater = GameObject.Find("water").GetComponent<WcWater>();
    }

    public void SpawnObject(GameObject objToSpawn)
    {
        Debug.Log("instanciation de " + objToSpawn.name);
        
        var newObject = GameObject.Instantiate(objToSpawn, gameObject.transform.position, gameObject.transform.rotation);
        newObject.name = objToSpawn.name;
        newObject.GetComponent<Rigidbody>().AddForce(0,4,1, ForceMode.Impulse);
        wcWater.teleportMusic.Play();
        
    }
}
