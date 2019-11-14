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

        void Awake()
        {
            animator = GetComponent<Animator>();
            movimentador = GetComponent<Movimentador>();
            movimentacao = GetComponent<Movimentacao>();
            ctrl = GetComponent<Controlador>();
        }

        void Start()
        {
            animator.SetInteger(
                "cena atual", SceneManager.GetActiveScene().buildIndex
            );
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
    }
}
