using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace SoftWarmBeds;

//Tweak to the bed's secondary color
[HarmonyPatch(typeof(Building_Bed), nameof(Building_Bed.DrawColorTwo), MethodType.Getter)] // Wait for a Harmony fix!
public static class Building_Bed_DrawColorTwo
{
    public static void Postfix(object __instance, ref Color __result)
    {
        if (__instance is not Building_Bed bed)
        {
            return;
        }

        var bedComp = bed.TryGetComp<CompMakeableBed>();
        if (bedComp == null && !bed.def.MadeFromStuff) // unmakeable non-stuffed beds aren't affected
        {
            return;
        }

        var forPrisoners = bed.ForPrisoners;
        var medical = bed.Medical;
        var invertedColorDisplay = SoftWarmBedsSettings.ColorDisplayOption == ColorDisplayOption.Blanket;
        if (forPrisoners || medical || invertedColorDisplay)
        {
            return;
        }

        if (bedComp is { loaded: true, blanketDef: null }) // bedding color for beds that are made
        {
            __result = bedComp.blanketStuff.stuffProps.color;
        }
        else if (bed.def.MadeFromStuff) // stuff color for umade beds & bedrolls
        {
            __result = bed.DrawColor;
        }
    }
}