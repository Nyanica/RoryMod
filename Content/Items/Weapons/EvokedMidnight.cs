using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using RoryMod.Content.Projectiles;

namespace RoryMod.Content.Items.Weapons
{
	public class EvokedMidnight : ModItem
	{

		public override void SetDefaults() {
			// DefaultToStaff handles setting various Item values that magic staff weapons use.
			// Hover over DefaultToStaff in Visual Studio to read the documentation!
			// Shoot a black bolt, also known as the projectile shot from the onyx blaster.
			Item.DefaultToStaff(ModContent.ProjectileType<DarkTear>(), 16, 25, 12);
			//Item.DefaultToStaff(ProjectileID.BloodShot, 16, 25, 12);
			Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
			Item.UseSound = SoundID.Item71;

			// A special method that sets the damage, knockback, and bonus critical strike chance.
			// This weapon has a crit of 32% which is added to the players default crit chance of 4%
			Item.SetWeaponValues(14, 5, 21);

			Item.SetShopValues(ItemRarityColor.Blue1, 10000);
		}

		public override void ModifyManaCost(Player player, ref float reduce, ref float mult) {
			// We can use ModifyManaCost to dynamically adjust the mana cost of this item, similar to how Space Gun works with the Meteor armor set.
			// See ExampleHood to see how accessories give the reduce mana cost effect.
			if (player.statLife < player.statLifeMax2 / 2) {
				mult *= 0.5f; // Half the mana cost when at low health. Make sure to use multiplication with the mult parameter.
			}
		}
	}
}
