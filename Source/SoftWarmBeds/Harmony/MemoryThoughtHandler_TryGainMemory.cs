using HarmonyLib;
using RimWorld;
using Verse;

namespace SoftWarmBeds;

//Makes bed thoughts consider the bed stats when judging confortable temperature (new, more direct & mod-friendly approach)
[HarmonyPatch(typeof(MemoryThoughtHandler), nameof(MemoryThoughtHandler.TryGainMemory), typeof(ThoughtDef),
    typeof(Pawn), typeof(Precept))]
internal static class MemoryThoughtHandler_TryGainMemory
{
    public static bool Prefix(ThoughtDef def, Pawn ___pawn)
    {
        if (!def.IsMemory)
        {
            return true;
        }

        var bed = ___pawn.CurrentBed();
        if (bed == null)
        {
            return true;
        }

        if (def == ThoughtDefOf.SleptInCold)
        {
            var minTempInBed = ___pawn.def.GetStatValueAbstract(StatDefOf.ComfyTemperatureMin) -
                               bed.GetStatValue(BedStatDefOf.Bed_Insulation_Cold);
            return ___pawn.AmbientTemperature < minTempInBed;
        }

        if (def != ThoughtDefOf.SleptInHeat)
        {
            return true;
        }

        var maxTempInBed = ___pawn.def.GetStatValueAbstract(StatDefOf.ComfyTemperatureMax) +
                           bed.GetStatValue(BedStatDefOf.Bed_Insulation_Heat);
        return ___pawn.AmbientTemperature > maxTempInBed;
    }
}