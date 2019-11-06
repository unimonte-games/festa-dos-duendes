using System.Collections.Generic;
using UnityEngine;

namespace Componentes.Jogador
{
    public enum Itens 
        { Pote }

    public class Inventario : MonoBehaviour
    {
        public List<Itens> inv = new List<Itens>();
    }
}
