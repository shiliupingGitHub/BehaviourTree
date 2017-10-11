using UnityEngine;
using System.Collections.Generic;
namespace Behaviour
{
    public class BehaviourNode
    {
        public string mName;
        public Rect mPos = new Rect(0, 0, 0, 0);
        public List<BehavourCondition> mCondition = new List<BehavourCondition>();
        public List<BehaviourNode> mSubNodes = new List<BehaviourNode>();
        public List<BehavourAction> mActions = new List<BehavourAction>();
        public Vector3 P0
        {
            get
            {
              return  new Vector3(mPos.x + mPos.width - 23, (int)(mPos.y + mPos.height - 15));
            }
        }
        public Vector3 P1
        {
            get
            {
                return new Vector3(mPos.x + 5, mPos.y + (int)(mPos.height - 15));
            }
        }
        public bool Run(BehaviourTree tree, List<BehavourAction> mRunActions,System.Object o, bool baction = true)
        {
            foreach (var c in mCondition)
            {
                if (null != tree.mCondition)
                {
                    if (!tree.mCondition(c))
                        return false;
                }
            }
            foreach (var s in mSubNodes)
            {
                if (!s.Run(tree, mRunActions,o, baction))
                    return false;

            }

            foreach (var a in mActions)
            {
                mRunActions.Add(a);
                if (baction)
                {
                    if (null != tree.mAcion)
                        tree.mAcion(a,o);
                }

            }


            return true;
        }
    }
}
