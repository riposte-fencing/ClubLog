CREATE TABLE Fencers (
    FencerId uniqueidentifier not null
        constraint pkFencers primary key,
    FirstName nvarchar(100) not null,
    LastName nvarchar(100) not null,
    MemberId nvarchar(200) null
        constraint fkFencersToMembers foreign key references Members(MemberId)
)

create nonclustered index idxMemberFirstName on Fencers(FirstName)
create nonclustered index idxMemberLastName on Fencers(LastName)
create nonclustered index idxMemberFirstNameLastName on Fencers(FirstName, LastName)