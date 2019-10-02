using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    public Transform casaAtual;
    public GameObject canvasCarta, canvasDirecao;
    [HideInInspector]
    public int proximaCor;

    void Start()
    {
        setCasa(casaAtual);
        canvasDirecao.SetActive(false);
    }

    public void setCasa(Transform novaCasa)
    {
        casaAtual = novaCasa;
        transform.position = casaAtual.position;
    }

    public void ProcuraCasa(int corDesejada)
    {
        bool achou = false;
        Transform casaTemp = casaAtual;
        int corTemp;

        do
        {
            casaTemp = casaTemp.GetComponent<CasaBase>().casaSeguinte[0];
            corTemp = casaTemp.GetComponent<CasaBase>().tipoCasa;

            if (corTemp == 0)
            {
                CasaBase _casaBase = casaTemp.GetComponent<CasaBase>();
                if (_casaBase.casaSeguinte.Count > 1) //Se o conector tem multiplos caminhos
                {
                    achou = true;
                    proximaCor = corDesejada;

                    canvasDirecao.SetActive(true);
                    canvasCarta.SetActive(false);
                }
            }
            else if (corTemp == corDesejada)
                achou = true;

        } while (!achou);

        setCasa(casaTemp); //Avança posição
    }
}
