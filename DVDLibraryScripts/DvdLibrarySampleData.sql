USE DVDLibrary
Go

INSERT INTO Rating([Name]) values ('G');
INSERT INTO Rating([Name]) values ('PG'), ('PG-13'), ('R');

INSERT INTO Director([Name]) values ('Billy Bob Thornton'), ('Harold Ramis'), ('Steven Spielberg');

INSERT INTO DVD([Title], [ReleaseYear], [DirectorId], [RatingId], [Notes])
VALUES ('Sling Blade', 1996, 1, 4, 'ADO Notes'), ('Caddie Shack', 1980, 2, 4, 'More ADO notes'), ('E.T.', 1982, 3, 3, 'And more ADO notes');

