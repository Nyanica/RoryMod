using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
namespace RoryMod.Content.Buffs

{
    public class BangleManaSickness : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            int manaSickIndex = player.FindBuffIndex(BuffID.ManaSickness);
            if (manaSickIndex != -1)
            {

                bool extraFrame = manaSickIndex < buffIndex;
                int timeToAdd = player.buffTime[manaSickIndex] - (extraFrame ? 1 : 0);
                player.buffTime[manaSickIndex] = 1;

                int newBuffTime = player.buffTime[buffIndex] + timeToAdd;
                if (newBuffTime > Player.manaSickTimeMax)
                {
                    newBuffTime = Player.manaSickTimeMax;
                }

                player.buffTime[buffIndex] = newBuffTime;
            }

        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] += time;
            if (player.buffTime[buffIndex] > Player.manaSickTimeMax)
            {
                player.buffTime[buffIndex] = Player.manaSickTimeMax;
            }

            return true;
        }
    }

}
