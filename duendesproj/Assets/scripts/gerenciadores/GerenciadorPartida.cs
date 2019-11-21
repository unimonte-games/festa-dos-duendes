using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Componentes.Jogador;
using Componentes.Tabuleiro;
using Identificadores;

namespace Gerenciadores
{
    public class GerenciadorPartida : MonoBehaviour
    {
        public Transform paiConectores;
        public PainelCartas _painelCartas;
        public Telas.TabuleiroHUD _tabuleiroHUD;
        public GameObject[] jogadorPrefabs;
        public static string descricaoCarta;
        public static Movimentacao MovAtual;
        public static Inventario InvAtual;
        public static List<Transform> OrdemJogadores = new List<Transform>();

        [HideInInspector]
        public EscolheRota _escolheRota;

        private int rodada = 1, turno = 0;

        private void Start()
        {
            if (OrdemJogadores.Count == 0)
            {
                for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
                {
                    Transform jogador = Instantiate(jogadorPrefabs[i]).transform;
                    OrdemJogadores.Add(jogador.transform);

                    Transform casa = paiConectores.GetChild(i);
                    jogador.transform.position = casa.position;
                    jogador.GetComponent<Movimentacao>().casaAtual = casa;
                }

                MovAtual = OrdemJogadores[0].GetComponent<Movimentacao>();
                InvAtual = OrdemJogadores[0].GetComponent<Inventario>();

                _escolheRota.estadoUIRota(false);
                _escolheRota.estadoUICarta(true);

                _tabuleiroHUD.fundos[turno].color = Color.green;
            }
        }

        public void NovaRodada()
        {
            _tabuleiroHUD.fundos[turno].color = Color.gray;

            turno++;
            if (turno == OrdemJogadores.Count)
            {
                turno = 0;
                rodada++;
            }

            //Altera jogador atual
            MovAtual = OrdemJogadores[turno].GetComponent<Movimentacao>();
            InvAtual = OrdemJogadores[turno].GetComponent<Inventario>();

            _tabuleiroHUD.fundos[turno].color = Color.green;

            //Jogador atual joga caso não esteja preso
            if (InvAtual.rodadasPreso > 0)
            {
                StartCoroutine(diminuiRodadasPreso(1f, 3f));
            }
            else
                _escolheRota.estadoUICarta(true);

            if (InvAtual.rodadasSemObj > 0)
                InvAtual.rodadasSemObj--;
        }

        public IEnumerator WaitNovaRodada(float tempo)
        {
            yield return new WaitForSeconds(tempo);
            NovaRodada();
        }

        public IEnumerator diminuiRodadasPreso(float tempoAnda, float tempoPara)
        {
            if (--InvAtual.rodadasPreso == 0)
            {
                yield return new WaitForSeconds(tempoAnda);
                InvAtual.transform.GetChild(1).gameObject.SetActive(false);

                //Mostra Carta de movimentação para o Jogador
                _escolheRota.estadoUICarta(true);
            }
            else
            {
                yield return new WaitForSeconds(tempoPara);
                NovaRodada();
            }
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

                TiposCasa tipo = casaJogador.GetComponent<CasaBase>().tipoCasa;
                if (tipo != TiposCasa.Garrafa && tipo != TiposCasa.Moeda)
                    _painelCartas.MudaDescricao(tipo, descricaoCarta);
            }
            else
            {
                _escolheRota.paraFrente = MovAtual.paraFrente;
                _escolheRota.estadoUIRota(true);
            }
        }

        public Transform ObterJogadorAtivo()
        {
            return OrdemJogadores[turno];
        }

        public IEnumerator VoltaParaInicio()
        {
            for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
            {
                Transform casa = paiConectores.GetChild(i);
                Movimentacao mov = OrdemJogadores[i].GetComponent<Movimentacao>();
                mov.casaAtual = casa;
                yield return StartCoroutine(mov.Pulinho(casa.position, Time.time));
            }
        }

        public IEnumerator TeleportAleatorio()
        {
            int qtdCasas = paiConectores.childCount;
            foreach (Transform jogador in OrdemJogadores)
            {
                int rand = Random.Range(0, qtdCasas);
                Transform casa = paiConectores.GetChild(rand);

                Movimentacao mov = jogador.GetComponent<Movimentacao>();
                mov.casaAtual = casa;
                yield return StartCoroutine(mov.Pulinho(casa.position, Time.time));
            }
        }
    }
}
