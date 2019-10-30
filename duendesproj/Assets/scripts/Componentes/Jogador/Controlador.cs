using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Componentes.Jogador {
    public class Controlador : MonoBehaviour
    {
        /// <summary>
        /// Contém os valores que representam os comandos do jogador, no caso,
        /// eixos horizontal e vertical para movimentação e botão de ação.
        /// </summary>
        public struct EntradaJogador
        {
            /// <summary>Valor do eixo horizontal.</summary>
            public float eixoH;

            /// <summary>Valor do eixo vertical.</summary>
            public float eixoV;

            /// <summary>
            /// Valor booliano indicando se o botão de ação
            /// foi pressionado (através do GetButtonDown).
            /// </summary>
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

        /// <summary>Retorna com a informação dos comandos do jogador.</summary>
        public EntradaJogador ObterEntradaJogador ()
        {
            return entradaJogador;
        }
    }
}
