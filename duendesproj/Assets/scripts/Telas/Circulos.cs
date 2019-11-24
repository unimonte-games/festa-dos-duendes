using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;

namespace Telas
{
    public class Circulos : MonoBehaviour
    {
        void Start()
        {
            Transform tr = transform;

            for (int i = 0; i < tr.childCount; i++)
            {
                Transform circulo = tr.GetChild(i);
                var mov = circulo.GetComponent<Movimentador>();
                mov.velocidade = Random.Range(0.5f, 1f);
            }
        }
    }
}
