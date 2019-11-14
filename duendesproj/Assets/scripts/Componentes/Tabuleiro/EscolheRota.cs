using UnityEngine;
using UnityEngine.UI;

namespace Componentes.Tabuleiro
{
    public class EscolheRota : MonoBehaviour
    {
        public Gerenciadores.GerenciadorPartida _gerenPartida;
        public Button botaoCarta;
        public GameObject UIDirecao, setaObj;
        [HideInInspector]
        public bool paraFrente;

        private Transform casaTemp;
        private Jogador.Movimentacao jogador;
        private int indice;

        public void EscolherRota(bool confirmacao)
        {
            jogador = _gerenPartida.movAtual;
            CasaBase _casaBase = jogador.casaAtual.GetComponent<CasaBase>();

            if (confirmacao)
            {
                indice = 0;
                jogador.paraFrente = paraFrente;
                jogador.casaAtual = casaTemp; //Avança na rota escolhida
                StartCoroutine(jogador.ProcuraCasa(jogador.proximaCor)); //Avança para a cor certa   
                estadoUIRota(false); //Esconde a escolha de rota
            }
            else
            {
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
                jogador = _gerenPartida.movAtual;
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
            botaoCarta.interactable = estado;
        }
    }
}