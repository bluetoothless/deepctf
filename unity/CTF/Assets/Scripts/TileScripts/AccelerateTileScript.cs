using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateTileScript : TileBaseScript
{

    public AccelerateTileScript() : base()
    {
        SetSpeedChangeModifier(2f);
        SetLogMessage("ACCELERATE!! GIVE IT SOME SPEEEEEEED!");
    }

}
