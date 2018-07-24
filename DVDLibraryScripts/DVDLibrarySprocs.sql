USE DVDLibrary
GO

ALTER PROCEDURE AddADvd
(
	@Title varchar(150),
	@ReleaseYear int,
	@Director varchar(100),
	@Rating varchar(10),
	@Notes varchar(150)
)

AS

DECLARE @DirectorId int;
DECLARE @RatingId int;

IF EXISTS (SELECT * FROM Director d WHERE d.Name = @Director)
	BEGIN;
		SET @DirectorId = (SELECT DirectorId FROM Director WHERE Director.Name = @Director);
	END;
ELSE
	BEGIN;
		INSERT INTO Director VALUES(@Director);
		SELECT @DirectorId = SCOPE_IDENTITY();
	END;

IF EXISTS (SELECT * FROM Rating r WHERE r.Name = @Rating)
	BEGIN;
		SET @RatingId = (SELECT RatingId FROM Rating WHERE Rating.Name = @Rating);
	END;
ELSE
	BEGIN;
		INSERT INTO Rating VALUES(@Rating);
		SELECT @RatingId = SCOPE_IDENTITY();
	END;

INSERT INTO DVD([Title], [ReleaseYear], [DirectorId], [RatingId], [Notes]) 
VALUES
(
	@Title,
	@ReleaseYear,
	@DirectorId,
	@RatingId,
	@Notes
)

RETURN SCOPE_IDENTITY()


GO

CREATE PROCEDURE EditADvd
(
	@OriginalId int,
	@Title varchar(150),
	@ReleaseYear int,
	@Director varchar(100),
	@Rating varchar(10),
	@Notes varchar(150)
)

AS

DECLARE @DirectorId int;
DECLARE @RatingId int;

IF EXISTS (SELECT * FROM Director d WHERE d.Name = @Director)
	BEGIN;
		SET @DirectorId = (SELECT DirectorId FROM Director WHERE Director.Name = @Director);
	END;
ELSE
	BEGIN;
		INSERT INTO Director VALUES(@Director);
		SELECT @DirectorId = SCOPE_IDENTITY();
	END;

IF EXISTS (SELECT * FROM Rating r WHERE r.Name = @Rating)
	BEGIN;
		SET @RatingId = (SELECT RatingId FROM Rating WHERE Rating.Name = @Rating);
	END;
ELSE
	BEGIN;
		INSERT INTO Rating VALUES(@Rating);
		SELECT @RatingId = SCOPE_IDENTITY();
	END;

UPDATE DVD SET
	Title = @Title,
	ReleaseYear = @ReleaseYear,
	DirectorId = @DirectorId,
	RatingId = @RatingId,
	Notes = @Notes
	WHERE DVD.DVDId = @OriginalID;


RETURN @OriginalID


GO