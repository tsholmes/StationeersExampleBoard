using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Serialization;
using HarmonyLib;
using UnityEngine;

namespace ExampleBoard
{
  [HarmonyPatch]
  public class SaveDataPatch
  {
    [HarmonyPatch(typeof(XmlSaveLoad), nameof(XmlSaveLoad.AddExtraTypes))]
    public static void Prefix(ref List<System.Type> extraTypes)
    {
      extraTypes.Add(typeof(ExampleBoardSaveData));
      extraTypes.Add(typeof(SmallLetterSaveData));
    }
  }
}
