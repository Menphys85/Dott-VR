using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReceiver : MonoBehaviour
{
    public string objectToReceive;
    public UserInterface userInterface;

    public string verbalReaction;
    public string négativeVerbalReaction = "Que voulez-vous que je fasses de ça";
    public GameObject objectToInstanciate;
    public GameObject objectToHide;
    public PathFollower follower;

    // Start is called before the first frame update
    void Start()
    {
        userInterface = GameObject.Find("UserInterface").GetComponent<UserInterface>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == objectToReceive)
        {
            GameObject.Destroy(other);
            if (verbalReaction != null)
                userInterface.DisplayDescription(gameObject.name, verbalReaction);
            if (objectToInstanciate != null)
            {
                var transformToApply = gameObject;
                if (objectToHide != null)
                    transformToApply = objectToHide;

                var newObj = GameObject.Instantiate(objectToInstanciate, transformToApply.transform.position, transformToApply.transform.rotation);
                newObj.name = objectToInstanciate.name;
            }
            if (objectToHide != null)
                objectToHide.SetActive(false);
                
            if (follower != null)
            {
                StartCoroutine("startToFollow");
            }
                
        }
        else if (other.tag == "GrapableObject")
        {
            if(négativeVerbalReaction != null)
                userInterface.DisplayDescription(gameObject.name, négativeVerbalReaction);
        }

    }

    private IEnumerator startToFollow()
    {
        var animator = follower.gameObject.GetComponent<Animator>();

        yield return new WaitForSeconds(5);
        animator.SetBool("Walking", true);
        //animator.SetBool("Seat", false);
        yield return new WaitForSeconds(1);
        follower.enabled = true;
        yield return new WaitForSeconds(9);
        var gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.SaveGame();
    }

}
