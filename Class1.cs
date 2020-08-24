using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;

namespace TF47_Database_Tracking
{
    public partial class GameServerContext
    {
        public GameServerContext()
        {

        }

        public GameServerContext(DbContextOptions<GameServerContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

    public partial class GameServerContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Channel>();
            modelBuilder.HasPostgresEnum<VehicleType>();

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<PlayerInformation>(entity =>
            {
                entity.HasKey(e => e.PlayerId);
                entity.Property(e => e.Connections);
            });
        }

    }

    public enum Channel
    {
        Direct,
        Group,
        Side,
        Global,
        Command,
        Custom
    }

    public enum VehicleType
    {
        Foot,
        LightVehicle,
        Tank,
        Helicopter,
        Plane
    }

    public enum MissionType
    {
        COOP,
        CTI,
        TvT,
        PvP
    }

    public class Player
    {
        [Required]
        public uint Id { get; set; }
        [Required]
        public string PlayerUid { get; set; }
    }

    public class PlayerInformation 
    {
        public uint PlayerId { get; set; }
        [Required]
        public string NameFirstTimeSeen { get; set; }
        [Required]
        public string OriginalName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime FirstTimeSeen { get; set; }
        [Required]
        public DateTime LastTimeSeen { get; set; }
        [Required]
        public uint Connections { get; set; }
    }

    public class PlayerPlaytime
    {
        public uint PlayerId { get; set; }
        public uint TimeAsInfantry { get; set; }
        public uint TimeInVehicleSmall { get; set; }
        public uint TimeInVehicleTracked { get; set; }
        public uint TimeInVehicleHelicopter { get; set; }
        public uint TimeInVehiclePlane { get; set; }
        public uint TimeInObjective { get; set; }
        public uint TimeInBase { get; set; }

        public uint TotalPlayTime => TimeAsInfantry + TimeInVehicleSmall + TimeInVehicleTracked +
                                     TimeInVehicleHelicopter + TimeInVehiclePlane + TimeInObjective + TimeInBase;

        public uint TotalPlayTimeWithoutBase => TotalPlayTime - TimeInBase;
    }

    public class Campaign
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TimeStarted { get; set; }
    }

    public class Mission
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class Session
    {
        public uint Id { get; set; }
        public uint MissionId { get; set; }
        public uint CampaignId { get; set; }
        public DateTime TimeStarted { get; set; }
    }

    public class ChatLog
    {
        public uint Id { get; set; }
        public uint PlayerId { get; set; }
        public string Message { get; set; }
        public Channel Channel { get; set; }
        public string TimeSend { get; set; }
    }

    public class ActionType
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ActionLog
    {
        public uint Id { get; set; }
        public uint SessionId { get; set; }
        public uint ActionId { get; set; }
        public uint PlayerId { get; set; }
        public uint TargetId { get; set; }
        public string TargetVehicle { get; set; }
        public uint MissionTime { get; set; }
        public DateTime TimeTracked { get; set; }

    }

    public class PerformancePlayer
    {
        public uint Id { get; set; }
        public uint SessionId { get; set; }
        public uint PlayerId { get; set; }
        public uint ServerTime { get; set; }
        public uint Fps { get; set; }
        public uint SqfSpawned { get; set; }
        public uint SqfExecVm { get; set; }
        public uint SqfFsm { get; set; }
        public uint SqfCalled { get; set; }
        public uint ObjectCount { get; set; }
        public uint UnitCountLocal { get; set; }
    }

    public class PerformanceHeadless
    {
        public uint Id { get; set; }
        public string HeadlessName { get; set; }
        public uint SessionId { get; set; }
        public uint ServerTime { get; set; }
        public uint Fps { get; set; }
        public uint SqfSpawned { get; set; }
        public uint SqfExecVm { get; set; }
        public uint SqfFsm { get; set; }
        public uint SqfCalled { get; set; }
        public uint ObjectCount { get; set; }
        public uint UnitCountLocal { get; set; }
    }

    public class PerformanceServer
    {
        public uint Id { get; set; }
        public uint SessionId { get; set; }
        public uint ServerTime { get; set; }
        public uint Fps { get; set; }
        public uint SqfSpawned { get; set; }
        public uint SqfExecVm { get; set; }
        public uint SqfFsm { get; set; }
        public uint SqfCalled { get; set; }
        public uint ObjectCount { get; set; }
        public uint UnitCountTotal { get; set; }
        public uint UnitCountLocal { get; set; }
    }

    public class TicketLog
    {
        public uint Id { get; set; }
        public uint SessionId { get; set; }
        public uint TicketOld { get; set; }
        public uint TicketNow { get; set; }
        public int TicketChange { get; set; }
        public string Reason { get; set; }
        public uint MissionTime { get; set; }
        public DateTime TimeChanged { get; set; }
        public bool SessionFinished { get; set; }
    }

    public class Whitelist
    {
        public uint Id { get; set; }
        public string Description { get; set; }
    }

    public class PlayerWhitelisting
    {
        public uint Id { get; set; }
        public uint WhitelistId { get; set; }

        public Whitelist Whitelist { get; set; }
    }


    //TODO: verify 
    public class KillLog
    {
        public uint Id { get; set; }
        public uint KillerId { get; set; }
        public uint VictimId { get; set; }
        public string KillerVehicle { get; set; }
        public string KillerSide { get; set; }
        public string Weapon { get; set; }
        public string VictimSide { get; set; }
        public int Distance { get; set; }
        public DateTime TimeTracked { get; set; }
        public uint MissionTime { get; set; }

    }

    public class PositionTracking
    {
        public uint Id { get; set; }
        public uint SessionId { get; set; }
        public uint PlayerId { get; set; }
        public float[] Position { get; set; }
        public int Dir { get; set; }
        //public int Velocity { get; set; }
        public string VehicleType { get; set; }
        public string VehicleName { get; set; }
        public string Group { get; set; }
        public bool IsGroupLeader { get; set; }
        public uint MissionTime { get; set; }
    }

}
