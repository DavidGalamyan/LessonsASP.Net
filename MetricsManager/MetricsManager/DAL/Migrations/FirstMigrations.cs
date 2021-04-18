using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Down()
        {
            Delete.Table("cpumetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("networkmetrics");
            Delete.Table("rammetrics");
            Delete.Table("agentid");
        }

        public override void Up()
        {
            Create.Table("agentid")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("AgentId").AsInt32()
                  .WithColumn("AgentUrl").AsInt32();
            Create.Table("cpumetrics")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("AgentId").AsInt32()
                  .WithColumn("Value").AsInt32()
                  .WithColumn("Time").AsInt64();
            Create.Table("dotnetmetrics")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("AgentId").AsInt32()
                  .WithColumn("Value").AsInt32()
                  .WithColumn("Time").AsInt64();
            Create.Table("hddmetrics")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("AgentId").AsInt32()
                  .WithColumn("Value").AsInt32()
                  .WithColumn("Time").AsInt64();
            Create.Table("networkmetrics")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("AgentId").AsInt32()
                  .WithColumn("Value").AsInt32()
                  .WithColumn("Time").AsInt64();
            Create.Table("rammetrics")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("AgentId").AsInt32()
                  .WithColumn("Value").AsInt32()
                  .WithColumn("Time").AsInt64();
        }
    }
}
