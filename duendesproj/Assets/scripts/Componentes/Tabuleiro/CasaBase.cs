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
