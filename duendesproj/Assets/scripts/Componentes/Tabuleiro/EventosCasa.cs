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
        private Inventario invAtual;

        public void ativarCasa()
        {
            TiposCasa evento = GetComponent<CasaBase>().tipoCasa;
            MethodInfo metodo = GetType().GetMethod(evento.ToString());

            try { metodo.Invoke(this, null); }
            catch { Debug.LogError("Evento de Casa não encontrado"); }
        }

        public void Garrafa()
        {
            invAtual = GerenciadorPartida.InvAtual;
            invAtual.itens.Add(Itens.Garrafa);

            invAtual.transform.GetChild(1).gameObject.SetActive(true);
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
            invAtual = GerenciadorPartida.InvAtual;
            if (invAtual.moedas >= 25)
                GerenciadorGeral.TransitarParaMJ(minijogo);
        }

        public void Moeda()
        {
            invAtual = GerenciadorPartida.InvAtual;
            invAtual.moedas += 15;
        }
    }
}