using UnityEngine;

public class EscolheRota : MonoBehaviour
{
    public Movimentacao jogador;
    public GameObject canvasCarta, canvasDirecao;
    private int indice = 0;

    public void EscolherRota(bool confirmacao)
    {
        CasaBase _casaBase = jogador.casaAtual.GetComponent<CasaBase>();
        Transform casaTemp = _casaBase.casaSeguinte[indice];

        if (confirmacao)
        {
            jogador.setCasa(casaTemp); //Avança na rota escolhida
            jogador.ProcuraCasa(jogador.proximaCor); //Avança para a cor certa

            canvasCarta.SetActive(true);
            canvasDirecao.SetActive(false);
        }
        else
        {
            indice = ++indice % _casaBase.casaSeguinte.Count;
            Debug.Log("Casa " + indice);
            casaTemp = _casaBase.casaSeguinte[indice];
        }
    }
}