using UnityEngine;
using System.Collections;

public class Movimentacao : MonoBehaviour
{
    public Transform casaAtual;
    public float duracaoPulo;
    public EscolheRota _escolheRota;
    public GerenciadorPartida _gerenPartida;
    [HideInInspector]
    public int proximaCor;
    private Vector3 destino;
    private float tempoInicio;

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
                        //SetCasa(casaTemp); //Avança posição
                        _escolheRota.EstadoCanvasRota(true);
                    }
                }
                else if (corTemp == corDesejada || corTemp == proximaCor)
                {
                    achou = true;
                    //SetCasa(casaTemp); //Avança posição
                    _gerenPartida.NovaRodada();
                }

                destino = casaTemp.position;
                Debug.Log(destino);
                tempoInicio = Time.time;
                //StartCoroutine(Pulinho());

            } while (!achou);
        }
    }

    //public IEnumerator Pulinho()
    //{
    //    Vector3 center = (transform.position + destino) * 0.5F;
    //    center -= Vector3.up;

    //    Vector3 riseRelCenter = transform.position - center;
    //    Vector3 setRelCenter = destino - center;

    //    float x = (Time.time - tempoInicio) / duracaoPulo;

    //    transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, x);
    //    transform.position += center;

    //    yield return new WaitForSeconds(0.02f);

    //    if (x <= 1)
    //        yield return StartCoroutine(Pulinho());
    //    else
    //        yield return null;
    //}
}
