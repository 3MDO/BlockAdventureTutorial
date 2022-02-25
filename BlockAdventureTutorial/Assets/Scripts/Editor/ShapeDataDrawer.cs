using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShapeData), false)]
[CanEditMultipleObjects]
[System.Serializable]
public class ShapeDataDrawer : Editor
{
   private ShapeData ShapeDataInstance => target as ShapeData;
   public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ClearBoardButton();
        EditorGUILayout.Space();

        DrawColumnsInputFields();
        EditorGUILayout.Space();

        if (ShapeDataInstance.board != null && ShapeDataInstance.rows > 0)
        {
            DrawBoardTable();
        }
        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(ShapeDataInstance);
        }
    }
   private void ClearBoardButton()
    {
        if (GUILayout.Button(text: "Clear Board"))
        {
            ShapeDataInstance.Clear();
        }
    }
    private void DrawColumnsInputFields()
    {
        var columnsTemp = ShapeDataInstance.columns;
        var rowsTemp = ShapeDataInstance.rows;

        ShapeDataInstance.columns = EditorGUILayout.IntField(label: "Columns", ShapeDataInstance.columns);
        ShapeDataInstance.rows = EditorGUILayout.IntField(label: "Rows", ShapeDataInstance.rows);

        if ((ShapeDataInstance.columns != columnsTemp || ShapeDataInstance.rows != rowsTemp) && ShapeDataInstance.columns > 0 && ShapeDataInstance.rows > 0)
        {
            ShapeDataInstance.CreateNewBoard();
        }
    }
    private void DrawBoardTable()
    {
        var tableStyle = new GUIStyle(other: "box");
        tableStyle.padding = new RectOffset(left:10, right:10, top:10, bottom:10);
        tableStyle.margin.left = 32;

        var headerColumnStyle = new GUIStyle();
        headerColumnStyle.fixedWidth = 65;
        headerColumnStyle.alignment = TextAnchor.MiddleCenter;
        
        var rowSytle = new GUIStyle();
        rowSytle.fixedHeight = 25; 
        rowSytle.alignment = TextAnchor.MiddleCenter;

        var dataFieldStyle = new GUIStyle(EditorStyles.miniButtonMid);
        dataFieldStyle.normal.background = Texture2D.grayTexture;
        dataFieldStyle.onNormal.background= Texture2D.whiteTexture;

        for (var row = 0; row < ShapeDataInstance.rows; row++)
        {
            EditorGUILayout.BeginHorizontal(headerColumnStyle);

            for(var column = 0; column < ShapeDataInstance.columns; column++)
            {
                EditorGUILayout.BeginHorizontal(rowSytle);
                var data = EditorGUILayout.Toggle(ShapeDataInstance.board[row].column[column], dataFieldStyle);
                ShapeDataInstance.board[row].column[column] = data;
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
    }

}
