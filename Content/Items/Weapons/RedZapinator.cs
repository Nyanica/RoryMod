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


			// 150 seems reasonable within progression
			// - TODO: fact-check this claim 
			Item.damage = 150;

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

		public override Vector2? HoldoutOffset() {
			// Testing stuff
			
			return new Vector2(10f, 0f);
		}
    }
}
