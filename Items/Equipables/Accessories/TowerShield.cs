using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace Antiaris.Items.Equipables.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class TowerShield : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tower Shield");
            Tooltip.SetDefault("Has 33% chance to deflect enemy's projectiles\nProjectiles deal 50% less damage to player\nGrants immunity to knockback");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Башенный щит");
            Tooltip.AddTranslation(GameCulture.Russian, "Имеет 33% шанс отразить снаряды врагов\nСнаряды наносят на 50% меньше урона игроку\nДаёт иммунитет к отбрасыванию");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var aPlayer = player.GetModPlayer<AntiarisPlayer>(mod);
            aPlayer.towerShield = true;
            player.noKnockback = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EnchantedShard", 12);
            recipe.AddIngredient(ItemID.CobaltShield);
            recipe.SetResult(this);
            recipe.AddTile(16);
            recipe.AddRecipe();
        }
    }
}
