using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gerenciadores;
using Identificadores;

namespace Telas
{
    public class TelaVencedor : MonoBehaviour
    {
        public GameObject[] jogadores;

        void Start()
        {
            jogadores[(int)GerenciadorGeral.vencedorID].SetActive(true);
        }
    }
}
