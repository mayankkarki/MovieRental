namespace MovieRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateUserAndRoleForMovieManager : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a83cbd54-7db9-44fd-9070-648abb342537', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e0972708-2c0e-4cd4-bf50-ead5f166dc2a', N'moviemanager@gmail.com', 0, N'AG0wndmG71A461HfFbfpKAzNgdd5dfQqdWNkqznTjZdFvDMQnCBo2PJs/zLJjXp25A==', N'1f733fbb-0174-41ac-9a89-8c9b1c9f25e3', NULL, 0, 0, NULL, 1, 0, N'moviemanager@gmail.com')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e0972708-2c0e-4cd4-bf50-ead5f166dc2a', N'a83cbd54-7db9-44fd-9070-648abb342537')
");
        }
        
        public override void Down()
        {
        }
    }
}
