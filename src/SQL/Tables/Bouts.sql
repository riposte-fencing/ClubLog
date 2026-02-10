create table Bouts(
	BoutId uniqueidentifier not null 
		constraint pkBouts primary key,
	CreatedBy nvarchar(200) not null
		constraint fkBoutsToFencer foreign key references Members(MemberId),
    OpponentId uniqueidentifier
        constraint fkBoutsToFencer2 foreign key references Fencers(FencerId),
	OwnerScore smallint not null,
	OpponentScore smallint not null,
	Notes nvarchar(max),
	CoachNotes nvarchar(max),
	CreatedDate DateTime not null,
	BoutType smallint not null,
	EventType smallint not null
)

create nonclustered index idxBoutsCreatedBy on Bouts(CreatedBy)