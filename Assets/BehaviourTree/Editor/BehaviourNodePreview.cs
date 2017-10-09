using UnityEngine;
using System.Collections;
using UnityEditor;
public class BehaviourNodePreview : EditorWindow {
    public BehaviourNodeEditor mEditor;
    void OnGUI()
    {
        mEditor.Draw(0);
    }
}
