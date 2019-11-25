using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gerenciadores;
using UnityEngine.SceneManagement;

namespace Componentes.Tabuleiro
{
    public class TabuleiroRaiz : MonoBehaviour
    {
        static TabuleiroRaiz _instancia;
        public GameObject tronco_gbj;
        static Scene cenaOriginal;
        public GerenciadorPartida gp;

        void Start()
        {
            Debug.Log("START", gameObject);
            cenaOriginal = gameObject.scene;

            if (_instancia == null) // tabuleiro inicializado pela 1ª vez
            {
                Debug.Log("instancia = null ", gameObject);
                _instancia = GetComponent<TabuleiroRaiz>();
                tronco_gbj.SetActive(true);
                return;
            }
            else // já há um gbj TabuleiroRaiz em DontDestroyOnLoad
            {
                Debug.Log("instancia nao null ", gameObject);
                Ativar();
                Destroy(gameObject);
                return;
            }
        }

        void Reinicializa()
        {
            gp.StartCoroutine(gp.WaitNovaRodada(2.5f));
        }

        public static void Ativar()
        {
            _instancia.DefAtivacao(true);
        }

        public static void Desativar()
        {
            _instancia.DefAtivacao(false);
        }

        void DefAtivacao(bool def)
        {
            Debug.Log("DEFATIVACAO" + def.ToString(), gameObject);
            tronco_gbj.SetActive(def);

            if (def) // deve ir ao tabuleiro
            {
                SceneManager.MoveGameObjectToScene(
                    _instancia.gameObject,
                    cenaOriginal
                );
                _instancia.Reinicializa();
            }
            else // deve ir ao DontDestroyOnLoad
            {
                SceneManager.MoveGameObjectToScene(
                    _instancia.gameObject,
                    GerenciadorGeral.ObterInstancia().gameObject.scene
                );
            }
        }
    }
}
