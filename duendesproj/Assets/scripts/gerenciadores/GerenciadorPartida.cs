using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Componentes.Tabuleiro;

namespace Gerenciadores
{
    public class GerenciadorPartida : MonoBehaviour
    {
        public List<GameObject> ordemJogadores;
        public Text textoPartida;
        [HideInInspector]
        public Movimentacao jogadorAtual;
        public EscolheRota _escolheRota;
        private int rodada = 1, turno = 0;

        private void Awake()
        {
            jogadorAtual = ordemJogadores[0].GetComponent<Movimentacao>();
            _escolheRota.estadoUIRota(false);
            _escolheRota.estadoUICarta(true);
        }

        public void NovaRodada()
        {
            _escolheRota.estadoUICarta(true);

            turno++;
            if (turno == ordemJogadores.Count)
            {
                turno = 0;
                rodada++;
            }

            jogadorAtual = ordemJogadores[turno].GetComponent<Movimentacao>();

            textoPartida.text = "Jogador: " + (turno + 1) + "\nRodada: " + rodada;
        }

        public void MoverJogador(int casa)
        {
            _escolheRota.estadoUICarta(false);
            StartCoroutine(jogadorAtual.ProcuraCasa(casa));
        }

        public void fimMov(bool casaEncontrada)
        {
            if (casaEncontrada)
            {
                Transform casaJogador = jogadorAtual.GetComponent<Movimentacao>().casaAtual;
                EventosCasa _eventCasa = casaJogador.GetComponent<EventosCasa>();

                if (_eventCasa != null)
                    _eventCasa.ativarCasa();

                NovaRodada();
            }
            else
                _escolheRota.estadoUIRota(true);
        }

        
    }
}
