using System.Collections;
using System.Collections.Generic;
using LibConstruct;
using StationeersMods.Interface;
using UnityEngine;

namespace ExampleBoard
{
  public class SmallLetter : PlacementBoardStructure, IPatchOnLoad
  {
    public void PatchOnLoad()
    {
      foreach (var renderer in this.GetComponentsInChildren<MeshRenderer>())
      {
        renderer.sharedMaterial = StationeersModsUtility.GetMaterial(StationeersColor.WHITE, ShaderType.NORMAL);
      }
      this.BuildStates[0].Tool.ToolExit = StationeersModsUtility.FindTool(StationeersTool.DRILL);
    }
  }
}
