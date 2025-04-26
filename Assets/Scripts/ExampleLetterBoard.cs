using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Objects;
using LibConstruct;
using UnityEngine;

namespace ExampleBoard
{
  public class ExampleLetterBoard : PlacementBoard
  {
    public override IPlacementBoardStructure EquivalentStructure(Structure structure)
    {
      if (structure == null) return null;
      return structure.name switch
      {
        "StructureBigA" => Prefab.Find("StructureSmallA"),
        "StructureBigB" => Prefab.Find("StructureSmallB"),
        "StructureBigC" => Prefab.Find("StructureSmallC"),
        _ => null
      } as IPlacementBoardStructure;
    }
  }
}
