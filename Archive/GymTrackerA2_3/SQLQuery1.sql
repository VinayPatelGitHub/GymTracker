select * from Sets;
select * from Sessions;
select * from Exercises;
select * from Users;
select * from MuscleGroups;

--FOR 6 WEEK OVERVIEW
SELECT SessionDate
,MuscleGroupId
,UserId
FROM Sessions s
INNER JOIN Exercises e on s.ExerciseId = e.ExerciseId
WHERE UserId = 1

--FOR EXERSICE OVERVIEW
SELECT sts.SessionId
, ses.SessionDate
, ses.UserId
, e.ExerciseId
, sts.SetNumber
, sts.Weight
, sts.NumberofReps
FROM Sets sts
INNER JOIN Sessions ses on sts.SessionId = ses.SessionId
INNER JOIN Exercises e on ses.ExerciseId = e.ExerciseId
WHERE UserId = 1 and e.ExerciseId = 1
