using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;
using Identificadores;

namespace Gerenciadores {
    public class GerenciadorPescaEscorrega : MonoBehaviour
    {
        public float aceleracao,
                     desaceleracao,
                     limiteArena,
                     velocidadeMax;

        public float intervaloInstanciacao;
        public int limitePescadosAoMar;

        public GameObject pescadoGbj;

        GerenciadorMJLib gerenciadorMJ;
        public static int pescadosAoMar;
        float tempoPartidaAtual;

        void Awake ()
        {
            gerenciadorMJ = GetComponent<GerenciadorMJLib>();
        }

        void Start ()
        {
            gerenciadorMJ.evtAoIniciar.AddListener(AoIniciar);
            gerenciadorMJ.evtAoTerminar.AddListener(AoTerminar);
        }

        void Update()
        {
            bool partidaIniciada = gerenciadorMJ.partidaIniciada;
            bool partidaEncerrada = gerenciadorMJ.partidaEncerrada;
            float tempoPartida = gerenciadorMJ.tempoPartida;

            if (gerenciadorMJ.partidaIniciada &&
            !gerenciadorMJ.partidaEncerrada &&
            tempoPartida - tempoPartidaAtual >= intervaloInstanciacao &&
            pescadosAoMar <= limitePescadosAoMar)
            {
                tempoPartidaAtual = tempoPartida;
                pescadosAoMar++;
                InstanciarPescado();
            }
        }

        void AoIniciar ()
        {
            AplicarControladoresPescaEscorrega();
            tempoPartidaAtual = gerenciadorMJ.tempoPartida;
        }

        void AoTerminar ()
        {
            JogadorID jogadorCampeao = ObterCampeao();
            GerenciadorGeral.PontuarCampeaoMJ(jogadorCampeao);
        }

        void AplicarControladoresPescaEscorrega ()
        {
            Transform[] tr_jogadores = gerenciadorMJ.tr_jogadores;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                GameObject gbj_jogaodor = tr_jogadores[i].gameObject;
                gbj_jogaodor.AddComponent<ControladorPescaEscorrega>();
            }
        }

        JogadorID ObterCampeao ()
        {
            int qtdMaxPescados = -1;
            JogadorID jid_ganhador = JogadorID.J1;

            for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
            {
                Transform tr_j = gerenciadorMJ.tr_jogadores[i];
                var ctrl = tr_j.GetComponent<ControladorPescaEscorrega>();

                if (ctrl.pescados >= qtdMaxPescados)
                {
                    qtdMaxPescados = ctrl.pescados;
                    var id_comp = tr_j.GetComponent<IdentificadorJogador>();
                    jid_ganhador = id_comp.jogadorID;
                }
            }

            return jid_ganhador;
        }

        void InstanciarPescado()
        {
            float limInstanciacaoP = -limiteArena + (limiteArena/10);
            float limInstanciacaoN = limiteArena - (limiteArena/10);

            GameObject novo_pescado = Instantiate<GameObject>(
                pescadoGbj,
                new Vector3(
                    Random.Range(limInstanciacaoN, limInstanciacaoP),
                    0,
                    Random.Range(limInstanciacaoN, limInstanciacaoP)
                ),
                Quaternion.identity
            );
        }
    }
}
