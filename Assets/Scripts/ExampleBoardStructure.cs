using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.GridSystem;
using Assets.Scripts.Networking;
using Assets.Scripts.Objects;
using LibConstruct;
using StationeersMods.Interface;
using UnityEngine;

namespace ExampleBoard
{
  public class ExampleBoardStructure : SmallGrid, IPlacementBoardHost, IPatchOnLoad
  {
    public Transform BoardOrigin;
    public List<BoxCollider> BoardColliders = new();

    private BoardRef<ExampleLetterBoard> BoardRef;
    public ExampleLetterBoard Board => BoardRef?.Board;

    public override ThingSaveData SerializeSave()
    {
      var saveData = new ExampleBoardSaveData();
      var baseData = saveData as ThingSaveData;
      this.InitialiseSaveData(ref baseData);
      return saveData;
    }

    protected override void InitialiseSaveData(ref ThingSaveData baseData)
    {
      base.InitialiseSaveData(ref baseData);
      if (baseData is not ExampleBoardSaveData saveData)
        return;
      saveData.Board = BoardHostHooks.SerializeBoard(this, this.Board);
    }

    public override void DeserializeSave(ThingSaveData baseData)
    {
      base.DeserializeSave(baseData);
      if (baseData is not ExampleBoardSaveData saveData)
        return;
      BoardHostHooks.DeserializeBoard(this, saveData.Board, out this.BoardRef, this.BoardOrigin);
    }

    public override void OnFinishedLoad()
    {
      base.OnFinishedLoad();
      BoardHostHooks.OnFinishedLoadBoard(this, ref this.BoardRef, this.BoardOrigin);
    }

    public override void OnRegistered(Cell cell)
    {
      base.OnRegistered(cell);
      BoardHostHooks.OnRegisteredBoard(this, ref this.BoardRef, this.BoardOrigin);
    }

    public override void OnDestroy()
    {
      base.OnDestroy();
      this.Board?.RemoveHost(this);
    }

    public override void SerializeOnJoin(RocketBinaryWriter writer)
    {
      base.SerializeOnJoin(writer);
      BoardHostHooks.SerializeBoardOnJoin(writer, this, this.Board);
    }

    public override void DeserializeOnJoin(RocketBinaryReader reader)
    {
      base.DeserializeOnJoin(reader);
      BoardHostHooks.DeserializeBoardOnJoin(reader, this, out this.BoardRef, this.BoardOrigin);
    }

    public IEnumerable<BoxCollider> CollidersForBoard(PlacementBoard board)
    {
      if (board == this.Board)
        return this.BoardColliders;
      return new List<BoxCollider>();
    }

    public IEnumerable<PlacementBoard> GetPlacementBoards()
    {
      yield return this.Board;
    }

    public void PatchOnLoad()
    {
      foreach (var renderer in this.GetComponentsInChildren<MeshRenderer>())
      {
        renderer.sharedMaterial = StationeersModsUtility.GetMaterial(StationeersColor.BLACK, ShaderType.NORMAL);
      }
      this.BuildStates[0].Tool.ToolExit = StationeersModsUtility.FindTool(StationeersTool.DRILL);
    }

    public void OnBoardStructureRegistered(PlacementBoard board, IPlacementBoardStructure structure) { }

    public void OnBoardStructureDeregistered(PlacementBoard board, IPlacementBoardStructure structure) { }
  }
}
