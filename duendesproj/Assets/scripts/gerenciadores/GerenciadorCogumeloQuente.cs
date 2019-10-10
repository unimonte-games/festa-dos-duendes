using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;
using Identificadores;

namespace Gerenciadores
{
    public class GerenciadorCogumeloQuente : MonoBehaviour
    {
        public GameObject cogumeloGbj;
        public float intervaloPassar,
                     velocidadeCogumelo;

        GerenciadorMJLib gerenciadorMJ;
        float tempoPartidaAtual;

        ControladorCogumeloQuente[] controladores =
            new ControladorCogumeloQuente[4];

        CogumeloQuente_Cogumelo cogumeloComp;

        int indiceComCogumelo;

        void Awake()
        {
            gerenciadorMJ = GetComponent<GerenciadorMJLib>();
        }

        void Start()
        {
            gerenciadorMJ.evtAoIniciar.AddListener(AoIniciar);
            gerenciadorMJ.evtAoTerminar.AddListener(AoTerminar);
        }

        void Update ()
        {
            bool partidaIniciada = gerenciadorMJ.partidaIniciada;
            bool partidaEncerrada = gerenciadorMJ.partidaEncerrada;
            float tempoPartida = gerenciadorMJ.tempoPartida;
            float diferencaTempo = tempoPartida - tempoPartidaAtual;

            if (partidaIniciada && !partidaEncerrada
            && diferencaTempo >= intervaloPassar)
            {
                tempoPartidaAtual = tempoPartida;
                PassarCogumelo();
            }
        }

        void AoIniciar()
        {
            AplicarControladorCogumeloQuente();
            tempoPartidaAtual = gerenciadorMJ.tempoPartida;

            cogumeloComp = cogumeloGbj.GetComponent<CogumeloQuente_Cogumelo>();

            cogumeloComp.DefinirAlvo(
                controladores[indiceComCogumelo].GetComponent<Transform>()
            );
        }

        void AoTerminar()
        {
            JogadorID jogadorCampeao = ObterCampeao();
            GerenciadorGeral.PontuarCampeaoMJ(jogadorCampeao);
        }

        void AplicarControladorCogumeloQuente()
        {
            Transform[] tr_jogadores = gerenciadorMJ.tr_jogadores;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                GameObject gbj_jogaodor = tr_jogadores[i].gameObject;
                controladores[i] =
                    gbj_jogaodor.AddComponent<ControladorCogumeloQuente>();
            }
        }

        JogadorID ObterCampeao()
        {
            JogadorID jid_ganhador = JogadorID.J1;

            for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
            {
                var ctrl_i = controladores[i];

                if (ctrl_i.vivo)
                {
                    var id_comp = ctrl_i.GetComponent<IdentificadorJogador>();
                    jid_ganhador = id_comp.jogadorID;
                    break;
                }
            }

            return jid_ganhador;
        }

        void PassarCogumelo()
        {
            controladores[indiceComCogumelo].comCogumelo = false;
            indiceComCogumelo = Mathf.Clamp(indiceComCogumelo + 1, 0, 3);
            controladores[indiceComCogumelo].comCogumelo = true;

            cogumeloComp.DefinirAlvo(
                controladores[indiceComCogumelo].GetComponent<Transform>()
            );

        }
    }
}
