using Microsoft.EntityFrameworkCore;
using TF47_Database.Model;
using TF47_Database.Model.Enums;

namespace TF47_Database
{
    public partial class Database : DbContext
    {
        public virtual DbSet<ActionLog> ActionLogs { get; set; }
        public virtual DbSet<ActionType> ActionTypes { get; set; }
        public virtual DbSet<Campaign> Campaigns { get; set; }
        public virtual DbSet<KillLog> KillLog { get; set; }
        public virtual DbSet<Mission> Missions { get; set; }
        public virtual DbSet<PerformanceHeadless> PerformanceHeadlessClients { get; set; }
        public virtual DbSet<PerformancePlayer> PerformancePlayers { get; set; }
        public virtual DbSet<PerformanceServer> PerformanceServers { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerInformation> PlayerInformation { get; set; }
        public virtual DbSet<PlayerPlaytime> PlayerPlaytimes { get; set; }
        public virtual DbSet<PlayerWhitelisting> PlayerWhitelistings { get; set; }
        public virtual DbSet<PositionTracking> PositionTrackings { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<TicketLog> TicketLog { get; set; }
        public virtual DbSet<Whitelist> Whitelists { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Channel>();
            modelBuilder.HasPostgresEnum<VehicleType>();
            modelBuilder.HasPostgresEnum<MissionType>();

            modelBuilder.UseSerialColumns();

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.PlayerUid).IsUnique();
                entity.Property(e => e.PlayerUid).HasMaxLength(255);
            });

            modelBuilder.Entity<PlayerInformation>(entity =>
            {
                entity.HasKey(e => e.PlayerId);

                entity.Property(e => e.NameFirstTimeSeen).IsRequired().HasMaxLength(120);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
                entity.Property(e => e.FirstTimeSeen).IsRequired().HasComputedColumnSql("NOW()");
                entity.Property(e => e.Connections).IsRequired().HasComputedColumnSql("0");
                entity.Property(e => e.LastTimeSeen).IsRequired().HasComputedColumnSql("NOW()");

                entity.HasOne(x => x.Player).WithOne(x => x.PlayerInformation)
                    .HasForeignKey<PlayerInformation>(x => x.PlayerId);
            });

            modelBuilder.Entity<PlayerPlaytime>(entity =>
            {
                entity.HasKey(e => e.PlayerId);

                entity.Property(e => e.TimeAsInfantry).IsRequired().HasComputedColumnSql("0");
                entity.Property(e => e.TimeInVehicleSmall).IsRequired().HasComputedColumnSql("0");
                entity.Property(e => e.TimeInVehicleTracked).IsRequired().HasComputedColumnSql("0");
                entity.Property(e => e.TimeInVehicleHelicopter).IsRequired().HasComputedColumnSql("0");
                entity.Property(e => e.TimeInVehiclePlane).IsRequired().HasComputedColumnSql("0");
                entity.Property(e => e.TimeInBase).IsRequired().HasComputedColumnSql("0");
                entity.Property(e => e.TimeInObjective).IsRequired().HasComputedColumnSql("0");

                entity.Ignore(e => e.TotalPlayTime);
                entity.Ignore(e => e.TotalPlayTimeWithoutBase);
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(5000);
                entity.Property(e => e.TimeStarted).IsRequired().HasDefaultValueSql("NOW()");
            });

            modelBuilder.Entity<Mission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
                entity.Property(e => e.Type).IsRequired().HasDefaultValue(MissionType.COOP);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(x => x.MissionId);

                entity.Property(e => e.TimeStarted).IsRequired().HasDefaultValueSql("NOW()");
                entity.Property(e => e.MissionId).IsRequired();
                entity.Property(e => e.CampaignId).IsRequired(false);

