create table Clubs (
	ClubId bigint identity(1,1) not null 
		constraint pkClubs primary key,
	ClubName nvarchar(100) not null,
	ClubType smallint not null default 0
)