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

    public void ProcuraCasa(int tipo)
    {
        int i = 0;
        bool achou = false;
        do
        {
            Transform proximaCasa = casaAtual.GetComponent<CasaBase>().proximaCasa[0];
            int tipoCasa = proximaCasa.GetComponent<CasaBase>().tipoCasa;
            Debug.Log(tipoCasa);

            if (tipoCasa == 0)
            {

            }
            if (tipoCasa == tipo)
            {
                achou = true;
                casaAtual = proximaCasa;
            }
            i++;
        } while (!achou || i==30);

        setCasa(casaAtual);
    }

    //public int EscolheLado(Conector conexao)
    //{
    //    if (Input.GetKeyDown(KeyCode '1'))
    //}
}
