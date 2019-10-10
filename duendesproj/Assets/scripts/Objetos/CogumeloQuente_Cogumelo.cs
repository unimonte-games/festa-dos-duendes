using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gerenciadores;

public class CogumeloQuente_Cogumelo : MonoBehaviour
{
    Transform alvo_tr;
    Transform tr;
    GerenciadorCogumeloQuente gerenCQ;

    void Awake()
    {
        tr = GetComponent<Transform>();
        gerenCQ = FindObjectOfType<GerenciadorCogumeloQuente>();
    }

    void Update()
    {
        tr.position = Vector3.Slerp(
            tr.position,
            alvo_tr.position,
            gerenCQ.velocidadeCogumelo * Time.deltaTime
        );
    }

    public void DefinirAlvo(Transform def)
    {
        alvo_tr = def;
    }
}
