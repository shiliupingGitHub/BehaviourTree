using UnityEngine;
using System.Collections.Generic;

public class BehaviourNode  {
    public string mName;
    public Rect mPos = new Rect(0,0,0,0);
    public List<BehavourCondition> mCondition = new List<BehavourCondition>();
   public List<BehaviourNode> mSubNodes = new List<BehaviourNode>();
    public List<BehavourAction> mActions = new List<BehavourAction>();
    public bool Run(BehaviourTree tree)
    {
        foreach(var c in mCondition)
        {
            if(null != tree.mCondition)
            {
                if (!tree.mCondition(c))
                    return false;
            }
        }
        foreach(var s in mSubNodes)
        {
            if (!s.Run(tree))
                return false;
        }
        foreach(var a in mActions)
        {
            if (null != tree.mAcion)
                tree.mAcion(a);
        }
        return true;
    }
}
