using BrandMonitorTestTask.Infrastructure;
using FluentMigrator;

namespace BrandMonitorTestTask.DataAccess.Migrations;

[Migration(1)]
public class InitMigration : ForwardOnlyMigration
{
    public override void Up()
    {
        Execute.Sql($@"CREATE TYPE record_status AS ENUM ('{string.Join("','", Enum.GetNames<RecordStatus>())}');");
        Create.Table(PgTables.Record)
            .WithColumn("id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("status").AsCustom("record_status")
            .WithColumn("updated_at").AsDateTime();
    }
}