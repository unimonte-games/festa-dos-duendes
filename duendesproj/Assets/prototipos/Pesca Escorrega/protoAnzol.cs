using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protoAnzol : MonoBehaviour
{
    public float velocidadeMaxMov, velocidadeMaxRot;
    public float aceleracaoMov, aceleracaoRot;
    public float desaceleracaoMov, desaceleracaoRot;

    public string teclaW, teclaA, teclaS, teclaD;

    Transform tr;

    float velMovFinal, velRotFinal;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(teclaD))
            AddRot(aceleracaoRot);
        else if (Input.GetKey(teclaA))
            AddRot(-aceleracaoRot);

        if (Input.GetKey(teclaW))
            AddMov(aceleracaoMov);
        else if (Input.GetKey(teclaS))
            AddMov(-aceleracaoMov);

        tr.Translate(0, 0, velMovFinal);
        tr.Rotate(0, velRotFinal, 0);

        velMovFinal *= desaceleracaoMov;
        velRotFinal *= desaceleracaoRot;
    }

    void AddMov(float vel)
    {
        velMovFinal = Mathf.Clamp(
            velMovFinal + vel, -velocidadeMaxMov, velocidadeMaxMov
        );
    }

    void AddRot(float vel)
    {
        velRotFinal = Mathf.Clamp(
            velRotFinal + vel, -velocidadeMaxRot, velocidadeMaxRot
        );
    }
}
