create table Members (
	MemberId nvarchar(200) not null
		constraint pkMembers primary key,
	FirstName nvarchar(100) not null,
	LastName nvarchar(100) not null,
	ClubId bigint not null
		constraint fkMembersToClubs foreign key references Clubs(ClubId)
);

create nonclustered index idxMembersMemberIdClubId on Members(MemberId, ClubId)