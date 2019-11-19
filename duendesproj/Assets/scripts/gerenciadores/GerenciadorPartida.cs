using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Componentes.Jogador;
using Componentes.Tabuleiro;
using Identificadores;
using System.Collections;

namespace Gerenciadores
{
    public class GerenciadorPartida : MonoBehaviour
    {
        public Transform paiConectores;
        public GameObject[] jogadorPrefabs;
        public Text textoPartida;
        [HideInInspector]
        public EscolheRota _escolheRota;
        public static Movimentacao MovAtual { get; set; }
        public static Inventario InvAtual { get; set; }

        private List<Transform> ordemJogadores = new List<Transform>();
        private int rodada = 1, turno = 0;

        private void Start()
        {
            for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
            {
                Transform jogador = Instantiate(jogadorPrefabs[i]).transform;
                ordemJogadores.Add(jogador.transform);

                Transform casa = paiConectores.GetChild(i);
                jogador.transform.position = casa.position;
                jogador.GetComponent<Movimentacao>().casaAtual = casa;
            }

            MovAtual = ordemJogadores[0].GetComponent<Movimentacao>();
            InvAtual = ordemJogadores[0].GetComponent<Inventario>();

            _escolheRota.estadoUIRota(false);
            _escolheRota.estadoUICarta(true);
        }

        public void NovaRodada()
        {
            turno++;
            if (turno == ordemJogadores.Count)
            {
                turno = 0;
                rodada++;
            }

            MovAtual = ordemJogadores[turno].GetComponent<Movimentacao>();
            InvAtual = ordemJogadores[turno].GetComponent<Inventario>();

            textoPartida.text = "Jogador: " + (turno + 1) + "\nRodada: " + rodada;

            if (InvAtual.itens.Contains(Itens.NaoPegaObj))
            {
                if (InvAtual.tirarNaoPegaObj)
                {
                    //Sem pegar obj para retirar
                    InvAtual.tirarNaoPegaObj = false;
                    InvAtual.itens.Remove(Itens.NaoPegaObj);
                }
                else
                {
                    //Sem pegar obj sem retirar
                    InvAtual.tirarNaoPegaObj = true;
                }
            }

            if (InvAtual.itens.Contains(Itens.Garrafa))
            {
                if (InvAtual.tirarGarrafa)
                {
                    //Com garrafa para retirar
                    StartCoroutine(tiraGarrafa(1f));
                }
                else
                {
                    //Com garrafa sem retirar
                    InvAtual.tirarGarrafa = true;
                    StartCoroutine(waitNovaRodada(2.5f));
                }
            }
            else
                //Sem garrafa
                _escolheRota.estadoUICarta(true);
        }

        public IEnumerator tiraGarrafa(float tempo)
        {
            yield return new WaitForSeconds(tempo);
            InvAtual.transform.GetChild(1).gameObject.SetActive(false);
            InvAtual.itens.Remove(Itens.Garrafa);

            //Mostra Carta de movimentação para o Jogador
            _escolheRota.estadoUICarta(true);
        }

        public IEnumerator waitNovaRodada(float tempo)
        {
            yield return new WaitForSeconds(tempo);
            NovaRodada();
        }

        public void MoverJogador(int casa)
        {
            _escolheRota.estadoUICarta(false);
            StartCoroutine(MovAtual.ProcuraCasa((TiposCasa)casa));
        }

        public void fimMov(bool casaEncontrada)
        {
            if (casaEncontrada)
            {
                Transform casaJogador = MovAtual.GetComponent<Movimentacao>().casaAtual;
                EventosCasa _eventCasa = casaJogador.GetComponent<EventosCasa>();
                _eventCasa.ativarCasa();

                NovaRodada();
            }
            else
            {
                _escolheRota.paraFrente = MovAtual.paraFrente;
                _escolheRota.estadoUIRota(true);
            }
        }

        public Transform ObterJogadorAtivo()
        {
            return ordemJogadores[turno];
        }
    }
}
