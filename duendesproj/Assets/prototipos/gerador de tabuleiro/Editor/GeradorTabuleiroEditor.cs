using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GeradorTabuleiro))]
public class GeradorTabuleiroEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GeradorTabuleiro gt = (GeradorTabuleiro)target;

        if (GUILayout.Button("Gerar Tabuleiro"))
        {
            gt.GerarCasas();
        }
    }
}
