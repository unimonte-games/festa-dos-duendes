using UnityEngine;
using System.Collections;
using Componentes.Tabuleiro;
using Identificadores;

namespace Componentes.Jogador
{
    public class Movimentacao : MonoBehaviour
    {
        //Achar casa
        public Gerenciadores.GerenciadorPartida _gerenPart;
        public Transform casaAtual;
        public TiposCasa proximaCor;
        public bool emPulinho;
        public bool paraFrente;
        public bool inicioMov;

        //Animar pulo
        private float duracaoPulo = 0.25f;

        void Start()
        {
            inicioMov = false;
            SetCasaAtual(casaAtual);
            inicioMov = true;
        }

        public void SetCasaAtual(Transform casa)
        {
            casaAtual = casa;
            transform.position = casa.position;
        }

        public IEnumerator ProcuraCasa(TiposCasa corDesejada)
        {           
            bool achou = false;
            Transform casaTemp = casaAtual;
            TiposCasa corTemp = casaTemp.GetComponent<CasaBase>().tipoCasa;

            if (inicioMov)
            {
                inicioMov = false;
                proximaCor = corDesejada; //Salva cor desejada
            }
            else
            {
                if (corTemp != 0 && corTemp == proximaCor)
                {
                    achou = true;
                    proximaCor = 0;
                }
                else
                {
                    do
                    {
                        if (paraFrente)
                            casaTemp = casaTemp.GetComponent<CasaBase>().casaSeguinte[0];
                        else
                            casaTemp = casaTemp.GetComponent<CasaBase>().casaAnterior[0];
                        corTemp = casaTemp.GetComponent<CasaBase>().tipoCasa;

                        yield return StartCoroutine(Pulinho(casaTemp.position, Time.time));

                        if (corTemp == 0)
                        {
                            CasaBase _casaBase = casaTemp.GetComponent<CasaBase>();
                            //Se o conector tem multiplos caminhos
                            if (_casaBase.casaSeguinte.Count > 1 || _casaBase.casaAnterior.Count > 1)
                            {
                                proximaCor = corDesejada; //Salva cor desejada
                                casaAtual = casaTemp;
                                break;
                            }
                        }
                        else if (corTemp == corDesejada || corTemp == proximaCor)
                        {
                            achou = true;
                            casaAtual = casaTemp;
                            proximaCor = 0;
                        }
                    } while (!achou);
                }

                yield return new WaitForSeconds(0.5f);
            }

            _gerenPart.fimMov(achou);
        }

        public IEnumerator Pulinho(Vector3 destino, float tempoInicio)
        {
            emPulinho = true;

            Vector3 centro = (transform.position + destino) * 0.5F;
            centro -= Vector3.up;

            Vector3 inicioRel = transform.position - centro;
            Vector3 finalRel = destino - centro;

            float x = (Time.time - tempoInicio) / duracaoPulo;

            transform.position = Vector3.Slerp(inicioRel, finalRel, x);
            transform.position += centro;

            yield return new WaitForSeconds(0.02f);

            if (x <= 1)
                yield return StartCoroutine(Pulinho(destino, tempoInicio));
            else
                emPulinho = false;
        }
    }
}
