using UnityEngine;
using System.Collections.Generic;

public class Conector : CasaBase
{
    [Tooltip("Aponta para o(s) próximo(s) conectore(s)")]
    public List<Transform> conexoes;
    [Tooltip("Quantidade de casas até o próximo conector")]
    public List<int> qtdCasas;

    [HideInInspector]
    public int ultimoIndice = 0;

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
