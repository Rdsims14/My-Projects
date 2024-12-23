/*
Programmers: Ian Bickford, Shaun Poole, Roddey Sims
	Course:      CSCI/ISAT - B321 Database Driven Application Developement
	Assignment:  B321 Final Project
	File Name:   b321_StoredProcedures.sql
	Version:     December 10th, 2024

	Purpose:	 The purpose of this script is to hold the Stored procedures with the data of our creation script.
				 The script will include procedures for when the user needs to login .


*/

drop procedure if exists LoginAdvisee;
drop procedure if exists CreateScheduledAppointment;
drop procedure if exists CancelAppointment;
drop procedure if exists RescheduleAppointment;
drop procedure if exists GetAdvisees;
drop procedure if exists GetAdvisors;
drop procedure if exists GetModalities;
drop procedure if exists GetApptTypes;
drop procedure if exists GetLocations;
drop procedure if exists GetAvailability;
drop procedure if exists GetUserAppointments;
drop procedure if exists CheckForOverlappingAppointments;
drop procedure if exists CheckForOverlappingAppointmentsReschedule;

select AdviseeUsername, 
	   AdviseePassword
from Advisees
go

create procedure LoginAdvisee
	@Username NVARCHAR(50),
	@Password NVARCHAR(50)
as 
begin
	select AdviseeID, AdviseeFName, AdviseeLName
	from Advisees
	where AdviseeUsername = @Username and AdviseePassword = @Password
end;
execute LoginAdvisee @Username = '', @Password = '';
go
/* This procedure handles the logic of making an appointment by taking the info that the user makes in the front end and inserts it in the database. 
   The appointment could then be removed/canceled or Reschedule the appointment.
*/
create procedure CreateScheduledAppointment
    @LocationID int,
    @ModalityID int,
    @AdvisorID int,
    @AdviseeID int,
    @AppointmentTypeID int,
    @AppointmentDate date,
    @AppointmentStartTime time,
    @AppointmentDuration int = 30,
    @AppointmentDescription varchar(300) = NULL
as
begin
    
    insert into ScheduledAppointments (
        LocationID, 
        ModalityID, 
        AdvisorID, 
        AdviseeID, 
        AppointmentTypeID, 
        AppointmentDate, 
        AppointmentStartTime, 
        AppointmentDuration, 
        AppointmentDescription
    )
    values (
        @LocationID, 
        @ModalityID, 
        @AdvisorID, 
        @AdviseeID, 
        @AppointmentTypeID, 
        @AppointmentDate, 
        @AppointmentStartTime, 
        @AppointmentDuration, 
        @AppointmentDescription
    );

   
    declare @NewAppointmentID int = SCOPE_IDENTITY();

    
    select 'Appointment created successfully' as ConfirmationMessage,
           AppointmentID,
           LocationID,
           ModalityID,
           AdvisorID,
           AdviseeID,
           AppointmentTypeID,
           AppointmentDate,
           AppointmentStartTime,
           AppointmentDuration,
           AppointmentDescription
    from ScheduledAppointments
    where AppointmentID = @NewAppointmentID;
end;
go




go

/* This procedure deals with the logic of taking a scheduled appointment
   that the user makes and if the user decides to cancel a appointment
   the procedure will take the ID that was created with the appointment and delete 
   everything tied to that ID.
*/
create procedure CancelAppointment
    @AppointmentID int
as
begin
    
    IF NOT EXISTS (select 1 from ScheduledAppointments where AppointmentID = @AppointmentID)
    begin
        select 'Error: Appointment not found.' as ErrorMessage;
        return;
    end

    
    select * 
    into #AppointmentDetails
    from ScheduledAppointments 
    where AppointmentID = @AppointmentID;

    -- delete the appointment
    delete from ScheduledAppointments where AppointmentID = @AppointmentID;

    
    select 'Appointment canceled successfully' as ConfirmationMessage, *
    from #AppointmentDetails;

    drop table #AppointmentDetails;
end;


