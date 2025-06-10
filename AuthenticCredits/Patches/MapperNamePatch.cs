using HarmonyLib;
using SiraUtil.Affinity;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Linq;

namespace AuthenticCredits.Patches
{
    [HarmonyPatch(typeof(SongCore.Patches.LevelListTableCellDataPatch), "Postfix")]
    public static class LevelListTableCellTranspiler
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var code = new List<CodeInstruction>(instructions);
            int codeLoc = -1;

            for (int i = 0; i < code.Count - 1; i++)
            {
                if (code[i].opcode == OpCodes.Ldstr && (string)code[i].operand == ", ") //Start of var authors =
                {
                    codeLoc = i;
                    break;
                }
            }
            if (codeLoc != -1)
            {
                code.RemoveRange(codeLoc, 9);
                code.Insert(codeLoc, new CodeInstruction(OpCodes.Ldarg_1));
                code.Insert(codeLoc + 1, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(LevelListTableCellTranspiler), "OverrideAuthor")));
                code.Insert(codeLoc + 2, new CodeInstruction(OpCodes.Stloc_0));
            }
            return code;
        }
        public static string OverrideAuthor(BeatmapLevel level)
        {
            if (Config.Instance.enabled)
            {
                if (Utilities.Credits.IDToMappers.TryGetValue(level.levelID, out var value))
                {
                    return ListString(value);
                }
            }
            return ListString(level.allMappers.Concat(level.allLighters));
        }

        public static string ListString(IEnumerable<string> elements)
        {
            return string.Join(", ", elements.Distinct());
        }
    }
}
