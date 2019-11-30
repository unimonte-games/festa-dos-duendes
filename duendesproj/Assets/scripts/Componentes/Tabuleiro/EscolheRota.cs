using UnityEngine;
using UnityEngine.UI;
using Gerenciadores;
using Photon.Pun;
using Identificadores;
using Componentes.Jogador;

namespace Componentes.Tabuleiro
{
    public class EscolheRota : MonoBehaviour
    {
        public GameObject UIJogada, UIDirecao, UIPowerUps, setaObj;
        [HideInInspector]
        public bool paraFrente;
        public bool estadoPowerUp = false;

        private Transform casaTemp;
        private Jogador.Movimentacao jogador;
        private int indice;

        PhotonView meuPV;
        GerenciadorPartida gerenP;

        void Awake()
        {
            meuPV = GetComponent<PhotonView>();
            gerenP = FindObjectOfType<GerenciadorPartida>();
        }

        public void EscolherRota(bool confirmacao)
        {
            if (RPCDeJogadores.DeveUsarRPC())
            {
                RPCDeJogadores.UsarRPCArg("RPC_EscolherRota", confirmacao);
                return;
            }

            jogador = GerenciadorPartida.MovAtual;

            if (confirmacao)
            {
                indice = 0;
                jogador.paraFrente = paraFrente;

                jogador.SetCasaAtual(casaTemp);
                StartCoroutine(jogador.ProcuraCasa(jogador.proximaCor));

                estadoUIRota(false); //Esconde a escolha de rota
            }
            else
            {
                CasaBase _casaBase = jogador.casaAtual.GetComponent<CasaBase>();
                indice++;

                if (paraFrente && indice == _casaBase.casaSeguinte.Count)
                {
                    paraFrente = false;
                    indice = 0;
                }
                else if (!paraFrente && indice == _casaBase.casaAnterior.Count)
                {
                    paraFrente = true;
                    indice = 0;
                }

                if (paraFrente)
                    casaTemp = _casaBase.casaSeguinte[indice];
                else
                    casaTemp = _casaBase.casaAnterior[indice];

                setaObj.transform.position = casaTemp.position;
            }
        }

        public void estadoUIRota(bool estado)
        {
            Debug.LogFormat("estadoUIRota({0})", estado);

            if (estado)
            {
                jogador = GerenciadorPartida.MovAtual;
                CasaBase _casaBase = jogador.casaAtual.GetComponent<CasaBase>();
                if (paraFrente)
                    casaTemp = _casaBase.casaSeguinte[indice];
                else
                    casaTemp = _casaBase.casaAnterior[indice];

                setaObj.transform.position = casaTemp.position;
            }

            if (!GerenciadorGeral.modoOnline) {
                setaObj.SetActive(estado);
                UIDirecao.SetActive(estado);
            }
            else if (PhotonNetwork.IsMasterClient)
                meuPV.RPC("RPC_DirESetaSetActives", RpcTarget.All, estado, gerenP.ObterJogadorAtivo());
        }

        [PunRPC]
        void RPC_DirESetaSetActives(bool estado, int jogadorAtivo)
        {

            int jidLocal = (int)GerenciadorPartida
                                .ObterPVLocal()
                                .GetComponent<IdentificadorJogador>()
                                .jogadorID;

            bool estadoL = estado ? jidLocal == jogadorAtivo : false;

            Debug.LogFormat(
                "estado: {0}, jogador Ativo: {1}, ismasterclient: {2}, estadoL: {3}, jidlocal: {4}",
                estado, jogadorAtivo, PhotonNetwork.IsMasterClient,
                estadoL, jidLocal
            );

            setaObj.SetActive(estado);
            UIDirecao.SetActive(estadoL);
        }

        public void estadoUICarta(bool estado)
        {
            Debug.LogFormat("estadoUICarta({0})", estado);

            if (!GerenciadorGeral.modoOnline)
                UIJogada.SetActive(estado);
            else if (PhotonNetwork.IsMasterClient) {
                meuPV.RPC(
                    "RPC_estadoUICarta",
                    RpcTarget.All,
                    estado,
                    gerenP.ObterJogadorAtivo()
                );
            }
        }

        [PunRPC]
        void RPC_estadoUICarta(bool estado, int jogadorAtivo)
        {
            Debug.LogFormat("RPC_estadoUICarta({0}, {1})", estado, jogadorAtivo);

            int jidLocal = (int)GerenciadorPartida
                                .ObterPVLocal()
                                .GetComponent<IdentificadorJogador>()
                                .jogadorID;

            UIJogada.SetActive(estado ? jidLocal == jogadorAtivo : false);
        }

        public void AlteraEstadoPowerUps(int i = -1)
        {
            if (RPCDeJogadores.DeveUsarRPC())
            {
                RPCDeJogadores.UsarRPCArg("RPC_AlteraEstadoPowerUps", i);
                return;
            }

            if (i < 0) i = GerenciadorPartida.Turno;
            estadoPowerUp = !estadoPowerUp;

            if (!GerenciadorGeral.modoOnline)
                UIPowerUps.transform.GetChild(i).gameObject.SetActive(estadoPowerUp);
            else
                meuPV.RPC("RPC_EstadoPowerUpSetActive", RpcTarget.All, i, estadoPowerUp);
        }

        [PunRPC]
        void RPC_EstadoPowerUpSetActive(int i, bool estadoPowerUp)
        {
            UIPowerUps.transform.GetChild(i).gameObject.SetActive(estadoPowerUp);
        }
    }
}
