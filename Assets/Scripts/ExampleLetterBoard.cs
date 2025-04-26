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
      if (structure.name == "StructureBigA")
        return Prefab.Find("StructureSmallB") as IPlacementBoardStructure;
      return null;
    }
  }
}
