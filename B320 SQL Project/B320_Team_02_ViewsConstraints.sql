/***********************************************************************************
                Developers: Roddey Sims and Shaun Poole
				Script Name: B320_Team_02_ViewsConstraints.sql 
                Script version: 2024
                Last updated: 2024.04.21

                Purpose:
                Create and check Constraits for all tables in the Creation Script
				Creates two views including: 

				Students info including the StudentID, First name, Last name, their GPA and their academic level of college
				and
				Instructors Average including the InstructorID, First name, Last name, Number of courses they have taught, 
				Number of students who've completed the course and the average letter grade

                Instructions:
                Execute this script in your course database
***********************************************************************************/

/*************************************** Create and Check FK Relationships *******************************************/
-- Locations FK Constraints
ALTER TABLE Locations WITH NOCHECK
	ADD CONSTRAINT FK_Locations_Institutions FOREIGN KEY(InstitutionID)
		REFERENCES Institutions (InstitutionID)
GO
ALTER TABLE Locations CHECK CONSTRAINT FK_Locations_Institutions
GO

-- Departments FK Constraints
ALTER TABLE Departments WITH NOCHECK
	ADD CONSTRAINT FK_Departments_Institutions FOREIGN KEY(InstitutionID)
		REFERENCES Institutions (InstitutionID)
GO
ALTER TABLE Departments CHECK CONSTRAINT FK_Departments_Institutions
GO

-- Department_Instructors FK Constraints
ALTER TABLE Department_Instructors WITH NOCHECK
	ADD CONSTRAINT FK_DeptInst_Instructors FOREIGN KEY(InstructorID)
		REFERENCES Instructors (InstructorID),
	CONSTRAINT FK_DeptInst_Departments FOREIGN KEY(DepartmentID)
		REFERENCES Departments (DepartmentID);
GO
ALTER TABLE Department_Instructors CHECK CONSTRAINT FK_DeptInst_Instructors
GO

-- Subjects FK Constraints
ALTER TABLE Subjects WITH NOCHECK
	ADD CONSTRAINT FK_Subjects_Departments FOREIGN KEY(DepartmentID)
		REFERENCES Departments (DepartmentID)
GO
ALTER TABLE Subjects CHECK CONSTRAINT FK_Subjects_Departments
GO

-- Course_Catalog FK Constraints
ALTER TABLE Course_Catalog WITH NOCHECK
	ADD CONSTRAINT FK_Catalog_Subjects FOREIGN KEY(SubjectID)
		REFERENCES Subjects (SubjectID)
GO
ALTER TABLE Course_Catalog CHECK CONSTRAINT FK_Catalog_Subjects
GO

-- Course_Offerings FK Constraints
ALTER TABLE Course_Offerings WITH NOCHECK
	ADD CONSTRAINT FK_Offerings_Catalog FOREIGN KEY(CourseID)
		REFERENCES Course_Catalog (CourseID),

	CONSTRAINT FK_Offerings_Terms FOREIGN KEY(AcademicTermID)
		REFERENCES Academic_Terms (AcademicTermID),

	CONSTRAINT FK_Offerings_Instructors FOREIGN KEY (InstructorID)
		REFERENCES Instructors (InstructorID),

	CONSTRAINT FK_Offerings_Locations FOREIGN KEY (LocationID)
		REFERENCES Locations (LocationID);
GO
ALTER TABLE Course_Offerings 
	CHECK CONSTRAINT FK_Offerings_Catalog, FK_Offerings_Terms, 
					 FK_Offerings_Instructors, FK_Offerings_Locations
GO

-- Student_Enrollments FK Constraints
ALTER TABLE Student_Enrollments WITH NOCHECK
	ADD CONSTRAINT FK_Enrollments_Students FOREIGN KEY(StudentID)
		REFERENCES Students (StudentID),
	CONSTRAINT FK_Enrollments_Offerings FOREIGN KEY(CourseOfferingsID)
		REFERENCES Course_Offerings (CourseOfferingsID),
	CONSTRAINT FK_Enrollments_Grades FOREIGN KEY(GradeID)
		REFERENCES Grades (GradeID);
GO

ALTER TABLE Student_Enrollments 
	CHECK CONSTRAINT FK_Enrollments_Students, FK_Enrollments_Offerings, 
					 FK_Enrollments_Grades
GO


/********************************** Additional Constraints for Data Storage ***************************************/
-- Grades GradeType Contraint
ALTER TABLE Grades ADD CONSTRAINT CHK_GradeType
			CHECK (GradeType IN 
						( 'A','B+','B','C+','C',
						  'D+','D','F','W','WF',
						  'F','I','P' ))
GO
ALTER TABLE Grades CHECK CONSTRAINT CHK_GradeType
GO

-- AcdTerms Semester Constraint
ALTER TABLE Academic_Terms ADD CONSTRAINT CHK_Semester
			CHECK (Semester IN ('Fall', 'Spring', 'Summer'))
GO
ALTER TABLE Academic_Terms CHECK CONSTRAINT CHK_Semester
GO

