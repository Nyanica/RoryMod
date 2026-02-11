using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using RoryMod.Content.Projectiles;
using System.IO.Pipes;
using System.Numerics;

namespace RoryMod.Content.Items.Weapons
{
	public class EvokedMidnight : ModItem
	{

		public override void SetDefaults() {
			// DefaultToStaff handles setting various Item values that magic staff weapons use.
			// Hover over DefaultToStaff in Visual Studio to read the documentation!
			// Shoot a black bolt, also known as the projectile shot from the onyx blaster.
			Item.DefaultToStaff(ModContent.ProjectileType<DarkTear>(), 6, 25, 8);
			Item.width = 42;
			Item.height = 42;
			//Item.DefaultToStaff(ProjectileID.BloodShot, 16, 25, 12);
			Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
			Item.UseSound = SoundID.Item73;

			// A special method that sets the damage, knockback, and bonus critical strike chance.
			// This weapon has a crit of 32% which is added to the players default crit chance of 4%
			Item.SetWeaponValues(13, 5, 17);

			Item.SetShopValues(ItemRarityColor.Blue1, 3000);
		}

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<EvokedMidnight>(), 1);
            recipe.AddIngredient(ItemID.DemoniteBar, 8);
            recipe.AddIngredient(ItemID.ManaCrystal);
			recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}
