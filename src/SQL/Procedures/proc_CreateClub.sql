CREATE PROCEDURE proc_CreateClub
    @clubName nvarchar(100),
    @clubType smallint
AS
    
    INSERT INTO Clubs (ClubName, ClubType)
    VALUES (@clubName, @clubType);
    
RETURN 0 