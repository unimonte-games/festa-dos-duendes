using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Componentes.Jogador
{
    public class ControladorFlautaHero : MonoBehaviour
    {
        public int pontos;

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


        }
    }
}
