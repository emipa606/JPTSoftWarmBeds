using HarmonyLib;
using RimWorld;
using Verse;

namespace SoftWarmBeds;

[HarmonyPatch(typeof(ThingMaker), nameof(ThingMaker.MakeThing))]
public static class ThingMaker_MakeThing
{
    public static bool Act;

    public static void Postfix(ThingDef def, ref Thing __result)
    {
        if (def.IsBed && Act)
        {
            var comp = __result.TryGetComp<CompMakeableBed>();
            if (comp != null)
            {
                var stuff = GenStuff.RandomStuffInexpensiveFor(comp.Props.beddingDef,
                    SymbolResolver_SingleThing_Resolve.cachedFaction);
                if (stuff != null)
                {
                    comp.LoadBedding(stuff);
                }
            }
        }

        Act = false;
    }
}