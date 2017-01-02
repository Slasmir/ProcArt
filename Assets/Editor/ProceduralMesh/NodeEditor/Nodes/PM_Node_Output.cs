using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM_Node_Output : PM_EditorNodeBase {


    public PM_Node_Output(Vector2 _Location, PM_Window _ParentWindow) : base(_Location, _ParentWindow)
    {
        rectColor = new Color(0, 1, 0);
        InputConnector = new PM_Connector_Float(true);
        InputConnector.SetLocation(PM_ConnectorPlacement.SingleInputPlacement(this, InputConnector));
        AllConnectors.Add(InputConnector);

        AddConnectorsToParent();
    }


    //Inputs
    PM_ConnectorBase InputConnector;

    //Outputs

    public override void CalculateNode()
    {

    }

    protected override void DrawInputs()
    {
        base.DrawInputs();
        InputConnector.DrawInput();
    }
}
