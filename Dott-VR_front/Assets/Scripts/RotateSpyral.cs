using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSpyral : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10.0f;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate( Vector3.forward, speed );
    }
}
