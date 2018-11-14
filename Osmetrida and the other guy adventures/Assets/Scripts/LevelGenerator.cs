using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class LevelGenerator : MonoBehaviour
{
    public int[] probabilities = new int[15];

    public int mapSizeX;
    public int mapSizeY;
    public int MapLength;

    public int seed = -1;

    private int[,] theMapMatrix;
    private Chamber[,] chamberMatrix;
    private Queue<Vector2> chambersIncomplete = new Queue<Vector2>();

    private Chamber[][] chamberPool = new Chamber[15][];
    public RuleTile[] tiles;
    void Start()
    {
        SetRandomSeed();
        theMapMatrix = new int[mapSizeX,mapSizeY];
        GenerateMapMatrix(ref theMapMatrix);
        chamberMatrix = ChamberGetter(ref theMapMatrix);
        string path = @"c:\temp\MyTest.txt";
        using (StreamWriter sw = File.CreateText(path)) 
        {
            for(int i=theMapMatrix.GetLength(1) -1; i>0; i--)
            {
                string line = "";
                for(int j=0; j<theMapMatrix.GetLength(0); j++)
                {
                    Debug.Log(chamberMatrix[i,j].name + " " + chamberMatrix[i,j].type);
                    if(theMapMatrix[j,i] != 0)
                    {
                        line += "[ ]";
                    }
                    else
                    {
                        line += "   ";
                    }
                }
                sw.WriteLine(line);
            }
        }
    }

    private void SetRandomSeed()
    {
        if(seed == -1)
        {
            seed = Random.Range(-4000000,4000000);
        }
        Random.InitState(seed);
    }


    private void GenerateMapMatrix(ref int[,] mapMatrix)
    {
        int positionsCreated = 0;
        Vector2 initialChamberPosition = GenerateInitialChamber(ref mapMatrix);
        chambersIncomplete.Enqueue(initialChamberPosition);
        while(positionsCreated < MapLength)
        {
            int initialX = mapSizeX/2;
            int initialY = mapSizeY/2;
            InsertNewChamber(ref mapMatrix);
            positionsCreated++;
        }
        CloseMap( ref mapMatrix);
    }

    private Vector2 GenerateInitialChamber(ref int[,] mapMatrix)
    {
        int chosenType = Random.Range((int) ChamberConnectionType.Left , (int) ChamberConnectionType.Right + 1);
        int initialX = mapSizeX/2;
        int initialY = mapSizeY/2;
        mapMatrix[initialX,initialY] = chosenType;
        return new Vector2(initialX, initialY);
    }

    private void InsertNewChamber(ref int[,] mapMatrix)
    {
        Vector2 positionToTest = chambersIncomplete.Dequeue();
        while(IsComplete(positionToTest, ref mapMatrix))
        {
            positionToTest = chambersIncomplete.Dequeue();
        }
        Vector2 positionToCreate = GetPositionToCreate(positionToTest, ref mapMatrix);
        List<ChamberConnectionType> newChamberPossibilities = availableChamberConnectionTypes(positionToCreate, ref mapMatrix);
        ChamberConnectionType chamberType = SetChamber(newChamberPossibilities);
        mapMatrix[(int)positionToCreate.x,(int)positionToCreate.y] = (int) chamberType;
        if(!IsComplete(positionToTest, ref mapMatrix))
        {
            chambersIncomplete.Enqueue(positionToTest);
        } 
        if(!IsComplete(positionToCreate, ref mapMatrix))
        {
            chambersIncomplete.Enqueue(positionToCreate);
        }
    }

    private void CloseMap(ref int[,] mapMatrix)
    {
        while(chambersIncomplete.Count > 0)
        {
            Vector2 positionToTest = chambersIncomplete.Dequeue();
            while(IsComplete(positionToTest, ref mapMatrix))
            {
                positionToTest = chambersIncomplete.Dequeue();
            }
            Vector2 positionToCreate = GetPositionToCreate(positionToTest, ref mapMatrix);
            List<ChamberConnectionType> newChamberPossibilities = availableChamberConnectionTypes(positionToCreate, ref mapMatrix, true);
            ChamberConnectionType chamberType = SetChamber(newChamberPossibilities);
            mapMatrix[(int)positionToCreate.x,(int)positionToCreate.y] = (int) chamberType;
            if(!IsComplete(positionToTest, ref mapMatrix))
            {
                chambersIncomplete.Enqueue(positionToTest);
            }
        }
    }

    private Vector2 GetPositionToCreate(Vector2 postionToCheck, ref int[,] mapMatrix)
    {
        bool[] positionConnections = AvailabilityByType(mapMatrix[(int) postionToCheck.x, (int) postionToCheck.y]);
        bool[] donePositions = new bool[4];
        donePositions = CheckPositions(postionToCheck, ref mapMatrix);
        List<ChamberConnectionType> availableConnections = new List<ChamberConnectionType>();
        for(int i= 0; i< 4; i++)
        {
            if(positionConnections[i] && donePositions[i])
            {
                availableConnections.Add((ChamberConnectionType) i+1);
            }
        }
        ChamberConnectionType chosenType = availableConnections[Random.Range(0,availableConnections.Count)];
        Vector2 result = new Vector2();
        switch(chosenType)
        {
            case ChamberConnectionType.Top:
                result = new Vector2(postionToCheck.x, postionToCheck.y + 1);
                return result;
            case ChamberConnectionType.Bottom:
                result = new Vector2(postionToCheck.x, postionToCheck.y - 1);
                return result;
            case ChamberConnectionType.Left:
                result = new Vector2(postionToCheck.x -1, postionToCheck.y);
                return result;
            default:
                result = new Vector2(postionToCheck.x +1, postionToCheck.y);
                return result;
        }
    }


    private bool[] AvailabilityByType(int type)
    {
        ChamberConnectionType typeToCheck = (ChamberConnectionType) type;
        bool[] result = new bool[4];
        switch(typeToCheck)
        {
            case ChamberConnectionType.Top:
                result[(int)ChamberConnectionType.Top -1] = true;
                return result;
            case ChamberConnectionType.Bottom:
                result[(int)ChamberConnectionType.Bottom -1] = true;
                return result;
            case ChamberConnectionType.Left:
                result[(int)ChamberConnectionType.Left -1] = true;
                return result;
            case ChamberConnectionType.Right:
                result[(int)ChamberConnectionType.Right -1] = true;
                return result;
            case ChamberConnectionType.LeftRight:
                result[(int)ChamberConnectionType.Left -1] = true;
                result[(int)ChamberConnectionType.Right -1] = true;
                return result;
            case ChamberConnectionType.TopBottom:
                result[(int)ChamberConnectionType.Top -1] = true;
                result[(int)ChamberConnectionType.Bottom -1] = true;
                return result;
            case ChamberConnectionType.BottomLeft:
                result[(int)ChamberConnectionType.Left -1] = true;
                result[(int)ChamberConnectionType.Bottom -1] = true;
                return result;
            case ChamberConnectionType.BottomRight:
                result[(int)ChamberConnectionType.Bottom -1] = true;
                result[(int)ChamberConnectionType.Right -1] = true;
                return result;
            case ChamberConnectionType.TopLeft:
                result[(int)ChamberConnectionType.Left -1] = true;
                result[(int)ChamberConnectionType.Top -1] = true;
                return result;
            case ChamberConnectionType.TopRight:
                result[(int)ChamberConnectionType.Top -1] = true;
                result[(int)ChamberConnectionType.Right -1] = true;
                return result;
            case ChamberConnectionType.EverythingButLeft:
                result[(int)ChamberConnectionType.Top -1] = true;
                result[(int)ChamberConnectionType.Bottom -1] = true;
                result[(int)ChamberConnectionType.Right -1] = true;
                return result;
            case ChamberConnectionType.EverythingButRight:
                result[(int)ChamberConnectionType.Top -1] = true;
                result[(int)ChamberConnectionType.Bottom -1] = true;
                result[(int)ChamberConnectionType.Left -1] = true;
                return result;
            case ChamberConnectionType.EverythingButTop:
                result[(int)ChamberConnectionType.Left -1] = true;
                result[(int)ChamberConnectionType.Bottom -1] = true;
                result[(int)ChamberConnectionType.Right -1] = true;
                return result;
            case ChamberConnectionType.EverythingButBottom:
                result[(int)ChamberConnectionType.Top -1] = true;
                result[(int)ChamberConnectionType.Left -1] = true;
                result[(int)ChamberConnectionType.Right -1] = true;
                return result;
            default:
                result[(int)ChamberConnectionType.Top -1] = true;
                result[(int)ChamberConnectionType.Bottom -1] = true;
                result[(int)ChamberConnectionType.Left -1] = true;
                result[(int)ChamberConnectionType.Right -1] = true;
                return result;

        }
    }

    private bool[] CheckPositions(Vector2 originOfCheck, ref int[,] mapMatrix)
    {
        bool[] result = new bool[4];
        int originX = (int) originOfCheck.x;
        int originY = (int) originOfCheck.y;
        if(originX + 1 < mapSizeX && mapMatrix[originX + 1, originY] == 0)
        {
            result[(int) ChamberConnectionType.Right - 1] = true;
        }
        if(originX -1 > 0 && mapMatrix[originX - 1, originY] == 0)
        {
            result[(int) ChamberConnectionType.Left - 1] = true;
        }
        if(originY + 1 < mapSizeY && mapMatrix[originX, originY + 1] == 0)
        {
            result[(int) ChamberConnectionType.Top - 1] = true;
        }
        if(originY -1 >= 0 && mapMatrix[originX, originY - 1] == 0)
        {
            result[(int) ChamberConnectionType.Bottom - 1] = true;
        }
        return result;
    }


    private List<ChamberConnectionType> availableChamberConnectionTypes(Vector2 positionToCheck, ref int[,] mapMatrix, bool closing = false)
    {
        //Check the positions adjacent to the new chamber to determine its possible connections\\
        int[] adjacentsStages = new int[4];
        adjacentsStages[(int) ChamberConnectionType.Top -1] = CheckTopStage(positionToCheck, ref mapMatrix);
        adjacentsStages[(int) ChamberConnectionType.Bottom -1] = CheckBottomStage(positionToCheck, ref mapMatrix);
        adjacentsStages[(int) ChamberConnectionType.Left -1] = CheckLeftStage(positionToCheck, ref mapMatrix);
        adjacentsStages[(int) ChamberConnectionType.Right -1] = CheckRightStage(positionToCheck,ref mapMatrix);
        List<ChamberConnectionType> possibleConnections = new List<ChamberConnectionType>();
        if(!closing)
        {
            possibleConnections = ListConnectionCreator.ConnectionListGetter(adjacentsStages);
        }
        else
        {
            possibleConnections = ListConnectionCreator.ConnectionListGetterClosing(adjacentsStages);
        }
        return possibleConnections;
    }

    private int CheckLeftStage(Vector2 positionOrigin, ref int[,] mapMatrix)
    { //Returns if the postition to the left of the new chamber is unavailable, available or mandatory
        int LeftX =(int) positionOrigin.x - 1;
        int LeftY = (int) positionOrigin.y;

        if(LeftX < 0)
        {//Such position doesnt exist\\
            return 0;
        }
        if(mapMatrix[LeftX,LeftY] == 0)
        {//The left position is unocupied\\
            return 1;
        }
        else
        {//The left position is ocupied
            ChamberConnectionType leftPosition = (ChamberConnectionType) mapMatrix[LeftX,LeftY];
            switch(leftPosition)
            {
                case ChamberConnectionType.Right:
                    return 2;
                case ChamberConnectionType.LeftRight:
                    return 2;
                case ChamberConnectionType.BottomRight:
                    return 2;
                case ChamberConnectionType.TopRight:
                    return 2;
                case ChamberConnectionType.EverythingButTop:
                    return 2;
                case ChamberConnectionType.EverythingButLeft:
                    return 2;
                case ChamberConnectionType.EverythingButBottom:
                    return 2;
                case ChamberConnectionType.AllFour:
                    return 2;
                default:
                    return 0;
            }
        }
    }

    private int CheckRightStage(Vector2 positionOrigin, ref int[,] mapMatrix)
    { //Returns if the postition to the right of the new chamber is unavailable, available or mandatory
        int toCheckX =(int) positionOrigin.x + 1;
        int toCheckY = (int) positionOrigin.y;

        if(toCheckX >= mapSizeX)
        {//Such position doesnt exist\\
            return 0;
        }
        if(mapMatrix[toCheckX,toCheckY] == 0)
        {//The left position is unocupied\\
            return 1;
        }
        else
        {//The left position is ocupied
            ChamberConnectionType toCheckPosition = (ChamberConnectionType) mapMatrix[toCheckX,toCheckY];
            switch(toCheckPosition)
            {
                case ChamberConnectionType.Left:
                    return 2;
                case ChamberConnectionType.LeftRight:
                    return 2;
                case ChamberConnectionType.BottomLeft:
                    return 2;
                case ChamberConnectionType.TopLeft:
                    return 2;
                case ChamberConnectionType.EverythingButTop:
                    return 2;
                case ChamberConnectionType.EverythingButRight:
                    return 2;
                case ChamberConnectionType.EverythingButBottom:
                    return 2;
                case ChamberConnectionType.AllFour:
                    return 2;
                default:
                    return 0;
            }
        }
    }

    private int CheckTopStage(Vector2 positionOrigin, ref int[,] mapMatrix)
    { //Returns if the postition on top of the new chamber is unavailable, available or mandatory
        int toCheckX =(int) positionOrigin.x;
        int toCheckY = (int) positionOrigin.y + 1;

        if(toCheckY >= mapSizeY)
        {//Such position doesnt exist\\
            return 0;
        }
        if(mapMatrix[toCheckX,toCheckY] == 0)
        {//The left position is unocupied\\
            return 1;
        }
        else
        {//The left position is ocupied
            ChamberConnectionType toCheckPosition = (ChamberConnectionType) mapMatrix[toCheckX,toCheckY];
            switch(toCheckPosition)
            {
                case ChamberConnectionType.Bottom:
                    return 2;
                case ChamberConnectionType.TopBottom:
                    return 2;
                case ChamberConnectionType.BottomLeft:
                    return 2;
                case ChamberConnectionType.BottomRight:
                    return 2;
                case ChamberConnectionType.EverythingButTop:
                    return 2;
                case ChamberConnectionType.EverythingButRight:
                    return 2;
                case ChamberConnectionType.EverythingButLeft:
                    return 2;
                case ChamberConnectionType.AllFour:
                    return 2;
                default:
                    return 0;
            }
        }
    }

    private int CheckBottomStage(Vector2 positionOrigin, ref int[,] mapMatrix)
    { //Returns if the postition below of the new chamber is unavailable, available or mandatory
        int toCheckX =(int) positionOrigin.x;
        int toCheckY = (int) positionOrigin.y - 1;

        if(toCheckY < 0)
        {//Such position doesnt exist\\
            return 0;
        }
        if(mapMatrix[toCheckX,toCheckY] == 0)
        {//The left position is unocupied\\
            return 1;
        }
        else
        {//The left position is ocupied
            ChamberConnectionType toCheckPosition = (ChamberConnectionType) mapMatrix[toCheckX,toCheckY];
            switch(toCheckPosition)
            {
                case ChamberConnectionType.Top:
                    return 2;
                case ChamberConnectionType.TopBottom:
                    return 2;
                case ChamberConnectionType.TopLeft:
                    return 2;
                case ChamberConnectionType.TopRight:
                    return 2;
                case ChamberConnectionType.EverythingButBottom:
                    return 2;
                case ChamberConnectionType.EverythingButRight:
                    return 2;
                case ChamberConnectionType.EverythingButLeft:
                    return 2;
                case ChamberConnectionType.AllFour:
                    return 2;
                default:
                    return 0;
            }
        }
    }


    private ChamberConnectionType SetChamber(List<ChamberConnectionType> possibleConnections)
    { //To be updated taking into acount the probabilities\\

        int sumOfProbabilities = 0;
        possibleConnections.Sort();
        for(int i= 0; i<possibleConnections.Count; i++)
        {
            sumOfProbabilities += probabilities[(int) possibleConnections[i] -1];
        }
        int chosenConnection = Random.Range(0,sumOfProbabilities);
        int theConnection = 0;
        int accumulated = 0;
        for(int i= 0; i< possibleConnections.Count; i++)
        {
            if(chosenConnection < accumulated + probabilities[(int) possibleConnections[i] -1])
            {
                theConnection = i;
                break;
            }
            else
            {
                accumulated += probabilities[(int) possibleConnections[i] -1];
            }
        }
        return possibleConnections[theConnection];
    }

    private bool IsComplete(Vector2 position, ref int[,] mapMatrix)
    {
        bool[] positionConnections = AvailabilityByType(mapMatrix[(int) position.x, (int) position.y]);
        bool[] donePositions = new bool[4];
        donePositions = CheckPositions(position, ref mapMatrix);
        List<ChamberConnectionType> availableConnections = new List<ChamberConnectionType>();
        for(int i= 0; i< 4; i++)
        {
            if(positionConnections[i] && donePositions[i])
            {
                availableConnections.Add((ChamberConnectionType) i+1);
            }
        }
        if(availableConnections.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void LoadChambers()
    {
        for(int i= 0; i< chamberPool.Length; i++)
        {
            chamberPool[i] = Resources.LoadAll<Chamber>("Chambers/" + ((ChamberConnectionType) i + 1));
        }
    }

    private Chamber[,] ChamberGetter(ref int[,] mapMatrix)
    {
        Chamber[,] result = new Chamber[mapSizeX,mapSizeY];
        LoadChambers();
        for(int i=0; i<result.GetLength(0) ; i++)
        {
            for(int j=0; j<result.GetLength(1); j++)
            {
                result[i,j] = chamberPool[(int) mapMatrix[i,j] -1][Random.Range(0,chamberPool[(int) mapMatrix[i,j] -1].Length)];
            }
        }
        return result;
    }
}
