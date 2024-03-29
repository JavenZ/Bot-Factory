using UnityEngine;
using Verse;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotFactory.SettingsEnums;

namespace BotFactory
{
    public class BotFactory_Settings : ModSettings
    {
        // GENERAL SETTINGS
            // Settings for android gender
        public static bool androidsHaveGenders;
        public static bool androidsPickGenders;
        public static Gender androidsFixedGender;
        public static float androidsGenderRatio;

            // Settings for Permissions
        public static HashSet<string> thingsAllowedAsRepairStims = new HashSet<string> { };
        public static HashSet<string> blacklistedMechanicalHediffs = new HashSet<string> { };
        public static HashSet<string> blacklistedMechanicalTraits = new HashSet<string> { };
        public static bool bedRestrictionDefaultsToAll;

            // Settings for what is considered mechanical and massive
        public static bool isUsingCustomConsiderations;
        public static HashSet<string> isConsideredMechanicalAnimal;
        public static HashSet<string> isConsideredMechanicalAndroid;
        public static HashSet<string> isConsideredMechanicalDrone;
        public static HashSet<string> isConsideredMechanical;
        public static HashSet<string> hasSpecialStatus;

            // Settings for mechanical factions
        public static bool androidFactionsNeverFlee;

            // Settings for mechanical/organic rights
        public static bool factionsWillDeclareRightsWars;
        public static HashSet<string> antiMechanicalRightsFaction;
        public static HashSet<string> antiOrganicRightsFaction;
        public static bool dronesTriggerRightsWars;
        public static bool prisonersTriggerRightsWars;
        public static bool slavesTriggerRightsWars;
        public static bool surrogatesTriggerRightsWars;

        // Settings for what needs mechanical androids have
        public static bool androidsHaveJoyNeed;
        public static bool androidsHaveBeautyNeed;
        public static bool androidsHaveComfortNeed;
        public static bool androidsHaveOutdoorsNeed;

        // POWER SETTINGS
        public static int wattsConsumedPerBodySize;
        public static bool chargeCapableMeansDifferentBioEfficiency;
        public static float chargeCapableBioEfficiency;
        public static float batteryChargeRate;

        public static HashSet<string> canUseBattery;

        // SECURITY SETTINGS
            // Settings for Enemy hacks
        public static bool enemyHacksOccur;
        public static float chanceAlliesInterceptHack;
        public static float pointsGainedOnInterceptPercentage;
        public static float enemyHackAttackStrengthModifier;
        public static float percentageOfValueUsedForRansoms;

            // Settings for player hacks
        public static bool playerCanHack = true;
        public static bool receiveHackingAlert = true;
        public static float retaliationChanceOnFailure = 0.4f;
        public static float minHackSuccessChance = 0.05f;
        public static float maxHackSuccessChance = 0.95f;

        // HEALTH SETTINGS
            // Settings for Surgeries
        public static bool medicinesAreInterchangeable = false;
        public static bool showMechanicalSurgerySuccessChance = false;
        public static float maxChanceMechanicOperationSuccess = 1.0f;
        public static float chanceFailedOperationMinor = 0.75f;
        public static float chancePartSavedOnFailure = 0.75f;

            // Settings for Maintenance
        public static bool maintenanceNeedExists = true;
        public static bool receiveMaintenanceFailureLetters = true;
        public static float maintenancePartFailureRateFactor = 1.0f;
        public static float maintenanceFallRateFactor = 1.0f;
        public static float maintenanceGainRateFactor = 1.0f;

        // CONNECTIVITY SETTINGS
            // Settings for Surrogates
        public static bool surrogatesAllowed = true;
        public static bool otherFactionsAllowedSurrogates = true;
        public static int minGroupSizeForSurrogates = 5;
        public static float minSurrogatePercentagePerLegalGroup = 0.2f;
        public static float maxSurrogatePercentagePerLegalGroup = 0.7f;

        public static bool displaySurrogateControlIcon = true;
        public static int safeSurrogateConnectivityCountBeforePenalty = 1;

            // Settings for Skill Points
        public static bool receiveSkillAlert = true;
        public static int skillPointInsertionRate = 100;
        public static float skillPointConversionRate = 0.5f;
        public static int passionSoftCap = 8;
        public static float basePointsNeededForPassion = 5000f;

            // Settings for Cloud
        public static bool uploadingToSkyMindKills = true;
        public static bool uploadingToSkyMindPermaKills = true;
        public static int timeToCompleteSkyMindOperations = 24;
        public static HashSet<string> factionsUsingSkyMind = new HashSet<string> { "BF_AndroidUnion", "BF_MechanicalMarauders" };


        // STATS SETTINGS

        // INTERNAL SETTINGS
            // Settings page
        public OptionsTab activeTab = OptionsTab.General;
        public SettingsPreset ActivePreset = SettingsPreset.None;
        public bool settingsEverOpened = false;

        public void StartupChecks()
        {
            if (isConsideredMechanicalAndroid == null)
                isConsideredMechanicalAndroid = new HashSet<string>();
            if (isConsideredMechanicalDrone == null)
                isConsideredMechanicalDrone = new HashSet<string>();
            if (isConsideredMechanicalAnimal == null)
                isConsideredMechanicalAnimal = new HashSet<string>();
            if (isConsideredMechanical == null)
                isConsideredMechanical = new HashSet<string>();
            if (hasSpecialStatus == null)
                hasSpecialStatus = new HashSet<string>();
            if (antiMechanicalRightsFaction == null)
                antiMechanicalRightsFaction = new HashSet<string>();
            if (antiOrganicRightsFaction == null)
                antiOrganicRightsFaction = new HashSet<string>();
            if (canUseBattery == null)
                canUseBattery = new HashSet<string>();
            if (ActivePreset == SettingsPreset.None)
            {
                settingsEverOpened = false;
                ApplyPreset(SettingsPreset.Default);
            }
            if (!isUsingCustomConsiderations)
            {
                RebuildCaches();
            }
        }

