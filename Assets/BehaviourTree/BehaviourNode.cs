using UnityEngine;
using System.Collections.Generic;

public class BehaviourNode  {
    public int mId;
    public string mAction;
    public Rect mPos = new Rect(0,0,0,0);
   public List<BehaviourNode> mSubNodes = new List<BehaviourNode>();
}
