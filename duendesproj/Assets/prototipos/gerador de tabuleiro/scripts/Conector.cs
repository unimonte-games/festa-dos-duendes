using UnityEngine;
using System.Collections.Generic;

public class Conector : CasaBase
{
    [HideInInspector] public int primeiraCasa;
    public List<Transform> conexoes;
    public int qtdCasas;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        for (int i = 0; i < conexoes.Count; i++)
        {
            Gizmos.DrawLine(transform.position, conexoes[i].position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        for (int i = 0; i < conexoes.Count; i++)
        {
            Gizmos.DrawWireSphere(conexoes[i].position, 0.3f);
        }
    }
}
