using HarmonyLib;
using RimWorld;
using Verse;

namespace SoftWarmBeds;

//Instructions to deal with the used bedding on de-spawn
[HarmonyPatch(typeof(Building_Bed), nameof(Building_Bed.DeSpawn))]
public class Building_Bed_DeSpawn
{
    public static void Prefix(object __instance, DestroyMode mode)
    {
        if (mode == DestroyMode.WillReplace)
        {
            return;
        }

        if (__instance is not Building_Bed bed)
        {
            return;
        }

        var bedComp = bed.TryGetComp<CompMakeableBed>();
        if (bedComp is { loaded: true, NotTheBlanket: true })
        {
            bedComp.Unmake();
        }
    }
}