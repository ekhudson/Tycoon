using UnityEngine;
using System.Collections;

public class GrendelProjectData : ScriptableObject
{
    public string ProjectTitle = "Grendel Project";
    public string ProjectVersion = "1.0";
    public bool DebugBuild = true;

    public GrendelGameOptions GameOptions;
    public GrendelConsoleOptions ConsoleOptions;
    [HideInInspector]public GrendelAudioOptions AudioOptions;	
}
