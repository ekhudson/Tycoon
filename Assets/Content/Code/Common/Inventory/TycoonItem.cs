using UnityEngine;
using System.Collections;

public class TycoonItem : ScriptableObject 
{
    public string Name = "New Item";
    public string Description = "This is a new item";
    public float Cost = 1.0f;
    public Texture2D Icon;
    public int StackSize = 10;
}
