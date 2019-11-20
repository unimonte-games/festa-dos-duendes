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

        float escalaXAlvo;
        float xDiff, zDiff;
        int lado = 1;

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

            StartCoroutine(ReconhecedorDeLado());
            escalaXAlvo = tr.localScale.x;
        }

        void Update()
        {
            animator.SetBool("andando", EstaAndando());

            if (ctrl)
                if (ctrl.ObterEntradaJogador().acao1)
                    animator.SetTrigger("acao");

            AjustarEscala();
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

        IEnumerator ReconhecedorDeLado()
        {
            // 1: antes; 2: agora
            float x1 = 0f, x2 = 0f, z1 = 0f, z2 = 0f;
            float escala_x = tr.localScale.x;

            while (true)
            {
                x1 = x2;
                z1 = z2;

                yield return new WaitForSeconds(0.1f);

                x2 = tr.position.x;
                z2 = tr.position.z;

                xDiff = x2 - x1;
                zDiff = z2 - z1;

                if (xDiff < -0.01f || zDiff < -0.01f)
                    lado = -1;
                else if (xDiff > 0.01f || zDiff > 0.01f)
                    lado = 1;

                escalaXAlvo = escala_x * lado;
            }
        }

        void AjustarEscala()
        {
            Vector3 escala = tr.localScale;

            escala.x = Mathf.Lerp(
                escala.x,
                escalaXAlvo,
                Time.deltaTime * 4
            );

            tr.localScale = escala;
        }
    }
}
