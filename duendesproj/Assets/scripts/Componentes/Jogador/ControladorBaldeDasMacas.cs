using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Componentes.Jogador
{
    public class ControladorBaldeDasMacas : MonoBehaviour
    {
        int macasPegas;
        bool emPulo;

        Transform tr;
        Controlador ctrl;
        Movimentador mov;
        Gerenciadores.GerenciadorBaldeDasMacas gerenBM;

        static readonly Vector3 v3_r = Vector3.right;

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

            if (!emPulo)
            {
                float posx = Mathf.Abs(tr.position.x);

                if (posx >= gerenBM.limX)
                    mov.direcao = Vector3.zero;
                else
                {
                    mov.direcao = v3_r * entradaJogador.eixoH;

                    if (entradaJogador.acao1)
                    {
                        emPulo = true;
                        Pular();
                    }
                }
            }

        }

        public void PontuarMaca()
        {
            macasPegas++;
        }

        void Pular()
        {
            StartCoroutine(Co_Pulo());
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
