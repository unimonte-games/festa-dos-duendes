using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gerenciadores;
using UnityEngine.SceneManagement;

public class TabuleiroRaiz : MonoBehaviour
{
    static TabuleiroRaiz _instancia;
    public GameObject tronco_gbj;
    static Scene cenaOriginal;

    void Start()
    {
        cenaOriginal = gameObject.scene;

        if (_instancia == null) // tabuleiro inicializado pela 1ª vez
        {
            _instancia = GetComponent<TabuleiroRaiz>();
            tronco_gbj.SetActive(true);
            return;
        }
        else // já há um gbj TabuleiroRaiz em DontDestroyOnLoad
        {
            Ativar();
            Destroy(gameObject);
            return;
        }
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
        tronco_gbj.SetActive(def);

        if (def) // deve ir ao tabuleiro
        {
            SceneManager.MoveGameObjectToScene(
                _instancia.gameObject,
                cenaOriginal
            );
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
