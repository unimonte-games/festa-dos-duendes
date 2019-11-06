using UnityEngine;
using System.Collections.Generic;
using Identificadores;

namespace Componentes.Tabuleiro
{
    [System.Serializable]
    public struct Rota
    {
        public Transform conector;
        [Range(0, 20)]
        public int qtdCasas;
    }

    public class Conector : CasaBase
    {
        [SerializeField]
        public List<Rota> rotas;

        [ContextMenu("Re-Gerar Rotas")]
        public void GerarRota()
        {
            casaSeguinte.Clear();
            FindObjectOfType<GeradorTabuleiro>().InstanciaRotas(transform);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            for (int i = 0; i < rotas.Count; i++)
            {
                if (rotas[i].conector != null)
                    Gizmos.DrawLine(transform.position, rotas[i].conector.position);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            for (int i = 0; i < rotas.Count; i++)
            {
                if (rotas[i].conector != null)
                    Gizmos.DrawWireSphere(rotas[i].conector.position, 0.3f);
            }
        }
    }
}
