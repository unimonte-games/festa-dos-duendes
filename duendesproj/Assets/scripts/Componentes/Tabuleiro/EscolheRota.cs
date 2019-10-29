using UnityEngine;
using UnityEngine.UI;

namespace Componentes.Tabuleiro
{
    public class EscolheRota : MonoBehaviour
    {
        public GerenciadorPartida _gerenPartida;
        public Button botaoCarta;
        public GameObject UIDirecao, setaObj;

        private Movimentacao jogador;
        private int indice = 0;
        private Vector3 setaPosi;

        public void EscolherRota(bool confirmacao)
        {
            jogador = _gerenPartida.jogadorAtual;
            CasaBase _casaBase = jogador.casaAtual.GetComponent<CasaBase>();
            Transform casaTemp = _casaBase.casaSeguinte[indice];

            if (confirmacao)
            {
                jogador.SetCasaAtual(casaTemp); //Avança na rota escolhida
                StartCoroutine(jogador.ProcuraCasa(jogador.proximaCor)); //Avança para a cor certa   
                estadoUIRota(false); //Esconde a escolha de rota
            }
            else
            {
                indice = ++indice % _casaBase.casaSeguinte.Count;
                casaTemp = _casaBase.casaSeguinte[indice];
                setaObj.transform.position = casaTemp.position;
            }
        }

        public void estadoUIRota(bool estado)
        {
            if (estado)
            {
                jogador = _gerenPartida.jogadorAtual;
                CasaBase _casaBase = jogador.casaAtual.GetComponent<CasaBase>();
                setaObj.transform.position = _casaBase.casaSeguinte[indice].position;
            }
            setaObj.SetActive(estado);
            UIDirecao.SetActive(estado);
        }

        public void estadoUICarta(bool estado)
        {
            botaoCarta.interactable = estado;
        }
    }
}