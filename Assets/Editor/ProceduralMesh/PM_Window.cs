using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class PM_Window : EditorWindow{

    List<PM_EditorNodeBase> Nodes = new List<PM_EditorNodeBase>();
    PM_EditorNodeBase SelectedNode = null;

    Vector2 MousePos = Vector2.zero;
    Vector2 DeltaMousePos = Vector2.zero;

    bool IsLeftMouseDown = false;
    bool IsMiddleMouseDown = false;

    [MenuItem("Procedural Mesh/Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PM_Window));
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            Debug.Log("sssss");

        Repaint();
    }

    void OnGUI()
    {
        Event currentEvent = Event.current;

        //Value Updates
        CleanupNodes();
        CalculateMouseDelta();

        //Event Handlers
        LeftClickHandle(currentEvent);
        LeftHoldHandle(currentEvent);
        RightClickHandle(currentEvent);
        MiddleClickHandle(currentEvent);
        MiddleHoldHandle(currentEvent);

        //Draw
        DrawNodes();
        Repaint();
    }

    //Calculates the deltaposition of the mouse every frame (This can be done only when a mouse is helt down, but for now its done every frame). 
    void CalculateMouseDelta()
    {
        Vector2 NewMousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        DeltaMousePos = NewMousePos - MousePos;
        MousePos = NewMousePos;
    }

    //Left Click Event
    void LeftClickHandle(Event currentEvent)
    {
        if (currentEvent.type == EventType.MouseDown && currentEvent.button == 0)
        {
            SelectMouseoverNode(currentEvent);
            if (!IsLeftMouseDown)
            {
                IsLeftMouseDown = true; 
            }
        }

        if(currentEvent.type == EventType.MouseUp && currentEvent.button == 0)
        {
            if (IsLeftMouseDown)
            {
                IsLeftMouseDown = false;
            }
        }
    }

    //Left Hold event
    void LeftHoldHandle(Event currentEvent)
    {
        if (!IsLeftMouseDown) return;

        if(SelectedNode != null)
            MoveNode(SelectedNode);
    }

    //Right CLick Event 
    void RightClickHandle(Event currentEvent)
    {
        if (currentEvent.type == EventType.ContextClick)
        {
            Vector2 mousePos = currentEvent.mousePosition;
            PM_EditorNodeBase CurrentNodeSelection = MouseOverNode(currentEvent);

            if(CurrentNodeSelection != null)
            {
                CurrentNodeSelection.RightClickMenu(currentEvent);
            }

            //Show default Menu
            else
            {
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Add Default Node"), false, AddNewNode, currentEvent.mousePosition);
                menu.AddSeparator("");
                menu.ShowAsContext();
                currentEvent.Use();
            }
        }
    }

    void MiddleClickHandle(Event currentEvent)
    {
        if (currentEvent.type == EventType.MouseDown && currentEvent.button == 2)
        {
            if (!IsMiddleMouseDown)
            {
                IsMiddleMouseDown = true;
            }
        }

        if (currentEvent.type == EventType.MouseUp && currentEvent.button == 2)
        {
            if (IsMiddleMouseDown)
            {
                IsMiddleMouseDown = false;
            }
        }
    }

    void MiddleHoldHandle(Event currentEvent)
    {
        if (!IsMiddleMouseDown) return;
        MoveAllNodes();
    }

    //returns a node if the mouse is hovering it. 
    PM_EditorNodeBase MouseOverNode(Event currentEvent)
    {
        Vector2 mousePos = currentEvent.mousePosition;
        PM_EditorNodeBase SelectedNode = null;
        foreach (PM_EditorNodeBase node in Nodes)
        {
            if (node.CurrentRect.Contains(mousePos))
            {
                SelectedNode = node;
                break;
            }
        }
        return SelectedNode;
    }

    //Selects the node corelating to mouse position
    void SelectMouseoverNode(Event currentEvent)
    {
        SelectNode(MouseOverNode(currentEvent));
    }

    //Selects node
    void SelectNode(PM_EditorNodeBase node)
    {
        if (node == null)
        {
            DeselectNode();
            return;
        }

        if (SelectedNode != null)
        {
            SelectedNode.DeselectNode();
        }

        SelectedNode = node;

        if (SelectedNode != null)
        {
            SelectedNode.SelectNode();
        }
    }

    //Deselect node
    void DeselectNode()
    {
        if(SelectedNode != null)
        {
            SelectedNode.DeselectNode();
        }
        SelectedNode = null;
    }

    //Draw all nodes
    void DrawNodes()
    {
        if (Nodes.Count != 0)
        {
            foreach (PM_EditorNodeBase node in Nodes)
            {
                node.DrawNode();
            }
        }
    }

    //MovesAllNodes
    void MoveAllNodes()
    {
        foreach(PM_EditorNodeBase node in Nodes)
        {
            node.MoveNode(DeltaMousePos);
        }
    }

    //Moves a specifik node
    void MoveNode(PM_EditorNodeBase node)
    {
        node.MoveNode(DeltaMousePos);
    }

    //Creates a new node and adds it to the list
    void AddNewNode(object Location)
    {
        Vector2 CursorLocation = (Vector2)Location;
        Nodes.Add(new PM_EditorNodeBase(CursorLocation));
    }


    //Makes sure NULL Nodes are removed from the existing node list. 
    void CleanupNodes()
    {
        bool Rerun = false;

        for (int i = 0; i < Nodes.Count; i++)
        {
            if (Nodes[i] == null)
            {
                Nodes.RemoveAt(i);
                Rerun = true;
                break;
            }
        }

        if (Rerun == true) CleanupNodes();
    }
}
