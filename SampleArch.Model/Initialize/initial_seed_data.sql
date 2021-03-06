            
            IF((SELECT COUNT(*) from [dbo].aspnet_SchemaVersions) = 0)
	            BEGIN
		            PRINT 'START - inserting [aspnet_SchemaVersions] data into DB'
		            -- Add 6 rows to [dbo].[aspnet_SchemaVersions]
		            INSERT INTO [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'Category', N'1', 1)

		            INSERT INTO [dbo].[aspnet_Applications] ([ApplicationId], [ApplicationName], [LoweredApplicationName], [Description]) VALUES ('d9972acc-4c2e-458d-b7b8-bd5297f8d88a', N'Positive', N'positive', NULL)
	            END


            declare @applicationID as uniqueidentifier
            declare @adminUserID as uniqueidentifier

            select top 1 @applicationID =  applicationId from aspnet_applications where loweredapplicationname = 'positive'

            print @applicationID


            if not exists(select * from aspnet_users where UserName = 'admin')
            begin
                set @adminUserID =  newid()
	            insert into aspnet_users values(@adminUserID, @applicationID, 'admin', 'admin', null, 0, '2015-05-25 00:00:0:00')
            end

            else
            begin
	            select @adminUserID = userID from aspnet_users where UserName = 'admin'
            end



            print 'define users'
            if not exists(select * from aspnet_users where UserName = 'hseyrek')
            insert into aspnet_users values( newid(),  @applicationID, 'hseyrek', 'hseyrek', null, 0, '2012-12-01 00:00:0:00')
            if not exists(select * from aspnet_users where UserName = 'edemirer')
            insert into aspnet_users values( newid(), @applicationID, 'edemirer', 'edemirer', null, 0, '2012-12-01 00:00:0:00')

            print @adminUserID

            print 'define modules'
            if not exists(select * from modules where MdlDescriptionLowered = 'category')
            insert into modules values ('Category', 'category', 1,   @adminUserID, '2012-12-26 00:00:00',  @adminUserID, '2012-12-26 00:00:00' )



            print 'define Roles'
            if not exists(select * from aspnet_roles where RoleName = 'administrator')
            insert into aspnet_roles values( NEWID(), @applicationID, 'Administrator', 'administrator', 'the Supervisors')
            if not exists(select * from aspnet_roles where RoleName = 'clerk')
            insert into aspnet_roles values( NEWID(), @applicationID, 'Clerk', 'clerk', 'the clerks')


            print 'define menus'
            if not exists(select * from Menus where MnDescriptionLowered = 'definitions')
            insert into Menus values(null, null, 'Tanımlamalar', 'definitions', 1, '',@adminUserID, '2012-12-26 00:00:00', @adminUserID, '2012-12-26 00:00:00' )
            if not exists(select * from Menus where MnDescriptionLowered = 'categories')
            insert into Menus values((select top 1 MdlId from modules where MdlDescriptionLowered = 'category'), (select top 1 MnId from Menus where MnDescriptionLowered = 'definitions'), 'Categoires', 'categories', 1, 'Categories/definitions',@adminUserID, '2012-12-26 00:00:00',  @adminUserID, '2012-12-26 00:00:00' )


            print 'define users in roles'

            if not exists(select * from aspnet_usersinroles where userID = (select UserID from aspnet_users where UserName = 'hseyrek') and roleId = (select RoleId from aspnet_roles where RoleName = 'clerk'))
            insert into aspnet_usersinroles values ((select RoleId from aspnet_roles where RoleName = 'clerk'), (select UserID from aspnet_users where UserName = 'hseyrek'))

            if not exists(select * from aspnet_usersinroles where userID = (select UserID from aspnet_users where UserName = 'edemirer') and roleId = (select RoleId from aspnet_roles where RoleName = 'clerk'))
            insert into aspnet_usersinroles values ((select RoleId from aspnet_roles where RoleName = 'clerk'), (select UserID from aspnet_users where UserName = 'edemirer'))


            print 'define modules in roles'
            if not exists(select * from dbo.ModulesInRoles where 
	             MdlRoleMdlId = (select MdlId from Modules where MdlDescriptionLowered = 'category') and 
	             MdlRoleRoleId = (select RoleId from aspnet_roles where RoleName = 'clerk'))
            insert into dbo.ModulesInRoles values(
	             (select top 1 MdlId from Modules where MdlDescriptionLowered = 'category'), 
	             (select top 1 RoleId from aspnet_roles where RoleName = 'clerk'),
	             1,1,1,1, 
	             @adminUserID, '2012-12-01 00:00:00', @adminUserID,  '2012-12-01 00:00:00')

            if not exists(select * from dbo.ModulesInRoles where 
		             MdlRoleMdlId = (select MdlId from Modules where MdlDescriptionLowered = 'category') and 
	             MdlRoleRoleId = (select RoleId from aspnet_roles where RoleName = 'administrator'))
            insert into dbo.ModulesInRoles values(
	             (select top 1 MdlId from Modules where MdlDescriptionLowered = 'category'), 
	             (select RoleId from aspnet_roles where RoleName = 'administrator'),
	             1,1,1,1, 
	             @adminUserID, '2012-12-01 00:00:00', @adminUserID,  '2012-12-01 00:00:00')
	 
	 

	             if( not exists(select * from aspnet_membership where userID IN (select UserId from aspnet_users where UserName = 'hseyrek')))
			            insert into aspnet_membership values(
			            (select UserId from aspnet_users where UserName = 'hseyrek'),
		            @applicationID, 
		
		            '123456', 
		            0, 
		            0, 
		            null, 
		            '.eu', 
		            'omer@test.eu', 
		            null, 
		            null,
		            1, 
		            0, 
		            '2012-12-01 00:00:00', 
		            '2012-12-01 00:00:00', 
		            '2012-12-01 00:00:00', 
		            '2012-12-01 00:00:00', 
		            0, 
		            '1754-01-01 00:00:00.000', 
		            0,
		            '1754-01-01 00:00:00.000',
		            null)   

	            insert into aspnet_membership values(
			            (select UserId from aspnet_users where UserName = 'edemirer'),
		            @applicationID, 

		            '123456', 
		            0, 
		            0, 
		            null, 
		            '.eu', 
		            'ermand@test.eu', 
		            null, 
		            null,
		            1, 
		            0, 
		            '2012-12-01 00:00:00', 
		            '2012-12-01 00:00:00', 
		            '2012-12-01 00:00:00', 
		            '2012-12-01 00:00:00', 
		            0, 
		            '1754-01-01 00:00:00.000', 
		            0,
		            '1754-01-01 00:00:00.000',
		            null)   