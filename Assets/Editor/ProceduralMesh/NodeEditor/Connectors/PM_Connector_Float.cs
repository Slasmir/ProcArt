using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM_Connector_Float : PM_ConnectorBase{

    public PM_Connector_Float (bool _IsInput) : base(_IsInput) { }

    public float floatValue
    {
        get { return FloatValue; }
        set { value = FloatValue; }
    }

    float FloatValue = 0;

}
