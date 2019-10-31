using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripe : MonoBehaviour
{
    public float velMov, velRot;
    public Transform alvo, cam;

    Transform tr;

    void Awake ()
    {
        tr = GetComponent<Transform>();
    }

    void Update ()
    {
        if (alvo == null || cam == null)
            return;

        float dt = Time.deltaTime;

        Vector3 dirParaOlhar = tr.position - cam.position;
        Quaternion rotParaOlhar = Quaternion.LookRotation(dirParaOlhar);

        cam.rotation = Quaternion.Lerp(cam.rotation, rotParaOlhar, velRot * dt);

        Vector3 camEuler = cam.rotation.eulerAngles;
        camEuler.z = 0;
        cam.rotation = Quaternion.Euler(camEuler);

        tr.position = Vector3.Lerp(tr.position, alvo.position, velMov * dt);
    }
}
