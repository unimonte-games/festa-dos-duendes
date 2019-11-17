using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Componentes.Jogador
{
    public class ParamAnimSync_Jogador : MonoBehaviour
    {
        Animator animator;
        Movimentador movimentador;
        Movimentacao movimentacao;
        Controlador ctrl;
        Transform spriteTr, tr;

        void Awake()
        {
            animator = GetComponent<Animator>();
            movimentador = GetComponent<Movimentador>();
            movimentacao = GetComponent<Movimentacao>();
            ctrl = GetComponent<Controlador>();
            tr = GetComponent<Transform>();
            spriteTr = tr.GetChild(0);
        }

        void Start()
        {
            animator.SetInteger(
                "cena atual", SceneManager.GetActiveScene().buildIndex
            );

            StartCoroutine(AjustadorDeLado());
        }

        void Update()
        {
            animator.SetBool("andando", EstaAndando());

            if (ctrl)
                if (ctrl.ObterEntradaJogador().acao1)
                    animator.SetTrigger("acao");
        }

        bool EstaAndando ()
        {
            if (movimentador) {
                return (
                    movimentador.velocidade > 0.1f &&
                    movimentador.direcao.magnitude > 0.1f
                );
                    ;
            } else if (movimentacao) {
                return (
                    movimentacao.emPulinho
                );
            }

            return false;
        }

        IEnumerator AjustadorDeLado()
        {
            // 1: antes; 2: agora
            float x1 = 0f, x2 = 0f, z1 = 0f, z2 = 0f;
            Vector3 escalaOriginal = tr.localScale;
            Vector3 escala = escalaOriginal;

            while (true)
            {
                x1 = x2;
                z1 = z2;

                yield return new WaitForSeconds(0.1f);

                x2 = tr.position.x;
                z2 = tr.position.z;

                float xDiff = x2 - x1;
                float zDiff = z2 - z1;
                float lado = 0f;

                if (xDiff < -0.01f || zDiff < -0.01f)
                    lado = -1f;
                else if (xDiff > 0.01f || zDiff > 0.01f)
                    lado = 1f;

                float ladoAbs = Mathf.Abs(lado);

                escala.x = escalaOriginal.x * (ladoAbs > 0.2f ? lado : 1f);
                tr.localScale = escala;
            }
        }
    }
}
