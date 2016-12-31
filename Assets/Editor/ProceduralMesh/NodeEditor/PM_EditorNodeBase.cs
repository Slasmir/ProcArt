using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class PM_EditorNodeBase {

    public Vector2 location { get { return Location; } }
    Vector2 Location = Vector2.zero;
    
    public Vector2 size { get { return Size; } }
    Vector2 Size = new Vector2(125, 50);

    public Color rectColor
    {
        get { return RectColor; }
        set { RectColor = value; }
    }
    Color RectColor = new Color(1, 1, 1);

    protected PM_Window ParentWindow;
    protected List<PM_ConnectorBase> AllConnectors = new List<PM_ConnectorBase>();

    //Get the current rect that is drawn
    public Rect CurrentRect
    {
        get { return new Rect(Location, Size); }
    }

    //Constructor
    public PM_EditorNodeBase(Vector2 _Location, PM_Window _ParentWindow)
    {
        ParentWindow = _ParentWindow;
        Location = _Location;
    }

    //Draw Update Call
    public void DrawNode()
    {
        Rect drawRect = new Rect(Location, Size);
        EditorGUI.DrawRect(drawRect, RectColor);
        DrawInputs();
    }

    //Used to draw the inputs if the node have any.
    protected virtual void DrawInputs(){ }

    //Custom Right click menu for the node
    public void RightClickMenu(Event currentEvent)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("I dont do anything"), false, DebugLog, "test");
        menu.ShowAsContext();
        currentEvent.Use();
    }

    //Calculate the information in the node, only used in children
    public virtual void CalculateNode() { }

    //Select the node, this can only be done from the Window
    public void SelectNode() {
        RectColor = new Color(1, 0, 0);
    }

    //Deselect the node, have to be done before another node can be selected. 
    public void DeselectNode()
    {
        RectColor = new Color(1, 1, 1);
    }

    //Will remove the node from memory
    public void DeleteNode()
    {
        DeselectNode();
    }

    //Moves the node acording to deltaLocation
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

    //Adds all nodes from the connector list to the windows known connectors.
    protected void AddConnectorsToParent()
    {
        foreach (PM_ConnectorBase connector in AllConnectors)
        {
            ParentWindow.AddConnectorToList(connector);
        }
    }

    //Temp function to remember how menus work
    private void DebugLog(object s)
    {
        Debug.Log(s);
    }
}