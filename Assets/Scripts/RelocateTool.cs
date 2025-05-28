using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Inventory;
using Assets.Scripts.Objects.Items;
using StationeersMods.Interface;
using UnityEngine;

namespace ExampleBoard
{
  public class RelocateTool : ManualTool, IPatchOnLoad
  {
    public void PatchOnLoad()
    {
      var src = StationeersModsUtility.FindTool(StationeersTool.SCREWDRIVER);
      this.Blueprint = src.Blueprint;
      this.Thumbnail = src.Thumbnail;
    }
  }
}
