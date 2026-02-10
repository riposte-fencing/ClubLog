create table Tags(
	TagId uniqueidentifier not null
		constraint pkTags primary key,
	TagName nvarchar(30) not null,
	ClubId bigint not null 
		constraint fkTagsOnClubs foreign key references Clubs(ClubId),
	CreatedBy nvarchar(200) not null
		constraint fkTagsOnMembers foreign key references Members(MemberId)
)

create nonclustered index idxTagsClubId on Tags(ClubId)