using UnityEngine;
using System.Collections;

namespace Componentes.Tabuleiro
{
    public class Movimentacao : MonoBehaviour
    {
        public Transform casaAtual;
        [HideInInspector]
        public int proximaCor;
        private Vector3 destino;
        private float tempoInicio;
        private float duracaoPulo;

        void Start()
        {
            SetCasa(casaAtual);
        }

        public void SetCasa(Transform novaCasa)
        {
            casaAtual = novaCasa;
            transform.position = casaAtual.position;
        }

        public bool ProcuraCasa(int corDesejada)
        {
            bool achou = false;
            Transform casaTemp = casaAtual;
            int corTemp = casaTemp.GetComponent<CasaBase>().tipoCasa;

            if (corTemp != 0 && corTemp == proximaCor)
            {
                proximaCor = 0;
                SetCasa(casaTemp); //Avança posição
            }
            else
            {
                do
                {
                    casaTemp = casaTemp.GetComponent<CasaBase>().casaSeguinte[0];
                    corTemp = casaTemp.GetComponent<CasaBase>().tipoCasa;

                    if (corTemp == 0)
                    {
                        CasaBase _casaBase = casaTemp.GetComponent<CasaBase>();
                        if (_casaBase.casaSeguinte.Count > 1) //Se o conector tem multiplos caminhos
                        {
                            proximaCor = corDesejada; //Salva cor desejada
                            SetCasa(casaTemp); //Avança posição
                            return false;
                        }
                    }
                    else if (corTemp == corDesejada || corTemp == proximaCor)
                    {
                        achou = true;
                        SetCasa(casaTemp); //Avança posição
                    }
                } while (!achou);
            }

            return true;
        }

        //public IEnumerator Pulinho()
        //{
        //    Vector3 center = (transform.position + destino) * 0.5F;
        //    center -= Vector3.up;

        //    Vector3 riseRelCenter = transform.position - center;
        //    Vector3 setRelCenter = destino - center;

        //    float x = (Time.time - tempoInicio) / duracaoPulo;

        //    transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, x);
        //    transform.position += center;

        //    yield return new WaitForSeconds(0.02f);

        //    if (x <= 1)
        //        yield return StartCoroutine(Pulinho());
        //    else
        //        yield return null;
        //}
    }
}
