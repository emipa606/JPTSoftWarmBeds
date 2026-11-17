using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace SoftWarmBeds;

//Adjusts the info report on comfortable temperatures - final value
[HarmonyPatch(typeof(StatsReportUtility), "StatsToDraw", typeof(Thing))]
public static class StatsReportUtility_StatsToDraw
{
    public static IEnumerable<StatDrawEntry> Postfix(IEnumerable<StatDrawEntry> original, Thing thing)
    {
        foreach (var entry in original)
        {
            var pawn = thing as Pawn;
            var statDef = entry.stat;
            if (pawn == null || !pawn.InBed() ||
                statDef != StatDefOf.ComfyTemperatureMin && statDef != StatDefOf.ComfyTemperatureMax)
            {
                yield return entry;
                continue;
            }

            if (!statDef.Worker.IsDisabledFor(thing))
            {
                //sneak transform:
                var statValue = statDef.Worker.GetValueUnfinalized(StatRequest.For(thing));
                var subtract = statDef == StatDefOf.ComfyTemperatureMin;
                var modifier = subtract
                    ? BedStatDefOf.Bed_Insulation_Cold
                    : BedStatDefOf.Bed_Insulation_Heat;
                var bedStatValue = pawn.CurrentBed().GetStatValue(modifier);
                var bedOffset = subtract ? bedStatValue * -1 : bedStatValue;
                statValue += bedOffset;

                if (statDef.showOnDefaultValue || statValue != statDef.defaultBaseValue)
                {
                    yield return new StatDrawEntry(statDef.category, statDef, statValue, StatRequest.For(thing));
                }

                continue;
            }

            yield return new StatDrawEntry(statDef.category, statDef);
        }
    }
}