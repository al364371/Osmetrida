using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class ChamberCreator : MonoBehaviour
{
    public ChamberConnectionType connectionType;
    public ChamberType chamberType;
    public string chamberName;
    public string chamberCode;

    public bool Top, Bottom, Left, Right, Top2, Bottom2, Left2, Right2;
    public int  TopPos, BottomPos, LeftPos, RightPos, TopPos2, BottomPos2, LeftPos2, RightPos2, sizeX, sizeY;

    public void GenerateChamber()
    {
        Chamber generatedChamber = new Chamber();
        generatedChamber.type = chamberType;
        generatedChamber.name = chamberName;
        generatedChamber.connectionType = connectionType;
        GenerateConnectionBools(generatedChamber);
        GenerateConnectionInts(generatedChamber);
        GenerateMatrix(generatedChamber);
//        GenerateAsset(generatedChamber);
    }
    public void GenerateConnectionBools(Chamber target)
    {
        target.allowedConnections[0] = Top;
        target.allowedConnections[1] = Bottom;
        target.allowedConnections[2] = Left;
        target.allowedConnections[3] = Right;
        target.allowedConnections[4] = Top2;
        target.allowedConnections[5] = Bottom2;
        target.allowedConnections[6] = Left2;
        target.allowedConnections[7] = Right2;
    }
    public void GenerateConnectionInts(Chamber target)
    {
        target.connectionPoints[0] = TopPos;
        target.connectionPoints[1] = BottomPos;
        target.connectionPoints[2] = LeftPos;
        target.connectionPoints[3] = RightPos;
        target.connectionPoints[4] = TopPos2;
        target.connectionPoints[5] = BottomPos2;
        target.connectionPoints[6] = LeftPos2;
        target.connectionPoints[7] = RightPos2;
    }
    public void GenerateMatrix(Chamber target)
    {
        char[] arraypositions = chamberCode.ToCharArray();
        target.mapMatrix = new int[sizeX * sizeY];
        for(int position = 0; position < target.mapMatrix.Length; position++)
        {
                target.mapMatrix[position] = int.Parse(arraypositions[position].ToString());
        }
    }



/* 
    [MenuItem("Asset/Create/Chamber")]
    public void GenerateAsset(Chamber target)
    {
        string path = "Assets/Resources/Chambers/"+ connectionType+ "/"+ chamberType;
        System.IO.Directory.CreateDirectory(path);
        AssetDatabase.CreateAsset(target, path + "/" + chamberName + ".asset");
    }*/
}
/* 
[CustomEditor(typeof(ChamberCreator))]
public class ChamberCreatorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        ChamberCreator generador = (ChamberCreator)target;
        DrawDefaultInspector();

        if(GUILayout.Button("Crear camara"))
        {
            generador.GenerateChamber();
        }
    }
}
*/
public enum ChamberConnectionType
{
    Top = 1,
    Bottom = 2,
    Left = 3,
    Right = 4,
    LeftRight = 5,
    TopBottom = 6,
    BottomLeft = 7,
    BottomRight = 8,
    TopLeft = 9,
    TopRight = 10,
    EverythingButLeft = 11,
    EverythingButRight = 12,
    EverythingButTop = 13,
    EverythingButBottom = 14,
    AllFour = 15,
}
