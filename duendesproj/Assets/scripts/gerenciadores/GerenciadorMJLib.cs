using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Componentes.Jogador;
using Identificadores;

namespace Gerenciadores {
    public class GerenciadorMJLib : MonoBehaviour
    {
        /// <summary>
        /// Corresponde aos prefabs dos 4 personagens jogáveis que serão
        /// instanciados em jogo; deve ser preenchido na edição da
        /// cena do minijogo.
        /// </summary>
        public GameObject[] duendesPrefab;

        float tempoInicialPartida;

        /// <summary>
        /// quando a diferença entre o tempo de jogo e o tempo do início da
        /// partida do minijogo ultrapassar esse número, a partida do minijogo
        /// se finalizará definindo partidaEncerrada como verdadeiro e a
        /// chamada de evtAoTerminar.
        /// </summary>
        public float duracaoPartida;

        [Header("Não modifique pelo Inspector:")]
        /// <summary>
        /// Componentes de transformação dos jogadores instanciados,
        /// é gerenciado automaticamente e não deve ser modificado externamente.
        /// </summary>
        public Transform[] tr_jogadores;

        /// <summary>
        /// É a diferença entre o tempo de jogo e o tempo do início da partida
        /// do minijogo; não deve ser modificada externamente.
        /// </summary>
        public float tempoPartida;

        /// <summary>
        /// Indica se a partida do minijogo foi iniciada;
        /// não deve ser modificada externamente.
        /// </summary>
        public bool partidaIniciada;

        /// <summary>
        /// Indica se a partida do minijogo foi iniciada;
        /// não deve ser modificada externamente.
        /// </summary>
        public bool partidaEncerrada;

        /// <summary>Evento que é chamado ao se iniciar a partida.</summary>
        public UnityEvent evtAoIniciar;

        /// <summary>Evento que é chamado ao se encerrar a partida</summary>
        public UnityEvent evtAoTerminar;

        void Start()
        {
            InstanciarJogadores();
        }

        void Update()
        {
            if (!partidaIniciada)
                return;

            float tempoAtual = Time.time;
            tempoPartida = tempoAtual - tempoInicialPartida;

            if (tempoPartida > duracaoPartida && !partidaEncerrada)
            {
                EncerrarPartida();
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

        [ContextMenu("Iniciar Partida")]
        void IniciaPartida()
        {
            tempoInicialPartida = Time.time;
            partidaIniciada = true;
            evtAoIniciar.Invoke(); // -> AplicarControlador();
        }

        void EncerrarPartida()
        {
            partidaEncerrada = true;
            evtAoTerminar.Invoke();
        }
    }
}
