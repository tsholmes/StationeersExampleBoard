using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Objects;
using LibConstruct;
using StationeersMods.Interface;
using UnityEngine;

namespace ExampleBoard
{
  public class ExampleBoardStructure : SmallGrid, IPlacementBoardHost, IPatchOnLoad
  {
    public Transform BoardOrigin;
    public BoxCollider BoardCollider;

    public ExampleLetterBoard Board;

    public override void Awake()
    {
      base.Awake();
      if (!this.IsCursor && !Structure.IsCursorCreating)
      {
        Board = new ExampleLetterBoard()
        {
          Origin = BoardOrigin,
          GridSize = 0.5f / 8,
        };
        Board.AddHost(this);
      }
    }

    public override void OnDestroy()
    {
      base.OnDestroy();
      if (this.Board != null)
      {
        this.Board.RemoveHost(this);
      }
    }

    public IEnumerable<BoxCollider> CollidersForBoard(PlacementBoard board)
    {
      if (board == this.Board)
        yield return this.BoardCollider;
    }

    public void PatchOnLoad()
    {
      foreach (var renderer in this.GetComponentsInChildren<MeshRenderer>())
      {
        renderer.sharedMaterial = StationeersModsUtility.GetMaterial(StationeersColor.BLACK, ShaderType.NORMAL);
      }
      this.BuildStates[0].Tool.ToolExit = StationeersModsUtility.FindTool(StationeersTool.DRILL);
    }
  }
}
