using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
namespace Behaviour
{

    public class BehaviourNodeView
    {
        public BehaviourNode node;
        public static BehaviourNode mLastSelectConnect = null;
        float size = 40;
        public void Draw(int id)
        {
            WindowFunction(id);
        }
       public void DrawSubNode()
        {
            int t = 0;
            foreach (var n in node.mSubNodes)
            {
                t++;
                Vector3 p0 = node.P0;
                Vector3 p1 = n.P1;
                BehaviourTreeEditor.mInstance.DrawNodeCurve(p0, p1, Color.red);
            }
        }
        public void DrawSimple(int id)
        {
            node.mPos = GUILayout.Window(id, node.mPos, WindowFunctionSimple, node.mName);
        }
        void WindowFunctionSimple(int id)
        {


            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("preview"))
            {

                BehaviourNodePreview.Instance = EditorWindow.GetWindow<BehaviourNodePreview>(true);
                BehaviourNodePreview.Instance.mEditor = this;
            }
            if (GUILayout.Button("delete"))
            {
                BehaviourTreeEditor.mInstance.DeleteNode(this);
            }
            if (GUILayout.Button("link", GUILayout.MaxWidth(size)))
                OnConnectSunNode();
            EditorGUILayout.EndVertical();
            GUI.DragWindow();
        }
        void OnConnectSunNode()
        {
            if (mLastSelectConnect == null)
                mLastSelectConnect = this.node;
            else
            {
                if (mLastSelectConnect != node)
                {
                    if (!mLastSelectConnect.mSubNodes.Contains(node))
                        mLastSelectConnect.mSubNodes.Add(node);
                }

                mLastSelectConnect = null;
                if (BehaviourTreeEditor.mInstance != null)
                    BehaviourTreeEditor.mInstance.Repaint();
                if (null != BehaviourNodePreview.Instance)
                    BehaviourNodePreview.Instance.Repaint();

            }
        }
        
        void WindowFunction(int id)
        {

            if (null == node)
                return;
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label("NodeName:");
            node.mName = GUILayout.TextField(node.mName);
            GUILayout.EndHorizontal();
            #region dragPure
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();



            GUILayout.BeginHorizontal();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            List<BehaviourNode> r = new List<BehaviourNode>();
            foreach (var n in node.mSubNodes)
            {

                if (GUILayout.Button("del:action:" + n.mName))
                {
                    r.Add(n);

                }
            }
            foreach (var t in r)
            {
                node.mSubNodes.Remove(t);
                if (BehaviourTreeEditor.mInstance != null)
                    BehaviourTreeEditor.mInstance.Repaint();
                BehaviourNodePreview.Instance.Repaint();
            }
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
                c.mDataType = (BehavourCondition.DataType)EditorGUILayout.EnumPopup("", c.mDataType, GUILayout.MaxWidth(size));
                c.mType = EditorGUILayout.TextField(c.mType, GUILayout.MaxWidth(size));
                switch (c.mDataType)
                {
                    case BehavourCondition.DataType.FLOAT:
                        {
                            c.mOperation = (BehavourCondition.OPREATION)EditorGUILayout.EnumPopup("", c.mOperation, GUILayout.MaxWidth(size));
                            c.mFloatValue = EditorGUILayout.FloatField(c.mFloatValue);
                        }
                        break;
                    case BehavourCondition.DataType.INT:
                        {
                            c.mOperation = (BehavourCondition.OPREATION)EditorGUILayout.EnumPopup("", c.mOperation, GUILayout.MaxWidth(size));
                            c.mIntValue = EditorGUILayout.IntField(c.mIntValue);
                        }
                        break;
                    case BehavourCondition.DataType.STRING:
                        {
                            c.mStr = EditorGUILayout.TextField(c.mStr);
                        }
                        break;
                    case BehavourCondition.DataType.BOOL:
                        c.mB = EditorGUILayout.Toggle(c.mB);
                        break;
                }
                if (GUILayout.Button("del", GUILayout.MaxWidth(size)))
                {
                    r2.Add(c);

                }
                GUILayout.EndHorizontal();
            }
            foreach (var t2 in r2)
            {
                node.mCondition.Remove(t2);
            }
            if (GUILayout.Button("add", GUILayout.MaxWidth(size)))
                node.mCondition.Add(new BehavourCondition());
            #endregion


            GUILayout.BeginVertical();
            GUILayout.Space(1);
            GUILayout.Label("----action-----");

            List<BehavourAction> r3 = new List<BehavourAction>();
            foreach (var a in node.mActions)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Name:", GUILayout.MaxWidth(size));
                a.mType = EditorGUILayout.TextField(a.mType, GUILayout.MaxWidth(size));
                if (GUILayout.Button("del", GUILayout.MaxWidth(size)))
                {
                    r3.Add(a);

                }
                GUILayout.EndHorizontal();
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Float:", GUILayout.MaxWidth(size));
                for (int i = 0; i < a.mFloat.Count; i++)
                {
                    a.mFloat[i] = EditorGUILayout.FloatField(a.mFloat[i], GUILayout.MaxWidth(size));
                }
                if(GUILayout.Button("add",GUILayout.MaxWidth(size)))
                {
                    a.mFloat.Add(0);
                }
                if(a.mFloat.Count > 0)
                {
                    if (GUILayout.Button("del", GUILayout.MaxWidth(size)))
                    {
                        a.mFloat.RemoveAt(a.mFloat.Count - 1);
                    }
                }
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Int:", GUILayout.MaxWidth(size));
                for (int i = 0; i < a.mInt.Count; i++)
                {
                    a.mInt[i] = EditorGUILayout.IntField(a.mInt[i], GUILayout.MaxWidth(size));
                }
                if (GUILayout.Button("add", GUILayout.MaxWidth(size)))
                {
                    a.mInt.Add(0);
                }
                if (a.mInt.Count > 0)
                {
                    if (GUILayout.Button("del", GUILayout.MaxWidth(size)))
                    {
                        a.mInt.RemoveAt(a.mInt.Count - 1);
                    }
                }
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("String:", GUILayout.MaxWidth(size));
                for (int i = 0; i < a.mStr.Count; i++)
                {
                    a.mStr[i] = EditorGUILayout.TextField(a.mStr[i], GUILayout.MaxWidth(size));
                }
                if (GUILayout.Button("add", GUILayout.MaxWidth(size)))
                {
                    a.mStr.Add("0");
                }
                if (a.mStr.Count > 0)
                {
                    if (GUILayout.Button("del", GUILayout.MaxWidth(size)))
                    {
                        a.mStr.RemoveAt(a.mStr.Count - 1);
                    }
                }
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Bool:", GUILayout.MaxWidth(size));
                for (int i = 0; i < a.mBool.Count; i++)
                {
                    a.mBool[i] = EditorGUILayout.Toggle(a.mBool[i], GUILayout.MaxWidth(size));
                }
                if (GUILayout.Button("add", GUILayout.MaxWidth(size)))
                {
                    a.mBool.Add(true);
                }
                if (a.mBool.Count > 0)
                {
                    if (GUILayout.Button("del", GUILayout.MaxWidth(size)))
                    {
                        a.mBool.RemoveAt(a.mBool.Count - 1);
                    }
                }
                GUILayout.EndHorizontal();



                GUILayout.EndVertical();
                GUILayout.EndHorizontal();

            }
            foreach(var t in r3)
            {
                node.mActions.Remove(t);
            }
            if (GUILayout.Button("add", GUILayout.MaxWidth(size)))
                node.mActions.Add(new BehavourAction());
            GUILayout.EndVertical();



            GUILayout.EndVertical();
        }
    }
}