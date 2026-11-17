using System.Text;
using HarmonyLib;
using RimWorld;
using Verse;

namespace SoftWarmBeds;

//Adds info on used bedding material to the inspector pane
[HarmonyPatch(typeof(Building_Bed), nameof(Building_Bed.GetInspectString))]
public static class Building_Bed_GetInspectString
{
    public static void Postfix(object __instance, ref string __result)
    {
        if (__instance is not Building_Bed bed)
        {
            return;
        }

        var bedComp = bed.TryGetComp<CompMakeableBed>();
        if (bedComp == null)
        {
            return;
        }

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(bedComp.Loaded
            ? "BedMade".Translate(bedComp.blanketStuff.LabelCap, bedComp.blanketStuff)
            : "BedNotMade".Translate());

        __result += stringBuilder.ToString().TrimEndNewlines();
    }
}