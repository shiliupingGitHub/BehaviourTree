using UnityEngine;
using System.Collections;
namespace Behaviour
{
    public class BehavourAction
    {

        public enum DataType
        {
            FLOAT,
            INT,
            STRING,
        }
        public DataType mDataType;
        public float f0;
        public float f1;
        public int n0;
        public int n1;
        public string mStr;
        public string mType = "attack";
    }
}