-- AcdTerms AcdPeriod Constraint
ALTER TABLE Academic_Terms ADD CONSTRAINT CHK_AcdPeriod
			CHECK (AcdPeriod IN ('1st Half Term', '2nd Half Term', 'Full Term', 'May Term'))
GO
ALTER TABLE Academic_Terms CHECK CONSTRAINT CHK_AcdPeriod
GO
/*************************Our Views ***************************/


/**********************View 1: StudentInfo*********************/
	-- This statement Calculates Quality Points for each grade
	SELECT GradeType,
			COUNT(*) AS GradeCount,
			SUM(CreditHours) AS TotalCreditHours,
			Asso_GPA * SUM(CreditHours) AS QualityPoints
	FROM Student_Enrollments se
		INNER JOIN Grades g
			ON g.gradeID = se.gradeid
		INNER JOIN Course_Offerings co
			ON co.CourseOfferingsID = se.CourseOfferingsID
	GROUP BY GradeType, Asso_GPA
	ORDER BY GradeType



-- Step 1: Declare a Function to calculate GPA for any StudentID passed
-- Check if function exists and DROP for integrity
IF OBJECT_ID (N'dbo.Calc_GPA', N'FN') IS NOT NULL
	DROP FUNCTION Calc_GPA
GO

-- Create function
CREATE FUNCTION dbo.Calc_GPA (@StudentID INT)
RETURNS FLOAT
BEGIN 
	DECLARE @CumulativeGPA FLOAT
	DECLARE @QualityPoints FLOAT 
	DECLARE @TotalCredHours INT

	--DECLARE @StudentID INT
	--SET @StudentID = 2 -- Manually Calculated QP = 388.5

	-- Calculate Total Quality Points
	SELECT @QualityPoints = SUM(PointTotals)
	FROM (	-- First Calculate GPA * TotalHours for each wGrade.
			SELECT Asso_GPA * SUM(CreditHours) AS PointTotals
			FROM Student_Enrollments se
			INNER JOIN Grades g
				ON g.gradeID = se.gradeid
			INNER JOIN Course_Offerings co
				ON co.CourseOfferingsID = se.CourseOfferingsID
			WHERE StudentID = @StudentID
				AND Asso_GPA IS NOT NULL -- Filters out non-Standard Letter grading
			GROUP BY Asso_GPA
			-- Something to keep in mind.. If a student has ANY classes not recorded as
			-- standard letter, Asso_GPA will be a null value and may default to 0. A fix to this
			-- is a simple "IN" Keyword followed by list of GPA... OR better yet, a IS NOT NULL check.
		) AS tbl
	--SELECT @QualityPoints

	-- Next, Get total Credit Hours Student has taken
	SELECT @TotalCredHours = SUM(CreditHours)
	FROM Student_Enrollments se
		INNER JOIN Course_Offerings co
			ON se.CourseOfferingsID = co.CourseOfferingsID
	WHERE StudentID = @StudentID
	--GROUP BY @StudentID

	-- Calculate GPA
	SET @CumulativeGPA = ROUND( @QualityPoints / @TotalCredHours, 3)

	-- Testing
	--SELECT @QualityPoints AS TotalQP, 
			--@TotalCredHours AS TotalCreditHours,
			--ROUND(@CumulativeGPA, 3) AS CalculatedGPA

	RETURN @CumulativeGPA
END
GO

-- Check if View Exists and DROP
DROP VIEW IF EXISTS [StudentInfo]
GO

-- Create View which is just acting as a wrapper
CREATE VIEW [StudentInfo] AS
(
	
	SELECT	s.StudentID,
			StudentFName, 
			StudentLName,
			dbo.Calc_GPA(s.StudentID) AS GPA,
			CASE 
				WHEN SUM(CreditHours) BETWEEN 0 AND 30
					THEN '1st Year'
				WHEN SUM(CreditHours) BETWEEN 31 AND 60
					THEN '2nd Year'
				WHEN SUM(CreditHours) BETWEEN 61 AND 90
					THEN '3rd Year'
				WHEN SUM(CreditHours) BETWEEN 91 AND 120
					THEN '4th Year'
				WHEN SUM(CreditHours) >= 120
					THEN 'Alumni'
			END AS AcademicLevel
	FROM Students s
		INNER JOIN Student_Enrollments se
			ON s.StudentID = se.StudentID
		INNER JOIN Grades g
			ON se.GradeID = g.GradeID
		INNER JOIN Course_Offerings co
			ON se.CourseOfferingsID = co.CourseOfferingsID
	GROUP BY s.StudentID,
				StudentFName, 
				StudentLName
)
GO

SELECT * FROM StudentInfo

/**********************View 2:Instructor Average*********************/

DROP VIEW IF EXISTS [InstructorAvg]
GO
-- This view shows the InstructorID, Instructor's first name, Instructors last name,
-- The number of course they taught, the number of students who have completed the course and the avergage grade for that course.
-- Roddey used a count expression for the InstructorsID in course offerings
-- Roddey used a count expression for the studentID in student enrollments
-- Roddey used a avg expression with cases to run through the possible grades by converting the students average GPA to a letter grade in the class.

