using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Antiaris.Items.Weapons.Melee.Spears
{
	public class GooSpear : ModItem
	{
		public override void HoldItem(Player player) { AntiarisGlowMask2.AddGlowMask(mod.ItemType(GetType().Name), "Antiaris/Glow/" + GetType().Name + "_GlowMask"); }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI) { AntiarisUtils.DrawItemGlowMaskWorld(spriteBatch, item, mod.GetTexture("Glow/" + GetType().Name + "_GlowMask"), rotation, scale); }
		
		public override void SetDefaults()
		{
			item.damage = 10;
			item.useStyle = 5;
			item.useAnimation = 18;
			item.useTime = 29;
			item.shootSpeed = 4f;
			item.knockBack = 10f;
			item.width = 52;
			item.height = 52;
			item.scale = 1f;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("GooSpear");
			item.value = Item.sellPrice(0, 0, 14, 0);
			item.noMelee = true; 
			item.noUseGraphic = true;
			item.melee = true;
            item.autoReuse = false;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goo Spear");
            DisplayName.AddTranslation(GameCulture.Chinese, "凝胶矛");
            DisplayName.AddTranslation(GameCulture.Russian, "Копье из слизи");
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "LiquifiedGoo", 1);
            recipe.AddRecipeGroup("Antiaris:WoodenSpear");
            recipe.SetResult(this);
            recipe.AddTile(16);
            recipe.AddRecipe();
        }
	}
}