        Vector2 scrollPosition = Vector2.zero;
        float cachedScrollHeight = 0;

        bool cachedExpandFirst = true;
        bool cachedExpandSecond = true;
        bool cachedExpandThird = true;
        bool cachedExpandFourth = false;

        void ResetCachedExpand() 
        { 
            cachedExpandFirst = true; 
            cachedExpandSecond = true; 
            cachedExpandThird = true;
            cachedExpandFourth = false;
        }

        internal void DoSettingsWindowContents(Rect inRect)
        {
            settingsEverOpened = true;
            bool hasChanged = false;
            void onChange() { hasChanged = true; }
            void onConsiderationChange()
            {
                onChange();
                isUsingCustomConsiderations = true;
            }

            Color colorSave = GUI.color;
            TextAnchor anchorSave = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleCenter;
            
            var headerRect = inRect.TopPartPixels(50);
            var restOfRect = new Rect(inRect);
            restOfRect.y += 50;
            restOfRect.height -= 50;

            Listing_Standard prelist = new Listing_Standard();
            prelist.Begin(headerRect);

            prelist.EnumSelector("BF_SettingsTabTitle".Translate(), ref activeTab, "BF_SettingsTabOption_", valueTooltipPostfix: null, onChange: ResetCachedExpand);
            prelist.GapLine();

            prelist.End();

            bool needToScroll = cachedScrollHeight > inRect.height;
            var viewRect = new Rect(restOfRect);
            if (needToScroll)
            {
                viewRect.width -= 20f;
                viewRect.height = cachedScrollHeight;
                Widgets.BeginScrollView(restOfRect, ref scrollPosition, viewRect);
            }

            Listing_Standard listingStandard = new Listing_Standard
            {
                maxOneColumn = true
            };
            listingStandard.Begin(viewRect);

            switch (activeTab)
            {
                case OptionsTab.General:
                {
                    // PRESET SETTINGS
                    if (listingStandard.ButtonText("BF_ApplyPreset".Translate()))
                    {
                        List<FloatMenuOption> options = new List<FloatMenuOption>();
                        foreach (SettingsPreset s in Enum.GetValues(typeof(SettingsPreset)))
                        {
                            if (s == SettingsPreset.None) // Can not apply the None preset.
                            {
                                continue;
                            }
                            options.Add(new FloatMenuOption(("BF_SettingsPreset" + s.ToString()).Translate(), () => ApplyPreset(s)));
                        }
                        Find.WindowStack.Add(new FloatMenu(options));
                    }
                    listingStandard.GapLine();
                        
                    // GENDER SETTINGS
                    listingStandard.CheckboxLabeled("BF_AndroidsHaveGenders".Translate(), ref androidsHaveGenders, tooltip:"BF_AndroidGenderNotice".Translate(), onChange: onChange);

                    if (androidsHaveGenders)
                    {
                        listingStandard.CheckboxLabeled("BF_AndroidsPickGenders".Translate(), ref androidsPickGenders, tooltip: "BF_AndroidGenderNotice".Translate(), onChange: onChange);
                    }

                    if (androidsHaveGenders && !androidsPickGenders)
                    { 
                        bool fixedGender = androidsFixedGender == Gender.Female;
                        listingStandard.CheckboxLabeled("BF_AndroidsFixedGenderSelector".Translate(), ref fixedGender, tooltip: "BF_AndroidGenderNotice".Translate(), onChange: onChange);
                        androidsFixedGender = fixedGender ? Gender.Female: Gender.Male;
                    }

                    if (androidsHaveGenders && androidsPickGenders)
                    { 
                        listingStandard.SliderLabeled("BF_AndroidsGenderRatio".Translate(), ref androidsGenderRatio, 0.0f, 1.0f, displayMult: 100, onChange: onChange);
                    }
                    listingStandard.GapLine();

                    // PERMISSION SETTINGS
                    listingStandard.CheckboxLabeled("BF_bedRestrictionDefaultsToAll".Translate(), ref bedRestrictionDefaultsToAll, tooltip: "BF_bedRestrictionDefaultsToAllDesc".Translate(), onChange: onChange);
                    listingStandard.GapLine();

                    // CONSIDERATION SETTINGS
                    listingStandard.Label("BF_RestartRequiredSectionDesc".Translate());
                    if (listingStandard.ButtonTextLabeled("BF_isUsingCustomConsiderations".Translate(isUsingCustomConsiderations.ToString()), "BF_resetCustomConsiderations".Translate(), tooltip: "BF_isUsingCustomConsiderationsDesc".Translate()))
                    {
                            RebuildCaches();
                            isUsingCustomConsiderations = false;
                    }

                    if (listingStandard.ButtonText("BF_ExpandMenu".Translate()))
                    {
                            cachedExpandFirst = !cachedExpandFirst;
                    }
                    if (cachedExpandFirst)
                        listingStandard.PawnSelector(FilteredGetters.FilterByIntelligence(FilteredGetters.GetValidPawns(), Intelligence.Humanlike), isConsideredMechanicalAndroid, "BF_SettingsConsideredAndroid".Translate(), "BF_SettingsNotConsideredAndroid".Translate(), onConsiderationChange);
                    
                    if (listingStandard.ButtonText("BF_ExpandMenu".Translate()))
                    {
                        cachedExpandSecond = !cachedExpandSecond;
                    }
                    if (cachedExpandSecond)
                        listingStandard.PawnSelector(FilteredGetters.FilterByIntelligence(FilteredGetters.GetValidPawns(), Intelligence.Humanlike), isConsideredMechanicalDrone, "BF_SettingsConsideredDrone".Translate(), "BF_SettingsNotConsideredDrone".Translate(), onConsiderationChange);
                    
                    if (listingStandard.ButtonText("BF_ExpandMenu".Translate()))
                    {
                        cachedExpandThird = !cachedExpandThird;
                    }
                    if (cachedExpandThird)
                        listingStandard.PawnSelector(FilteredGetters.FilterByIntelligence(FilteredGetters.GetValidPawns(), Intelligence.Animal), isConsideredMechanicalAnimal, "BF_SettingsConsideredAnimal".Translate(), "BF_SettingsNotConsideredAnimals".Translate(), onConsiderationChange);
                    
                    listingStandard.GapLine();

                    // ANDROID FACTION SETTINGS
                    listingStandard.CheckboxLabeled("BF_AndroidFactionsNeverFlee".Translate(), ref androidFactionsNeverFlee, onChange: onChange);
                    listingStandard.GapLine();

                    // RIGHTS SETTINGS

                    listingStandard.CheckboxLabeled("BF_factionsWillDeclareRightsWars".Translate(), ref factionsWillDeclareRightsWars, tooltip: "BF_factionsWillDeclareRightsWarsDesc".Translate(), onChange: onChange);
                    if (factionsWillDeclareRightsWars && listingStandard.ButtonText("BF_ExpandMenu".Translate()))
                    {
                        cachedExpandFourth = !cachedExpandFourth;
                    }
                    if (factionsWillDeclareRightsWars && cachedExpandFourth)
                    {
                        listingStandard.DefSelector(DefDatabase<FactionDef>.AllDefsListForReading, ref antiMechanicalRightsFaction, "BF_SettingsAntiMechanicalFaction".Translate(), "BF_SettingsTolerateMechanicalFaction".Translate(), onChange);
                        listingStandard.DefSelector(DefDatabase<FactionDef>.AllDefsListForReading, ref antiOrganicRightsFaction, "BF_SettingsAntiOrganicFaction".Translate(), "BF_SettingsTolerateOrganicFaction".Translate(), onChange);
                    }
                    if (factionsWillDeclareRightsWars)
                    {
                        listingStandard.CheckboxLabeled("BF_dronesTriggerRightsWars".Translate(), ref dronesTriggerRightsWars, onChange: onChange);
                        listingStandard.CheckboxLabeled("BF_prisonersTriggerRightsWars".Translate(), ref prisonersTriggerRightsWars, onChange: onChange);
                        listingStandard.CheckboxLabeled("BF_slavesTriggerRightsWars".Translate(), ref slavesTriggerRightsWars, onChange: onChange);
                        listingStandard.CheckboxLabeled("BF_surrogatesTriggerRightsWars".Translate(), ref surrogatesTriggerRightsWars, onChange: onChange);
                    }
                    listingStandard.GapLine();

                    // NEEDS SETTINGS
                    listingStandard.CheckboxLabeled("BF_AndroidsNeedJoy".Translate(), ref androidsHaveJoyNeed, tooltip: "BF_AndroidOnlyNotice".Translate(), onChange: onChange);
                    listingStandard.CheckboxLabeled("BF_AndroidsNeedBeauty".Translate(), ref androidsHaveBeautyNeed, tooltip: "BF_AndroidOnlyNotice".Translate(), onChange: onChange);
                    listingStandard.CheckboxLabeled("BF_AndroidsNeedComfort".Translate(), ref androidsHaveComfortNeed, tooltip: "BF_AndroidOnlyNotice".Translate(), onChange: onChange);
                    listingStandard.CheckboxLabeled("BF_AndroidsNeedOutdoors".Translate(), ref androidsHaveOutdoorsNeed, tooltip: "BF_AndroidOnlyNotice".Translate(), onChange: onChange);
                    break;
                }
                case OptionsTab.Power:
                {
                    listingStandard.SliderLabeled("BF_batteryPercentagePerTick".Translate(), ref batteryChargeRate, 0.1f, 4f, onChange: onChange);

                    listingStandard.GapLine();

                    listingStandard.CheckboxLabeled("BF_mechanicalsHaveDifferentBioprocessingEfficiency".Translate(), ref chargeCapableMeansDifferentBioEfficiency, onChange: onChange);
                    if (chargeCapableMeansDifferentBioEfficiency)
                    {
                        listingStandard.SliderLabeled("BF_mechanicalBioprocessingEfficiency".Translate(), ref chargeCapableBioEfficiency, 0.1f, 2.0f, displayMult: 100, valueSuffix: "%", onChange: onChange);
                    }
                    break;
                }
                case OptionsTab.Security:
                {
                    listingStandard.CheckboxLabeled("BF_EnemyHacksOccur".Translate(), ref enemyHacksOccur, onChange: onChange);
                    if (enemyHacksOccur)
                    {
                        listingStandard.SliderLabeled("BF_EnemyHackAttackStrengthModifier".Translate(), ref enemyHackAttackStrengthModifier, 0.01f, 5f, displayMult: 100, valueSuffix: "%", tooltip: "BF_EnemyHackAttackStrengthModifierDesc".Translate(), onChange: onChange);
                        listingStandard.SliderLabeled("BF_ChanceAlliesInterceptHack".Translate(), ref chanceAlliesInterceptHack, 0.01f, 1f, displayMult: 100, valueSuffix: "%", tooltip: "BF_ChanceAlliesInterceptHackDesc".Translate(), onChange: onChange);
                        listingStandard.SliderLabeled("BF_PointsGainedOnInterceptPercentage".Translate(), ref pointsGainedOnInterceptPercentage, 0.00f, 3f, displayMult: 100, valueSuffix: "%", tooltip: "BF_PointsGainedOnInterceptPercentageDesc".Translate(), onChange: onChange);
                        listingStandard.SliderLabeled("BF_PercentageOfValueUsedForRansoms".Translate(), ref percentageOfValueUsedForRansoms, 0.01f, 2f, displayMult: 100, valueSuffix:"%", onChange: onChange);
                    }



                    listingStandard.CheckboxLabeled("BF_PlayerCanHack".Translate(), ref playerCanHack, onChange: onChange);
                    if (playerCanHack)
                    {
                        listingStandard.CheckboxLabeled("BF_receiveFullHackingAlert".Translate(), ref receiveHackingAlert, onChange: onChange);
                        listingStandard.SliderLabeled("BF_RetaliationChanceOnFailure".Translate(), ref retaliationChanceOnFailure, 0.0f, 1f, displayMult: 100, valueSuffix: "%", onChange: onChange);
                        listingStandard.SliderLabeled("BF_MinHackSuccessChance".Translate(), ref minHackSuccessChance, 0.0f, maxHackSuccessChance, displayMult: 100, valueSuffix: "%", onChange: onChange);
                        listingStandard.SliderLabeled("BF_MaxHackSuccessChance".Translate(), ref maxHackSuccessChance, minHackSuccessChance, 1f, displayMult: 100, valueSuffix: "%", onChange: onChange);
                    }
                    break;
                }
                case OptionsTab.Health:
                {
                    // MEDICAL
                    listingStandard.CheckboxLabeled("BF_medicinesAreInterchangeable".Translate(), ref medicinesAreInterchangeable, onChange: onChange);
                    listingStandard.CheckboxLabeled("BF_showMechanicalSurgerySuccessChance".Translate(), ref showMechanicalSurgerySuccessChance, onChange: onChange);
                    listingStandard.SliderLabeled("BF_maxChanceMechanicOperationSuccess".Translate(), ref maxChanceMechanicOperationSuccess, 0.01f, 1f, displayMult: 100, valueSuffix: "%", onChange: onChange);
                    listingStandard.SliderLabeled("BF_chanceFailedOperationMinor".Translate(), ref chanceFailedOperationMinor, 0.01f, 1f, displayMult: 100, valueSuffix: "%", onChange: onChange);
                    listingStandard.SliderLabeled("BF_chancePartSavedOnFailure".Translate(), ref chancePartSavedOnFailure, 0.01f, 1f, displayMult: 100, valueSuffix: "%", onChange: onChange);
                    listingStandard.GapLine();

                    // MAINTENANCE
                    listingStandard.CheckboxLabeled("BF_maintenanceNeedExists".Translate(), ref maintenanceNeedExists, onChange: onChange);
                    if (maintenanceNeedExists)
                    {
                        listingStandard.CheckboxLabeled("BF_receiveMaintenanceFailureLetters".Translate(), ref receiveMaintenanceFailureLetters, onChange: onChange);
                        listingStandard.SliderLabeled("BF_maintenancePartFailureRateFactor".Translate(), ref maintenancePartFailureRateFactor, 0.5f, 5f, displayMult: 100, valueSuffix: "%", onChange: onChange);
                        listingStandard.SliderLabeled("BF_maintenanceFallRateFactor".Translate(), ref maintenanceFallRateFactor, 0.5f, 5f, displayMult: 100, valueSuffix: "%", onChange: onChange);
                        listingStandard.SliderLabeled("BF_maintenanceGainRateFactor".Translate(), ref maintenanceGainRateFactor, 0.5f, 5f, displayMult: 100, valueSuffix: "%", onChange: onChange);
                    }
                    listingStandard.GapLine();

                    // HEDIFFS
                    listingStandard.Label("BF_hediffBlacklistWarning".Translate());
                    if (listingStandard.ButtonText("BF_ExpandMenu".Translate()))
                    {
                        cachedExpandFirst = !cachedExpandFirst;
                    }
                    if (!cachedExpandFirst)
                    {
                        listingStandard.DefSelector(DefDatabase<HediffDef>.AllDefsListForReading, ref blacklistedMechanicalHediffs, "BF_settingsBlacklistedMechanicalHediffs".Translate(), "BF_settingsAllowedMechanicalHediffs".Translate(), onChange);
                    }
                    listingStandard.GapLine();

                    break;
                }
                case OptionsTab.Connectivity:
                {
                    // SURROGATES
                    listingStandard.CheckboxLabeled("BF_surrogatesAllowed".Translate(), ref surrogatesAllowed, onChange: onChange);
                    if (surrogatesAllowed)
                    {
                        listingStandard.CheckboxLabeled("BF_otherFactionsAllowedSurrogates".Translate(), ref otherFactionsAllowedSurrogates, onChange: onChange);
                        if (otherFactionsAllowedSurrogates)
                        {
                            string minGroupSizeForSurrogatesBuffer = minGroupSizeForSurrogates.ToString();
                            listingStandard.TextFieldNumericLabeled("BF_minGroupSizeForSurrogates".Translate(), ref minGroupSizeForSurrogates, ref minGroupSizeForSurrogatesBuffer, 1, 50);
                            listingStandard.SliderLabeled("BF_minSurrogatePercentagePerLegalGroup".Translate(), ref minSurrogatePercentagePerLegalGroup, 0.01f, 1f, displayMult: 100, valueSuffix: "%", onChange: onChange);
                            listingStandard.SliderLabeled("BF_maxSurrogatePercentagePerLegalGroup".Translate(), ref maxSurrogatePercentagePerLegalGroup, 0.01f, 1f, displayMult: 100, valueSuffix: "%", onChange: onChange);
                        }
                        listingStandard.CheckboxLabeled("BF_displaySurrogateControlIcon".Translate(), ref displaySurrogateControlIcon, onChange: onChange);
                        string safeSurrogateConnectivityCountBeforePenaltyBuffer = safeSurrogateConnectivityCountBeforePenalty.ToString();
                        listingStandard.TextFieldNumericLabeled("BF_safeSurrogateConnectivityCountBeforePenalty".Translate(), ref safeSurrogateConnectivityCountBeforePenalty, ref safeSurrogateConnectivityCountBeforePenaltyBuffer, 1, 40);
                    }
                    listingStandard.GapLine();

                    // SKILL POINTS
                    string skillPointInsertionRateBuffer = skillPointInsertionRate.ToString();
                    string skillPointConversionRateBuffer = skillPointConversionRate.ToString();
                    string passionSoftCapBuffer = passionSoftCap.ToString();
                    string basePointsNeededForPassionBuffer = basePointsNeededForPassion.ToString();
                    listingStandard.CheckboxLabeled("BF_receiveFullSkillAlert".Translate(), ref receiveSkillAlert, onChange: onChange);
                    listingStandard.TextFieldNumericLabeled("BF_skillPointInsertionRate".Translate(), ref skillPointInsertionRate, ref skillPointInsertionRateBuffer, 1f);
                    listingStandard.TextFieldNumericLabeled("BF_skillPointConversionRate".Translate(), ref skillPointConversionRate, ref skillPointConversionRateBuffer, 0.01f, 10);
                    listingStandard.TextFieldNumericLabeled("BF_passionSoftCap".Translate(), ref passionSoftCap, ref passionSoftCapBuffer, 0, 50);
                    listingStandard.TextFieldNumericLabeled("BF_basePointsNeededForPassion".Translate(), ref basePointsNeededForPassion, ref basePointsNeededForPassionBuffer, 10, 10000);
                    listingStandard.GapLine();

                    // CLOUD
                    listingStandard.CheckboxLabeled("BF_UploadingKills".Translate(), ref uploadingToSkyMindKills, onChange: onChange);
                    listingStandard.CheckboxLabeled("BF_UploadingPermakills".Translate(), ref uploadingToSkyMindPermaKills, onChange: onChange);
                    string SkyMindOperationTimeBuffer = timeToCompleteSkyMindOperations.ToString();
                    listingStandard.TextFieldNumericLabeled("BF_SkyMindOperationTimeRequired".Translate(), ref timeToCompleteSkyMindOperations, ref SkyMindOperationTimeBuffer, 1, 256);
                    listingStandard.GapLine();

                    break;
                }
                case OptionsTab.Stats:
                {
                    // Traits
                    listingStandard.Label("BF_traitBlacklistWarning".Translate());
                    if (listingStandard.ButtonText("BF_ExpandMenu".Translate()))
                    {
                        cachedExpandFirst = !cachedExpandFirst;
                    }
                    if (!cachedExpandFirst)
                    {
                        listingStandard.DefSelector(DefDatabase<TraitDef>.AllDefsListForReading, ref blacklistedMechanicalTraits, "BF_settingsBlacklistedMechanicalTraits".Translate(), "BF_settingsAllowedMechanicalTraits".Translate(), onChange);
                    }
                    break;
                }
                default:
                {
                    break;
                }
            }
            // Ending

            cachedScrollHeight = listingStandard.CurHeight;
            listingStandard.End();

            if (needToScroll)
            {
                Widgets.EndScrollView();
            }


            if (hasChanged)
                ApplyPreset(SettingsPreset.Custom);

            GUI.color = colorSave;
            Text.Anchor = anchorSave;
        }
        
