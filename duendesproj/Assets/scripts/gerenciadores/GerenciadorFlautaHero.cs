using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;
using Identificadores;

namespace Gerenciadores {
    public class GerenciadorFlautaHero : MonoBehaviour
    {
        public float velocidadeMov;

        [HideInInspector]
        public GerenciadorMJLib gerenMJ;

        public float[] tempos = {
            1, 2, 3, 4, 5, 6,
            7.2f, 7.4f, 8f,
        };

        public int temposAtual = 0;
        public bool atualUtilizado;

        float tempoInicio;

        void Awake ()
        {
            gerenMJ = GetComponent<GerenciadorMJLib>();
        }

        void Start ()
        {
            gerenMJ.evtAoIniciar.AddListener(AoIniciar);
            gerenMJ.evtAoTerminar.AddListener(AoTerminar);
        }

        void Update()
        {
            if (temposAtual < tempos.Length-1)
            {
                if (gerenMJ.tempoPartida+1 >= tempos[temposAtual])
                {
                    atualUtilizado = false;
                    temposAtual++;
                }
            }
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
            Transform[] tr_jogadores = gerenMJ.tr_jogadores;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                GameObject gbj_jogador = tr_jogadores[i].gameObject;
                gbj_jogador.AddComponent<ControladorFlautaHero>();
            }
        }

        JogadorID ObterCampeao()
        {
            float pontos = 0;

            JogadorID jid_ganhador = JogadorID.J1;

            for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
            {
                Transform tr_j = gerenMJ.tr_jogadores[i];
                var ctrl = tr_j.GetComponent<ControladorFlautaHero>();

                if (ctrl.pontos >= pontos)
                {
                    var id_comp = tr_j.GetComponent<IdentificadorJogador>();
                    jid_ganhador = id_comp.jogadorID;
                }
            }

            return jid_ganhador;
        }
    }
}
