using System.Reflection;
using UnityEngine;
using Componentes.Jogador;
using Gerenciadores;
using Identificadores;

namespace Componentes.Tabuleiro
{
    public class EventosCasa : MonoBehaviour
    {
        public CenaID minijogo;

        public void ativarCasa()
        {
            TiposCasa evento = GetComponent<CasaBase>().tipoCasa;
            MethodInfo metodo = GetType().GetMethod(evento.ToString());

            try { metodo.Invoke(this, null); }
            catch { Debug.LogError("Evento de Casa não encontrado"); }
        }

        public void Garrafa()
        {
            Debug.Log("Garrafou");
        }

        public void BemMal()
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

        public void MiniJogo()
        {
            Gerenciadores.GerenciadorGeral.TransitarParaMJ(minijogo);
        }

        public void Moeda()
        {
            Debug.Log("Dinheiro");
        }
    }
}