CREATE VIEW [InstructorAvg] AS
(
SELECT 
    I.InstructorID,
    I.InstructorFName,
    I.InstructorLName,
    COUNT(DISTINCT CO.CourseOfferingsID) AS NumCoursesTaught,
    COUNT(DISTINCT CASE WHEN SE.StudentID IS NOT NULL THEN SE.StudentID END) AS NumStudentsCompleted,
    CASE 
        WHEN AVG(DISTINCT CASE WHEN SE.StudentID IS NOT NULL THEN SE.StudentID END) > 0 THEN
            CASE 
                WHEN AVG(CASE 
                            WHEN G.GradeType = 'A' THEN 4.0
                            WHEN G.GradeType = 'B+' THEN 3.5
                            WHEN G.GradeType = 'B' THEN 3.0
                            WHEN G.GradeType = 'C+' THEN 2.5
                            WHEN G.GradeType = 'C' THEN 2.0
                            WHEN G.GradeType = 'D+' THEN 1.5
                            WHEN G.GradeType = 'D' THEN 1.0
                            WHEN G.GradeType = 'F' THEN 0.0
                            ELSE NULL
                        END) >= 3.5 THEN 'A'
                WHEN AVG(CASE 
                            WHEN G.GradeType = 'A' THEN 4.0
                            WHEN G.GradeType = 'B+' THEN 3.5
                            WHEN G.GradeType = 'B' THEN 3.0
                            WHEN G.GradeType = 'C+' THEN 2.5
                            WHEN G.GradeType = 'C' THEN 2.0
                            WHEN G.GradeType = 'D+' THEN 1.5
                            WHEN G.GradeType = 'D' THEN 1.0
                            WHEN G.GradeType = 'F' THEN 0.0
                            ELSE NULL
                        END) >= 3.0 THEN 'B'
                WHEN AVG(CASE 
                            WHEN G.GradeType = 'A' THEN 4.0
                            WHEN G.GradeType = 'B+' THEN 3.5
                            WHEN G.GradeType = 'B' THEN 3.0
                            WHEN G.GradeType = 'C+' THEN 2.5
                            WHEN G.GradeType = 'C' THEN 2.0
                            WHEN G.GradeType = 'D+' THEN 1.5
                            WHEN G.GradeType = 'D' THEN 1.0
                            WHEN G.GradeType = 'F' THEN 0.0
                            ELSE NULL
                        END) >= 2.5 THEN 'C+'
                WHEN AVG(CASE 
                            WHEN G.GradeType = 'A' THEN 4.0
                            WHEN G.GradeType = 'B+' THEN 3.5
                            WHEN G.GradeType = 'B' THEN 3.0
                            WHEN G.GradeType = 'C+' THEN 2.5
                            WHEN G.GradeType = 'C' THEN 2.0
                            WHEN G.GradeType = 'D+' THEN 1.5
                            WHEN G.GradeType = 'D' THEN 1.0
                            WHEN G.GradeType = 'F' THEN 0.0
                            ELSE NULL
                        END) >= 2.0 THEN 'C'
                WHEN AVG(CASE 
                            WHEN G.GradeType = 'A' THEN 4.0
                            WHEN G.GradeType = 'B+' THEN 3.5
                            WHEN G.GradeType = 'B' THEN 3.0
                            WHEN G.GradeType = 'C+' THEN 2.5
                            WHEN G.GradeType = 'C' THEN 2.0
                            WHEN G.GradeType = 'D+' THEN 1.5
                            WHEN G.GradeType = 'D' THEN 1.0
                            WHEN G.GradeType = 'F' THEN 0.0
                            ELSE NULL
                        END) >= 1.5 THEN 'D+'
                WHEN AVG(CASE 
                            WHEN G.GradeType = 'A' THEN 4.0
                            WHEN G.GradeType = 'B+' THEN 3.5
                            WHEN G.GradeType = 'B' THEN 3.0
                            WHEN G.GradeType = 'C+' THEN 2.5
                            WHEN G.GradeType = 'C' THEN 2.0
                            WHEN G.GradeType = 'D+' THEN 1.5
                            WHEN G.GradeType = 'D' THEN 1.0
                            WHEN G.GradeType = 'F' THEN 0.0
                            ELSE NULL
                        END) >= 1.0 THEN 'D'
                ELSE 'F'
            END
        ELSE
            NULL
    END AS AvgLetterGrade
FROM 
    Instructors I
INNER JOIN 
    Course_Offerings CO ON I.InstructorID = CO.InstructorID
INNER JOIN 
    Student_Enrollments SE ON CO.CourseOfferingsID = SE.CourseOfferingsID
INNER JOIN 
    Grades G ON SE.GradeID = G.GradeID
GROUP BY 
    I.InstructorID, I.InstructorFName, I.InstructorLName
)
GO
SELECT * FROM InstructorAvg