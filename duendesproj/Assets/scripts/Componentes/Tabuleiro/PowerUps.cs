using UnityEngine;
using System.Reflection;
using Identificadores;

namespace Componentes.Tabuleiro
{
    public class PowerUps : MonoBehaviour
    {
        public void AtivarPowerUp(int powerUp)
        {
            MethodInfo metodo = GetType().GetMethod(
                ((PowerUp)powerUp).ToString());

            try
            {
                metodo.Invoke(this, null);
            }
            catch (System.Exception e) { Debug.LogError(e); }
        }

        public static void GincanaGratis()
        {
            //Não tem o que executar aqui mesmo
        }

        public static void TrocaTudo()
        {
            Debug.Log("Teste");

        }

        public static void PoeiraNosOlhos()
        {
            Debug.Log("Teste");

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