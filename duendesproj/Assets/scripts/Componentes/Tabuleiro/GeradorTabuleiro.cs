using UnityEngine;
using System.Linq;

namespace Componentes.Tabuleiro
{
    public class GeradorTabuleiro : MonoBehaviour
    {
        [Tooltip("Pai dos objetos conectores")]
        public Transform paiConectores;
        [Tooltip("Pai onde ficarão as casas")]
        public Transform paiCasas;
        [Tooltip("Prefabs de casas a serem instanciadas em ordem")]
        public GameObject[] ordemCores;

        private Transform ultimaCasa;

        public void GerarCasas()
        {
            ResetTabuleiro();

            //Laço de todas conexões (tabuleiro inteiro)
            foreach (Transform conector in paiConectores)
            {
                Conector _conector = conector.GetComponent<Conector>();

                //Laço de rotas entre conectores
                for (int i = 0; i < _conector.rotas.Count; i++)
                {
                    Vector3 passo = (conector.position - _conector.rotas[i].conector.position) / (_conector.rotas[i].qtdCasas + 1);
                    Vector3 posiAtual = _conector.transform.position;

                    ultimaCasa = conector;
                    int corCasa = _conector.ultimaCor;

                    //Laço de casas
                    for (int j = 0; j < _conector.rotas[i].qtdCasas; j++)
                    {
                        posiAtual -= passo;
                        Instanciador(posiAtual, corCasa);

                        corCasa = ++corCasa % ordemCores.Length;
                    }

                    //Última casa aponta para o próximo conector
                    ultimaCasa.GetComponent<CasaBase>().SetCasaSeguinte(_conector.rotas[i].conector);
                    //Conector atual aponta para a última casa
                    _conector.rotas[i].conector.GetComponent<CasaBase>().SetCasaAnterior(ultimaCasa);

                    //Define último índice do proximo conector
                    _conector.rotas[i].conector.GetComponent<Conector>().ultimaCor = corCasa;
                }
            }
        }

        void Instanciador(Vector3 posicao, int i)
        {
            GameObject novaCasa = Instantiate(ordemCores[i], posicao, Quaternion.identity);
            novaCasa.transform.parent = paiCasas;

            //Última Casa aponta para a Nova
            ultimaCasa.GetComponent<CasaBase>().SetCasaSeguinte(novaCasa.transform);
            //Nova Casa aponta para a Última 
            novaCasa.GetComponent<CasaBase>().SetCasaAnterior(ultimaCasa);
            //Define indice da casa
            novaCasa.GetComponent<CasaBase>().tipoCasa = i + 1;

            ultimaCasa = novaCasa.transform;
        }

        [ContextMenu("Resetar Tabuleiro")]
        void ResetTabuleiro()
        {
            foreach (Transform conexao in paiConectores)
            {
                Conector con = conexao.GetComponent<Conector>();
                con.ultimaCor = 0;
                con.casaSeguinte.Clear();
                con.casaSeguinte.Capacity = 0;
                con.casaAnterior.Clear();
                con.casaAnterior.Capacity = 0;
            }

            var temp = paiCasas.Cast<Transform>().ToList();
            foreach (Transform casa in temp)
            {
                DestroyImmediate(casa.gameObject);
            }
        }
    }
}