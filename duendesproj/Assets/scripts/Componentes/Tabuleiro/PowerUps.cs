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
        public EscolheRota _escolheRota;
        public GameObject pnlEscolherJogador;
        public Text textoBtn;
        private TipoPowerUps powerUpEscolhido;
        private static int jogadorEscolhido = -1;

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

        public void AtivarPowerUp()
        {
            if (jogadorEscolhido != -1)
            {
                pnlEscolherJogador.SetActive(false);
                _escolheRota.AlteraEstadoPowerUps();

                MethodInfo metodo = GetType().GetMethod(powerUpEscolhido.ToString());

                metodo.Invoke(this, null);

                GerenciadorPartida gp = FindObjectOfType<GerenciadorPartida>();
                gp.NovaRodada();
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
            Inventario jogador = 
                GerenciadorPartida.OrdemJogadores[jogadorEscolhido]
                .GetComponent<Inventario>();

            PowerUp[] tempList = jogador.powerUps.ToArray();
            jogador.powerUps = GerenciadorPartida.InvAtual.powerUps;
            GerenciadorPartida.InvAtual.powerUps = tempList.ToList();
        }

        public static void PoeiraNosOlhos()
        {
            Inventario jogador =
                GerenciadorPartida.OrdemJogadores[jogadorEscolhido]
                .GetComponent<Inventario>();

            jogador.rodadasSemObj = 2;
        }

        public static void Teletransporte()
        {
            Movimentacao jogador =
                GerenciadorPartida.OrdemJogadores[jogadorEscolhido]
                .GetComponent<Movimentacao>();

            Transform casaTemp = jogador.casaAtual;
            jogador.SetCasaAtual(GerenciadorPartida.MovAtual.casaAtual);
            GerenciadorPartida.MovAtual.SetCasaAtual(casaTemp);
        }

        public static void Espanador()
        {
            Inventario jogador =
                GerenciadorPartida.OrdemJogadores[jogadorEscolhido]
                .GetComponent<Inventario>();

            jogador.powerUps.RemoveAt(jogador.powerUps.Count - 1);
        }

        public static void MaoEscorregadia()
        {
            Inventario jogador =
                GerenciadorPartida.OrdemJogadores[jogadorEscolhido]
                .GetComponent<Inventario>();

            TipoPowerUps pwTemp = jogador.powerUps[jogador.powerUps.Count - 1].tipo;
            jogador.powerUps.RemoveAt(jogador.powerUps.Count - 1);

            jogador = GerenciadorPartida.InvAtual;
            if (jogador.powerUps.Count < 3)
                jogador.AddPowerUp(pwTemp);
        }

        public static void Emprestador()
        {
            Inventario jogador =
                GerenciadorPartida.OrdemJogadores[jogadorEscolhido]
                .GetComponent<Inventario>();

            Objetos objTemp = jogador.objetos[jogador.objetos.Count - 1];
            jogador.RemoveObjeto(1);
            jogador = GerenciadorPartida.InvAtual;
            jogador.AddObjeto(objTemp);
        }

        public static void LadraoDeBanco()
        {
            Inventario jogador =
                GerenciadorPartida.OrdemJogadores[jogadorEscolhido]
                .GetComponent<Inventario>();

            jogador.AlteraMoeda(-10);
            jogador = GerenciadorPartida.InvAtual;
            jogador.AlteraMoeda(+10);
        }

        public static void PausaParaBanheiro()
        {
            Inventario jogador =
                GerenciadorPartida.OrdemJogadores[jogadorEscolhido]
                .GetComponent<Inventario>();

            jogador.rodadasPreso = 2;
        }
        public static void SuperEspanador()
        {
            Inventario jogador =
                GerenciadorPartida.OrdemJogadores[jogadorEscolhido]
                .GetComponent<Inventario>();

            jogador.powerUps.RemoveAt(jogador.powerUps.Count - 1);
            jogador.powerUps.RemoveAt(jogador.powerUps.Count - 1);
        }

        public static void SuperEmprestador()
        {
            Emprestador();
            Emprestador();
        }
    }
}