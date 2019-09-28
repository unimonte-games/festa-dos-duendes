using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Rota : object
{
    public Transform conector;
    public int qtdCasas;
}

public class Conector : CasaBase
{
    public List<Rota> rotas;
    [HideInInspector]
    public int ultimoIndice = 0;

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
