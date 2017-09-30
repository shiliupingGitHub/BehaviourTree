using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
public class BehaviourTreeEditor : EditorWindow {
    public static BehaviourTreeEditor mInstance;
    BehaviourNode mNode;
    Dictionary<int, BehaviourNode> mNodes = new Dictionary<int, BehaviourNode>();
    int mCurMaxId = 0;
    [MenuItem("Window/BehaviourTreeWindow")]
    
    static void OpenWindow()
    {
       
        mInstance = GetWindow<BehaviourTreeEditor>(true);
        mInstance.mNode = new BehaviourNode();
        mInstance.mNode.mId = 0;
        mInstance.mNode.mAction = string.Empty;
        mInstance.mNode.mPos = new Rect(0, (int)(mInstance.position.height / 2.0f), 0, 0);
        mInstance.mNodes[0] = mInstance.mNode;
    }
    void OnGUI()
    {
        BeginWindows();
        foreach(var n in mNodes)
        {
            DrawNode(n.Value);
        }
        EndWindows();
    }
    BehaviourNode GetNode(int id, BehaviourNode root)
    {
        if (root.mId == id)
            return root;
        foreach(var n in root.mSubNodes)
        {
            BehaviourNode ret = GetNode(id,n);
            if (null != ret)
                return ret;
        }
        return null;
    }
    void WindowFunction(int id)
    {
        BehaviourNode n = GetNode(id,mNode);
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Label("id:");
        n.mId =EditorGUILayout.IntField(n.mId);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("action:");
        n.mAction = GUILayout.TextField(n.mAction);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }
    void DrawNode(BehaviourNode node)
    {
        mCurMaxId = Mathf.Max(node.mId, mCurMaxId);
        node.mPos = GUILayout.Window(node.mId, node.mPos, WindowFunction, node.mAction);
        foreach(var n in node.mSubNodes)
        {
            Vector3 p0 = new Vector3(n.mPos.x + n.mPos.width, (int)(n.mPos.y + n.mPos.height / 2.0));
            Vector3 p1 = new Vector3(n.mPos.x, n.mPos.y + (int)(n.mPos.height / 2.0f));
            Handles.DrawLine(p0, p1);
        }
        //Rect nPos = new Rect(x , y, width, height);
        //GUILayout.Window(node.mId, nPos, WindowFunction, node.mAction);

        //int nx =x+ width + gap;
        //float totalLen = (height + gap) * (node.mSubNodes.Count - 1);
        //int ny = (int)(position.height / 2 - totalLen / 2);

        //foreach (var n in node.mSubNodes)
        //{


        //    //Rect r =  DrawNode(n, nx, ny);
        //    //Vector3 p0 = new Vector3(nPos.x + width, (int)(nPos.y +height/2.0));
        //    //Vector3 p1 = new Vector3(r.x , r.y + (int)(height/2.0f));
        //    //Handles.DrawLine(p0, p1);
        //    //ny += height + gap;
        //}
        //  return nPos;


    }
	
}
