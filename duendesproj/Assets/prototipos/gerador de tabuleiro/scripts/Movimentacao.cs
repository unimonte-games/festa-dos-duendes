using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    public Transform casaAtual;
    public EscolheRota _escolheRota;
    [HideInInspector]
    public int proximaCor;

    void Start()
    {
        setCasa(casaAtual);
        _escolheRota.EstadoCanvasRota(false);
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
                    setCasa(casaTemp);

                    _escolheRota.EstadoCanvasRota(true);
                }
            }
            else if (corTemp == corDesejada)
                achou = true;

        } while (!achou);

        setCasa(casaTemp); //Avança posição
    }
}
