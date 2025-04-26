using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Objects;
using LibConstruct;
using StationeersMods.Interface;
using UnityEngine;

namespace ExampleBoard
{
  public class SmallLetter : SmallGrid, IPlacementBoardStructure, IPatchOnLoad
  {
    public override void Awake()
    {
      base.Awake();
      BoardStructureHooks.Awake(this);
    }

    public PlacementBoard Board { get; set; }
    public PlacementBoard.BoardCell[] BoardCells { get; set; }

    public void PatchOnLoad()
    {
      foreach (var renderer in this.GetComponentsInChildren<MeshRenderer>())
      {
        renderer.sharedMaterial = StationeersModsUtility.GetMaterial(StationeersColor.WHITE, ShaderType.NORMAL);
      }
      this.BuildStates[0].Tool.ToolExit = StationeersModsUtility.FindTool(StationeersTool.DRILL);
    }

    public override CanConstructInfo CanConstruct()
    {
      return BoardStructureHooks.CanConstruct(this);
    }
  }
}
