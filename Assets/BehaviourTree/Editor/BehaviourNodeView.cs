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
            GUILayout.BeginHorizontal();
            GUILayout.Label("action:");
            node.mName = GUILayout.TextField(node.mName);
            GUILayout.EndHorizontal();

            foreach (var a in node.mActions)
            {
                GUILayout.BeginHorizontal();
                a.mDataType = (BehavourAction.DataType)EditorGUILayout.EnumPopup("", a.mDataType, GUILayout.MaxWidth(size));
                a.mType = EditorGUILayout.TextField(a.mType, GUILayout.MaxWidth(size));
                switch (a.mDataType)
                {
                    case BehavourAction.DataType.FLOAT:
                        {
                            a.f0 = EditorGUILayout.FloatField(a.f0);
                            a.f1 = EditorGUILayout.FloatField(a.f1);
                        }
                        break;
                    case BehavourAction.DataType.INT:
                        {
                            a.n0 = EditorGUILayout.IntField(a.n0);
                            a.n1 = EditorGUILayout.IntField(a.n1);
                        }
                        break;
                    case BehavourAction.DataType.STRING:
                        {
                            a.mStr = EditorGUILayout.TextField(a.mStr);
                        }
                        break;
                }
                GUILayout.EndHorizontal();

            }

            if (GUILayout.Button("add", GUILayout.MaxWidth(size)))
                node.mActions.Add(new BehavourAction());
            GUILayout.EndVertical();



            GUILayout.EndVertical();
        }
    }
}