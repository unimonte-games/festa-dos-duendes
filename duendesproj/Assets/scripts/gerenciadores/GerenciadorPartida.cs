using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Componentes.Jogador;
using Componentes.Tabuleiro;

namespace Gerenciadores
{
    public class GerenciadorPartida : MonoBehaviour
    {
        public List<Transform> ordemJogadores;
        public Text textoPartida;
        [HideInInspector]
        public EscolheRota _escolheRota;
        private int rodada = 1, turno = 0;
        public Movimentacao movAtual;
        public static Inventario InvAtual { get; set; }

        private void Awake()
        {
            movAtual = ordemJogadores[0].GetComponent<Movimentacao>();
            InvAtual = ordemJogadores[0].GetComponent<Inventario>();

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

            movAtual = ordemJogadores[turno].GetComponent<Movimentacao>();
            InvAtual = ordemJogadores[turno].GetComponent<Inventario>();

            textoPartida.text = "Jogador: " + (turno + 1) + "\nRodada: " + rodada;
        }

        public void MoverJogador(int casa)
        {
            _escolheRota.estadoUICarta(false);
            StartCoroutine(movAtual.ProcuraCasa((Identificadores.TiposCasa)casa));
        }

        public void fimMov(bool casaEncontrada)
        {
            if (casaEncontrada)
            {
                Transform casaJogador = movAtual.GetComponent<Movimentacao>().casaAtual;
                EventosCasa _eventCasa = casaJogador.GetComponent<EventosCasa>();
                if (_eventCasa != null)
                    _eventCasa.ativarCasa();

                NovaRodada();
            }
            else
                _escolheRota.estadoUIRota(true);
        }

        public Transform ObterJogadorAtivo()
        {
            return ordemJogadores[turno];
        }
    }
}
