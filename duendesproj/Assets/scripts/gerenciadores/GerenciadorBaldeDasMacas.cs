using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;
using Identificadores;

namespace Gerenciadores
{
    public class GerenciadorBaldeDasMacas : MonoBehaviour
    {
        GerenciadorMJLib gerenMJ;

        public GameObject macaGbj;
        public float intervaloInstanciacao;
        public float velocidadeMov, velPulo, tamPulo;
        public float limX;

        float tempoPartidaAtual;

        void Awake()
        {
            gerenMJ = GetComponent<GerenciadorMJLib>();
        }

        void Start()
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
            float diferencaTempo = tempoPartida - tempoPartidaAtual;

            if (partidaIniciada && !partidaEncerrada
            && diferencaTempo >= intervaloInstanciacao)
            {
                tempoPartidaAtual = tempoPartida;
                InstanciarMaca();
            }
        }

        void AoIniciar()
        {
            AplicarControladorBaldeDasMacas();
            tempoPartidaAtual = gerenMJ.tempoPartida;
        }

        void AoTerminar()
        {
            RetirarControladorBaldeDasMacas();
            gerenMJ.jogadorCampeao = ObterCampeao();
        }

        void AplicarControladorBaldeDasMacas()
        {
            Transform[] tr_jogadores = gerenMJ.tr_jogadores;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                GameObject gbj_jogador = tr_jogadores[i].gameObject;
                gbj_jogador.AddComponent<ControladorBaldeDasMacas>();
            }
        }

        void RetirarControladorBaldeDasMacas()
        {
            Transform[] tr_jogadores = gerenMJ.tr_jogadores;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                GameObject gbj_jogador = tr_jogadores[i].gameObject;
                gbj_jogador.GetComponent<Movimentador>().velocidade = 0;
                Destroy(gbj_jogador.GetComponent<ControladorBaldeDasMacas>());
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
                Transform tr_j = gerenMJ.tr_jogadores[i];
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
