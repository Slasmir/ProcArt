using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class PM_EditorNodeBase {
    Vector2 Location = Vector2.zero;
    Vector2 Size = new Vector2(125, 50);
    Color RectColor = new Color(1, 1, 1);

    List<PM_ConnectorBase> AllConnectors = new List<PM_ConnectorBase>();

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
        EditorGUI.DrawRect(drawRect, RectColor);
    }

    public void RightClickMenu(Event currentEvent)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("I dont do anything"), false, DebugLog, "test");
        menu.ShowAsContext();
        currentEvent.Use();
    }

    public void SelectNode() {
        RectColor = new Color(1, 0, 0);
    }

    public void DeselectNode()
    {
        RectColor = new Color(1, 1, 1);
    }

    public void MoveNode(Vector2 DeltaLocation)
    {
        Location += DeltaLocation;
        if(AllConnectors.Count != 0)
        {
            foreach(PM_ConnectorBase Connector in AllConnectors)
            {
                Connector.MoveNode(DeltaLocation);
            }
        }
    }

    private void DebugLog(object s)
    {
        Debug.Log(s);
    }
}