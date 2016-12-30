using UnityEngine;
using UnityEditor;
using System.Collections;

public class PM_EditorNodeBase {
    Vector2 Location = Vector2.zero;
    Vector2 Size = new Vector2(125, 50);
    public Rect CurrentRect
    {
        get { return new Rect(Location, Size); }
    }

    public PM_EditorNodeBase(Vector2 _Location)
    {
        Location = _Location;
    }

    public void DrawNode()
    {
        Rect drawRect = new Rect(Location, Size);
        Color RectColor = new Color(1, 1, 1);
        EditorGUI.DrawRect(drawRect, RectColor);
    }

    public void RightClickMenu(Event currentEvent)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("I dont do anything"), false, DebugLog, "test");
        menu.ShowAsContext();
        currentEvent.Use();
    }

    public void MoveNode(Vector2 DeltaLocation)
    {
        Location += DeltaLocation;
    }

    private void DebugLog(object s)
    {
        Debug.Log(s);
    }
}