        public void ApplyBaseSettings()
        {
            // Reset Gender Settings
            androidsHaveGenders = false;
            androidsPickGenders = false;
            androidsFixedGender = 0;
            androidsGenderRatio = 0.5f;

            // Permissions
            thingsAllowedAsRepairStims = new HashSet<string> { };
            blacklistedMechanicalHediffs = new HashSet<string> { };
            blacklistedMechanicalTraits = new HashSet<string> { };
            bedRestrictionDefaultsToAll = false;

            // Considerations
            isUsingCustomConsiderations = false;

            // Android Factions
            androidFactionsNeverFlee = false;

            // Rights
            factionsWillDeclareRightsWars = true;
            antiMechanicalRightsFaction = new HashSet<string> { "Empire" };
            antiOrganicRightsFaction = new HashSet<string> { "BF_MechanicalMarauders" };
            dronesTriggerRightsWars = true;
            prisonersTriggerRightsWars = false;
            slavesTriggerRightsWars = true;
            surrogatesTriggerRightsWars = true;

            // Needs Settings
            androidsHaveJoyNeed = true;
            androidsHaveBeautyNeed = true;
            androidsHaveComfortNeed = true;
            androidsHaveOutdoorsNeed = true;

            // POWER SETTINGS
            wattsConsumedPerBodySize = 500;
            chargeCapableMeansDifferentBioEfficiency = true;
            chargeCapableBioEfficiency = 0.5f;
            batteryChargeRate = 1f;

            // SECURITY SETTINGS
            enemyHacksOccur = true;
            chanceAlliesInterceptHack = 0.05f;
            pointsGainedOnInterceptPercentage = 0.25f;
            enemyHackAttackStrengthModifier = 1.0f;
            percentageOfValueUsedForRansoms = 0.25f;

            playerCanHack = true;
            receiveHackingAlert = true;
            retaliationChanceOnFailure = 0.4f;
            minHackSuccessChance = 0.05f;
            maxHackSuccessChance = 0.95f;

            // HEALTH SETTINGS
                // Medical
            medicinesAreInterchangeable = false;
            showMechanicalSurgerySuccessChance = false;
            maxChanceMechanicOperationSuccess = 1f;
            chanceFailedOperationMinor = 0.75f;
            chancePartSavedOnFailure = 0.75f;

            // Maintenance
            maintenanceNeedExists = true;
            receiveMaintenanceFailureLetters = true;
            maintenancePartFailureRateFactor = 1.0f;
            maintenanceFallRateFactor = 1.0f;
            maintenanceGainRateFactor = 1.0f;

            // CONNECTIVITY SETTINGS
                // Surrogates
            surrogatesAllowed = true;
            otherFactionsAllowedSurrogates = true;
            minGroupSizeForSurrogates = 5;
            minSurrogatePercentagePerLegalGroup = 0.2f;
            maxSurrogatePercentagePerLegalGroup = 0.7f;
            displaySurrogateControlIcon = true;
            safeSurrogateConnectivityCountBeforePenalty = 1;

            // Skills
            skillPointInsertionRate = 100;
            skillPointConversionRate = 0.5f;
            passionSoftCap = 8;
            basePointsNeededForPassion = 5000f;

            // Cloud
            receiveSkillAlert = true;
            uploadingToSkyMindKills = true;
            uploadingToSkyMindPermaKills = true;
            timeToCompleteSkyMindOperations = 24;

            RebuildCaches();
        }

