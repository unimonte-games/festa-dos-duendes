using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Componentes.Jogador;
using Componentes.Tabuleiro;
using Identificadores;
using Photon.Pun;
using Photon.Realtime;

namespace Gerenciadores
{
    public class GerenciadorPartida : MonoBehaviour
    {
        public Transform paiConectores;
        public PainelCartas _painelCartas;
        public GameObject[] jogadorPrefabs;
        public static string descricaoCarta;
        public static Movimentacao MovAtual;
        public static Inventario InvAtual;
        public static List<Transform> OrdemJogadores = new List<Transform>();

        [HideInInspector]
        public EscolheRota _escolheRota;

        public static int Turno = 0;

        bool jaInicializado, autoridade;

        private void Start()
        {
            GameObject tronco_gbj = FindObjectOfType<TabuleiroRaiz>().tronco_gbj;
            jaInicializado = OrdemJogadores.Count != 0;

            autoridade = GerenciadorGeral.modoOnline
                            ? PhotonNetwork.IsMasterClient
                            : true;

            if (!autoridade)
                return;

            if (!jaInicializado)
            {
                for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
                {
                    Transform jogador;

                    if (!GerenciadorGeral.modoOnline)
                    {
                        jogador = Instantiate(
                            jogadorPrefabs[i],
                            Vector3.zero,
                            Quaternion.identity,
                            tronco_gbj.transform
                        ).transform;
                    }
                    else
                    {
                        jogador = PhotonNetwork.InstantiateSceneObject(
                            jogadorPrefabs[i].name,
                            Vector3.zero,
                            Quaternion.identity
                        ).transform;
                        jogador.SetParent(tronco_gbj.transform);

                        if (i > 0)
                        {
                            var jogador_pv = jogador.GetComponent<PhotonView>();
                            var player = PhotonNetwork.CurrentRoom.Players[i+1];
                            jogador_pv.TransferOwnership(player);
                        }
                    }

                    OrdemJogadores.Add(jogador.transform);
                }

                MovAtual = OrdemJogadores[0].GetComponent<Movimentacao>();
                InvAtual = OrdemJogadores[0].GetComponent<Inventario>();

                _escolheRota.estadoUIRota(false);
                _escolheRota.estadoUICarta(true);

                TabuleiroHUD.AlteraFundo(Color.green);
            }
        }

        public void NovaRodada()
        {
            if (!autoridade)
                return;

            Debug.Log("nova rodada");

            TabuleiroHUD.AlteraFundo(Color.gray);
            _escolheRota.estadoPowerUp = true;
            _escolheRota.AlteraEstadoPowerUps(Turno);

            //Aumenta turno
            Turno = ++Turno % OrdemJogadores.Count;

            //Altera jogador atual
            MovAtual = OrdemJogadores[Turno].GetComponent<Movimentacao>();
            InvAtual = OrdemJogadores[Turno].GetComponent<Inventario>();

            TabuleiroHUD.AlteraFundo(Color.green);

            //Jogador atual joga caso não esteja preso
            if (InvAtual.rodadasPreso > 0)
            {
                Debug.Log("StartCoroutine(diminuiRodadasPreso(1f, 3f))");
                StartCoroutine(diminuiRodadasPreso(1f, 3f));
            }
            else {
                Debug.Log("estadoUICarta(true)");
                _escolheRota.estadoUICarta(true);
            }

            if (InvAtual.rodadasSemObj > 0)
                InvAtual.rodadasSemObj--;
        }

        public IEnumerator WaitNovaRodada(float tempo)
        {
            Debug.Log(gameObject.activeInHierarchy + "JJJJJJJ" + tempo.ToString() + this.enabled);
            yield return new WaitForSecondsRealtime(tempo);
            Debug.Log("AAAAAAAAAA");
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
            if (!autoridade)
                return;

            _escolheRota.estadoUICarta(false);
            StartCoroutine(MovAtual.ProcuraCasa((TiposCasa)casa));
        }

        public void fimMov(bool casaEncontrada)
        {
            if (!autoridade)
                return;

            if (casaEncontrada)
            {
                Transform casaJogador = MovAtual.GetComponent<Movimentacao>().casaAtual;
                EventosCasa _eventCasa = casaJogador.GetComponent<EventosCasa>();
                _eventCasa.ativarCasa();

                TiposCasa tipo = casaJogador.GetComponent<CasaBase>().tipoCasa;

                if (tipo == TiposCasa.BemMal || tipo == TiposCasa.Acontecimento)
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
            return OrdemJogadores[Turno];
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

        public static PhotonView ObterPVLocal()
        {
            Player jogadorLocal = PhotonNetwork.LocalPlayer;
            GameObject[] jogadores = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < jogadores.Length; i++)
            {
                PhotonView pv = jogadores[i].GetComponent<PhotonView>();

                if (pv.Owner != null)
                {
                    if (pv.Owner.ActorNumber == jogadorLocal.ActorNumber)
                        return pv;
                }
            }

            return null;
        }
    }
}
