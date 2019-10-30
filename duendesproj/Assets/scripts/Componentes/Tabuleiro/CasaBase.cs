using UnityEngine;
using System.Collections.Generic;

namespace Componentes.Tabuleiro
{
    public class CasaBase : MonoBehaviour
    {
        public List<Transform> casaSeguinte;
        public List<Transform> casaAnterior;

        [HideInInspector]
        public int tipoCasa;

        public virtual void SetCasaSeguinte(Transform casa)
        {
            casaSeguinte.Add(casa);
        }

        public virtual void SetCasaAnterior(Transform casa)
        {
            casaAnterior.Add(casa);
        }
    }
}
