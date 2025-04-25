using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Objects;
using StationeersMods.Interface;
using UnityEngine;

namespace ExampleBoard
{
  public class BigLetter : SmallGrid, IPatchOnLoad
  {
    public void PatchOnLoad()
    {
      foreach (var renderer in this.GetComponentsInChildren<MeshRenderer>())
      {
        renderer.sharedMaterial = StationeersModsUtility.GetMaterial(StationeersColor.WHITE, ShaderType.NORMAL);
      }
    }
  }
}
