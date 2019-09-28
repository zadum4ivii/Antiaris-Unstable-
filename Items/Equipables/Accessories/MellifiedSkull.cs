using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace Antiaris.Items.Equipables.Accessories
{
    public class MellifiedSkull : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 30;
            item.rare = 2;
            item.value = Item.buyPrice(0, 0, 65, 0);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mellified Skull");
            Tooltip.SetDefault("Getting hit increases life regeneration for 5 seconds");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Расплавленный череп");
            Tooltip.AddTranslation(GameCulture.Russian, "Получение урона повышает восстановление здоровья на 5 секунд");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var aPlayer = player.GetModPlayer<AntiarisPlayer>(mod);
            aPlayer.mellifiedSkull = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ObsidianSkull);
            recipe.AddIngredient(ItemID.BottledHoney, 12);
            recipe.AddIngredient(ItemID.HoneyBlock, 15);
            recipe.SetResult(this);
            recipe.AddTile(16);
            recipe.AddRecipe();
        }
    }
}
