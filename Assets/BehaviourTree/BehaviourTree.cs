using UnityEngine;
using System.Collections.Generic;
namespace Behaviour
{
    public class BehaviourTree
    {
        public System.Func<BehavourCondition, bool> mCondition;
        public System.Action<BehavourAction> mAcion;
        public BehaviourNode mRoot;
        public List<BehaviourNode> mNodes = new List<BehaviourNode>();
        public void LoadTree(string text)
        {
            mNodes.Clear();
            mRoot = XmlHelper.XmlDeserialize<BehaviourNode>(text, System.Text.Encoding.UTF8);
            if (null != mRoot)
            {
                AddNode(mRoot, mNodes);
            }
        }
        public void Run(List<BehavourAction> actions, bool baction = true)
        {
            mRoot.Run(this, actions, baction);
        }
        void AddNode(BehaviourNode n, List<BehaviourNode> ns)
        {
            ns.Add(n);
            foreach (var t in n.mSubNodes)
            {
                AddNode(t, ns);
            }
        }
        public string SaveTree()
        {
            string ret = XmlHelper.XmlSerialize(mRoot, System.Text.Encoding.UTF8);
            return ret;
        }
    }
}
