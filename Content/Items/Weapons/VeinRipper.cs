using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using RoryMod.Content.Projectiles;
using System.IO.Pipes;
using Microsoft.Xna.Framework;

namespace RoryMod.Content.Items.Weapons
{
	public class VeinRipper : ModItem
	{

		public override void SetDefaults()
		{
			// DefaultToStaff handles setting various Item values that magic staff weapons use.

			Item.DefaultToStaff(ModContent.ProjectileType<BloodyTear>(), 18, 34, 10);
			//Item.DefaultToStaff(ProjectileID.BloodShot, 16, 25, 8);
			Item.width = 42;
			Item.height = 42;
			Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
			Item.UseSound = SoundID.Item73;

			// A special method that sets the damage, knockback, and bonus critical strike chance.
			// This weapon has a crit of 17% which is added to the players default crit chance of 4%
			Item.SetWeaponValues(16, 5, 17);

			Item.SetShopValues(ItemRarityColor.Blue1, 3000);
		}

		public override void AddRecipes()
		{
			/// 8 Crimtane and a Mana Crystal
			Recipe recipe = Recipe.Create(ModContent.ItemType<VeinRipper>(), 1);
			recipe.AddIngredient(ItemID.CrimtaneBar, 8);
			recipe.AddIngredient(ItemID.ManaCrystal);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}

		public override Vector2? HoldoutOffset()
		{
			// Testing stuff

			return new Vector2(10f, 0f);
		}
	}
}
