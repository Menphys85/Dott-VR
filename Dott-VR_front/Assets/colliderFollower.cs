using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderFollower : MonoBehaviour
{

    public Transform headTransform;
    public Vector3 capsulePosition;
    public Vector3 headPosition;

    // Start is called before the first frame update
    void Start()
    {
        capsulePosition = gameObject.GetComponent<CapsuleCollider>().center;
        headPosition = headTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(headTransform.localPosition.x, 0.91f, headTransform.localPosition.z);

        Debug.Log("head: (x:" + capsulePosition.x + ", y:" + capsulePosition.y + ", z:" + capsulePosition.z + ")");
    }
}
