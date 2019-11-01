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

        GerenciadorMJLib gerenMJ;
        float tempoPartidaAtual;

        ControladorCogumeloQuente[] controladores =
            new ControladorCogumeloQuente[4];

        CogumeloQuente_Cogumelo cogumeloComp;

        int indiceComCogumelo;
        int microPartida;

        void Awake()
        {
            gerenMJ = GetComponent<GerenciadorMJLib>();
        }

        void Start()
        {
            gerenMJ.evtAoIniciar.AddListener(AoIniciar);
            gerenMJ.evtAoTerminar.AddListener(AoTerminar);
        }

        void Update ()
        {
            if (!gerenMJ.partidaIniciada || gerenMJ.partidaEncerrada)
                return;

            bool partidaIniciada = gerenMJ.partidaIniciada;
            bool partidaEncerrada = gerenMJ.partidaEncerrada;
            float tempoPartida = gerenMJ.tempoPartida;
            float duracaoPartida = gerenMJ.duracaoPartida;

            float diferencaTempo = tempoPartida - tempoPartidaAtual;
            int qtdJogadores = GerenciadorGeral.qtdJogadores;

            if (
                tempoPartida - ((duracaoPartida/qtdJogadores) * microPartida)
                >= duracaoPartida/qtdJogadores
                && microPartida < qtdJogadores-1
            )
            {
                controladores[indiceComCogumelo].Queimar();
                microPartida++;
            }
            else if (partidaIniciada && !partidaEncerrada
            && diferencaTempo >= intervaloPassar)
            {
                tempoPartidaAtual = tempoPartida;
                PassarCogumelo();
            }
        }

        void AoIniciar()
        {
            AplicarControladorCogumeloQuente();
            tempoPartidaAtual = gerenMJ.tempoPartida;

            GameObject novoCogumeloGbj = Instantiate<GameObject>(
                cogumeloGbj, Vector3.zero, Quaternion.identity
            );

            cogumeloComp = novoCogumeloGbj.GetComponent<CogumeloQuente_Cogumelo>();

            cogumeloComp.DefinirAlvo(
                controladores[indiceComCogumelo].GetComponent<Transform>()
            );
        }

        void AoTerminar()
        {
            RetirarControladorCogumeloQuente();
            gerenMJ.jogadorCampeao = ObterCampeao();
        }

        void AplicarControladorCogumeloQuente()
        {
            Transform[] tr_jogadores = gerenMJ.tr_jogadores;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                GameObject gbj_jogador = tr_jogadores[i].gameObject;
                controladores[i] =
                    gbj_jogador.AddComponent<ControladorCogumeloQuente>();
            }
        }

        void RetirarControladorCogumeloQuente()
        {
            Transform[] tr_jogadores = gerenMJ.tr_jogadores;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                GameObject gbj_jogador = tr_jogadores[i].gameObject;
                Destroy(gbj_jogador.GetComponent<ControladorCogumeloQuente>());
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

        public void PassarCogumelo()
        {
            int qtdJogadores = GerenciadorGeral.qtdJogadores;

            do {
                controladores[indiceComCogumelo].comCogumelo = false;
                indiceComCogumelo = (indiceComCogumelo + 1) % qtdJogadores;
                controladores[indiceComCogumelo].comCogumelo = true;
            } while(!controladores[indiceComCogumelo].vivo);

            cogumeloComp.DefinirAlvo(
                controladores[indiceComCogumelo].GetComponent<Transform>()
            );

            tempoPartidaAtual = gerenMJ.tempoPartida;
        }
    }
}
