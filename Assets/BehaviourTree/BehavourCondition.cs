using UnityEngine;
using System.Collections;

public class BehavourCondition  {
    public enum OPREATION
    {
        GREATER,
        EQUAL,
        LESS,
        GREATER_EQUAL,
        LESS_EQUAL,
        
    }
    public enum DataType
    {
        FLOAT,
        INT,
        STRING,
        BOOL,
    }
    public DataType mDataType = DataType.FLOAT;
    public string mType = "attack";
    public float mFloatValue = 0;
    public int mIntValue;
    public string mStr;
    public bool mB;
   public OPREATION mOperation = OPREATION.GREATER;
}
