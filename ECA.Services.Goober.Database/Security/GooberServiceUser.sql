CREATE ROLE [GooberServiceUser]
	AUTHORIZATION [dbo];
GO

grant select on schema :: dbo to GooberServiceUser;
GO
grant update on schema :: dbo to GooberServiceUser;
GO
grant insert on schema :: dbo to GooberServiceUser;
GO
grant delete on schema :: dbo to GooberServiceUser;
GO
grant execute on schema :: dbo to GooberServiceUser;
GO
