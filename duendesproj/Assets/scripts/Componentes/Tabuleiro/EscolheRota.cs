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
                EstadoCanvasRota(false); //Esconde os itens de escolha de rota
                jogador.SetCasa(casaTemp); //Avança na rota escolhida
                jogador.ProcuraCasa(jogador.proximaCor); //Avança para a cor certa        
            }
            else
            {
                indice = ++indice % _casaBase.casaSeguinte.Count;
                casaTemp = _casaBase.casaSeguinte[indice];
                setaObj.transform.position = casaTemp.position;
            }
        }

        public void EstadoCanvasRota(bool estado)
        {
            //TRUE = mostra os itens de escolha de rota
            //FALSE = esconde os itens de escolha de rota

            if (estado)
            {
                jogador = _gerenPartida.jogadorAtual;
                CasaBase _casaBase = jogador.casaAtual.GetComponent<CasaBase>();
                setaObj.transform.position = _casaBase.casaSeguinte[indice].position;
            }

            setaObj.SetActive(estado);
            UIDirecao.SetActive(estado);
            botaoCarta.interactable = !estado;
        }
    }
}