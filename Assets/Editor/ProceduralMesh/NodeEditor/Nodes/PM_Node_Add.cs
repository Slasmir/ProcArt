using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM_Node_Add : PM_EditorNodeBase {

    public PM_Node_Add(Vector2 _Location, PM_Window _ParentWindow) : base(_Location, _ParentWindow)
    {
        rectColor = new Color(0, 1, 0);
        InputConnectorOne = new PM_Connector_Float(true);
        InputConnectorOne.SetLocation(PM_ConnectorPlacement.DualInputPlacementOne(this, InputConnectorOne));

        InputConnectorTwo = new PM_Connector_Float(true);
        InputConnectorTwo.SetLocation(PM_ConnectorPlacement.DualInputPlacementTwo(this, InputConnectorTwo));

        OutputConnector = new PM_Connector_Float(false);
        OutputConnector.SetLocation(PM_ConnectorPlacement.SingleOutputPlacement(this, OutputConnector));

        AllConnectors.Add(InputConnectorOne);
        AllConnectors.Add(InputConnectorTwo);
        AllConnectors.Add(OutputConnector);

        AddConnectorsToParent();
    }

    public float HardCodeFloatOne = 0;
    public float HardCodeFloatTwo = 0;

    //Inputs
    PM_Connector_Float InputConnectorOne;
    PM_Connector_Float InputConnectorTwo;

    //Outputs
    PM_Connector_Float OutputConnector;

    public override void CalculateNode()
    {
        if (OutputConnector == null) return;

        float ValueOne = HardCodeFloatOne;
        float ValueTwo = HardCodeFloatTwo;
        if (InputConnectorOne != null) ValueOne = InputConnectorOne.floatValue;
        if (InputConnectorTwo != null) ValueTwo = InputConnectorTwo.floatValue;

        OutputConnector.floatValue = ValueOne + ValueTwo;        
    }

    protected override void DrawInputs()
    {
        base.DrawInputs();
        InputConnectorOne.DrawInput();
        InputConnectorTwo.DrawInput();
        OutputConnector.DrawInput();
    }
}

