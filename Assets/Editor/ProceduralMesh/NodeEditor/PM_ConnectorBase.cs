using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
 
public class PM_ConnectorBase {

    public Vector2 location { get { return Location; } }
    public Vector2 drawLineLocation { get { return Location + (Size / 2f); } }
    Vector2 Location = Vector2.zero;

    public Vector2 size { get { return Size; } }
    Vector2 Size = Vector2.one * 20f;

    public Rect currentRect { get { return new Rect(Location, Size); } }

    string InfoText = "This is not written";
    string DisplayText = "inp";

    public bool isInput { get { return IsInput; } }
    bool IsInput = false;

    List<PM_ConnectorBase> Connections = new List<PM_ConnectorBase>();

    Color InputColor = new Color(.7f, .7f, .7f);


    public PM_ConnectorBase (bool _IsInput)
    {
        IsInput = _IsInput;
    }

    //Set the text on the hover over node
    void SetInfoText(string newText)
    {
        InfoText = newText;
    }


    //Set the text displayed on the connector
    void setDisplayText(string newText)
    {
        DisplayText = newText;
    }

    public void SetLocation(Vector2 NewLocation)
    {
        Location = NewLocation;
    }

    //Move the node, only called from parent PM_EditorNodeBase
    public void MoveNode(Vector2 DeltaLocation)
    {
        Location += DeltaLocation;
    }

    //Draw the input box
    public void DrawInput()
    {
        Rect drawRect = new Rect(Location, Size);
        EditorGUI.DrawRect(drawRect, InputColor);
        DrawInputLine();
    }

    //Connect to antoher Connector
    public void Connect(PM_ConnectorBase other)
    {
        Connections.Add(other); 
    }

    //Remove Connection from another connector
    public void RemoveConnect(PM_ConnectorBase other)
    {
        if (Connections.Contains(other))
        {
            Connections.Remove(other);
        }
    }

    //Draw lines to all connections
    public void DrawInputLine()
    {
        if (Connections.Count == 0) return;

        foreach(PM_ConnectorBase Connection in Connections)
        {
            Handles.BeginGUI();
            Handles.color = Color.red;
            Vector2 drawLocation = Location + (Size / 2f);
            Handles.DrawLine(drawLocation, Connection.drawLineLocation);
        }
    }

    //Draw the active drag line
    public void DrawActiveInputLine(Vector2 MouseLocation)
    {
        Handles.BeginGUI();
        Handles.color = Color.red;
        Vector2 drawLocation = Location + (Size / 2f);
        Handles.DrawLine(drawLocation, MouseLocation);
    }
}
