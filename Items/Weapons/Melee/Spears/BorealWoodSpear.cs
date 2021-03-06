﻿using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Weapons.Melee.Spears
{
	public class BorealWoodSpear : ModItem
	{
	    public override void SetDefaults()
		{
			item.damage = 7;
			item.useStyle = 5;
			item.useAnimation = 35;
			item.useTime = 35;
			item.shootSpeed = 2f;
			item.knockBack = 10f;
			item.width = 52;
			item.height = 52;
			item.scale = 1f;
			item.rare = 0;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("BorealWoodSpear");
			item.value = Item.sellPrice(0, 0, 0, 21);
			item.noMelee = true; 
			item.noUseGraphic = true;
			item.melee = true;
			item.autoReuse = false;
		}

	    public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boreal Wood Spear");
            DisplayName.AddTranslation(GameCulture.Chinese, "北地木矛");
            DisplayName.AddTranslation(GameCulture.Russian, "Копье из северной древесины");
		}

	    public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BorealWood, 9);
            recipe.SetResult(this);
            recipe.AddTile(18);
            recipe.AddRecipe();
        }
	}
}
