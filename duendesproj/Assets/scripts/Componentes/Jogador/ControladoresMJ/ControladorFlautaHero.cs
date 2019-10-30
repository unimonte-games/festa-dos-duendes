using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Componentes.Jogador
{
    public class ControladorFlautaHero : MonoBehaviour
    {
        public float pontos;

        Transform tr;
        Controlador ctrl;
        Movimentador mov;
        Gerenciadores.GerenciadorFlautaHero gerenFH;

        void Awake ()
        {
            tr = GetComponent<Transform>();
            ctrl = GetComponent<Controlador>();
            mov = GetComponent<Movimentador>();
            gerenFH = FindObjectOfType<Gerenciadores.GerenciadorFlautaHero>();
        }

        void Start ()
        {
            mov.velocidade = gerenFH.velocidadeMov;
        }

        void Update ()
        {
            Controlador.EntradaJogador entradaJogador =
                ctrl.ObterEntradaJogador();

            float eixoH = entradaJogador.eixoH;
            float eixoV = entradaJogador.eixoV;
            bool acao1 = entradaJogador.acao1;

            if (acao1) {
                CalcEAdicionaPonto();
            }
        }

        void CalcEAdicionaPonto()
        {
            pontos += gerenFH.CalcPonto();
        }
    }
}
