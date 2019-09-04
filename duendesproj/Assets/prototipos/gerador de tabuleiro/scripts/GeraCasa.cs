using UnityEngine;
using System.Collections.Generic;

public class GeraCasa : CasaBase
{
    public List<GameObject> casas;
    public int quantidadeCasas;
    public List<Transform> proximoConector;
    private Transform ultimaCasa;

    void Awake()
    {

        for (int i = 0; i < proximoConector.Count; i++)
        {
            //Torna o conector a ultima casa padrão
            ultimaCasa = transform;

            GerarCasa(proximoConector[i]);
        }

        //Libera listas da memória
        proximoConector.Clear();
        casas.Clear();
    }

    void GerarCasa(Transform proximoConector)
    {
        Vector3 passo = (transform.position - proximoConector.position) / (quantidadeCasas + 1);
        Vector3 posiAtual = transform.position;

        for (int i = 0; i < quantidadeCasas; i++)
        {
            //Instancia
            posiAtual -= passo;
            
            GameObject novaCasa = Instantiate(casas[RetornaCasa()], posiAtual, transform.rotation);
            novaCasa.transform.parent = transform.parent;

            //Set proxima casa
            ultimaCasa.GetComponent<CasaBase>().AddProximaCasa(novaCasa.transform);
            ultimaCasa = novaCasa.transform;
        }

        //A ultima casa aponta para o conector
        ultimaCasa.GetComponent<CasaBase>().AddProximaCasa(proximoConector);
    }

    int RetornaCasa()
    {
        int rand = Random.Range(0, 100);
        if (rand <= 70) return 0;
        else if (rand <= 90) return 1;
        else return 2;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        for (int i = 0; i < proximoConector.Count; i++)
        {
            Gizmos.DrawLine(transform.position, proximoConector[i].position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        for (int i = 0; i < proximoConector.Count; i++)
        {
            Gizmos.DrawWireSphere(proximoConector[i].position, 0.3f);
        }
    }
}
