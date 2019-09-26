using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Componentes.Jogador;
using Identificadores;

namespace Gerenciadores
{
    public class GerenciadorQuebraBotao : MonoBehaviour
    {
        public GameObject[] duendesPrefab;

        public float duracaoPartida;

        Transform[] tr_jogadores;
        float tempoInicialPartida;

        public float tamanhoPasso;

        bool partidaIniciada;
        bool partidaEncerrada;

        void Start()
        {
            InstanciarJogadores();
        }

        void Update()
        {
            if (!partidaIniciada)
                return;

            float tempoAtual = Time.time;
            float diferencaTempo = tempoAtual - tempoInicialPartida;

            if (diferencaTempo > duracaoPartida && !partidaEncerrada)
            {
                EncerrarPartida();
                partidaEncerrada = true;
            }
        }

        void InstanciarJogadores()
        {
            tr_jogadores = new Transform[GerenciadorGeral.qtdJogadores];

            for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
            {
                GameObject novo_jogador = Instantiate<GameObject>(
                    duendesPrefab[i],
                    new Vector3(i*2f, 0f, 0f),
                    Quaternion.identity
                );

                tr_jogadores[i] = novo_jogador.GetComponent<Transform>();
            }
        }

        void AplicarControladorQuebraBotao ()
        {
            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                GameObject gbj_jogador = tr_jogadores[i].gameObject;
                gbj_jogador.AddComponent<ControladorQuebraBotao>();
            }
        }

        [ContextMenu("Iniciar Partida")]
        void IniciaPartida()
        {
            tempoInicialPartida = Time.time;
            partidaIniciada = true;
            AplicarControladorQuebraBotao();
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

            // itera sobre todos os jogadores, vê quem está mais longe
            // no eixo Z
            for (int i = 0; i < tr_jogadores.Length; i++)
            {
                float jogador_i_posz = tr_jogadores[i].position.z;
                if (jogador_i_posz > maisLonge)
                {
                    maisLonge = jogador_i_posz;
                    maisLonge_i = i;
                }
            }

            // Obtém o jogadorID do campeão
            IdentificadorJogador idJogador =
                tr_jogadores[maisLonge_i].GetComponent<IdentificadorJogador>();

            // retorna jogadorID obtido
            return idJogador.jogadorID;
        }
    }
}
