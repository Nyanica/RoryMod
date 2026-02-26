using RoryMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System.ComponentModel.DataAnnotations;
using System;

namespace RoryMod.Content.Projectiles
{
	public class SkyFractureProjectile : ModProjectile
	{
		public override void SetDefaults() {

			Projectile.CloneDefaults(ProjectileID.SkyFracture);

			AIType = ProjectileID.SkyFracture;

		}
	}
}