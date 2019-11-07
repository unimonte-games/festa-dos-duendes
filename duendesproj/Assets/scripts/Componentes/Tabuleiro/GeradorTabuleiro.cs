using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Componentes.Tabuleiro
{
    public class GeradorTabuleiro : MonoBehaviour
    {
        [Tooltip("Pai dos objetos conectores")]
        public Transform paiConectores;
        [Tooltip("Pai onde ficarão as casas")]
        public Transform paiCasas;
        public GameObject casaPrefab;
        public static List<Material> coresCasas = new List<Material>();
        private Transform ultimaCasa;

        public void GerarCasas()
        {
            ResetTabuleiro();

            //Laço de todas conexões (tabuleiro inteiro)
            foreach (Transform conector in paiConectores)
            {
                InstanciaRotas(conector);                
            }
        }

        public void AtualizaCasas()
        {
            foreach (Transform casa in paiCasas)
            {
                CasaBase _casaBase = casa.GetComponent<CasaBase>();
                _casaBase.AtualizaCasa();
            }
        }

        public void InstanciaRotas(Transform conObj)
        {
            Conector _conector = conObj.GetComponent<Conector>();

            for (int i = 0; i < _conector.rotas.Count; i++)
            {
                Vector3 passo = (conObj.position - _conector.rotas[i].conector.position) / (_conector.rotas[i].qtdCasas + 1);
                Vector3 posiAtual = _conector.transform.position;

                ultimaCasa = conObj;

                //Laço de casas
                for (int j = 0; j < _conector.rotas[i].qtdCasas; j++)
                {
                    posiAtual -= passo;
                    InstanciaCasa(posiAtual);
                }

                //Última casa aponta para o próximo conector
                ultimaCasa.GetComponent<CasaBase>().SetCasaSeguinte(_conector.rotas[i].conector);
                //Conector atual aponta para a última casa
                _conector.rotas[i].conector.GetComponent<CasaBase>().SetCasaAnterior(ultimaCasa);
            }
        }

        private void InstanciaCasa(Vector3 posicao)
        {
            GameObject novaCasa = Instantiate(casaPrefab, posicao, Quaternion.identity);
            novaCasa.transform.parent = paiCasas;

            //Última Casa aponta para a Nova
            ultimaCasa.GetComponent<CasaBase>().SetCasaSeguinte(novaCasa.transform);
            //Nova Casa aponta para a Última 
            novaCasa.GetComponent<CasaBase>().SetCasaAnterior(ultimaCasa);

            ultimaCasa = novaCasa.transform;
        }

        [ContextMenu("Resetar Tabuleiro")]
        private void ResetTabuleiro()
        {
            foreach (Transform conexao in paiConectores)
            {
                Conector con = conexao.GetComponent<Conector>();
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