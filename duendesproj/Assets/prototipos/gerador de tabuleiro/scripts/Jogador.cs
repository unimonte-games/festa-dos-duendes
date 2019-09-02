using UnityEngine;

public class Jogador : MonoBehaviour
{
    public Transform casaAtual;

    public void setCasa(Transform novaCasa)
    {
        casaAtual = novaCasa;
        transform.Translate(casaAtual.position, Space.World);
    }

    public void MoveProximo(int i)
    {
        Transform proximaCasa = casaAtual.GetComponent<CasaBase>().proximaCasa[i];
        casaAtual = proximaCasa;
        transform.position = casaAtual.position;
    }
}
