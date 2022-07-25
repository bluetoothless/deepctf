using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandTileScript : TileBaseScript
{
    
    public SandTileScript() : base()
    {
        SetSpeedChangeModifier(0.5f);
        SetLogMessage("SAND!! Slow the Agent!");
    }

}
