using UnityEngine;
using System.Collections;

public class TycoonUserInput : UserInput <TycoonUserInput>
{
    [HideInInspector]public GrendelKeyBinding UseKey01 = new GrendelKeyBinding("Use01", KeyCode.W, KeyCode.UpArrow, GrendelKeyBinding.MouseButtons.None, GrendelKeyBinding.MouseButtons.None);
    [HideInInspector]public GrendelKeyBinding UseKey02 = new GrendelKeyBinding("Use02", KeyCode.S, KeyCode.DownArrow, GrendelKeyBinding.MouseButtons.None, GrendelKeyBinding.MouseButtons.None);

}
