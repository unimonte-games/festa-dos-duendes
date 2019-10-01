using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Componentes.Jogador;
using Identificadores;

namespace Gerenciadores {
    public class GerenciadorMJLib : MonoBehaviour
    {
        public GameObject[] duendesPrefab;

        float tempoInicialPartida;

        public float duracaoPartida;

        [Header("Não modifique pelo Inspector:")]
        public Transform[] tr_jogadores;
        public float tempoPartida;
        public bool partidaIniciada;
        public bool partidaEncerrada;

        public UnityEvent evtAoIniciar, evtAoTerminar;

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
