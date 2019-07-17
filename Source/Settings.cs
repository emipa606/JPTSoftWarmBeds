﻿using System;
using System.Linq;
using Verse;
using UnityEngine;


namespace SoftWarmBeds
{
    public enum ColorDisplayOption
    {
        Pillow = 0,
        Blanket = 1,
    }

    public class SoftWarmBedsSettings : ModSettings
    {
        public static ColorDisplayOption colorDisplayOption = ColorDisplayOption.Pillow;

        public static float colorWash = 0.4f;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref colorDisplayOption, "colorDisplayOption", ColorDisplayOption.Pillow);
            Scribe_Values.Look(ref colorWash, "colorWash", 0.4f);
            base.ExposeData();
        }

        public static void DoWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);
            if (listing.RadioButton("ColorDisplayOptionPillowLabel".Translate(), colorDisplayOption == ColorDisplayOption.Pillow, 0, "ColorDisplayOptionPillowTooltip".Translate()))
            {
                colorDisplayOption = ColorDisplayOption.Pillow;
            }
            if (listing.RadioButton("ColorDisplayOptionBlanketLabel".Translate(), colorDisplayOption == ColorDisplayOption.Blanket, 0, "ColorDisplayOptionBlanketTooltip".Translate()))
            {
                colorDisplayOption = ColorDisplayOption.Blanket;
            }
            listing.Gap(12f);
            int colorWashPercent = (int)(Mathf.Round(colorWash*10) * 10);
            if (colorWash < 0.05f)
            {
                listing.Label("ColorWashLevel".Translate() + ": " + colorWashPercent + "% (" + "ColorWashNone".Translate() + ")", -1f, null);
            }
            else if (colorWash > 0.35f && colorWash < 0.45f)
            {
                listing.Label("ColorWashLevel".Translate() + ": " + colorWashPercent + "% (" + "ColorWashNormal".Translate() + ")", -1f, null);
            }
            else if (colorWash > 0.95f)
            {
                listing.Label("ColorWashLevel".Translate() + ": " + colorWashPercent + "% (" + "ColorWashTotal".Translate() + ")", -1f, null);
            }
            else
            {
                listing.Label("ColorWashLevel".Translate() + ": " + colorWashPercent + "%", -1f, null);
            }
            colorWash = listing.Slider(colorWash, 0f, 1f);
            listing.Gap(12f);
            if (listing.ButtonText("Reset", null))
            {
                colorDisplayOption = ColorDisplayOption.Pillow;
                colorWash = 0.4f;
            }
            listing.End();
        }
    }

    public class SoftWarmBedsMod : Mod
    {
        SoftWarmBedsSettings settings;

        public SoftWarmBedsMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<SoftWarmBedsSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            SoftWarmBedsSettings.DoWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "SoftWarmBeds".Translate();
        }

        public override void WriteSettings()
        {
            base.WriteSettings();
            if (Current.Game != null && Current.ProgramState == ProgramState.Playing)
            {
                foreach (Building_SoftWarmBed bed in Find.Maps.SelectMany((Map map) => map.listerBuildings.AllBuildingsColonistOfClass<Building_SoftWarmBed>()))
                {
                    bed.Notify_ColorChanged();
                }
            }
        }
    }
}