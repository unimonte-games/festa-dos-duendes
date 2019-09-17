using UnityEngine;
using System.Linq;

public class GeradorTabuleiro : MonoBehaviour
{
    [Tooltip("Pai dos objetos conectores")]
    public Transform conectores;
    [Tooltip("Pai onde ficarão as casas")]
    public Transform casas;
    [Tooltip("Prefabs dos tipos de casas")]
    public GameObject[] tiposCasas;

    private Transform ultimaCasa;

    public void GerarCasas()
    {
        ResetTabuleiro();

        //Laço de todas conexões (tabuleiro inteiro)
        foreach (Transform conexao in conectores)
        {
            Conector con = conexao.GetComponent<Conector>();

            //Laço de rotas entre conexões
            for (int i = 0; i < con.conexoes.Count; i++)
            {
                Vector3 passo = (conexao.position - con.conexoes[i].position) / (con.qtdCasas + 1);
                Vector3 posiAtual = con.transform.position;

                ultimaCasa = conexao;
                int indiceCasa = con.ultimoIndice;

                //Laço de casas
                for (int j = 0; j < con.qtdCasas; j++)
                {
                    posiAtual -= passo;
                    Instanciador(posiAtual, indiceCasa);

                    indiceCasa = ++indiceCasa % 6;
                }

                //Última casa aponta para o próximo conector
                ultimaCasa.GetComponent<CasaBase>().SetProximaCasa(con.conexoes[i]);
                //Conector atual aponta para a última casa
                con.conexoes[i].GetComponent<CasaBase>().SetCasaAnterior(ultimaCasa);

                //Define último índice do proximo conector
                con.conexoes[i].GetComponent<Conector>().ultimoIndice = indiceCasa;
            }
        }
    }

    void Instanciador(Vector3 posicao, int i)
    {
        GameObject novaCasa = Instantiate(tiposCasas[i], posicao, Quaternion.identity);
        novaCasa.transform.parent = casas;

        //Última Casa aponta para a Nova
        ultimaCasa.GetComponent<CasaBase>().SetProximaCasa(novaCasa.transform);
        //Nova Casa aponta para a Última 
        novaCasa.GetComponent<CasaBase>().SetCasaAnterior(ultimaCasa);

        ultimaCasa = novaCasa.transform;
    }

    [ContextMenu("Resetar Tabuleiro")]
    void ResetTabuleiro()
    {
        foreach (Transform conexao in conectores)
        {
            Conector con = conexao.GetComponent<Conector>();
            con.ultimoIndice = 0;
            con.proximaCasa.Clear();
            con.casaAnterior.Clear();
        }

        var temp = casas.Cast<Transform>().ToList();
        foreach (Transform casa in temp)
        {
            DestroyImmediate(casa.gameObject);
        }
    }
}