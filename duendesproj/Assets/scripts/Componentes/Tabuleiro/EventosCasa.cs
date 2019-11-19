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
            catch(System.Exception e) { Debug.LogError(e); }
        }

        public void Garrafa()
        {
            invAtual = GerenciadorPartida.InvAtual;
            invAtual.itens.Add(Itens.Garrafa);

            invAtual.transform.GetChild(1).gameObject.SetActive(true);
            invAtual.tirarGarrafa = false;
        }

        public void BemMal()
        {
            MethodInfo[] metodos;
            System.Type tipo;
            float rand = Random.value;

            if (rand < 0.5f)
                tipo = typeof(Bencaos);
            else
                tipo = typeof(Maldicoes);

            metodos = tipo.GetMethods(BindingFlags.DeclaredOnly |
                                      BindingFlags.Static |
                                      BindingFlags.Public);

            int rd = Random.Range(0, metodos.Length);
            try
            {
                MethodInfo metodoRand = metodos[rd];
                metodoRand.Invoke(this, null);
            }
            catch (System.Exception e) { Debug.LogError(e); }
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