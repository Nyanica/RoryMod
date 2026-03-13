using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using RoryMod.Content.Projectiles;
using System.IO.Pipes;
using Microsoft.Xna.Framework;

namespace RoryMod.Content.Items.Weapons
{
	public class RedZapinator : ModItem
	{

		public override void SetDefaults() {

			// This should take most of the behaviors from the base Zapinators
			Item.CloneDefaults(ItemID.ZapinatorOrange);


			// 180 seems reasonable within progression
			// Comparing it to the sniper rifle's 200 predominantly
			// - TODO: fact-check this claim 
			Item.damage = 180;

			// Rarity roughly on-par with Golem-tier stuff
			Item.SetShopValues(ItemRarityColor.Yellow8, 200000);
		}

        public override void AddRecipes()
        {
            // holdover recipe until i learn how to add it to the Traveling Merchant shop
			Recipe recipe = Recipe.Create(ModContent.ItemType<RedZapinator>(), 1);
            recipe.AddIngredient(ItemID.LihzahrdBrick, 10);
            recipe.AddIngredient(ItemID.ZapinatorOrange);
			recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            if (player.spaceGun == true)
			{
				mult = 0f;
			}
        }

		public override Vector2? HoldoutOffset() {
			// Testing stuff
			
			return new Vector2(10f, 0f);
		}
    }
}
