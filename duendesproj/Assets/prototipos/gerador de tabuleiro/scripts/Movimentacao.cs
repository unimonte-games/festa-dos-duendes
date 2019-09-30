using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    public Transform casaAtual;
    [HideInInspector]
    public int lado = 0;

    void Start()
    {
        setCasa(casaAtual);
    }

    public void setCasa(Transform novaCasa)
    {
        casaAtual = novaCasa;
        transform.position = casaAtual.position;
    }

    public void ProcuraCasa(int tipo)
    {
        bool achou = false;
        Transform proximaCasa = casaAtual;
        int tipoCasa;

        do
        {
            proximaCasa = proximaCasa.GetComponent<CasaBase>().casaSeguinte[lado];
            tipoCasa = proximaCasa.GetComponent<CasaBase>().tipoCasa;

            if (tipoCasa == 0)
            {
                CasaBase _casaBase = proximaCasa.GetComponent<CasaBase>();
                //if (_casaBase.casaSeguinte.Count > 1)
            }
            else if (tipoCasa == tipo)
            {
                achou = true;
                casaAtual = proximaCasa;
            }

        } while (!achou);

        setCasa(casaAtual);
    }
}
