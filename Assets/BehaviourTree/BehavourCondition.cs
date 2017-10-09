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
    }
    public DataType mDataType = DataType.FLOAT;
    public string mType = "attack";
    public float mFloatValue = 0;
   public OPREATION mOperation = OPREATION.GREATER;
}
