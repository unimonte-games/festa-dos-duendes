using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLookAtCam : MonoBehaviour
{
    public Vector3 worldUp = Vector3.up;
    Transform cam, tr;

    void Awake()
    {
        cam = FindObjectOfType<Camera>().GetComponent<Transform>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tr.LookAt(cam, worldUp);
    }
}
