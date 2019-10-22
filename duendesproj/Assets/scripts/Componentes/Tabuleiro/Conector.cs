using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct Rota
{
    public Transform conector;
    [Range(0, 20)]
    public int qtdCasas;
}

public class Conector : CasaBase
{
    public List<Rota> rotas;
    [HideInInspector]
    public int ultimaCor = 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        for (int i = 0; i < rotas.Count; i++)
        {
            if (rotas[i].conector != null)
                Gizmos.DrawLine(transform.position, rotas[i].conector.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        for (int i = 0; i < rotas.Count; i++)
        {
            if (rotas[i].conector != null)
                Gizmos.DrawWireSphere(rotas[i].conector.position, 0.3f);
        }
    }
}