                entity.HasOne(x => x.Mission).WithMany(x => x.Sessions).HasForeignKey(x => x.MissionId);
                entity.HasOne(x => x.Campaign).WithMany(x => x.Sessions).HasForeignKey(x => x.CampaignId);
            });

            modelBuilder.Entity<ChatLog>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(x => x.PlayerId);
                entity.HasIndex(x => x.SessionId);

                entity.Property(e => e.Channel).IsRequired().HasDefaultValue(Channel.Side);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.TimeSend).IsRequired().HasDefaultValueSql("NOW()");

                entity.HasOne(e => e.Player).WithMany(x => x.Chats).HasForeignKey(x => x.PlayerId);
                entity.HasOne(e => e.Session).WithMany(x => x.Chats).HasForeignKey(x => x.SessionId);
            });

            modelBuilder.Entity<PerformancePlayer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Fps).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.ObjectCount).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.ServerTime).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.SqfCalled).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.SqfExecVm).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.SqfFsm).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.SqfSpawned).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.UnitCountLocal).IsRequired().HasDefaultValueSql("0");

                entity.HasOne(x => x.Session).WithMany(x => x.PerformancePlayers).HasForeignKey(x => x.SessionId);
                entity.HasOne(x => x.Player).WithMany(x => x.PlayerPerformance).HasForeignKey(x => x.PlayerId);
            });

            modelBuilder.Entity<PerformanceHeadless>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Fps).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.ObjectCount).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.ServerTime).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.SqfCalled).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.SqfExecVm).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.SqfFsm).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.SqfSpawned).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.UnitCountLocal).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.HeadlessName).IsRequired().HasMaxLength(100);

                entity.HasOne(x => x.Session).WithMany(x => x.PerformanceHeadless).HasForeignKey(x => x.SessionId);
            });

            modelBuilder.Entity<PerformanceServer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Fps).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.ObjectCount).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.ServerTime).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.SqfCalled).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.SqfExecVm).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.SqfFsm).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.SqfSpawned).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.UnitCountLocal).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.UnitCountTotal).IsRequired().HasDefaultValueSql("0");

                entity.HasOne(x => x.Session).WithMany(x => x.PerformanceServer).HasForeignKey(x => x.SessionId);
            });

            modelBuilder.Entity<TicketLog>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.SessionId);
                entity.HasIndex(e => e.PlayerId);

                entity.Property(e => e.MissionTime).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.Reason).IsRequired().HasMaxLength(512).HasDefaultValueSql("unknown reason");
                entity.Property(e => e.SessionFinished).IsRequired().HasDefaultValueSql("false");
                entity.Property(e => e.TicketChange).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.TicketNew).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.TicketOld).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.TimeChanged).IsRequired().HasDefaultValueSql("NOW()");
                entity.Property(e => e.PlayerId).IsRequired(false).HasDefaultValueSql("NULL");

                entity.HasOne(e => e.Session).WithMany(x => x.TicketLogEntries).HasForeignKey(x => x.SessionId);
                entity.HasOne(x => x.Player).WithMany(x => x.TicketLog).HasForeignKey(x => x.PlayerId);
            });

            modelBuilder.Entity<PositionTracking>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.SessionId);
                entity.HasIndex(e => e.PlayerId);

                entity.Property(e => e.MissionTime).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.Group).IsRequired().HasMaxLength(255).HasDefaultValueSql("unknown group");
                entity.Property(e => e.IsGroupLeader).IsRequired().HasDefaultValueSql("false");
                entity.Property(e => e.Position).IsRequired().HasDefaultValue(new float[] {0, 0, 0});
                entity.Property(e => e.Dir).IsRequired().HasDefaultValueSql("0");
                entity.Property(e => e.VehicleType).IsRequired().HasDefaultValue(VehicleType.Foot);
                entity.Property(e => e.VehicleName).IsRequired(false).HasDefaultValueSql("NULL");

                entity.HasOne(x => x.Session).WithMany(x => x.PositionTrackingPoints).HasForeignKey(x => x.SessionId);
            });

            modelBuilder.Entity<Whitelist>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Description).IsRequired().HasDefaultValueSql("unknown permission");
            });
            
            modelBuilder.Entity<PlayerWhitelisting>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(x => x.WhitelistId);

                entity.HasOne(x => x.Whitelist).WithMany(x => x.Whitelisting).HasForeignKey(x => x.WhitelistId);
            });
        }

    }
}