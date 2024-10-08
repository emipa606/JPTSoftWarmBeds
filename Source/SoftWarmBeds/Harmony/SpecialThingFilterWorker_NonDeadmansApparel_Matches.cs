﻿using HarmonyLib;
using RimWorld;
using Verse;

namespace SoftWarmBeds;

//Preventing Beddings from being targeted by apparel recipes regardless of not being either tainted or clean. 1/2
//Solution by NanoCE
[HarmonyPatch(typeof(SpecialThingFilterWorker_NonDeadmansApparel),
    nameof(SpecialThingFilterWorker_NonDeadmansApparel.Matches))]
public static class SpecialThingFilterWorker_NonDeadmansApparel_Matches
{
    public static void Postfix(ref bool __result, ref Thing t)
    {
        if (t is Bedding)
        {
            __result = true;
        }
    }
}