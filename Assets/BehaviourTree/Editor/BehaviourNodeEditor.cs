using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
public class BehaviourNodeEditor
{
    public BehaviourNode node;
    static BehaviourNode mLastSelectConnect = null;
    float size = 40;
    public void Draw(int id)
    {
       WindowFunction(id);
    }

    public void DrawSimple(int id)
    {
        node.mPos = GUILayout.Window(id, node.mPos, WindowFunctionSimple, node.mAction);
        int t = 0;
        foreach (var n in node.mSubNodes)
        {
            t++;
            Vector3 p0 = new Vector3(node.mPos.x + node.mPos.width, (int)(node.mPos.y + node.mPos.height / 2.0));
            Vector3 p1 = new Vector3(n.mPos.x, n.mPos.y + (int)(n.mPos.height / 2.0f));
            Handles.color = Color.red;
            Handles.DrawLine(p0, p1);

        }
    }
    void OnGUI()
    {

        this.Draw(0);
        if(GUI.changed)
        {
            if (BehaviourTreeEditor.mInstance != null)
                BehaviourTreeEditor.mInstance.Repaint();
        }
        
    }
    void WindowFunctionSimple(int id)
    {


        EditorGUILayout.BeginVertical();
        if(GUILayout.Button("preview"))
        {

            BehaviourNodePreview t =EditorWindow.GetWindow<BehaviourNodePreview>(true);
            t.mEditor = this;
        }
        if (GUILayout.Button("delete"))
        {
            BehaviourTreeEditor.mInstance.DeleteNode(this);
        }
        EditorGUILayout.EndVertical();
        GUI.DragWindow();
    }
    void OnConnectSunNode()
    {
        if (mLastSelectConnect == null)
            mLastSelectConnect = this.node;
        else
        {
            if(!mLastSelectConnect.mSubNodes.Contains(node))
                    mLastSelectConnect.mSubNodes.Add(node);
            if (BehaviourTreeEditor.mInstance != null)
                BehaviourTreeEditor.mInstance.Repaint();
            mLastSelectConnect = null;
        }
    }

    void WindowFunction(int id)
    {

        if (null == node)
            return;
        GUILayout.BeginVertical();
        #region dragPure
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("id:");
        node.mId = EditorGUILayout.IntField(node.mId, GUILayout.MaxWidth(size));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("action:");
        node.mAction = GUILayout.TextField(node.mAction);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        List<BehaviourNode> r = new List<BehaviourNode>();
        foreach (var n in node.mSubNodes)
        {
            
            if(GUILayout.Button("del:action:"+ n.mAction +"id:"+n.mId))
            {
                r.Add(n);
              
            }
        }
        foreach(var t in r)
        {
            node.mSubNodes.Remove(t);
            if (BehaviourTreeEditor.mInstance != null)
                BehaviourTreeEditor.mInstance.Repaint();
        }
        if (GUILayout.Button("link", GUILayout.MaxWidth(size)))
            OnConnectSunNode();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        #endregion


        #region condition
        GUILayout.Space(1);
        GUILayout.Label("----condition-----");
        List<BehavourCondition> r2 = new List<BehavourCondition>();
        foreach (var c in node.mCondition)
        {
            GUILayout.BeginHorizontal();
            c.mDataType =(BehavourCondition.DataType) EditorGUILayout.EnumPopup("", c.mDataType,GUILayout.MaxWidth(size));
            c.mType = EditorGUILayout.TextField(c.mType, GUILayout.MaxWidth(size));
            switch (c.mDataType)
            {
                case BehavourCondition.DataType.FLOAT:
                    {
                        c.mOperation = (BehavourCondition.OPREATION)EditorGUILayout.EnumPopup("", c.mOperation, GUILayout.MaxWidth(size));
                        c.mFloatValue = EditorGUILayout.FloatField(c.mFloatValue);
                    }
                    break;
            }
            if (GUILayout.Button("del", GUILayout.MaxWidth(size)))
            {
                r2.Add(c);
                
            }
            GUILayout.EndHorizontal();
        }
        foreach(var t2 in r2)
        {
            node.mCondition.Remove(t2);
        }
        if (GUILayout.Button("add", GUILayout.MaxWidth(size)))
            node.mCondition.Add(new BehavourCondition());
        #endregion
        GUILayout.EndVertical();
    }
}
