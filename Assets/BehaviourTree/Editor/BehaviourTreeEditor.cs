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
        mInstance.mTree.mRoot.mName = "root";
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
                mCurMaxId = 0;
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
        t.mName = "attack";
        t.mPos = new Rect(pos.x, pos.y, 0, 0);
        mTree.mNodes.Add(t);
        BehaviourNodeEditor d = new BehaviourNodeEditor();
        d.node = t;
        mDraws.Add(d);
        if (mTree.mRoot == null)
            mTree.mRoot = d.node;
    }
    void OnSaveTree(System.Object o)
    {
        string file = EditorUtility.SaveFilePanel(string.Empty, string.Empty, string.Empty, "xml");
        if(!string.IsNullOrEmpty(file))
         {
            string ret =  mTree.SaveTree();
            System.IO.File.WriteAllText(file, ret);
        }
   
      }
    void OnLoadTree(System.Object o)
    {
       string file = EditorUtility.OpenFilePanel("load tree",string.Empty,"xml");
        if(!string.IsNullOrEmpty(file))
        {
           string t = System.IO.File.ReadAllText(file);
            mTree.LoadTree(t);
            mDraws.Clear();
            foreach(var n in mTree.mNodes)
            {
                BehaviourNodeEditor e = new BehaviourNodeEditor();
                e.node = n;
                mDraws.Add(e);
            }
            Repaint();
        }
    }
    void OnOperationMenu()
    {
       // mOperation = OPERATION.MENU;
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("New Node"), false, OnMenuNodeNode, Event.current.mousePosition);
        menu.AddItem(new GUIContent("Save Tree"), false, OnSaveTree, Event.current.mousePosition);
        menu.AddItem(new GUIContent("Load Tree"), false, OnLoadTree, Event.current.mousePosition);
        menu.ShowAsContext();
     
    }
    void DrawNode(BehaviourNodeEditor d)
    {
        d.DrawSimple(mDrawId);
        mDrawId++;
    }
	
}
