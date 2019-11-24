using UnityEngine;
using UnityEngine.UI;
using Gerenciadores;
using Photon.Pun;

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

        public void EscolherRota(bool confirmacao)
        {
            if (GerenciadorGeral.modoOnline && !PhotonNetwork.IsMasterClient)
            {
                PhotonView pvLocal = GerenciadorPartida.ObterPVLocal();
                pvLocal.RPC("RPC_EscolherRota", RpcTarget.MasterClient, confirmacao);
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
            setaObj.SetActive(estado);
            UIDirecao.SetActive(estado);
        }

        public void estadoUICarta(bool estado)
        {
            UIJogada.SetActive(estado);
        }

        public void AlteraEstadoPowerUps(int i = -1)
        {
            if (i < 0) i = GerenciadorPartida.Turno;
            estadoPowerUp = !estadoPowerUp;
            UIPowerUps.transform.GetChild(i).gameObject.SetActive(estadoPowerUp);
        }
    }
}
