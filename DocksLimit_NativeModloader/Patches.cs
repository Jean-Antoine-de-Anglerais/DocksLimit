using ai.behaviours;
using HarmonyLib;
using HarmonyLib.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DocksLimit_NativeModloader
{
    public static class Patches
    {
        //[HarmonyTranspiler]
        //[HarmonyPatch(typeof(Docks), nameof(Docks.docksAtBoatLimit))]
        public static IEnumerable<CodeInstruction> docksAtBoatLimit_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            int index = codes.FindIndex(instr => instr.opcode == OpCodes.Ldfld && ((FieldInfo)instr.operand).GetType() == typeof(int));

            if (index == -1)
            {
                Console.WriteLine("docksAtBoatLimit_Transpiler: index not found");
                return codes.AsEnumerable();
            }

            codes.RemoveAt(index - 1);
            codes.RemoveAt(index);

            codes.Insert(index, new CodeInstruction(OpCodes.Ldc_I4, 10));

            return codes.AsEnumerable();
        }

        //[HarmonyTranspiler]
        //[HarmonyPatch(typeof(Docks), nameof(Docks.buildBoatFromHere))]
        public static IEnumerable<CodeInstruction> buildBoatFromHere_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            int index = codes.FindIndex(instr => instr.opcode == OpCodes.Ldfld && ((FieldInfo)instr.operand).GetType() == typeof(int));

            if (index == -1)
            {
                Console.WriteLine("buildBoatFromHere_Transpiler: index not found");
                return codes.AsEnumerable();
            }

            codes.RemoveAt(index - 1);
            codes.RemoveAt(index);

            codes.Insert(index, new CodeInstruction(OpCodes.Ldc_I4, 10));

            return codes.AsEnumerable();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Docks), nameof(Docks.docksAtBoatLimit))]
        public static void docksAtBoatLimit_Prefix(Docks __instance)
        {
            __instance.limit = 100;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Docks), nameof(Docks.buildBoatFromHere))]
        public static void buildBoatFromHere_Prefix(Docks __instance)
        {
            __instance.limit = 100;
        }
    }
}
