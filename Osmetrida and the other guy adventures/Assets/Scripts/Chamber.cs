using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Camara", menuName = "Chamber", order = 1)]
public class Chamber : ScriptableObject
{
    public int[] mapMatrix;
    public bool[] allowedConnections = new bool[8];
    public int[] connectionPoints = new int[8];
    public Chamber[] connections = new Chamber[8];
    public ChamberType type;
    public ChamberConnectionType connectionType;

    public Vector2 Origin;
}

public enum ChamberType
{
    Normal,
    Jump,
    DoubleJump,
    Dash,
    Blink,
    Save,
    Start,
    End,
}
