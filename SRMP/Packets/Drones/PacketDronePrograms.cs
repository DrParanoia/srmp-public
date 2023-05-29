﻿using Lidgren.Network;
using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRMultiplayer.Packets
{
    [Packet(PacketType.DronePrograms)]
    public class PacketDronePrograms : Packet
    {
        public string ID;
        public DroneModel.ProgramData[] Programs { get; set; }

        public PacketDronePrograms() { }
        public PacketDronePrograms(NetIncomingMessage im) { Deserialize(im); }

        public override void Serialize(NetOutgoingMessage om)
        {
            base.Serialize(om);

            om.Write(Programs.Length);
            foreach(var program in Programs)
            {
                om.Write(program.target);
                om.Write(program.source);
                om.Write(program.destination);
            }
        }

        public override void Deserialize(NetIncomingMessage im)
        {
            base.Deserialize(im);

            Programs = new DroneModel.ProgramData[im.ReadInt32()];
            for(int i = 0; i < Programs.Length; i++)
            {
                Programs[i] = new DroneModel.ProgramData()
                {
                    target = im.ReadString(),
                    source = im.ReadString(),
                    destination = im.ReadString()
                };
            }
        }
    }
}
