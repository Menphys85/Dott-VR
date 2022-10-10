using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceRightDoor : MonoBehaviour
{
    public bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            if (gameObject.transform.localPosition.x < 4.13f)
            {
                gameObject.transform.Translate(Vector3.right * 0.01f);
            }
        }
        else
        {
            if (gameObject.transform.localPosition.x >= 3.12f)
            {
                gameObject.transform.Translate(Vector3.left * 0.01f);
            }
        }
    }
}