/*This stored procedure will take a scheduled appointment that is made in the frontend.
  the appointment that was made and then reschedule/make a new appointment for the user by updating
  the scheduled appointment with the new inform

*/
go
create procedure RescheduleAppointment
    @AppointmentID int,
    @NewDate date,
    @NewStartTime time
as
begin
    
    IF NOT EXISTS (select 1 from ScheduledAppointments where AppointmentID = @AppointmentID)
    begin
        select 'Error: Appointment not found.' as ErrorMessage;
        return;
    end

    
    update ScheduledAppointments
    set AppointmentDate = @NewDate,
        AppointmentStartTime = @NewStartTime
    where AppointmentID = @AppointmentID;

    
    select 'Appointment rescheduled successfully' as ConfirmationMessage,
           AppointmentID, 
           LocationID, 
           ModalityID, 
           AdvisorID, 
           AdviseeID, 
           AppointmentTypeID, 
           AppointmentDate, 
           AppointmentStartTime, 
           AppointmentDuration, 
           AppointmentDescription
    from ScheduledAppointments
    where AppointmentID = @AppointmentID;
end;





-- Get Info for front end --
-- Advisees
go
create procedure GetAdvisees
as
	select * from Advisees
	order by AdviseeID
;
-- Advisors
go
create procedure GetAdvisors
as
	select * from Advisors
	order by AdvisorID
;
-- Modalities
go
create procedure GetModalities
as
	select *
	from Modality
;
-- App Types
go
create procedure GetApptTypes
as
	select *
	from AppointmentType
;
-- Locations (With building names. No ID needed because that is contained in the Locations ID)
go
create procedure GetLocations
as
	select	LocationID, BuildingTitle, 
			BuildingAddress1, BuildingAddress2,
			BuildingCity, BuildingState, LocationDescription
	from Locations l
	inner join Buildings b
		on l.BuildingID = b.BuildingID
;

-- Appointments for Each advisor for time population
go
create procedure GetAvailability
as
	select AdvisorID, AppointmentDate, 
			AppointmentStartTime, AppointmentDuration
	from ScheduledAppointments
	where AppointmentDate >= GETDATE()
;
-- Appointments for Current User
go
create procedure GetUserAppointments
	@UserID int -- assume only students
as
begin
	select AppointmentID, AppointmentDate, AppointmentStartTime, AppointmentDuration,
			sa.AdvisorID, AdvisorLName + ', ' + AdvisorFName as AdvisorFullName, AdvisorEmail,
			bu.BuildingAddress1, AdviseeID
	from ScheduledAppointments sa
		inner join Advisors adv
			on adv.AdvisorID = sa.AdvisorID
		inner join Locations loc
			on sa.LocationID = loc.LocationID
		inner join Buildings bu
			on loc.BuildingID = bu.BuildingID
	where sa.AdviseeID = @UserID;
end
-- Checks for if a users new appointment will overlap with another appointment
go
create procedure CheckForOverlappingAppointments
    @LocationID int,
    @ModalityID int,
    @AdvisorID int,
    @AdviseeID int,
    @AppointmentTypeID int,
    @AppointmentDate date,
    @AppointmentStartTime time,
    @AppointmentDuration int
as
begin
    declare @EndTime time = dateadd(minute, @AppointmentDuration, @AppointmentStartTime)

    if EXISTS (
        select 1
        from ScheduledAppointments
        where LocationID = @LocationID
        and ModalityID = @ModalityID
        and AdvisorID = @AdvisorID
        and AdviseeID = @AdviseeID
        and AppointmentTypeID = @AppointmentTypeID
        and AppointmentDate = @AppointmentDate
        and (
            (@AppointmentStartTime >= AppointmentStartTime and @AppointmentStartTime < dateadd(minute, AppointmentDuration, AppointmentStartTime))
            OR (@EndTime > AppointmentStartTime and @EndTime <= dateadd(minute, AppointmentDuration, AppointmentStartTime))
            OR (@AppointmentStartTime <= AppointmentStartTime and @EndTime >= dateadd(minute, AppointmentDuration, AppointmentStartTime))
        )
    )
    begin
        raiserror ('An overlapping appointment exists.', 16, 1)
    end
end