        public void ApplyPreset(SettingsPreset preset)
        {
            if (preset == SettingsPreset.None)
                throw new InvalidOperationException("[ATR] Applying the None preset is illegal - were the mod options properly initialized?");

            ActivePreset = preset;
            if (preset == SettingsPreset.Custom) // Custom settings are inherently not a preset, so apply no new settings.
                return;

            ApplyBaseSettings();

            switch (preset)
            {
                case SettingsPreset.Default:
                    break;
                default:
                    throw new InvalidOperationException("Attempted to apply a nonexistent preset.");
            }

        }
        
        // Caches for ThingDefs must be rebuilt manually. Configuration uses the BF_MechTweaker by default and will capture all pawn thing defs with that modExtension.
        private void RebuildCaches()
        {
            IEnumerable<ThingDef> validPawns = FilteredGetters.GetValidPawns();

            HashSet<string> matchingAndroids = new HashSet<string>();
            HashSet<string> matchingDrones = new HashSet<string>();
            HashSet<string> matchingMechanicals = new HashSet<string>();
            HashSet<string> matchingSpecials = new HashSet<string>();
            HashSet<string> matchingChargers = new HashSet<string>();
            foreach (ThingDef validHumanlike in FilteredGetters.FilterByIntelligence(validPawns, Intelligence.Humanlike).Where(thingDef => thingDef.HasModExtension<BF_MechTweaker>()))
            {
                BF_MechTweaker modExt = validHumanlike.GetModExtension<BF_MechTweaker>();
                // Mechanical Androids are humanlikes with global learning factor >= 0.5 that have the ModExtension. Or are simply marked as canBeAndroid and not canBeDrone.
                if (modExt.canBeAndroid && (validHumanlike.statBases?.GetStatValueFromList(StatDefOf.GlobalLearningFactor, 0.5f) >= 0.5f || !modExt.canBeDrone))
                {
                    matchingAndroids.Add(validHumanlike.defName);
                    // A special bool in the mod extension marks this as a special android.
                    if (modExt.isSpecialMechanical)
                        matchingSpecials.Add(validHumanlike.defName);

                    // All mechanical humanlikes may charge inherently.
                    matchingChargers.Add(validHumanlike.defName);
                    matchingMechanicals.Add(validHumanlike.defName);
                }
                // Mechanical Drones are humanlikes with global learning factor < 0.5 that have the ModExtension. Or are simply marked as canBeDrone and not canBeAndroid.
                else if (modExt.canBeDrone && (validHumanlike.statBases?.GetStatValueFromList(StatDefOf.GlobalLearningFactor, 0.5f) < 0.5f || !modExt.canBeAndroid))
                {
                    matchingDrones.Add(validHumanlike.defName);
                    // All mechanical humanlikes may charge inherently.
                    matchingChargers.Add(validHumanlike.defName);
                    matchingMechanicals.Add(validHumanlike.defName);
                }
                else
                {
                    Log.Warning("[ATR] A humanlike race " + validHumanlike + " with the BF_MechTweaker mod extension was unable to automatically select its categorization! This will leave it as being considered organic.");
                }
            }
            // Mechanical animals are animals that have the ModExtension
            HashSet<ThingDef> validAnimals = FilteredGetters.FilterByIntelligence(validPawns, Intelligence.Animal).Where(thingDef => thingDef.HasModExtension<BF_MechTweaker>()).ToHashSet();
            HashSet<string> matchingAnimals = new HashSet<string>();

            // Mechanical animals of advanced intelligence may charge.
            foreach (ThingDef validAnimal in validAnimals)
            {
                matchingAnimals.Add(validAnimal.defName);
                matchingMechanicals.Add(validAnimal.defName);
                // Advanced mechanical animals may charge.
                if (validAnimal.race.trainability == TrainabilityDefOf.Advanced)
                    matchingChargers.Add(validAnimal.defName);
            }

            isConsideredMechanicalAndroid = matchingAndroids;
            isConsideredMechanicalDrone = matchingDrones;
            isConsideredMechanicalAnimal = matchingAnimals;
            isConsideredMechanical = matchingMechanicals;
            hasSpecialStatus = matchingSpecials;
            canUseBattery = matchingChargers;
        }

