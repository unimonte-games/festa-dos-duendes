using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;
using Identificadores;

namespace Gerenciadores {
    public class GerenciadorBaldeDasMacas : MonoBehaviour
    {
        public GerenciadorMJLib gerenciadorMJ;

        public GameObject macaGbj;
        public float intervaloInstanciacao;
        public float velocidadeMov, velPulo, tamPulo;
        public float limX;

        float tempoPartidaAtual;

        void Awake()
        {
            gerenciadorMJ = GetComponent<GerenciadorMJLib>();
        }

        void Start()
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
            tempoPartida - tempoPartidaAtual >= intervaloInstanciacao)
            {
                tempoPartidaAtual = tempoPartida;
                InstanciarMaca();
            }
        }

        void AoIniciar()
        {
            AplicarControladorBaldeDasMacas();
            tempoPartidaAtual = gerenciadorMJ.tempoPartida;
        }

        void AoTerminar()
        {
            JogadorID jogadorCampeao = ObterCampeao();
            GerenciadorGeral.PontuarCampeaoMJ(jogadorCampeao);
        }

        void AplicarControladorBaldeDasMacas()
        {
            Transform[] tr_jogadores = gerenciadorMJ.tr_jogadores;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                GameObject gbj_jogaodor = tr_jogadores[i].gameObject;
                gbj_jogaodor.AddComponent<ControladorBaldeDasMacas>();
            }
        }

        void InstanciarMaca()
        {
            GameObject nova_maca = Instantiate<GameObject>(
                macaGbj, Vector3.zero, Quaternion.identity
            );

            Transform tr_nova_maca = nova_maca.transform;
            tr_nova_maca.localPosition = Vector3.zero;
        }

        JogadorID ObterCampeao()
        {
            int qtdMaxMacas = -1;
            JogadorID jid_ganhador = JogadorID.J1;

            for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
            {
                Transform tr_j = gerenciadorMJ.tr_jogadores[i];
                var ctrl = tr_j.GetComponent<ControladorBaldeDasMacas>();

                if (ctrl.macasPegas >= qtdMaxMacas)
                {
                    qtdMaxMacas = ctrl.macasPegas;
                    var id_comp = tr_j.GetComponent<IdentificadorJogador>();
                    jid_ganhador = id_comp.jogadorID;
                }
            }

            return jid_ganhador;
        }
    }
}
