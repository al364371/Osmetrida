using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ListConnectionCreator
{
    public static List<ChamberConnectionType> ConnectionListGetter(int[] stages)
    {
        ChamberConnectionType[] allTypes = (ChamberConnectionType[]) Enum.GetValues(typeof(ChamberConnectionType));
        bool[] typesallowed = new bool[allTypes.Length];
        for(int i=0; i<typesallowed.Length; i++)
        {
            typesallowed[i] = true;
        }
        List<ChamberConnectionType> result = new List<ChamberConnectionType>();
        typesallowed[(int) ChamberConnectionType.Bottom -1] = false;
        typesallowed[(int) ChamberConnectionType.Left -1] = false;
        typesallowed[(int) ChamberConnectionType.Right -1] = false;
        typesallowed[(int) ChamberConnectionType.Top -1] = false;

        if(stages[(int) ChamberConnectionType.Top - 1] == 2)
        { //Top is required\\
          typesallowed[(int) ChamberConnectionType.Bottom -1] = false;
          typesallowed[(int) ChamberConnectionType.Left -1] = false;
          typesallowed[(int) ChamberConnectionType.Right -1] = false;
          typesallowed[(int) ChamberConnectionType.LeftRight -1] = false;
          typesallowed[(int) ChamberConnectionType.BottomLeft -1] = false;
          typesallowed[(int) ChamberConnectionType.BottomRight -1] = false;
          typesallowed[(int) ChamberConnectionType.EverythingButTop -1] = false;
        }
        else if(stages[(int) ChamberConnectionType.Top - 1] == 0)
        {//Top is not allowed\\
            typesallowed[(int) ChamberConnectionType.Top -1] = false;
            typesallowed[(int) ChamberConnectionType.TopBottom -1] = false;
            typesallowed[(int) ChamberConnectionType.TopLeft -1] = false;
            typesallowed[(int) ChamberConnectionType.TopRight -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButBottom -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButLeft -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButRight -1] = false;
            typesallowed[(int) ChamberConnectionType.AllFour -1] = false;
        }
        if(stages[(int) ChamberConnectionType.Left - 1] == 2)
        { //Left is required\\
          typesallowed[(int) ChamberConnectionType.Bottom -1] = false;
          typesallowed[(int) ChamberConnectionType.Top -1] = false;
          typesallowed[(int) ChamberConnectionType.Right-1] = false;
          typesallowed[(int) ChamberConnectionType.TopBottom -1] = false;
          typesallowed[(int) ChamberConnectionType.TopRight -1] = false;
          typesallowed[(int) ChamberConnectionType.BottomRight -1] = false;
          typesallowed[(int) ChamberConnectionType.EverythingButLeft -1] = false;
        }
        else if(stages[(int) ChamberConnectionType.Left - 1] == 0)
        {//Left is not allowed\\
            typesallowed[(int) ChamberConnectionType.Left -1] = false;
            typesallowed[(int) ChamberConnectionType.LeftRight -1] = false;
            typesallowed[(int) ChamberConnectionType.TopLeft -1] = false;
            typesallowed[(int) ChamberConnectionType.BottomLeft -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButBottom -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButTop -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButRight -1] = false;
            typesallowed[(int) ChamberConnectionType.AllFour -1] = false;
        }
        if(stages[(int) ChamberConnectionType.Right - 1] == 2)
        { //Right is required\\
          typesallowed[(int) ChamberConnectionType.Bottom -1] = false;
          typesallowed[(int) ChamberConnectionType.Left -1] = false;
          typesallowed[(int) ChamberConnectionType.Top -1] = false;
          typesallowed[(int) ChamberConnectionType.TopBottom -1] = false;
          typesallowed[(int) ChamberConnectionType.BottomLeft -1] = false;
          typesallowed[(int) ChamberConnectionType.TopLeft -1] = false;
          typesallowed[(int) ChamberConnectionType.EverythingButRight -1] = false;
        }
        else if(stages[(int) ChamberConnectionType.Right - 1] == 0)
        {//Right is not allowed\\
            typesallowed[(int) ChamberConnectionType.Right -1] = false;
            typesallowed[(int) ChamberConnectionType.LeftRight -1] = false;
            typesallowed[(int) ChamberConnectionType.BottomRight -1] = false;
            typesallowed[(int) ChamberConnectionType.TopRight -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButBottom -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButLeft -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButTop -1] = false;
            typesallowed[(int) ChamberConnectionType.AllFour -1] = false;
        }
        if(stages[(int) ChamberConnectionType.Bottom - 1] == 2)
        { //Bottom is required\\
          typesallowed[(int) ChamberConnectionType.Top -1] = false;
          typesallowed[(int) ChamberConnectionType.Left -1] = false;
          typesallowed[(int) ChamberConnectionType.Right -1] = false;
          typesallowed[(int) ChamberConnectionType.LeftRight -1] = false;
          typesallowed[(int) ChamberConnectionType.TopLeft -1] = false;
          typesallowed[(int) ChamberConnectionType.TopRight -1] = false;
          typesallowed[(int) ChamberConnectionType.EverythingButBottom -1] = false;
        }
        else if(stages[(int) ChamberConnectionType.Bottom - 1] == 0)
        {//Bottom is not allowed\\
            typesallowed[(int) ChamberConnectionType.Bottom -1] = false;
            typesallowed[(int) ChamberConnectionType.TopBottom -1] = false;
            typesallowed[(int) ChamberConnectionType.BottomLeft -1] = false;
            typesallowed[(int) ChamberConnectionType.BottomRight -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButTop -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButLeft -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButRight -1] = false;
            typesallowed[(int) ChamberConnectionType.AllFour -1] = false;
        }



        //Check if it has been forced to an impossible state\\
        int numberOfMandatories = 0;
        for(int i= 0; i< stages.Length; i++)
        {
            if(stages[i] == 2 || stages[i] == 1)
            {
                numberOfMandatories++;
            }
        }
        if(numberOfMandatories == 1)
        {//The chamber must be left rigt top or bottom alone, but has been forced to be none of them\\
            for(int i=0; i<stages.Length; i++)
            {
                if(stages[i] == 2)
                {
                    typesallowed[i] = true;
                }
            }
        }
        for(int i=0; i<typesallowed.Length; i++)
        {
            if(typesallowed[i])
            {
                result.Add(allTypes[i]);
            }
        }
        return result;
    }

    public static List<ChamberConnectionType> ConnectionListGetterClosing(int[] stages)
    {
        ChamberConnectionType[] allTypes = (ChamberConnectionType[]) Enum.GetValues(typeof(ChamberConnectionType));
        bool[] typesallowed = new bool[allTypes.Length];
        for(int i=0; i<typesallowed.Length; i++)
        {
            typesallowed[i] = true;
        }
        List<ChamberConnectionType> result = new List<ChamberConnectionType>();

        if(stages[(int) ChamberConnectionType.Top - 1] == 2)
        { //Top is required\\
          typesallowed[(int) ChamberConnectionType.Bottom -1] = false;
          typesallowed[(int) ChamberConnectionType.Left -1] = false;
          typesallowed[(int) ChamberConnectionType.Right -1] = false;
          typesallowed[(int) ChamberConnectionType.LeftRight -1] = false;
          typesallowed[(int) ChamberConnectionType.BottomLeft -1] = false;
          typesallowed[(int) ChamberConnectionType.BottomRight -1] = false;
          typesallowed[(int) ChamberConnectionType.EverythingButTop -1] = false;
        }
        else
        {//Top is not allowed\\
            typesallowed[(int) ChamberConnectionType.Top -1] = false;
            typesallowed[(int) ChamberConnectionType.TopBottom -1] = false;
            typesallowed[(int) ChamberConnectionType.TopLeft -1] = false;
            typesallowed[(int) ChamberConnectionType.TopRight -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButBottom -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButLeft -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButRight -1] = false;
            typesallowed[(int) ChamberConnectionType.AllFour -1] = false;
        }
        if(stages[(int) ChamberConnectionType.Left - 1] == 2)
        { //Left is required\\
          typesallowed[(int) ChamberConnectionType.Bottom -1] = false;
          typesallowed[(int) ChamberConnectionType.Top -1] = false;
          typesallowed[(int) ChamberConnectionType.Right-1] = false;
          typesallowed[(int) ChamberConnectionType.TopBottom -1] = false;
          typesallowed[(int) ChamberConnectionType.TopRight -1] = false;
          typesallowed[(int) ChamberConnectionType.BottomRight -1] = false;
          typesallowed[(int) ChamberConnectionType.EverythingButLeft -1] = false;
        }
        else
        {//Left is not allowed\\
            typesallowed[(int) ChamberConnectionType.Left -1] = false;
            typesallowed[(int) ChamberConnectionType.LeftRight -1] = false;
            typesallowed[(int) ChamberConnectionType.TopLeft -1] = false;
            typesallowed[(int) ChamberConnectionType.BottomLeft -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButBottom -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButTop -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButRight -1] = false;
            typesallowed[(int) ChamberConnectionType.AllFour -1] = false;
        }
        if(stages[(int) ChamberConnectionType.Right - 1] == 2)
        { //Right is required\\
          typesallowed[(int) ChamberConnectionType.Bottom -1] = false;
          typesallowed[(int) ChamberConnectionType.Left -1] = false;
          typesallowed[(int) ChamberConnectionType.Top -1] = false;
          typesallowed[(int) ChamberConnectionType.TopBottom -1] = false;
          typesallowed[(int) ChamberConnectionType.BottomLeft -1] = false;
          typesallowed[(int) ChamberConnectionType.TopLeft -1] = false;
          typesallowed[(int) ChamberConnectionType.EverythingButRight -1] = false;
        }
        else
        {//Right is not allowed\\
            typesallowed[(int) ChamberConnectionType.Right -1] = false;
            typesallowed[(int) ChamberConnectionType.LeftRight -1] = false;
            typesallowed[(int) ChamberConnectionType.BottomRight -1] = false;
            typesallowed[(int) ChamberConnectionType.TopRight -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButBottom -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButLeft -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButTop -1] = false;
            typesallowed[(int) ChamberConnectionType.AllFour -1] = false;
        }
        if(stages[(int) ChamberConnectionType.Bottom - 1] == 2)
        { //Bottom is required\\
          typesallowed[(int) ChamberConnectionType.Top -1] = false;
          typesallowed[(int) ChamberConnectionType.Left -1] = false;
          typesallowed[(int) ChamberConnectionType.Right -1] = false;
          typesallowed[(int) ChamberConnectionType.LeftRight -1] = false;
          typesallowed[(int) ChamberConnectionType.TopLeft -1] = false;
          typesallowed[(int) ChamberConnectionType.TopRight -1] = false;
          typesallowed[(int) ChamberConnectionType.EverythingButBottom -1] = false;
        }
        else
        {//Bottom is not allowed\\
            typesallowed[(int) ChamberConnectionType.Bottom -1] = false;
            typesallowed[(int) ChamberConnectionType.TopBottom -1] = false;
            typesallowed[(int) ChamberConnectionType.BottomLeft -1] = false;
            typesallowed[(int) ChamberConnectionType.BottomRight -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButTop -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButLeft -1] = false;
            typesallowed[(int) ChamberConnectionType.EverythingButRight -1] = false;
            typesallowed[(int) ChamberConnectionType.AllFour -1] = false;
        }



        //Check if it has been forced to an impossible state\\
        int numberOfMandatories = 0;
        for(int i= 0; i< stages.Length; i++)
        {
            if(stages[i] == 2 || stages[i] == 1)
            {
                numberOfMandatories++;
            }
        }
        if(numberOfMandatories == 1)
        {//The chamber must be left rigt top or bottom alone, but has been forced to be none of them\\
            for(int i=0; i<stages.Length; i++)
            {
                if(stages[i] == 2)
                {
                    typesallowed[i] = true;
                }
            }
        }
        for(int i=0; i<typesallowed.Length; i++)
        {
            if(typesallowed[i])
            {
                result.Add(allTypes[i]);
            }
        }
        return result;
    }



}

public static class ChamberSectionChecking
{
    public static bool IsIntermediate(ChamberConnectionType chamberType)
    {
        if(chamberType == ChamberConnectionType.LeftRight || chamberType == ChamberConnectionType.TopBottom)
        { 
            return true;
        }
        return false;
    }

    public static bool IsExtreme(ChamberConnectionType chamberType)
    {
        if(   chamberType == ChamberConnectionType.Left || chamberType == ChamberConnectionType.Top 
           || chamberType == ChamberConnectionType.Right || chamberType == ChamberConnectionType.Bottom)
        { 
            return true;
        }
        return false;
    }
}
