using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gerenciadores;

namespace Componentes.Jogador
{
    public class ControladorCogumeloQuente : MonoBehaviour
    {
        public bool comCogumelo,
                    vivo;

        public bool acao1 = true;

        GerenciadorCogumeloQuente gerenCQ;
        Controlador ctrl;

        void Awake()
        {
            gerenCQ = FindObjectOfType<GerenciadorCogumeloQuente>();
            ctrl = GetComponent<Controlador>();
        }

        void Start()
        {
            vivo = true;
        }

        void Update()
        {
            Controlador.EntradaJogador entradaJogador =
                ctrl.ObterEntradaJogador();

            acao1 = entradaJogador.acao1;

            if (comCogumelo && acao1)
                gerenCQ.PassarCogumelo();
        }

        public void Queimar()
        {
            vivo = false;

            // TODO: ir pro canto
            // por hora isso substituirá:
            var p = transform.position;
            p.y = -20;
            transform.position = p;
        }

    }
}
