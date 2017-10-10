using UnityEngine;
using System.Collections;
using UnityEditor;
namespace Behaviour
{
    public class BehaviourNodePreview : EditorWindow
    {
        public BehaviourNodeView mEditor;
        public static BehaviourNodePreview Instance;
        void OnGUI()
        {
            mEditor.Draw(0);
        }
    }
}


