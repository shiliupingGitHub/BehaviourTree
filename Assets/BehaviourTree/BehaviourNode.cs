using UnityEngine;
using System.Collections.Generic;

public class BehaviourNode  {
    public enum TYPE
    {
        ACTION,
        SELECT,
    }
    public TYPE mType = TYPE.ACTION;
    public int mId;
    public string mAction;
    public Rect mPos = new Rect(0,0,0,0);
    public List<BehavourCondition> mCondition = new List<BehavourCondition>();
   public List<BehaviourNode> mSubNodes = new List<BehaviourNode>();
}
