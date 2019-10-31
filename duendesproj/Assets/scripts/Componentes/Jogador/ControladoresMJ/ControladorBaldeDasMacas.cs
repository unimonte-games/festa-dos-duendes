using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Componentes.Jogador
{
    public class ControladorBaldeDasMacas : MonoBehaviour
    {
        public int macasPegas;
        bool emPulo;

        Transform tr, macaAnterior;
        Controlador ctrl;
        Movimentador mov;
        Gerenciadores.GerenciadorBaldeDasMacas gerenBM;

        void Awake ()
        {
            tr = GetComponent<Transform>();
            ctrl = GetComponent<Controlador>();
            mov = GetComponent<Movimentador>();
            gerenBM = FindObjectOfType<Gerenciadores.GerenciadorBaldeDasMacas>();
        }

        void Start ()
        {
            mov.velocidade = gerenBM.velocidadeMov;
        }

        void Update()
        {
            Controlador.EntradaJogador entradaJogador =
                ctrl.ObterEntradaJogador();

            // localizando variáveis
            float eixoH = entradaJogador.eixoH;
            float pos_x = tr.position.x;

            // Ativando pulo se o jogador apertou o botão de ação 1
            if (!emPulo && entradaJogador.acao1)
            {
                emPulo = true;
                StartCoroutine(Co_Pulo());
            }

            // direcao de movimento, não pode pudar se estiver durante o pulo
            if (!emPulo)
            {
                if (eixoH > 0)
                    mov.direcao = Vector3.right;
                else if (eixoH < 0)
                    mov.direcao = Vector3.left;
                else
                    mov.direcao = Vector3.zero;
            }

            // limitando dentro do limite da fase através da velocidade
            if (Mathf.Abs(pos_x) >= gerenBM.limX)
            {
                if (!emPulo)
                {
                    if (eixoH > 0 && pos_x < 0 || eixoH < 0 && pos_x > 0)
                        mov.velocidade = gerenBM.velocidadeMov;
                    else
                        mov.velocidade = 0;
                }
                else
                    mov.velocidade = 0;
            }
        }

        public Transform PontuarMaca(Transform proximaMaca)
        {
            macasPegas++;
            Transform _macaAnterior = macaAnterior;
            macaAnterior = proximaMaca;
            return _macaAnterior;
        }

        IEnumerator Co_Pulo()
        {
            float tPulo = 0;
            float altura = 0;

            Vector3 pos = tr.position;

            while(emPulo)
            {
                tPulo += Time.deltaTime * gerenBM.velPulo;
                altura = Mathf.Sin(tPulo) * gerenBM.tamPulo;


                if (altura <= 0)
                {
                    emPulo = false;
                    altura = 0;
                }

                pos = tr.position;
                pos.y = altura;
                tr.position = pos;

                yield return new WaitForEndOfFrame();
            }

            pos.y = 0;
            tr.position = pos;
        }
    }
}
