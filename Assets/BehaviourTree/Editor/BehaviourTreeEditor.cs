using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
public class BehaviourTreeEditor : EditorWindow {
    public static BehaviourTreeEditor mInstance;
    BehaviourNode mNode;
    Dictionary<int, BehaviourNode> mNodes = new Dictionary<int, BehaviourNode>();
    [MenuItem("Window/BehaviourTreeWindow")]
    static void OpenWindow()
    {
       
        mInstance = GetWindow<BehaviourTreeEditor>(true);
        mInstance.mNodes.Clear();
        mInstance.mNode = new BehaviourNode();
        mInstance.mNode.mId = 0;
        mInstance.mNode.mAction = "attack";
        mInstance.mNodes[0] = mInstance.mNode;
    }
    void OnGUI()
    {
        BeginWindows();
        int x = 0, y = 0;
        DrawNode(mNode, x, y);
        EndWindows();
    }
    BehaviourNode GetNode(int id)
    {
        return mNodes[id];
    }
    void WindowFunction(int id)
    {
        BehaviourNode n = GetNode(id);
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Label("id:");
        n.mId =EditorGUILayout.IntField(n.mId);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("action:");
        n.mAction = GUILayout.TextField(n.mAction);
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }
    void DrawNode(BehaviourNode node,int x, int y)
    {
        GUILayout.Window(node.mId, new Rect(x+ position.width/2, y, 0, 0), WindowFunction, node.mAction);
    }
	
}
