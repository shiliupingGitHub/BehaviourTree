using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
public class BehaviourTreeEditor : EditorWindow {
    public static BehaviourTreeEditor mInstance;
    BehaviourTree mTree;
    List<BehaviourNodeEditor> mDraws = new List<BehaviourNodeEditor>();
    int mCurMaxId = 0;
    int mDrawId = 0;
    public enum OPERATION
    {
        NONE,
    }
    OPERATION mOperation = OPERATION.NONE;
    [MenuItem("Window/BehaviourTreeWindow")]
    
    static void OpenWindow()
    {
       
        mInstance = GetWindow<BehaviourTreeEditor>(true);
        mInstance.mTree = new BehaviourTree();
        mInstance.mDraws.Clear();
        mInstance.mTree.mRoot = new BehaviourNode();
        mInstance.mTree.mRoot.mId = 0;
        mInstance.mTree.mRoot.mAction = "root";
        mInstance.mTree.mRoot.mPos = new Rect(0, (int)(mInstance.position.height / 2.0f), 0, 0);
        mInstance.mTree.mNodes.Add(mInstance.mTree.mRoot);
        BehaviourNodeEditor d = new BehaviourNodeEditor();
        d.node = mInstance.mTree.mRoot;
        mInstance.mDraws.Add(d);
    }
    public void DeleteNode(BehaviourNodeEditor n)
    {
        mDraws.Remove(n);
        if(null!= mTree)
        {
            mTree.mNodes.Remove(n.node);
            if(mTree.mRoot == n.node)
            {
                mTree.mRoot = null;
                mTree.mNodes.Clear();
            }
            else
            {
                foreach(var t in mTree.mNodes)
                {
                    if(t.mSubNodes.Contains(n.node))
                    {
                        t.mSubNodes.Remove(n.node);
                    }
                }
            }
        }
    }
    Vector2 mSpos = Vector2.zero;
    void OnGUI()
    {


        BeginWindows();
        mDrawId = 0;
        foreach (var n in mDraws)
        {
            DrawNode(n);
        }
        EndWindows();
        OnEvent();
    }
    void OnEvent()
    {
        switch (Event.current.type)
        {
            case EventType.ContextClick:
                {
                    if(mOperation == OPERATION.NONE)
                    {
                        OnOperationMenu();
                        
                    }
                    Event.current.Use();
                }
                break;
           
        }

    }

    void OnMenuNodeNode(System.Object o)
    {
        Vector2 pos = (Vector2)o;
        mOperation = OPERATION.NONE;
        mCurMaxId++;
        BehaviourNode t = new BehaviourNode();
        t.mId = mCurMaxId;
        t.mAction = "attack";
        t.mPos = new Rect(pos.x, pos.y, 0, 0);
        mTree.mNodes.Add(t);
        BehaviourNodeEditor d = new BehaviourNodeEditor();
        d.node = t;
        mDraws.Add(d);
    }
    void OnOperationMenu()
    {
       // mOperation = OPERATION.MENU;
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("New Node"), false, OnMenuNodeNode, Event.current.mousePosition);
        menu.ShowAsContext();
     
    }
    void DrawNode(BehaviourNodeEditor d)
    {
        d.DrawSimple(mDrawId);
        mDrawId++;
    }
	
}
