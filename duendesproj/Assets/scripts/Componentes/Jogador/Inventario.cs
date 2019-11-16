using System.Collections.Generic;
using UnityEngine;
using Identificadores;

namespace Componentes.Jogador
{
    public class Inventario : MonoBehaviour
    {
        public int moedas = 0;
        public List<Itens> itens = new List<Itens>();
    }
}
