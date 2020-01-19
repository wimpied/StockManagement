using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnIdentifier : MonoBehaviour
{
    public enum ColumnNames
    {
        Main, 
        SecondColumn,
        ThirdColumn, 
        FourthColumn,
        FifthColumn,
        SixthColumn,
        SeventColumn,
        EightColumn

    }

    public ColumnNames ThisColumnName;
}
