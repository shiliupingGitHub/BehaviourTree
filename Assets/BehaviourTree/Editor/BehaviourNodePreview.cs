using UnityEngine;
using System.Collections;
using UnityEditor;
public class BehaviourNodePreview : EditorWindow {
    public BehaviourNodeEditor mEditor;
    public static BehaviourNodePreview Instance;
    void OnGUI()
    {
        mEditor.Draw(0);
    }
}
