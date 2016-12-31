using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PM_ConnectorPlacement {

    public static Vector2 SingleInputPlacement(PM_EditorNodeBase node, PM_ConnectorBase connector)
    {
        float y = (node.size.y / 2f) - (connector.size.y / 2f) + node.location.y;
        float x = node.location.x - connector.size.x;

        return new Vector2(x, y);
    }

    public static Vector2 SingleOutputPlacement(PM_EditorNodeBase node, PM_ConnectorBase connector)
    {
        float y = (node.size.y / 2f) - (connector.size.y / 2f) + node.location.y;
        float x = node.location.x + node.size.x;

        return new Vector2(x, y);
    }

    public static Vector2 DualInputPlacementOne(PM_EditorNodeBase node, PM_ConnectorBase connector)
    {
        float y = (node.size.y / 4f) - (connector.size.y / 2) + node.location.y;
        float x = node.location.x - connector.size.x;

        return new Vector2(x, y);
    }

    public static Vector2 DualInputPlacementTwo(PM_EditorNodeBase node, PM_ConnectorBase connector)
    {
        float y = ((node.size.y / 4f) * 3f) - (connector.size.y / 2) + node.location.y;
        float x = node.location.x - connector.size.x;

        return new Vector2(x, y);
    }
}
