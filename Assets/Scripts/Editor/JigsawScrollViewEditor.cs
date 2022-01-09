using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(JigsawScrollView))]
public class JigsawScrollViewEditor : Editor
{
    JigsawScrollView scollview;

    public void OnEnable()
    {
        scollview = target as JigsawScrollView;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Check inside"))
        {
            //scollview.CheckInsideScrollView(scollview.testCollider);
        }
    }
}
