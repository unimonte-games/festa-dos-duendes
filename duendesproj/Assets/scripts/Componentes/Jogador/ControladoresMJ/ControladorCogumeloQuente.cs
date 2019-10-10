using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Componentes.Jogador
{
    public class ControladorCogumeloQuente : MonoBehaviour
    {
        public bool comCogumelo,
                    vivo;

        public void Queimar()
        {
            vivo = false;
            // TODO: ir pro canto
        }
    }
}
