using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Componentes.Jogador
{
    public class ControladorPescaEscorrega : MonoBehaviour
    {
        public int pescados;

        float inerciaX, inerciaZ;
        Vector2 inerciaNorm;

        Transform tr;
        Controlador ctrl;
        Movimentador mov;
        Gerenciadores.GerenciadorPescaEscorrega gerenPE;

        void Awake ()
        {
            tr = GetComponent<Transform>();
            ctrl = GetComponent<Controlador>();
            mov = GetComponent<Movimentador>();
            gerenPE = FindObjectOfType<Gerenciadores.GerenciadorPescaEscorrega>();
        }

        void Start()
        {
            mov.velocidade = gerenPE.velocidadeMax;
        }

        void Update()
        {
            Controlador.EntradaJogador entradaJogador =
                ctrl.ObterEntradaJogador();

            // localizando variáveis
            float dt = Time.deltaTime;
            float aceleracao = gerenPE.aceleracao;
            float desaceleracao = gerenPE.desaceleracao;
            float lim = gerenPE.limiteArena;

            float pos_x = tr.position.x;
            float pos_z = tr.position.z;

            float eixoH = entradaJogador.eixoH;
            float eixoV = entradaJogador.eixoV;
            bool acao1 = entradaJogador.acao1;

            // controlando inércia
            // -> aceleração
            inerciaX = Mathf.Clamp(inerciaX + eixoH * aceleracao * dt, -1, 1);
            inerciaZ = Mathf.Clamp(inerciaZ + eixoV * aceleracao * dt, -1, 1);

            // -> desaceleração
            {
                inerciaX = Mathf.Clamp(
                    inerciaX - desaceleracao * dt * (inerciaX < 0 ? -1 : 1),
                    inerciaX < 0 ? -1 : 0,
                    inerciaX < 0 ?  0 : 1
                );

                inerciaZ = Mathf.Clamp(
                    inerciaZ - desaceleracao * dt * (inerciaZ < 0 ? -1 : 1),
                    inerciaZ < 0 ? -1 : 0,
                    inerciaZ < 0 ?  0 : 1
                );
            }


            // controlando direção do movimento
            inerciaNorm.x = inerciaX;
            inerciaNorm.y = inerciaZ;
            if (inerciaNorm.magnitude > 1)
                inerciaNorm.Normalize();

            mov.direcao.x = inerciaNorm.x;
            mov.direcao.z = inerciaNorm.y;

            // limitando dentro da arena pela direção do movimento
            if (pos_x >= lim && inerciaX > 0 || pos_x <= -lim && inerciaX < 0) {
                mov.direcao.x = 0;
                inerciaX = 0;
            }
            if (pos_z >= lim && inerciaZ > 0 || pos_z <= -lim && inerciaZ < 0) {
                mov.direcao.z = 0;
                inerciaZ = 0;
            }
        }

        public void PontuarPescado()
        {
            pescados++;
        }
    }
}
