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

            Conector[] conectores = gt.conectores.GetComponentsInChildren<Conector>();
            for (int i = 1; i < conectores.Length; i++)
                EditorUtility.SetDirty(conectores[i]);
        }
    }
}
