 USE DVDLibrary
 GO
 
 select 
 
 DVD.DVDId, DVD.Title, DVD.ReleaseYear, d.Name as Director, r.Name as Rating, DVD.Notes 

 from DVD
 inner join Director d on DVD.DirectorId = d.DirectorId
 inner join Rating r on DVD.RatingId = r.RatingId
 

 Select * from Director;
 Select * from Rating;

 UPDATE Director
 SET Name = 'Billy Bob Thorton'
 WHERE DirectorId = 1
 
 EXEC AddADvd 'tttt', 5555, 'testDIRECTOR', 'rate', 'NONONONONO...';
 EXEC AddADvd 'T1', 1111, 'T1', 'T1', 'T1...';
 EXEC AddADvd 'T2', 2222,'Harold Ramis', 'T2', 'T2';
 EXEC AddADvd 'T3', 3333, 'T3', 'R', 'T3...';
 EXEC AddADvd 'T4', 1111, 'T4', 'T4', 'T4...';

 EXEC EditADvd 8, 'Edited', 8888, 'EditDir', 'Edit', 'EditNote';


