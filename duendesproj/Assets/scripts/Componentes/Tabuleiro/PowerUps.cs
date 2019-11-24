using UnityEngine;
using System.Reflection;
using Identificadores;
using Gerenciadores;
using Componentes.Jogador;
using System.Linq;
using UnityEngine.UI;

namespace Componentes.Tabuleiro
{
    public class PowerUps : MonoBehaviour
    {
        public GameObject pnlEscolherJogador;
        public Text textoBtn;
        private int jogadorEscolhido = -1;
        private TipoPowerUps powerUpEscolhido;

        private void Start()
        {
            textoBtn = pnlEscolherJogador.GetComponentInChildren<Text>();
        }

        public void escolheJogador()
        {
            int qtdJogadores = GerenciadorGeral.qtdJogadores;

            jogadorEscolhido++;
            jogadorEscolhido %= qtdJogadores;

            if (jogadorEscolhido == GerenciadorPartida.Turno)
                jogadorEscolhido = ++jogadorEscolhido % qtdJogadores;

            textoBtn.text = "Jogador " + (jogadorEscolhido+1);
        }

        public void AtivarPowerUp(int powerUp)
        {
            if (jogadorEscolhido != -1)
            {
                MethodInfo metodo = GetType().GetMethod(((TipoPowerUps)powerUp).ToString());

                metodo.Invoke(this, null);

                GerenciadorPartida gp = FindObjectOfType<GerenciadorPartida>();
                gp.StartCoroutine(gp.WaitNovaRodada(2.5f));
            }
        }

        public void AtivarEscolha(int i)
        {
            Inventario inv = GerenciadorPartida.InvAtual;
            if (i < inv.powerUps.Count)
            {
                jogadorEscolhido = -1;
                textoBtn.text = "Escolher Jogador";
                powerUpEscolhido = inv.powerUps[i].tipo;
                pnlEscolherJogador.SetActive(true);
                Debug.Log(powerUpEscolhido);
            }
        }

        public static void GincanaGratis()
        {
            //Não tem o que executar aqui mesmo
        }

        public static void TrocaTudo()
        {
            //TODO: escolher jogador
            int rand = Random.Range(0, GerenciadorGeral.qtdJogadores);

            Inventario jogador = GerenciadorPartida.OrdemJogadores[rand-1].GetComponent<Inventario>();

            PowerUp[] tempList = jogador.powerUps.ToArray();
            jogador.powerUps = GerenciadorPartida.InvAtual.powerUps;
            GerenciadorPartida.InvAtual.powerUps = tempList.ToList();
        }

        public static void PoeiraNosOlhos()
        {
            //TODO: escolher jogador
            int rand = Random.Range(0, GerenciadorGeral.qtdJogadores);

            Inventario jogador = GerenciadorPartida.OrdemJogadores[rand-1].GetComponent<Inventario>();


        }

        public static void Teletransporte()
        {
            Debug.Log("Teste");

        }

        public static void Espanador()
        {
            Debug.Log("Teste");

        }

        public static void MaoEscorregadia()
        {

            Debug.Log("Teste");
        }

        public static void Emprestador()
        {
            Debug.Log("Teste");

        }

        public static void LadraoDeBanco()
        {
            Debug.Log("Teste");

        }

        public static void PilhaDeFolhas()
        {
            Debug.Log("Teste");

        }

        public static void PausaParaBanheiro()
        {
            Debug.Log("Teste");

        }
        public static void SuperEspanador()
        {
            Debug.Log("Teste");

        }

        public static void SuperEmprestador()
        {
            Debug.Log("Teste");

        }

        public static void SuperPilhaDeFolhas()
        {
            Debug.Log("Teste");

        }
    }
}