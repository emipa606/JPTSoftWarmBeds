using System.Text;
using HarmonyLib;
using RimWorld;
using Verse;

namespace SoftWarmBeds;

//Adjusts the info report on comfortable temperatures - explanation part
[HarmonyPatch(typeof(StatPart_GearStatOffset), nameof(StatPart_GearStatOffset.ExplanationPart))]
public static class StatPart_GearStatOffset_ExplanationPart
{
    public static string Postfix(string original, StatRequest req, StatDef ___apparelStat)
    {
        if (!req.HasThing || req.Thing is not Pawn pawn || !pawn.InBed())
        {
            return original;
        }

        var alteredText = new StringBuilder();
        var subtract = ___apparelStat == StatDefOf.Insulation_Cold;
        var modifier = subtract ? BedStatDefOf.Bed_Insulation_Cold : BedStatDefOf.Bed_Insulation_Heat;
        var bedStatValue = pawn.CurrentBed().GetStatValue(modifier);
        var bedOffset = subtract ? bedStatValue * -1 : bedStatValue;
        var signal = subtract ? null : "+";
        alteredText.AppendLine("StatsReport_BedInsulation".Translate() + ": " + signal +
                               bedOffset.ToStringTemperature());
        return alteredText.ToString();
    }
}