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

        bool jaInicializado;//, autoridade;

        public PhotonView meuPV;

#if UNITY_EDITOR
        [Header("Dev View")]
        public string _static_descricaoCarta;
        public Movimentacao _static_MovAtual;
        public Inventario _static_InvAtual;
        public List<Transform> _static_OrdemJogadores;
        public int _static_Turno = 0;

        void Update()
        {
            _static_descricaoCarta = descricaoCarta;
            _static_MovAtual = MovAtual;
            _static_InvAtual = InvAtual;
            _static_OrdemJogadores = OrdemJogadores;
            _static_Turno = Turno;
        }
#endif

        void DefAlvo(int idAlvo)
        {
            IdentificadorJogador[] idJs = FindObjectsOfType<IdentificadorJogador>();

            for (int i = 0; i < idJs.Length; i++)
            {
                if (idAlvo == (int)idJs[i].jogadorID)
                {
                    var tripeT = FindObjectOfType<TripeTabuleiro>();
                    tripeT.DefAlvo(idJs[i].GetComponent<Transform>());
                    break;
                }
            }
        }

        [PunRPC]
        void RPC_DefAlvo(int i)
        {
            DefAlvo(i);
        }

        [PunRPC]
        void RPC_DefQtd(int qtd)
        {
            GerenciadorGeral.qtdJogadores = qtd;
        }

        private void Start()
        {
            meuPV = GetComponent<PhotonView>();
            GameObject tronco_gbj = FindObjectOfType<TabuleiroRaiz>().tronco_gbj;
            jaInicializado = OrdemJogadores.Count != 0;

            bool autoridade = GerenciadorGeral.modoOnline
                                ? PhotonNetwork.IsMasterClient
                                : true;

            if (!autoridade)
                return;

            if (!jaInicializado)
            {
                meuPV.RPC("RPC_DefQtd", RpcTarget.Others, GerenciadorGeral.qtdJogadores);

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
                        //jogador.SetParent(tronco_gbj.transform);

                        //if (i > 0)
                        //{
                            //var jogador_pv = jogador.GetComponent<PhotonView>();
                            //DsvUtils.InspetorPhoton.ImprimirDicio(PhotonNetwork.CurrentRoom.Players);
                            //var player = PhotonNetwork.CurrentRoom.Players[i+1];
                            //jogador_pv.TransferOwnership(player);
                        //}

                        var jogador_pv = jogador.GetComponent<PhotonView>();
                        var player = PhotonNetwork.CurrentRoom.Players[i+1];
                        jogador_pv.RPC("RPC_DarPosse", RpcTarget.All, i);
                    }

                    OrdemJogadores.Add(jogador.transform);
                }

                MovAtual = OrdemJogadores[0].GetComponent<Movimentacao>();
                InvAtual = OrdemJogadores[0].GetComponent<Inventario>();

                _escolheRota.estadoUIRota(false);
                _escolheRota.estadoUICarta(true);

                TabuleiroHUD.FundoJogador(TabuleiroHUD.corOn);

                if (GerenciadorGeral.modoOnline)
                    meuPV.RPC("RPC_DefAlvo", RpcTarget.All, ObterJogadorAtivo());
                else
                    DefAlvo(ObterJogadorAtivo());
            }
        }

        public void NovaRodada()
        {
            //if (!autoridade)
            //    return;

            TabuleiroHUD.FundoJogador(TabuleiroHUD.corOff);

            //Aumenta turno
            Turno = ++Turno % OrdemJogadores.Count;

            //Altera jogador atual
            MovAtual = OrdemJogadores[Turno].GetComponent<Movimentacao>();
            InvAtual = OrdemJogadores[Turno].GetComponent<Inventario>();

            TabuleiroHUD.FundoJogador(TabuleiroHUD.corOn);

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

            if (GerenciadorGeral.modoOnline) {
                if (PhotonNetwork.IsMasterClient)
                    meuPV.RPC("RPC_DefAlvo", RpcTarget.All, Turno);
            }
            else
                DefAlvo(Turno);
        }

        [PunRPC]
        void RPC_GarrafaSync(bool ativ)
        {
            InvAtual.garrafa.SetActive(ativ);
        }

        public void GarrafaSync(bool ativ)
        {
            meuPV.RPC("RPC_GarrafaSync", RpcTarget.All, ativ);
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

                if (!GerenciadorGeral.modoOnline)
                    InvAtual.transform.GetChild(1).gameObject.SetActive(false);
                else
                    GarrafaSync(false);

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
            if (RPCDeJogadores.DeveUsarRPC())
            {
                RPCDeJogadores.UsarRPCArg("RPC_MoverJogador", casa);
                return;
            }
            Debug.LogFormat("MoverJogador({0})", casa);

            //if (!autoridade)
            //    return;

            _escolheRota.estadoUICarta(false);
            StartCoroutine(MovAtual.ProcuraCasa((TiposCasa)casa));
        }

        public void fimMov(bool casaEncontrada)
        {
            //if (!autoridade)
            //    return;

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

        public int ObterJogadorAtivo()
        {
            return (int)(OrdemJogadores[Turno]
                    .GetComponent<IdentificadorJogador>()
                    .jogadorID);
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
            IdentificadorJogador[] idJs = FindObjectsOfType<IdentificadorJogador>();

            for (int i = 0; i < idJs.Length; i++)
            {
                if (idJs[i].eMeu)
                    return idJs[i].GetComponent<PhotonView>();
            }

            return null;
        }
    }
}
