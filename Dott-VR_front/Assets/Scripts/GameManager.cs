using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Game activeGame = null;
    public Era activeEra = null;

    public List<GameObject> objectsPrefabs;
    public List<GameObject> npcsPrefabs;

    public NetworkManager networkManager;

    public GameObject playerActivityUI;
    public WcObjectSpawner wcObjectSpawner;

    public GameObject huaguiPic;
    public GameObject bernardPic;
    public GameObject lavernePic ;




    void Start(){
        DontDestroyOnLoad(this.gameObject);
    }

    public void CreateNewGame()
    {
        networkManager.CreateNewGame();
    }

    public void SaveGame()
    {
        //création de la liste des GrapableObjects
        var gosGapObjs = GameObject.FindGameObjectsWithTag("GrapableObject");
        var grapableObjects = new List<GrapableObject>();

        foreach (var go in gosGapObjs)
        {
            var grapableObject = new GrapableObject();
            grapableObject.name = go.name;
            grapableObject.position = go.transform.position;
            grapableObject.rotation = go.transform.rotation;
            grapableObjects.Add(grapableObject);
        }


        //création de la liste des Npcs
        var gosNpcs = GameObject.FindGameObjectsWithTag("Npc");
        var npcs = new List<Npc>();


        foreach (var go in gosNpcs)
        {
            var npc = new Npc();
            npc.name = go.name;
            npc.position = go.transform.position;
            npc.rotation = go.transform.rotation;
            npcs.Add(npc);
        }

        this.activeEra.grapableObjects = grapableObjects;
        this.activeEra.npcs = npcs;
        networkManager.SaveEra(activeEra);

        if (activeGame.isNew)
        {
            activeGame.isNew = false;
            networkManager.UnsetIsNewForGame(activeGame);
        }
    }

    internal void SendObjectTo(GameObject obj, Era targetEra)
    {
        var grapObj = new GrapableObject(); 

        grapObj.name = obj.name;
        grapObj.era_id = targetEra.id;
        grapObj.position = new Vector3(-4.3f, 1, -4.3f);
        grapObj.rotation = new Quaternion(0, 0, 0,0.9f);

        networkManager.sendObjectTo(grapObj);
        GameObject.Destroy(obj);
    }

    public void GetAreasOfTheActiveGame()
    {
        networkManager.GetErasOf(activeGame);

    }

    public void getObjectsData()
    {
        networkManager.GetGrapableObjects(activeEra);
    }

    public void getNpcsData()
    {
        networkManager.GetNpcs(activeEra);
    }

    public void UpdateObjects(GrapableObjectList objectList)
    {

        if (objectList.objects.Count == 0)
        {
            return;
        }

        var gos = GameObject.FindGameObjectsWithTag("GrapableObject");

        foreach (var go in gos)
        {
            bool objectNotInTheObjectsList = objectList.objects.Find((obj) => obj.name == go.name) == null;
            if (objectNotInTheObjectsList)
            {
                GameObject.Destroy(go);
            }
            else
            {
                var objReceived = objectList.objects.Find((obj) => obj.name == go.name);
                Debug.Log("object: " + objReceived.name + " pos: X:" + objReceived.position.x + " y:" + objReceived.position.y + " z:" + objReceived.position.z);
                go.transform.position = objReceived.position;
                go.transform.rotation = objReceived.rotation;
            }
        }
    }

    internal void GenerateObjects(GrapableObjectList objList)
    {
        
        // Si la partie est nouvelle, et qu'elle n'a pas encore de GrapableObject à ce stade, on en profite donc pour placer
        // les objets dans chaque era et les enregistrer en base de données.

        if (activeGame.isNew && objList.objects.Count == 0)
        {
            Era past = activeGame.eras.Find(e => e.name == "Past");
            Era present = activeGame.eras.Find(e => e.name == "Present");
            Era futur = activeGame.eras.Find(e => e.name == "Futur");

            past.grapableObjects = new List<GrapableObject>();
            present.grapableObjects = new List<GrapableObject>();
            futur.grapableObjects = new List<GrapableObject>();

            objectsPrefabs.ForEach(go => { 
               
                //On ne tient pas compte de la carte d'accès et du parchemin et de la carte d'accès qui ne doivent pas être instanciées au début du jeu.

                if(go.name != "Carte d'accès" && go.name != "Parchemin")
                {
                    var grapObj = new GrapableObject();

                    grapObj.name = go.name;
                    grapObj.position = go.transform.position;
                    grapObj.rotation = go.transform.rotation;

                    if (go.name == "Bouteille")
                    {
                        grapObj.era_id = past.id;
                        past.grapableObjects.Add(grapObj);
                    } 

                    else if (go.name == "Chapeau de cowboy")
                    {
                        grapObj.era_id = present.id;
                        present.grapableObjects.Add(grapObj);
                    }
                        

                    else if (go.name == "Chapeau de sorcière")
                    {
                        grapObj.era_id = present.id;
                        present.grapableObjects.Add(grapObj);
                    }
                        

                    else if (go.name == "Clé sécurisée")
                    {
                        grapObj.era_id = futur.id;
                        futur.grapableObjects.Add(grapObj);
                    }
                        

                    else if (go.name == "Livre")
                    {
                        grapObj.era_id = present.id;
                        present.grapableObjects.Add(grapObj);
                    }
                        

                    else if (go.name == "Remote")
                    {
                        grapObj.era_id = present.id;
                        present.grapableObjects.Add(grapObj);
                    }
                        

                    else if (go.name == "Stylo")
                    {
                        grapObj.era_id = present.id;
                        present.grapableObjects.Add(grapObj);
                    }

                    if(grapObj.era_id == activeEra.id)
                    {
                        objList.objects.Add(grapObj);
                    }
                }

            });

            npcsPrefabs.ForEach(npcPref => {

                //On ne tient pas compte de la carte d'accès et du parchemin et de la carte d'accès qui ne doivent pas être instanciées au début du jeu.

                
                var npcObj = new Npc();

                npcObj.name = npcPref.name;
                npcObj.position = npcPref.transform.position;
                npcObj.rotation = npcPref.transform.rotation;

                if (npcPref.name == "Hervé Concombre")
                {
                    npcObj.era_id = futur.id;
                    futur.npcs.Add(npcObj);
                }

                else if (npcPref.name == "John Doeuf")
                {
                    npcObj.era_id = past.id;
                    past.npcs.Add(npcObj);
                }

                if (npcObj.era_id == activeEra.id)
                {
                    var newNpc = GameObject.Instantiate(npcPref);
                    newNpc.name = npcPref.name;
                }
                
            });



            //Maintenant que chaque era a reçu ces Objets, on les sauve en DB.
            networkManager.SaveEra(past);
            networkManager.SaveEra(present);
            networkManager.SaveEra(futur);
        };

        objList.objects.ForEach(obj => {
            Debug.Log("instanciation de l'objet " + obj.name);
            var prefab = objectsPrefabs.Find(o => o.name == obj.name);
            var newObj = GameObject.Instantiate(prefab, obj.position, obj.rotation);
            newObj.name = obj.name;
        });
    }

    internal void GenerateNpcs(NpcList npcList)
    {
        npcList.npcs.ForEach(npc => {
            Debug.Log("instanciation de l'objet " + npc.name);
            var prefab = npcsPrefabs.Find(n => n.name == npc.name);
            var newObj = GameObject.Instantiate(prefab, npc.position, npc.rotation);
            newObj.name = npc.name;
        });
    }

    public void EnterInEra(Era era)
    {
        this.activeEra = era;
        networkManager.ConnectToEra(era);
    }

    public void ExitGame()
    {
        networkManager.ExitGame();
    }

    public void PlayerJoin(Era era)
    {
        if(activeGame != null)
        {
            activeGame.eras.ForEach(e => { 
                if(e.id == era.id)
                {
                    e.isFree = false;
                }
            });

            StartCoroutine("DisplayJoinTheGame", era);
        };

    }

    public void SpawnObjectFromWC(GrapableObject obj)
    {
        if(activeEra.id == obj.era_id)
        {
            Debug.Log("Object recu par le WC Spawner avec le nom: " + obj.name);

            var prefab = objectsPrefabs.Find(o => o.name.Equals(obj.name));
            wcObjectSpawner = GameObject.Find("WcObjectSpawner").GetComponent<WcObjectSpawner>();
            wcObjectSpawner.SpawnObject(prefab);
        }
    }

    public void PlayerLeft(Era era)
    {
        activeGame.eras.ForEach(e => {
            if (e.id == era.id)
            {
                e.isFree = true;
            }
        });

        StartCoroutine("DisplayLeftTheGame", era);
    }

    private IEnumerator DisplayJoinTheGame(Era era)
    {
        
        TextMeshProUGUI tmp = playerActivityUI.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;

        

        string characterName;

        if (era.name == "Past")
        {
            characterName = "Huagui";
            
            huaguiPic.SetActive(true);
        }
            

        else if (era.name == "Present")
        {
            characterName = "Bernard";
            bernardPic.SetActive(true);
        }

        else
        {
            characterName = "Laverne";
            lavernePic.SetActive(true);
        }
            

        tmp.text = characterName + " a rejoint la partie.";

        playerActivityUI.SetActive(true);

        yield return new WaitForSeconds(3);

        playerActivityUI.SetActive(false);
        huaguiPic.SetActive(false);
        bernardPic.SetActive(false);
        lavernePic.SetActive(false);
        yield return null;
    }

    private IEnumerator DisplayLeftTheGame(Era era)
    {

        TextMeshProUGUI tmp = playerActivityUI.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        string characterName;

        if (era.name == "Past")
        {
            characterName = "Huagui";

            huaguiPic.SetActive(true);
        }


        else if (era.name == "Present")
        {
            characterName = "Bernard";
            bernardPic.SetActive(true);
        }

        else
        {
            characterName = "Laverne";
            lavernePic.SetActive(true);
        }

        tmp.text = characterName + " a quité la partie.";

        playerActivityUI.SetActive(true);

        yield return new WaitForSeconds(3);

        playerActivityUI.SetActive(false);
        huaguiPic.SetActive(false);
        bernardPic.SetActive(false);
        lavernePic.SetActive(false);
        yield return null;
    }

    public void Exit()
    {
        Debug.Log("ExitGame lancé");
        Application.Quit();
    }

    
}
