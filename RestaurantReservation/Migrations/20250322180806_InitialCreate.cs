using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACCOUNT_ROLE_TYPE",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(name: "ROLE", type: "nvarchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCONT_ROLE_TYPES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NOTIFICATION_EVENTS",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Event = table.Column<string>(name: "EVENT", type: "nvarchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICATION_EVENTS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RESTAURANT_ATMOSPHERE",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Atmosphere = table.Column<string>(name: "ATMOSPHERE", type: "nvarchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESTAURANT_ATMOSPHERE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RESTAURANT_LOCATIONS",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(name: "LOCATION", type: "nvarchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESTAURANT_LOCATIONS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RESTAURANT_SPECIAL_CONDITIONS",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<byte[]>(name: "STATUS", type: "varbinary", nullable: false),
                    SpecialCondition = table.Column<string>(name: "SPECIAL_CONDITION", type: "nvarchar(65)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESTAURANT_SPECIAL_CONDITIONS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RESTAURANT_TABLE_TYPES",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableType = table.Column<string>(name: "TABLE_TYPE", type: "nvarchar(32)", nullable: false),
                    Seats = table.Column<short>(name: "SEATS", type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESTAURANT_TABLE_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ACCOUNTS",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<byte[]>(name: "STATUS", type: "varbinary", nullable: false),
                    RoleId = table.Column<short>(name: "ROLE_ID", type: "smallint", nullable: false),
                    Username = table.Column<string>(name: "USERNAME", type: "nvarchar(32)", nullable: false),
                    Password = table.Column<string>(name: "PASSWORD", type: "nvarchar(255)", nullable: false),
                    Email = table.Column<string>(name: "EMAIL", type: "nvarchar(32)", nullable: false),
                    Phone = table.Column<string>(name: "PHONE", type: "nvarchar(16)", nullable: false),
                    AccessFailCount = table.Column<short>(name: "ACCESS_FAIL_COUNT", type: "smallint", nullable: false),
                    CurrentAccessFailCount = table.Column<short>(name: "CURRENT_ACCESS_FAIL_COUNT", type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "CREATED_AT", type: "datetime", nullable: false),
                    LastChangeAt = table.Column<DateTime>(name: "LAST_CHANGE_AT", type: "datetime", nullable: false),
                    BlockedAt = table.Column<DateTime>(name: "BLOCKED_AT", type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ACCOUNTS_ACCONT_ROLE_TYPES_ROLE_ID",
                        column: x => x.RoleId,
                        principalTable: "ACCOUNT_ROLE_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CLIENTS",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<short>(name: "ACCOUNT_ID", type: "smallint", nullable: false),
                    FirstName = table.Column<string>(name: "FIRST_NAME", type: "nvarchar(32)", nullable: false),
                    LastName = table.Column<string>(name: "LAST_NAME", type: "nvarchar(32)", nullable: false),
                    Points = table.Column<int>(name: "POINTS", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CLIENTS_ACCOUNTS_ACCONUT_ID",
                        column: x => x.AccountId,
                        principalTable: "ACCOUNTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESTAURANTS",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<short>(name: "ACCOUNT_ID", type: "smallint", nullable: false),
                    CompanyName = table.Column<string>(name: "COMPANY_NAME", type: "nvarchar(64)", nullable: false),
                    Description = table.Column<string>(name: "DESCRIPTION", type: "nvarchar(510)", nullable: false),
                    Address = table.Column<string>(name: "ADDRESS", type: "nvarchar(64)", nullable: false),
                    Bulstat = table.Column<long>(name: "BULSTAT", type: "bigint", nullable: false),
                    AtmosphereId = table.Column<short>(name: "ATMOSPHERE_ID", type: "smallint", nullable: false),
                    Rating = table.Column<float>(name: "RATING", type: "float", nullable: false),
                    KeepResTableTime = table.Column<float>(name: "KEEP_RES_TABLE_TIME", type: "float", nullable: false),
                    DefaultMaxResDuration = table.Column<float>(name: "DEFAULT_MAX_RES_DURATION", type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESTAURANTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RESTAURANTS_ACCOUNTS_ACCOUNT_ID",
                        column: x => x.AccountId,
                        principalTable: "ACCOUNTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RESTAURANTS_RESTAURANT_ATMOSPHERE_ATMOSPKERE_ID",
                        column: x => x.AtmosphereId,
                        principalTable: "RESTAURANT_ATMOSPHERE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NOTIFICATION_DEFAULT_SETTINGS",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<short>(name: "RESTAURANT_ID", type: "smallint", nullable: true),
                    EventId = table.Column<short>(name: "EVENT_ID",type: "smallint", nullable: false),
                    Title = table.Column<string>(name: "TITLE", type: "nvarchar(64)", nullable: false),
                    Description = table.Column<string>(name: "DESCRIPTION", type: "nvarchar(1024)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICATION_DEFAULT_SETTINGS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NOTIFICATION_DEFAULT_SETTINGS_NOTIFICATION_EVENTS_EVENT_ID",
                        column: x => x.EventId,
                        principalTable: "NOTIFICATION_EVENTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NOTIFICATION_DEFAULT_SETTINGS_RESTAURANTS_RESTAURANT_ID",
                        column: x => x.RestaurantId,
                        principalTable: "RESTAURANTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESERVATIONS",
                columns: table => new
                {
                    Id = table.Column<short>(name:"ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<byte[]>(name: "STATUS", type: "varbinary", nullable: false),
                    RestaurantsId = table.Column<short>(name: "RESTAURANT_ID", type: "smallint", nullable: false),
                    ClientId = table.Column<short>(name: "CLIENT_ID", type: "smallint", nullable: false),
                    RegDate = table.Column<DateTime>(name: "REG_DATE", type: "datetime", nullable: false),
                    ReservationDate = table.Column<DateTime>(name: "RESERVATION_DATE", type: "datetime", nullable: false),
                    StatusChangeDate = table.Column<DateTime>(name: "STATUS_CHANGE_DATE", type: "datetime", nullable: false),
                    LocationId = table.Column<short>(name: "LOCATION_ID", type: "smallint", nullable: false),
                    TableTypeId = table.Column<short>(name: "TABLE_TYPE_ID", type: "smallint", nullable: false),
                    PeopleNumber = table.Column<int>(name: "NUMBER_PEOPLE", type: "int", nullable: false),
                    Duration = table.Column<float>(name: "DURATION", type: "float", nullable: false),
                    PointsUsed = table.Column<short>(name: "POINTS_USED", type: "smallint", nullable: false),
                    ResarvationName = table.Column<string>(name: "NAME_RESERVATION", type: "nvarchar(32)", nullable: false),
                    Note = table.Column<string>(name: "NOTE", type: "nvarchar(510)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESERVATIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RESERVATIONS_CLIENTS_CLIENT_ID",
                        column: x => x.ClientId,
                        principalTable: "CLIENTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RESERVATIONS_RESTAURANT_LOCATIONS_LOCATION_ID",
                        column: x => x.LocationId,
                        principalTable: "RESTAURANT_LOCATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RESERVATIONS_RESTAURANT_TABLE_TYPE_TABLE_TYPE_ID",
                        column: x => x.TableTypeId,
                        principalTable: "RESTAURANT_TABLE_TYPES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RESERVATIONS_RESTAURANTS_RESTAURANTS_ID",
                        column: x => x.RestaurantsId,
                        principalTable: "RESTAURANTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RESTAURANT_CAPACITY_SCHEME",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<short>(name: "RESTAURANT_ID", type: "smallint", nullable: false),
                    LocationId = table.Column<short>(name: "LOCATION_ID", type: "smallint", nullable: false),
                    TableTypeId = table.Column<short>(name: "TABLE_TYPE_ID", type: "smallint", nullable: false),
                    TableCount = table.Column<int>(name: "TABLE_COUNT", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESTAURANT_CAPACITY_SCHEME", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RESTAURANT_CAPACITY_SCHEME_RESTAURANT_LOCATIONS_LOCATION_ID",
                        column: x => x.LocationId,
                        principalTable: "RESTAURANT_LOCATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RESTAURANT_CAPACITY_SCHEME_RESTAURANT_TABLE_TYPE_TABLE_ID",
                        column: x => x.TableTypeId,
                        principalTable: "RESTAURANT_TABLE_TYPES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RESTAURANT_CAPACITY_SCHEME_RESTAURANTS_RESTAURANT_ID",
                        column: x => x.RestaurantId,
                        principalTable: "RESTAURANTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESTAURANT_IMAGES",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<short>(name: "RESTAURANT_ID", type: "smallint", nullable: false),
                    Image = table.Column<string>(name: "IMAGE", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESTAURANT_IMAGES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RESTAURANT_IMAGES_RESTAURANTS_RESTAURANT_ID",
                        column: x => x.RestaurantId,
                        principalTable: "RESTAURANTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESTAURANT_SCHEDULE_SETTINGS",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<short>(name: "RESTAURANT_ID", type: "smallint", nullable: true),
                    SpecificDate = table.Column<DateTime>(name: "SPECIFIC_DATE", type: "datetime", nullable: false),
                    WeekDay = table.Column<int>(name: "WEEK_DAY", type: "int", nullable: false),
                    HourFrom = table.Column<short>(name: "HOUR_FROM", type: "smallint", nullable: false),
                    HourTo = table.Column<short>(name: "HOUR_TO", type: "smallint", nullable: false),
                    RestaurantSpecialConditionId = table.Column<short>(name: "SPECIAL_CONDITION_ID", type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESTAURANTScheduleSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RESTAURANT_ScheduleSettings_RESTAURANT_SPECIAL_CONDITIONS_RESTAURANT_SPECIAL_CONDITION_ID",
                        column: x => x.RestaurantSpecialConditionId,
                        principalTable: "RESTAURANT_SPECIAL_CONDITIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RESTAURANT_SCHEDULE_SETTINGS_RESTAURANTS_RESTAURANT_ID",
                        column: x => x.RestaurantId,
                        principalTable: "RESTAURANTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "COMMENTS",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<short>(name: "RESERVATION_ID", type: "smallint", nullable: false),
                    Rate = table.Column<float>(name: "RATE", type: "real", nullable: false),
                    Comment = table.Column<string>(name: "COMMENT", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMMENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COMMENTS_RESERVATIONS_RESERVATIONS_ID",
                        column: x => x.ReservationId,
                        principalTable: "RESERVATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESERVATION_REQUEST_QUEUE",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<short>(name: "RESERVATION_ID", type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESERVATION_REQUEST_QUEUE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RESERVATION_REQUEST_QUEUE_RESERVATIONS_RESERVATIONS_ID",
                        column: x => x.ReservationId,
                        principalTable: "RESERVATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESTAURANT_MONTHLY_SCHEDULE",
                columns: table => new
                {
                    Id = table.Column<short>(name: "ID", type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<DateTime>(name: "DAY", type: "datetime2", nullable: false),
                    SchemeId = table.Column<short>(name: "SCHEME_ID", type: "smallint", nullable: false),
                    FreeTables = table.Column<int>(name: "FREE_TABLES", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESTAURANT_MONTHLY_SCHEDULE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RESTAURANT_MONTHLY_SCHEDULE_RESTAURANT_CAPACITY_SCHEME_SCHEME_ID",
                        column: x => x.SchemeId,
                        principalTable: "RESTAURANT_CAPACITY_SCHEME",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            /// <summary> индекси за FK </summary>
            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNTS_RoleId",
                table: "ACCOUNTS",
                column: "ROLE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTS_AccountId",
                table: "CLIENTS",
                column: "ACCOUNT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTS_RESERVATIONS_ID",
                table: "COMMENTS",
                column: "RESERVATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATION_DEFAULT_SETTINGS_EVENT_ID",
                table: "NOTIFICATION_DEFAULT_SETTINGS",
                column: "EVENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATION_DEFAULT_SETTINGS_RESTAURANT_ID",
                table: "NOTIFICATION_DEFAULT_SETTINGS",
                column: "RESTAURANT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATION_REQUEST_QUEUE_RESERVATIONS_ID",
                table: "RESERVATION_REQUEST_QUEUE",
                column: "RESERVATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATIONS_CLIENT_ID",
                table: "RESERVATIONS",
                column: "CLIENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATIONS_LocationId",
                table: "RESERVATIONS",
                column: "LOCATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATIONS_RESTAURANTSId",
                table: "RESERVATIONS",
                column: "RESTAURANT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATIONS_TableTypeId",
                table: "RESERVATIONS",
                column: "TABLE_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESTAURANT_CAPACITY_SCHEME_LocationId",
                table: "RESTAURANT_CAPACITY_SCHEME",
                column: "LOCATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESTAURANT_CAPACITY_SCHEME_RestaurantId",
                table: "RESTAURANT_CAPACITY_SCHEME",
                column: "RESTAURANT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESTAURANT_CAPACITY_SCHEME_TableTypeId",
                table: "RESTAURANT_CAPACITY_SCHEME",
                column: "TABLE_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESTAURANT_IMAGES_RESTAURANT_ID",
                table: "RESTAURANT_IMAGES",
                column: "RESTAURANT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESTAURANT_MONTHLY_SCHEDULE_SCHEME_ID",
                table: "RESTAURANT_MONTHLY_SCHEDULE",
                column: "SCHEME_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESTAURANTS_ACCOUNT_ID",
                table: "RESTAURANTS",
                column: "ACCOUNT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESTAURANTS_ATMOSPHERE_ID",
                table: "RESTAURANTS",
                column: "ATMOSPHERE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESTAURANT_SCHEDULE_SETTINGS_RESTAURANT_ID",
                table: "RESTAURANT_SCHEDULE_SETTINGS",
                column: "RESTAURANT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESTAURANT_SCHEDULE_SETTINGS_RESTAURANT_SPECIAL_CONDITION_ID",
                table: "RESTAURANT_SCHEDULE_SETTINGS",
                column: "SPECIAL_CONDITION_ID");

            /// <summary> уникални индекси </summary>
           migrationBuilder.CreateIndex(
                name: "UX_ACCOUNTS_USERNAME",
                table: "ACCOUNTS",
                column: "USERNAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_ACCOUNTS_EMAIL",
                table: "ACCOUNTS",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_ACCOUNTS_PHONE",
                table: "ACCOUNTS",
                column: "PHONE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_RESTAURANTS_BULSTAT",
                table: "RESTAURANTS",
                column: "BULSTAT",
                unique: true);

            /// <summary> Индекси за бърдо търсене </summary>
            migrationBuilder.CreateIndex(
                name: "IX_RESERVATIONS_SCHEME_RESTAURANT",
                table: "RESTAURANT_CAPACITY_SCHEME",
                column: "RESTAURANT_ID");

            migrationBuilder.CreateIndex(
               name: "IX_RESERVATIONS_SCHEME",
               table: "RESTAURANT_CAPACITY_SCHEME",
               columns: new[] { "RESTAURANT_ID", "LOCATION_ID", "TABLE_TYPE_ID" });

            migrationBuilder.CreateIndex(
                name: "IX_RESTAURANT_IMAGES",
                table: "RESTAURANT_IMAGES",
                column: "RESTAURANT_ID");

            migrationBuilder.CreateIndex(
               name: "IX_ACCOUNTS",
               table: "ACCOUNTS",
               columns: new[] { "USERNAME", "PASSWORD" });

            migrationBuilder.CreateIndex(
               name: "IX_RESERVATIONS_CLIENT",
               table: "RESERVATIONS",
               column: "CLIENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATIONS_RESTAURANT",
                table: "RESERVATIONS",
                column: "RESTAURANT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATIONS_RESTAURANT_RES_DATE",
                table: "RESERVATIONS",
                columns: new[] { "RESTAURANT_ID", "RESERVATION_DATE" });

            migrationBuilder.CreateIndex(
                name: "IX_RESTAURANT_SCHEDULE",
                table: "RESTAURANT_MONTHLY_SCHEDULE",
                column: "SCHEME_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESTAURANT_SCHEDULE_SETTINGS",
                table: "RESTAURANT_SCHEDULE_SETTINGS",
                column: "RESTAURANT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATION_RESTAURANT",
                table: "NOTIFICATION_DEFAULT_SETTINGS",
                columns: new[] { "RESTAURANT_ID", "EVENT_ID" });

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTS_RESTAURANT",
                table: "COMMENTS",
                column: "RESERVATION_ID");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COMMENTS");

            migrationBuilder.DropTable(
                name: "NOTIFICATION_DEFAULT_SETTINGS");

            migrationBuilder.DropTable(
                name: "RESERVATION_REQUEST_QUEUE");

            migrationBuilder.DropTable(
                name: "RESTAURANT_IMAGES");

            migrationBuilder.DropTable(
                name: "RESTAURANT_MONTHLY_SCHEDULE");

            migrationBuilder.DropTable(
                name: "RESTAURANTScheduleSettings");

            migrationBuilder.DropTable(
                name: "NOTIFICATION_EVENTS");

            migrationBuilder.DropTable(
                name: "RESERVATIONS");

            migrationBuilder.DropTable(
                name: "RESTAURANT_CAPACITY_SCHEME");

            migrationBuilder.DropTable(
                name: "RESTAURANT_SPECIAL_CONDITIONS");

            migrationBuilder.DropTable(
                name: "CLIENTS");

            migrationBuilder.DropTable(
                name: "RESTAURANT_LOCATIONS");

            migrationBuilder.DropTable(
                name: "RESTAURANT_TABLE_TYPE");

            migrationBuilder.DropTable(
                name: "RESTAURANTS");

            migrationBuilder.DropTable(
                name: "ACCOUNTS");

            migrationBuilder.DropTable(
                name: "RESTAURANT_ATMOSPHERE");

            migrationBuilder.DropTable(
                name: "ACCONT_ROLE_TYPES");
        }
    }
}