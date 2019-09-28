using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Antiaris.Items.Equipables.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class HoneyBadger : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 4;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.accessory = true;
            item.defense = 3;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honey Badger");
            Tooltip.SetDefault("Getting hit covers the player in a thick layer of honey for 5 seconds\nThe honey layer increases health regeneration and reduces damage taken by 30%\nGetting hit destroys the layer and makes it go on cooldown\nGrants immunity to knockback");
            DisplayName.AddTranslation(GameCulture.Chinese, "");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
            DisplayName.AddTranslation(GameCulture.Russian, "Медоед");
            Tooltip.AddTranslation(GameCulture.Russian, "Имеет шанс защитить игрока медовым слоем на 5 секунд при получении урона\nМедовый слой повышает восстановление здоровья и снижает получаемый урон\nПолучение урона уничтожает слой\nДаёт иммунитет к отбрасыванию");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var aPlayer = player.GetModPlayer<AntiarisPlayer>(mod);
            aPlayer.honeyBadger = true;
            player.noKnockback = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CobaltShield);
            recipe.AddIngredient(null, "MellifiedSkull");
            recipe.AddIngredient(null, "HoneyExtract", 14);
            recipe.AddIngredient(ItemID.HoneyBlock, 10);
            recipe.SetResult(this);
            recipe.AddTile(114);
            recipe.AddRecipe();
        }
    }
}