using System;
using ExampleBoard;
using HarmonyLib;
using StationeersMods.Interface;
[StationeersMod("ExampleBoardMod","ExampleBoardMod [StationeersMods]","0.2.4657.21547.1")]
public class ExampleBoardMod : ModBehaviour
{
    // private ConfigEntry<bool> configBool;
    
    public override void OnLoaded(ContentHandler contentHandler)
    {
        UnityEngine.Debug.Log("ExampleBoardMod says: Hello World!");
        
        //Config example
        // configBool = Config.Bind("Input",
        //     "Boolean",
        //     true,
        //     "Boolean description");
        
        Harmony harmony = new Harmony("ExampleBoardMod");
        PrefabPatch.prefabs = contentHandler.prefabs;
        harmony.PatchAll();
        UnityEngine.Debug.Log("ExampleBoardMod Loaded with " + contentHandler.prefabs.Count + " prefab(s)");
    }
}
