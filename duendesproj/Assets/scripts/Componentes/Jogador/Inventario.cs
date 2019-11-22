using System.Collections.Generic;
using UnityEngine;
using Identificadores;

namespace Componentes.Jogador
{
    public class Inventario : MonoBehaviour
    {
        public int moedas = 0;
        public List<Objetos> objetos = new List<Objetos>();
        public List<PowerUp> powerUps = new List<PowerUp>();
        public int rodadasPreso = 0;
        public int rodadasSemObj = 0;
    }
}
