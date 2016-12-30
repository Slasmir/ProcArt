using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class PM_Window : EditorWindow{

    List<PM_EditorNodeBase> Nodes = new List<PM_EditorNodeBase>();
    PM_EditorNodeBase SelectedNode = null;

    [MenuItem("Procedural Mesh/Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PM_Window));
    }

    void OnGUI()
    {
        Event currentEvent = Event.current;
        CleanupNodes();
        LeftClickHandle(currentEvent);
        RightClickHandle(currentEvent);
        DrawNodes();
    }
    void LeftClickHandle(Event currentEvent)
    {
        if(currentEvent.type == EventType.MouseDown && currentEvent.type != EventType.ContextClick)
            SelectNode(currentEvent);
        
    }

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

    void SelectNode(Event currentEvent)
    {
        Debug.Log("sss");
        //SelectedNode = MouseOverNode(currentEvent);
    }

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

    void MoveAllNodes()
    {

    }

    void AddNewNode(object Location)
    {
        Vector2 CursorLocation = (Vector2)Location;
        Nodes.Add(new PM_EditorNodeBase(CursorLocation));
    }

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
