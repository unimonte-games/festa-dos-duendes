using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Componentes.Jogador
{
    public class ControladorQuebraBotao : MonoBehaviour
    {
        Controlador ctrl;
        Movimentador mov;
        Gerenciadores.GerenciadorQuebraBotao gerenQB;

        void Awake ()
        {
            ctrl = GetComponent<Controlador>();
            mov = GetComponent<Movimentador>();
        }

        void Start ()
        {
            gerenQB = FindObjectOfType<Gerenciadores.GerenciadorQuebraBotao>();
            mov.velocidade = gerenQB.tamanhoPasso;
            mov.usarDeltaTime = false;
        }

        void Update ()
        {
            Controlador.EntradaJogador entradaJogador  =
                ctrl.ObterEntradaJogador();

            mov.direcao = entradaJogador.acao1 ? Vector3.forward : Vector3.zero;
        }
    }
}