        public override void ExposeData()
        {
            base.ExposeData();

            /* == INTERNAL === */
            Scribe_Values.Look(ref ActivePreset, "BF_ActivePreset", SettingsPreset.None, true);

            /* === GENERAL === */
            
            // Gender
            Scribe_Values.Look(ref androidsHaveGenders, "BF_androidsHaveGenders", false);
            Scribe_Values.Look(ref androidsPickGenders, "BF_androidsPickGenders", false);
            Scribe_Values.Look(ref androidsFixedGender, "BF_androidsFixedGender", Gender.None);
            Scribe_Values.Look(ref androidsGenderRatio, "BF_androidsGenderRatio", 0.5f);

            // Permissions
            Scribe_Collections.Look(ref thingsAllowedAsRepairStims, "BF_thingsAllowedAsRepairStims", LookMode.Value);
            Scribe_Collections.Look(ref blacklistedMechanicalHediffs, "BF_blacklistedMechanicalHediffs", LookMode.Value);
            Scribe_Collections.Look(ref blacklistedMechanicalTraits, "BF_blacklistedMechanicalTraits", LookMode.Value);
            Scribe_Values.Look(ref bedRestrictionDefaultsToAll, "BF_bedRestrictionDefaultsToAll", false);

            // Considerations
            Scribe_Values.Look(ref isUsingCustomConsiderations, "BF_isUsingCustomConsiderations", true); // TODO: after a critical save-break update, set this to false for the future.
            try
            {
                Scribe_Collections.Look(ref isConsideredMechanicalAnimal, "BF_isConsideredMechanicalAnimal", LookMode.Value);
                Scribe_Collections.Look(ref isConsideredMechanicalAndroid, "BF_isConsideredMechanicalAndroid", LookMode.Value);
                Scribe_Collections.Look(ref isConsideredMechanicalDrone, "BF_isConsideredMechanicalDrone", LookMode.Value);
                Scribe_Collections.Look(ref isConsideredMechanical, "BF_isConsideredMechanical", LookMode.Value);
                Scribe_Collections.Look(ref hasSpecialStatus, "BF_hasSpecialStatus", LookMode.Value);
            }
            catch (Exception ex)
            {
                Log.Warning("[ATR] Mod settings failed to load appropriately! Resetting to default to avoid further issues. " + ex.Message + " " + ex.StackTrace);
                RebuildCaches();
            }

            // Android Factions
            Scribe_Values.Look(ref androidFactionsNeverFlee, "BF_androidFactionsNeverFlee", false);

            // Rights
            Scribe_Values.Look(ref factionsWillDeclareRightsWars, "BF_factionsWillDeclareRightsWars", true);
            Scribe_Collections.Look(ref antiMechanicalRightsFaction, "BF_antiMechanicalRightsFaction", LookMode.Value);
            Scribe_Collections.Look(ref antiOrganicRightsFaction, "BF_antiOrganicRightsFaction", LookMode.Value);
            Scribe_Values.Look(ref dronesTriggerRightsWars, "BF_dronesTriggerRightsWars", true);
            Scribe_Values.Look(ref prisonersTriggerRightsWars, "BF_prisonersTriggerRightsWars", false);
            Scribe_Values.Look(ref slavesTriggerRightsWars, "BF_slavesTriggerRightsWars", true);
            Scribe_Values.Look(ref surrogatesTriggerRightsWars, "BF_surrogatesTriggerRightsWars", true);

            // Needs
            Scribe_Values.Look(ref androidsHaveJoyNeed, "BF_androidsHaveJoyNeed", true);
            Scribe_Values.Look(ref androidsHaveBeautyNeed, "BF_androidsHaveBeautyNeed", true);
            Scribe_Values.Look(ref androidsHaveComfortNeed, "BF_androidsHaveComfortNeed", true);
            Scribe_Values.Look(ref androidsHaveOutdoorsNeed, "BF_androidsHaveOutdoorsNeed", true);

            /* === POWER === */

            Scribe_Values.Look(ref wattsConsumedPerBodySize, "BF_wattsConsumedPerBodySize", 500);
            Scribe_Values.Look(ref chargeCapableMeansDifferentBioEfficiency, "BF_chargeCapableMeansDifferentBioEfficiency", true);
            Scribe_Values.Look(ref chargeCapableBioEfficiency, "BF_chargeCapableBioEfficiency", 0.5f);
            Scribe_Values.Look(ref batteryChargeRate, "BF_batteryChargeRate", 1f);

            try
            {
                Scribe_Collections.Look(ref canUseBattery, "BF_canUseBattery", LookMode.Value);
            }
            catch (Exception ex)
            {
                Log.Warning("[ATR] Mod settings failed to load appropriately! Resetting to default to avoid further issues. " + ex.Message + " " + ex.StackTrace);
                ApplyPreset(SettingsPreset.Default);
            }

            /* === SECURITY === */

            // Hostile Hacks
            Scribe_Values.Look(ref enemyHacksOccur, "BF_enemyHacksOccur", true);
            Scribe_Values.Look(ref chanceAlliesInterceptHack, "BF_chanceAlliesInterceptHack", 0.05f);
            Scribe_Values.Look(ref pointsGainedOnInterceptPercentage, "BF_pointsGainedOnInterceptPercentage", 0.25f);
            Scribe_Values.Look(ref enemyHackAttackStrengthModifier, "BF_enemyHackAttackStrengthModifier", 1.0f);
            Scribe_Values.Look(ref percentageOfValueUsedForRansoms, "BF_percentageOfValueUsedForRansoms", 0.25f);

            // Player Hacks
            Scribe_Values.Look(ref playerCanHack, "BF_playerCanHack", true);
            Scribe_Values.Look(ref receiveHackingAlert, "BF_receiveHackingAlert", true);
            Scribe_Values.Look(ref retaliationChanceOnFailure, "BF_retaliationChanceOnFailure", 0.4f);
            Scribe_Values.Look(ref minHackSuccessChance, "BF_minHackSuccessChance", 0.05f);
            Scribe_Values.Look(ref maxHackSuccessChance, "BF_maxHackSuccessChance", 0.95f);

            /* === HEALTH === */
            // Medical
            Scribe_Values.Look(ref medicinesAreInterchangeable, "BF_medicinesAreInterchangeable", false);
            Scribe_Values.Look(ref showMechanicalSurgerySuccessChance, "BF_showMechanicalSurgerySuccessChance", false);
            Scribe_Values.Look(ref maxChanceMechanicOperationSuccess, "BF_maxChanceMechanicOperationSuccess", 1f);
            Scribe_Values.Look(ref chanceFailedOperationMinor, "BF_chanceFailedOperationMinor", 0.75f);
            Scribe_Values.Look(ref chancePartSavedOnFailure, "BF_chancePartSavedOnFailure", 0.75f);

            // Maintenance
            Scribe_Values.Look(ref maintenanceNeedExists, "BF_maintenanceNeedExists", true);
            Scribe_Values.Look(ref receiveMaintenanceFailureLetters, "BF_receiveMaintenanceFailureLetters", true);
            Scribe_Values.Look(ref maintenancePartFailureRateFactor, "BF_maintenancePartFailureRateFactor", 1.0f);
            Scribe_Values.Look(ref maintenanceFallRateFactor, "BF_maintenanceFallRateFactor", 1.0f);
            Scribe_Values.Look(ref maintenanceGainRateFactor, "BF_maintenanceGainRateFactor", 1.0f);

            /* === CONNECTIVITY === */
            // Surrogates
            Scribe_Values.Look(ref surrogatesAllowed, "BF_surrogatesAllowed", true);
            Scribe_Values.Look(ref otherFactionsAllowedSurrogates, "BF_otherFactionsAllowedSurrogates", true);
            Scribe_Values.Look(ref minGroupSizeForSurrogates, "BF_minGroupSizeForSurrogates", 5);
            Scribe_Values.Look(ref minSurrogatePercentagePerLegalGroup, "BF_minSurrogatePercentagePerLegalGroup", 0.2f);
            Scribe_Values.Look(ref maxSurrogatePercentagePerLegalGroup, "BF_maxSurrogatePercentagePerLegalGroup", 0.7f);
            Scribe_Values.Look(ref displaySurrogateControlIcon, "BF_displaySurrogateControlIcon", true);
            Scribe_Values.Look(ref safeSurrogateConnectivityCountBeforePenalty, "BF_safeSurrogateConnectivityCountBeforePenalty", 1);

            // Skills
            Scribe_Values.Look(ref receiveSkillAlert, "BF_receiveSkillAlert", true);
            Scribe_Values.Look(ref skillPointInsertionRate, "BF_skillPointInsertionRate", 100);
            Scribe_Values.Look(ref skillPointConversionRate, "BF_skillPointConversionRate", 0.5f);
            Scribe_Values.Look(ref passionSoftCap, "BF_passionSoftCap", 8);
            Scribe_Values.Look(ref basePointsNeededForPassion, "BF_basePointsNeededForPassion", 5000f);

            // Cloud
            Scribe_Values.Look(ref uploadingToSkyMindKills, "BF_uploadingToSkyMindKills", true);
            Scribe_Values.Look(ref uploadingToSkyMindPermaKills, "BF_uploadingToSkyMindPermaKills", true);
            Scribe_Values.Look(ref timeToCompleteSkyMindOperations, "BF_timeToCompleteSkyMindOperations", 24);
        }
    }

}