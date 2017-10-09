using UnityEngine;
using System.Collections;

public class BehavourCondition  {
    public enum OPREATION
    {
        GREATER,
        EQUAL,
        LEASS,
    }
    public enum DataType
    {
        FLOAT,
        INT,
        STRING,
    }
    public DataType mDataType = DataType.FLOAT;
    public string mType = "attack";
    public float mFloatValue = 0;
    public int mIntValue;
    public string mStr;
   public OPREATION mOperation = OPREATION.GREATER;
}
