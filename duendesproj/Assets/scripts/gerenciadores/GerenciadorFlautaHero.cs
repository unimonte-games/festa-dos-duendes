using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;
using Identificadores;

namespace Gerenciadores {
    public class GerenciadorFlautaHero : MonoBehaviour
    {
        public float velocidadeMov;
        GerenciadorMJLib gerenciadorMJ;

        void Awake ()
        {
            gerenciadorMJ = GetComponent<GerenciadorMJLib>();
        }

        void Start ()
        {
            gerenciadorMJ.evtAoIniciar.AddListener(AoIniciar);
            gerenciadorMJ.evtAoTerminar.AddListener(AoTerminar);
        }

        void Update ()
        {

        }

        void AoIniciar()
        {
            AplicarControladorFlautaHero();
        }

        void AoTerminar()
        {
            JogadorID jogadorCampeao = ObterCampeao();
            GerenciadorGeral.PontuarCampeaoMJ(jogadorCampeao);
        }

        void AplicarControladorFlautaHero()
        {
            Transform[] tr_jogadores = gerenciadorMJ.tr_jogadores;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                GameObject gbj_jogador = tr_jogadores[i].gameObject;
                gbj_jogador.AddComponent<ControladorFlautaHero>();
            }
        }

        JogadorID ObterCampeao()
        {
            int pontos = -1;
            JogadorID jid_ganhador = JogadorID.J1;

            for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
            {
                Transform tr_j = gerenciadorMJ.tr_jogadores[i];
                var ctrl = tr_j.GetComponent<ControladorFlautaHero>();

                if (ctrl.pontos >= pontos)
                {
                    pontos = ctrl.pontos;
                    var id_comp = tr_j.GetComponent<IdentificadorJogador>();
                    jid_ganhador = id_comp.jogadorID;
                }
            }

            return jid_ganhador;
        }
    }
}
