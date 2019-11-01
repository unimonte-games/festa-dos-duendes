using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLookAtCam : MonoBehaviour
{
    Transform cam, tr;

    void Awake()
    {
        cam = FindObjectOfType<Camera>().GetComponent<Transform>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tr.LookAt(cam, (Vector3.up + Vector3.forward).normalized);
    }
}
