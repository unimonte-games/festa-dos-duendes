using UnityEngine;

public class Jogador : MonoBehaviour
{
    public Transform casaAtual;

    void Start()
    {
        setCasa(casaAtual);
    }

    public void setCasa(Transform novaCasa)
    {
        casaAtual = novaCasa;
        transform.position = casaAtual.position;
    }

    public void ProcuraCasa(int indice)
    {
        bool achou = false;
        do
        {
            CasaBase cbAtual = casaAtual.GetComponent<CasaBase>();
            if (cbAtual.indiceCasa == indice)
                achou = true;
            else
                casaAtual = cbAtual.proximaCasa[0];

        } while (!achou);

        setCasa(casaAtual);
    }


}
