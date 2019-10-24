using UnityEngine;

public class EscolheRota : MonoBehaviour
{
    public Movimentacao jogador;
    public GameObject canvasCarta, canvasDirecao, setaObj;
    private int indice = 0;

    private Vector3 setaPosi;
    private GameObject seta;

    private void Awake()
    { 
        seta = Instantiate(setaObj);
        seta.SetActive(false);
    }

    public void EscolherRota(bool confirmacao)
    {
        CasaBase _casaBase = jogador.casaAtual.GetComponent<CasaBase>();
        Transform casaTemp = _casaBase.casaSeguinte[indice];

        if (confirmacao)
        {
            EstadoCanvasRota(false); //Esconde os itens de escolha de rota
            jogador.setCasa(casaTemp); //Avança na rota escolhida
            jogador.ProcuraCasa(jogador.proximaCor); //Avança para a cor certa        
        }
        else
        {
            indice = ++indice % _casaBase.casaSeguinte.Count;
            casaTemp = _casaBase.casaSeguinte[indice];
            seta.transform.position = casaTemp.position;
        }
    }

    public void EstadoCanvasRota(bool estado)
    {
        //TRUE = mostra os itens de escolha de rota
        //FALSE = esconde os itens de escolha de rota

        if (estado)
        {
            CasaBase _casaBase = jogador.casaAtual.GetComponent<CasaBase>();
            seta.transform.position = _casaBase.casaSeguinte[indice].position;
        }

        seta.SetActive(estado);
        canvasDirecao.SetActive(estado);
        canvasCarta.SetActive(!estado);
    }
}