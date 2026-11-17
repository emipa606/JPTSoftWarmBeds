using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace SoftWarmBeds;

public class WorkGiver_MakeBeds : WorkGiver_Scanner
{
    protected virtual JobDef JobStandard => SoftWarmBeds_JobDefOf.MakeBed;

    public override PathEndMode PathEndMode => PathEndMode.Touch;

    protected virtual bool CanMakeBedThing(Thing t)
    {
        return t is Building_Bed;
    }

    public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        return CanMakeBedThing(t) && BedMakingWorkGiverUtility.CanMakeBed(pawn, (Building_Bed)t, forced);
    }

    public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        return BedMakingWorkGiverUtility.BedMakingJob(pawn, (Building_Bed)t, forced, JobStandard);
    }

    public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
    {
        foreach (var bed in pawn.Map.listerBuildings.AllBuildingsColonistOfClass<Building_Bed>()
                     .Where(b => b.TryGetComp<CompMakeableBed>() != null))
        {
            yield return bed;
        }
    }
}