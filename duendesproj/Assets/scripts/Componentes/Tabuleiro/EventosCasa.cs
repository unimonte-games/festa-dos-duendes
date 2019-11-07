using System.Reflection;
using UnityEngine;
using Componentes.Jogador;
using Gerenciadores;

namespace Componentes.Tabuleiro
{
    public class EventosCasa : MonoBehaviour
    {
        public string nomeEvento;
        public int aux;

        public void ativarCasa()
        {
            MethodInfo metodo = GetType().GetMethod(nomeEvento);
            try
            {
                metodo.Invoke(this, null);
            }
            catch { }
        }

        public void Pote()
        {
            Inventario _inventario = GerenciadorPartida.InvAtual;
            _inventario.inv.Add(Itens.Pote);
        }

        public void Elementos()
        {
            Debug.Log("Água, fogo, terra ou ar");
        }

        public void PowerUp()
        {
            Debug.Log("PowerUp aleatório");
        }

        public void Acontecimento()
        {
            Debug.Log("Acontecimento aleatório");
        }

        public void Minijogo()
        {
            Gerenciadores.GerenciadorGeral.TransitarParaMJ((Identificadores.CenaID)aux);
        }

        public void Portal()
        {
            Debug.Log("Teleportar");
        }
    }
}