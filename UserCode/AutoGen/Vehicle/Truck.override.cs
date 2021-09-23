﻿// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// <auto-generated from VehicleTemplate.cs />

namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Components.VehicleModules;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    
    [Serialized]
    [LocDisplayName("Truck")]
    [Weight(25000)]
    [AirPollution(0.5f)]
    [Ecopedia("Crafted Objects", "Vehicles", createAsSubPage: true, display: InPageTooltip.DynamicTooltip)]
    public partial class TruckItem : WorldObjectItem<TruckObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Modern truck for hauling sizable loads."); } }
    }


    [RequiresSkill(typeof(IndustrySkill), 2)]
    public partial class TruckRecipe : RecipeFamily
    {
        public TruckRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                "Truck",  //noloc
                Localizer.DoStr("Truck"),
                new List<IngredientElement>
                {
                    new IngredientElement(typeof(GearboxItem), 4, typeof(IndustrySkill)),
                    new IngredientElement(typeof(SteelPlateItem), 20, typeof(IndustrySkill)),
                    new IngredientElement(typeof(NylonFabricItem), 20, typeof(IndustrySkill)),
                    new IngredientElement(typeof(CombustionEngineItem), 1, true),
                    new IngredientElement(typeof(RubberWheelItem), 6, true),
                    new IngredientElement(typeof(RadiatorItem), 1, true),
                    new IngredientElement(typeof(SteelAxleItem), 1, true),
                },
                new List<CraftingElement>
                {
                    new CraftingElement<TruckItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 18;
            this.LaborInCalories = CreateLaborInCaloriesValue(2000, typeof(IndustrySkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(TruckRecipe), 10, typeof(IndustrySkill));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Truck"), typeof(TruckRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(FuelConsumptionComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(AirPollutionComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]
    public partial class TruckObject : PhysicsWorldObject, IRepresentsItem
    {
        static TruckObject()
        {
            WorldObject.AddOccupancy<TruckObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Truck"); } }
        public Type RepresentedItemType { get { return typeof(TruckItem); } }

        private static string[] fuelTagList = new string[]
        {
            "Liquid Fuel",
        };

        private TruckObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(72, 8000000);
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTagList);
            this.GetComponent<FuelConsumptionComponent>().Initialize(25);
            this.GetComponent<AirPollutionComponent>().Initialize(0.5f);
            this.GetComponent<VehicleComponent>().Initialize(20, 2, 2);
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2,2,3));
        }
    }
}
