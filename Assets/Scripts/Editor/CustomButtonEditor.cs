using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
[CustomEditor(typeof(CustomButton))]
public class CustomButtonEditor : ButtonEditor
{
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        var transition = new PropertyField(serializedObject.FindProperty(CustomButton.Transition));
        var curveEase = new PropertyField(serializedObject.FindProperty(CustomButton.CurveEase));
        var duration = new PropertyField(serializedObject.FindProperty(CustomButton.Duration));
        var strength = new PropertyField(serializedObject.FindProperty(CustomButton.Strength));
        var tweenLabel = new Label("Tween Settings");
        root.Add(new IMGUIContainer(OnInspectorGUI));
        root.Add(tweenLabel);
        root.Add(transition);
        root.Add(curveEase);
        root.Add(duration);
        root.Add(strength);
        return root;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        serializedObject.ApplyModifiedProperties();
    }
}

