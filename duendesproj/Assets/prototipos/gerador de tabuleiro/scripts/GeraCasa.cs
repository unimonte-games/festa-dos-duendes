using UnityEngine;
using System.Collections.Generic;

public class GeraCasa : MonoBehaviour
{
    public List<GameObject> casas;
    public List<GameObject> conectores;
    private Transform ultimaCasa;
    public Transform container;

    void Awake()
    {
        foreach (GameObject conObj in conectores)
        {
            ultimaCasa = conObj.transform;
            GerarCasas(conObj.transform, conObj.GetComponent<Conector>());
        }
    }

    void GerarCasas(Transform conTrans, Conector conScript)
    {
        for (int i = 0; i < conScript.conexoes.Count; i++)
        {
            int indiceCasa = conScript.primeiraCasa;

            Vector3 passo = (conTrans.position - conScript.conexoes[i].position) / (conScript.qtdCasas + 1);
            Vector3 posiAtual = conTrans.position;

            for (int j = 0; j < conScript.qtdCasas; j++)
            {
                posiAtual -= passo;
                Instanciador(posiAtual, indiceCasa);

                indiceCasa = ++indiceCasa % casas.Count;
            }

            Conector proximo = conScript.conexoes[i].GetComponentInParent<Conector>();
            proximo.primeiraCasa = indiceCasa;

            ultimaCasa.GetComponent<CasaBase>().setProximaCasa(conScript.conexoes[i]);
        }
    }

    void Instanciador(Vector3 posicao, int i)
    {
        GameObject novaCasa = Instantiate(casas[i], posicao, transform.rotation);
        novaCasa.transform.parent = container;

        ultimaCasa.GetComponent<CasaBase>().setProximaCasa(novaCasa.transform);
        ultimaCasa = novaCasa.transform;
    }
}