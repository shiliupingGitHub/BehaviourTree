﻿using UnityEngine;
using System.Collections.Generic;
using MyMVC;
public class BehaviourTree  {
  public System.Func<BehavourCondition, bool> mCondition;
    public System.Action<BehavourAction> mAcion;
   public BehaviourNode mRoot;
  public  List<BehaviourNode> mNodes = new List<BehaviourNode>();
   public void LoadTree(string text)
    {
        mNodes.Clear();
        mRoot = XmlHelper.XmlDeserialize<BehaviourNode>(text, System.Text.Encoding.UTF8);
        if(null != mRoot)
        {
            AddNode(mRoot, mNodes);
        }
    }
    public void Run()
    {
        mRoot.Run(this);
    }
    void AddNode(BehaviourNode n , List<BehaviourNode> ns)
    {
        ns.Add(n);
        foreach(var t in n.mSubNodes)
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
