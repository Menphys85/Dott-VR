using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeToContent : MonoBehaviour
{
    public RectTransform currentTransform;
    public RectTransform childTransform;

    // Start is called before the first frame update
    void Start()
    {
        currentTransform.sizeDelta = childTransform.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
