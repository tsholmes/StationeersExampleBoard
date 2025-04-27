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

    public override void OnDeregistered()
    {
      base.OnDeregistered();
      BoardStructureHooks.OnDeregistered(this);
    }

    public override ThingSaveData SerializeSave()
    {
      var saveData = new SmallLetterSaveData();
      var baseData = saveData as ThingSaveData;
      this.InitialiseSaveData(ref baseData);
      return saveData;
    }

    protected override void InitialiseSaveData(ref ThingSaveData baseData)
    {
      base.InitialiseSaveData(ref baseData);
      if (baseData is not SmallLetterSaveData saveData)
        return;
      saveData.Board = BoardStructureHooks.SerializeSave(this);
    }

    public override void DeserializeSave(ThingSaveData baseData)
    {
      base.DeserializeSave(baseData);
      if (baseData is not SmallLetterSaveData saveData)
        return;
      BoardStructureHooks.DeserializeSave(this, saveData.Board);
    }

    public override void SerializeOnJoin(RocketBinaryWriter writer)
    {
      base.SerializeOnJoin(writer);
      BoardStructureHooks.SerializeOnJoin(writer, this);
    }

    public override void DeserializeOnJoin(RocketBinaryReader reader)
    {
      base.DeserializeOnJoin(reader);
      BoardStructureHooks.DeserializeOnJoin(reader, this);
    }
  }
}
