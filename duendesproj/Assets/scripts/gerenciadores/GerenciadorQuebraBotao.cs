using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;

namespace Gerenciadores
{
    public class GerenciadorQuebraBotao : MonoBehaviour
    {
        public float duracaoPartida;

        Transform[] tr_jogadores;
        float tempoInicialPartida;
        bool partidaEncerrada;

        void Start()
        {
            ObterTrJogadores();
        }

        void Update()
        {
            float tempoAtual = Time.time;
            if (tempoAtual - tempoInicialPartida > duracaoPartida && !partidaEncerrada)
            {
                EncerrarPartida();
                partidaEncerrada = true;
            }
        }

        void ObterTrJogadores()
        {
            GameObject[] jogadores = GameObject.FindGameObjectsWithTag("Player");

#if UNITY_EDITOR
            // caso algo dê errado, isso aqui vai nos salvar um tempão
            Debug.Assert(
                jogadores.Length == GerenciadorGeral.qtdJogadores,
                "Quantidade de jogadores encontrados não correponde à"
                + " quantidade de jogadores cadastrados!:\n"
                + "\tjogadores.Length = " + jogadores.Length.ToString() + " - "
                + "GerenciadorGeral.qtdJogadores = "
                + GerenciadorGeral.qtdJogadores.ToString() + ".",
                gameObject
            );
#endif

            tr_jogadores = new Transform[GerenciadorGeral.qtdJogadores];

            for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
                tr_jogadores[i] = jogadores[i].GetComponent<Transform>();
        }

        void IniciaPartida()
        {
            tempoInicialPartida = Time.time;
        }

        void EncerrarPartida()
        {
            JogadorID jogadorCampeao = ObterCampeao();
            GerenciadorGeral.PontuarCampeaoMJ(jogadorCampeao);
        }

        JogadorID ObterCampeao()
        {
            float maisLonge = -100000;
            int maisLonge_i = -1;

            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                float jogador_i_posz = tr_jogadores[i].position.z;
                if (jogador_i_posz > maisLonge)
                {
                    maisLonge = jogador_i_posz;
                    maisLonge_i = i;
                }
            }

            IdentificadorJogador idJogador =
                tr_jogadores[maisLonge_i].GetComponent<IdentificadorJogador>();

            return idJogador.jogadorID;
        }
    }
}
