using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLookAtCam : MonoBehaviour
{
    public bool usarWorldUp;
    Transform cam, tr;

    void Awake()
    {
        cam = FindObjectOfType<Camera>().GetComponent<Transform>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (usarWorldUp)
            tr.LookAt(cam);
        else
            tr.LookAt(cam, (Vector3.up + Vector3.forward).normalized);
    }
}
