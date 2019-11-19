using System.Collections.Generic;
using UnityEngine;
using Identificadores;

namespace Componentes.Jogador
{
    public class Inventario : MonoBehaviour
    {
        public int moedas = 0;
        public List<Objetos> objetos = new List<Objetos>();
        public List<PowerUps> powerUps = new List<PowerUps>();
        public List<Itens> itens = new List<Itens>();
        public bool tirarNaoPegaObj = false;
        public bool tirarGarrafa = false;
    }
}
