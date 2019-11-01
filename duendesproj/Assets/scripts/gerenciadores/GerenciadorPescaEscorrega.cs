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

        GerenciadorMJLib gerenMJ;
        public static int pescadosAoMar;
        float tempoPartidaAtual;

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
            if (!gerenMJ.partidaIniciada || gerenMJ.partidaEncerrada)
                return;

            bool partidaIniciada = gerenMJ.partidaIniciada;
            bool partidaEncerrada = gerenMJ.partidaEncerrada;
            float tempoPartida = gerenMJ.tempoPartida;

            if (gerenMJ.partidaIniciada &&
            !gerenMJ.partidaEncerrada &&
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
            tempoPartidaAtual = gerenMJ.tempoPartida;
        }

        void AoTerminar ()
        {
            RetirarControladoresPescaEscorrega();
            gerenMJ.jogadorCampeao = ObterCampeao();
        }

        void AplicarControladoresPescaEscorrega ()
        {
            Transform[] tr_jogadores = gerenMJ.tr_jogadores;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                GameObject gbj_jogador = tr_jogadores[i].gameObject;
                gbj_jogador.AddComponent<ControladorPescaEscorrega>();
            }
        }

        void RetirarControladoresPescaEscorrega ()
        {
            Transform[] tr_jogadores = gerenMJ.tr_jogadores;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                GameObject gbj_jogador = tr_jogadores[i].gameObject;
                Destroy(gbj_jogador.GetComponent<ControladorPescaEscorrega>());
            }
        }

        JogadorID ObterCampeao ()
        {
            int qtdMaxPescados = -1;
            JogadorID jid_ganhador = JogadorID.J1;

            for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
            {
                Transform tr_j = gerenMJ.tr_jogadores[i];
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
