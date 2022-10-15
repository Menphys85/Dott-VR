using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alignRotationOn : MonoBehaviour
{
    public Transform headset;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("resetRotation");
    }

    private void LateUpdate()
    {

    }
    private IEnumerator resetRotation()
    {
        yield return new WaitUntil( () => headset.rotation.eulerAngles.y>0) ;
        Debug.Log(headset.rotation.eulerAngles.y);
        gameObject.transform.Rotate(0, -headset.rotation.eulerAngles.y, 0);

    }
}
