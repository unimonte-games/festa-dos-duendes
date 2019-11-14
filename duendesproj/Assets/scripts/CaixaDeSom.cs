using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaDeSom : MonoBehaviour
{
    public enum EfeitoSonoro
    {
        passoGrama,
    };

    static string[] efeitosArquivos = {
        "passoGrama"
    };

    public void CriarCaixa(EfeitoSonoro efeito)
    {
        AudioClip efeitoAudio = ObterAudio(efeito);
        if (efeitoAudio != null) {
            GameObject novaCaixa = Instantiate<GameObject>(
                new GameObject("Caixa de som " + efeito.ToString()),
                Vector3.zero,
                Quaternion.identity
            );

            AudioSource novaCaixa_as = novaCaixa.AddComponent<AudioSource>();
            novaCaixa_as.loop = false;
            novaCaixa_as.playOnAwake = false;
            novaCaixa_as.clip = efeitoAudio;
            novaCaixa_as.Play();

            Destroy(novaCaixa, novaCaixa_as.clip.length);
        }
    }

    AudioClip ObterAudio(EfeitoSonoro efeito)
    {
        switch (efeito)
        {
            case EfeitoSonoro.passoGrama:
                return Resources.Load<AudioClip>(efeitosArquivos[(int)efeito]);
        }

        Debug.LogWarning("efeito não encontrado: " + efeito.ToString());
        return null;
    }
}
