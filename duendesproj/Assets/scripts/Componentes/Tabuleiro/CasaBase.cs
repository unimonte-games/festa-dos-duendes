using UnityEngine;
using System.Collections.Generic;
using Identificadores;

namespace Componentes.Tabuleiro
{
    public class CasaBase : MonoBehaviour
    {
        public List<Transform> casaSeguinte;
        public List<Transform> casaAnterior;
        public TiposCasa tipoCasa;

        public static int contadorMiniJogo = 0;

        void Start()
        {
            if (tipoCasa == TiposCasa.MiniJogo)
            {
                contadorMiniJogo += 1;
                EventosCasa evtCasa = GetComponent<EventosCasa>();
                switch (contadorMiniJogo)
                {
                    case 1: evtCasa.minijogo = CenaID.QuebraBotao;    break;
                    case 2: evtCasa.minijogo = CenaID.BaldeDasMacas;  break;
                    case 3: evtCasa.minijogo = CenaID.PescaEscorrega; break;
                    case 4: evtCasa.minijogo = CenaID.CogumeloQuente; break;
                    case 5: evtCasa.minijogo = CenaID.FlautaHero;     break;
                }
            }
        }

        public virtual void SetCasaSeguinte(Transform casa)
        {
            casaSeguinte.Add(casa);
        }

        public virtual void SetCasaAnterior(Transform casa)
        {
            casaAnterior.Add(casa);
        }

        [ContextMenu("Atualizar Casa")]
        public virtual void AtualizaCasa()
        {
            string matPath = "MatCasas/" + tipoCasa.ToString();
            GetComponent<MeshRenderer>().material = (Material)Resources.Load(matPath);
        }

    }
}
