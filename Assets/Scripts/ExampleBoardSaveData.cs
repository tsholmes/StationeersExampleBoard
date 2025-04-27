using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Assets.Scripts.Objects;
using LibConstruct;
using UnityEngine;

namespace ExampleBoard
{
  [XmlInclude(typeof(ExampleBoardSaveData))]
  public class ExampleBoardSaveData : StructureSaveData
  {
    [XmlElement]
    public PlacementBoardHostSaveData Board;
  }
  [XmlInclude(typeof(SmallLetterSaveData))]
  public class SmallLetterSaveData : StructureSaveData
  {
    [XmlElement]
    public PlacementBoardStructureSaveData Board;
  }
}
