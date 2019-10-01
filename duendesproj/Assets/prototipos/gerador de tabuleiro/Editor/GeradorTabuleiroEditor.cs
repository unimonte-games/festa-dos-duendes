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
            Conector[] conectores = gt.paiConectores.GetComponentsInChildren<Conector>();
            for (int i = 0; i < conectores.Length; i++)
                EditorUtility.SetDirty(conectores[i]);

            gt.GerarCasas();
        }
    }
}
