using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Componentes.Jogador {
    public class Controlador : MonoBehaviour
    {
        public struct EntradaJogador
        {
            public float eixoH;
            public float eixoV;
            public bool  acao1;
            //public bool  acao2;
            //public bool  acao3;
        }

        ControlesJogador controles;
        EntradaJogador entradaJogador = new EntradaJogador();

        void Awake ()
        {
            controles = GetComponent<ControlesJogador>();
        }

        void Update ()
        {
            entradaJogador.eixoH = Input.GetAxisRaw(controles.eixoH);
            entradaJogador.eixoV = Input.GetAxisRaw(controles.eixoV);
            entradaJogador.acao1 = Input.GetButtonDown(controles.acao1);
            //entradaJogador.acao2 = Input.GetButtonDown(controles.acao2);
            //entradaJogador.acao3 = Input.GetButtonDown(controles.acao3);
        }

        public EntradaJogador ObterEntradaJogador ()
        {
            return entradaJogador;
        }
    }
}
