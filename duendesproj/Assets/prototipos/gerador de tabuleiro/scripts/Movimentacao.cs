using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    public Transform casaAtual;
    public EscolheRota _escolheRota;
    public GerenciadorPartida _gerenPartida;
    [HideInInspector]
    public int proximaCor;

    void Start()
    {
        SetCasa(casaAtual);
        _escolheRota.EstadoCanvasRota(false);
    }

    public void SetCasa(Transform novaCasa)
    {
        casaAtual = novaCasa;
        transform.position = casaAtual.position;
    }

    public void ProcuraCasa(int corDesejada)
    {
        bool achou = false;
        Transform casaTemp = casaAtual;
        int corTemp = casaTemp.GetComponent<CasaBase>().tipoCasa;

        if (corTemp != 0 && corTemp == proximaCor)
        {
            proximaCor = 0;
            SetCasa(casaTemp); //Avança posição
            _gerenPartida.NovaRodada();
        }
        else
        {
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
                        proximaCor = corDesejada; //Salva cor desejada
                        SetCasa(casaTemp); //Avança posição
                        _escolheRota.EstadoCanvasRota(true);
                    }
                }
                else if (corTemp == corDesejada || corTemp == proximaCor)
                {
                    achou = true;
                    SetCasa(casaTemp); //Avança posição
                    _gerenPartida.NovaRodada();
                }

            } while (!achou);
        }
    }
}
