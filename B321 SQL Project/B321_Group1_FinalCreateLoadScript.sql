/*
	
	Programmers: Ian Bickford, Shaun Poole, Roddey Sims
	Course:      CSCI/ISAT - B321 Database Driven Application Developement
	Assignment:  B321 Final Project
	File Name:   B321_Group1_FinalCreateLoadScript.sql
	Version:     Final

	Purpose:	 The purpose of this script is to create and populate a database that will be used
				 with a Windows Form application. The goal is to create a database (Back End) that is populated
				 with mock data, that forms "meaningful" relationships that can then be leveraged
				 with our Windows Form application (Front-End) to form a cohesive application. 
				 This application will allow you to input data into the database from the
				 front-end, process the data on the back-end, and output information back to the
				 front-end. Additionally, the database will be encapsulated in stored procedures for
				 proper security and protect the database from exploitation. This will all be done
				 with a hypothetical data set that is composed of advisors and advisees that make 
				 appointments in our Windows Form application and stored with all relevant details
				 in our database.
*/


-- Drop table statements to ensure error free create/load
drop table if exists ScheduledAppointments
drop table if exists AppointmentTimeSlots
drop table if exists Advisors
drop table if exists Locations
drop table if exists AppointmentType
drop table if exists Modality
drop table if exists Buildings
drop table if exists Advisees

-- Creation of tables required for the database
create table [Advisees] (
  [AdviseeID] int identity not null,
  [AdviseeUsername] varchar(30) unique not null, -- unique makes it so that we can reference the username later in the windows form easier, and prevents duplicate usernames.
  [AdviseePassword] varchar(25) not null,
  [AdviseeFName] varchar(25) not null,
  [AdviseeLName] varchar(50) not null,
  [AdviseePhone] varchar(12),
  [AdviseeEmail] varchar(50),
  [AdviseeAddress1] varchar(50),
  [AdviseeAddress2] varchar(50),
  [AdviseeCity] varchar(50),
  [AdviseeState] varchar(2),
  [AdviseeZip] varchar(5),
  primary key ([AdviseeID])
);

create table [Buildings] (
  [BuildingID] int identity not null,
  [BuildingTitle] varchar (50) not null,
  [BuildingAddress1] varchar(50) not null,
  [BuildingAddress2] varchar(50),
  [BuildingCity] varchar(50) not null,
  [BuildingState] varchar(2) not null,
  [BuildingZip] varchar(5),
  primary key ([BuildingID])
);

create table [Modality] (
  [ModalityID] int identity not null,
  [MeetingType] varchar(25) not null,
  [MeetingTypeDesc] varchar(300),
  primary key ([ModalityID])
);

create table [AppointmentType] (
  [AppointmentTypeID] int identity not null,
  [TypeName] varchar(25) not null,
  [TypeDescription] varchar(200),
  primary key ([AppointmentTypeID])
);

create table [Locations] (
  [LocationID] int identity not null,
  [BuildingID] int not null,
  [LocationDescription] varchar (100),
  primary key ([LocationID]),
  constraint [FK_Locations.BuildingID]
    foreign key ([BuildingID])
      references [Buildings]([BuildingID])
);

create table [Advisors] (
  [AdvisorID] int identity not null,
  [AdvisorUsername] varchar(30) unique not null, -- unique makes it so that we can reference the username later in the windows form easier, and prevents duplicate usernames.
  [AdvisorPassword] varchar(25) not null,
  [AdvisorFName] varchar(25) not null,
  [AdvisorLName] varchar(50) not null,
  [AdvisorPhone] varchar(12),
  [AdvisorEmail] varchar(50),
  [DefaultLocation] int,
  primary key ([AdvisorID]),
  constraint [FK_Advisors.DefaultLocation]
    foreign key ([DefaultLocation])
      references [Locations]([LocationID])
);

create table [ScheduledAppointments] (
  [AppointmentID] int identity not null,
  [LocationID] int not null,
  [ModalityID] int not null,
  [AdvisorID] int not null,
  [AdviseeID] int not null,
  [AppointmentTypeID] int not null,
  [AppointmentDate] date not null,
  [AppointmentStartTime] time not null,
  [AppointmentDuration] int not null,
  [AppointmentDescription] varchar(300),
  primary key ([AppointmentID]),
  constraint [FK_ScheduledAppointments.ModalityID]
    foreign key ([ModalityID])
      references [Modality]([ModalityID]),
  constraint [FK_ScheduledAppointments.AdvisorID]
    foreign key ([AdvisorID])
      references [Advisors]([AdvisorID]),
  constraint [FK_ScheduledAppointments.AdviseeID]
    foreign key ([AdviseeID])
      references [Advisees]([AdviseeID]),
  constraint [FK_ScheduledAppointments.LocationID]
    foreign key ([LocationID])
      references [Locations]([LocationID]),
  constraint [FK_ScheduledAppointments.AppointmentTypeID]
    foreign key ([AppointmentTypeID])
      references [AppointmentType]([AppointmentTypeID]),
  constraint CK_AppointmentDuration 
	check ([AppointmentDuration] between 1 and 60) -- this enforces the duration to be between 1 & 60 minutes if for some reason somebody breaks our windows form selection
);

--Insert statements for Advisees table
set identity_insert Advisees on
insert into Advisees(AdviseeID,AdviseeUsername,AdviseePassword,AdviseeFName,AdviseeLName,AdviseePhone,AdviseeEmail,AdviseeAddress1,AdviseeAddress2,AdviseeCity,AdviseeState,AdviseeZip) values
 (1,'bcuttler0','mZ3$n|b7!kGp&`Nc','Berton','Cuttler','574-140-1242','bcuttler0@microsoft.com','87355 Bluestem Pass','9th Floor','South Bend','IN',46634)
,(2,'awisker1','fS0&xR%o&}N','Amery','Wisker','518-793-4354','awisker1@businesswire.com','08517 Redwing Place','Suite 31','Albany','NY',12205)
,(3,'dmccallion2','pJ1@2hO<.mV*`YD','Drusy','McCallion','205-736-0921','dmccallion2@nytimes.com','23227 Barnett Crossing','Room 1756','Tuscaloosa','AL',35405)
,(4,'sgores3','uI2{.#t4+lE{tl','Sal','Gores','916-196-5434','sgores3@slashdot.org','5756 Upham Street','Room 1328','Sacramento','CA',94280)
,(5,'lwolver4','dV4}%Y7|u','Leonardo','Wolver','205-538-0033','lwolver4@devhub.com','054 Delladonna Street','Room 773','Birmingham','AL',35295)
,(6,'gcush5','uN5<*mcNWi@6','Garik','Cush','301-707-1011','gcush5@reference.com','48226 Luster Avenue','Suite 71','Baltimore','MD',21203)
,(7,'pjephcote6','kO8~&gO6Bjg@Xp','Pincus','Jephcote','757-247-5326','pjephcote6@sphinn.com','185 Debra Parkway','Suite 2','Newport News','VA',23612)
,(8,'acornbell7','yF5%EreE?335','Aldous','Cornbell','402-659-6398','acornbell7@salon.com','56 Mcguire Point','Room 1419','Omaha','NE',68134)
,(9,'lpadgham8','vM8/&1@5!Xfc','Lowe','Padgham','505-746-6517','lpadgham8@vistaprint.com','85 Jenna Drive','Suite 55','Santa Fe','NM',87505)
,(10,'rgrunder9','lW5<DCzHsbDSN?','Reamonn','Grunder','704-133-8144','rgrunder9@last.fm','8 Ryan Parkway','Suite 71','Charlotte','NC',28235)
,(11,'vygoua','zZ7*Jbjr','Viki','Ygou','609-471-3968','vygoua@dion.ne.jp','5283 Melrose Street','Suite 7','Trenton','NJ',8608)
,(12,'gwieldb','gT5`qV59svdb&ul','Giorgia','Wield','360-943-8101','gwieldb@domainmarket.com','36 Holmberg Trail','Suite 80','Seattle','WA',98166)
,(13,'jbudgeyc','uE3)ii)E','Jacquie','Budgey','419-207-0979','jbudgeyc@vk.com','7 Sunnyside Crossing','Room 883','Toledo','OH',43610)
,(14,'pzannottid','jP5%@LE|}ZiYMqh','Pearle','Zannotti','504-978-6434','pzannottid@hubpages.com','4346 Springview Way','Room 381','New Orleans','LA',70142)
,(15,'gscanlone','nN2%Rod}X8M`37=K','Gherardo','Scanlon','917-983-0610','gscanlone@ameblo.jp','76 Claremont Parkway','Room 1218','Bronx','NY',10474)
,(16,'dgernerf','hP5"x(<F!DX','Dulce','Gerner','646-195-7845','dgernerf@4shared.com','26 Basil Alley','PO Box 94011','New York City','NY',10099)
,(17,'apellewg','fM4&SQu_jDzGiW%_','Anatola','Pellew','501-494-2543','apellewg@walmart.com','31874 Anthes Crossing','Room 1766','Little Rock','AR',72231)
,(18,'cbowcockh','zF2"''*_''4D&Sg}','Chryste','Bowcock','901-104-0672','cbowcockh@vk.com','682 Bayside Pass','PO Box 75536','Memphis','TN',38126)
,(19,'zboakei','nB0?fq%J','Zola','Boake','410-439-1165','zboakei@kickstarter.com','4 Crowley Avenue','PO Box 63951','Baltimore','MD',21239)
,(20,'wtolsonj','kU8+imfQu27+k','Wilhelmine','Tolson','913-313-4419','wtolsonj@wp.com','914 3rd Plaza','16th Floor','Shawnee Mission','KS',66205)
,(21,'kstubbingsk','fR3=v6iasZ','Kikelia','Stubbings','214-275-3473','kstubbingsk@artisteer.com','653 Starling Alley','2nd Floor','Dallas','TX',75353)
,(22,'ebrislanl','jD7(%V4Pj&}hsO','Ethelda','Brislan','251-237-9960','ebrislanl@nps.gov','175 4th Alley','PO Box 21866','Mobile','AL',36628)
,(23,'bsyddiem','fB8"3KWKra','Brear','Syddie','339-827-6023','bsyddiem@naver.com','0389 Sycamore Circle','PO Box 3248','Woburn','MA',1813)
,(24,'dsalltern','zJ0%3zfBO.e','Dewey','Sallter','865-979-7555','dsalltern@ow.ly','17 Randy Plaza','1st Floor','Knoxville','TN',37924)
,(25,'efeechano','lK0{R&hy','Eugenius','Feechan','915-911-9203','efeechano@spotify.com','6 Hazelcrest Drive','Suite 22','El Paso','TX',88563)
,(26,'rmaxwellp','zO8"GqY(','Rorie','Maxwell','952-130-3761','rmaxwellp@ca.gov','506 Old Shore Place','9th Floor','Young America','MN',55557)
,(27,'wgarvaghq','yH9(liwp?','Way','Garvagh','850-995-6777','wgarvaghq@sina.com.cn','17 Crescent Oaks Crossing','PO Box 6053','Panama City','FL',32405)
,(28,'pvlasovr','cB9''Y+&o?8Y<64x','Prinz','Vlasov','703-166-3194','pvlasovr@creativecommons.org','7477 Del Mar Circle','Suite 76','Washington','DC',20016)
,(29,'cvoases','pJ0/)(E,aI5m','Cacilia','Voase','404-456-6521','cvoases@zdnet.com','6943 Vernon Court','Room 1722','Atlanta','GA',30343)
,(30,'rmeasent','vE3>}SGy7x68','Roz','Measen','952-309-8853','rmeasent@ezinearticles.com','16587 Grover Street','PO Box 75762','Minneapolis','MN',55436)
,(31,'roranu','nI1*K=>{I','Ronny','Oran','614-894-2623','roranu@uol.com.br','525 Sullivan Crossing','PO Box 58146','Columbus','OH',43210)
,(32,'cnockoldsv','yX0=0%%!Jp|+dY','Cam','Nockolds','978-952-8410','cnockoldsv@edublogs.org','8 Arapahoe Street','17th Floor','Boston','MA',2283)
,(33,'rbramhallw','aK3+xEiK','Richart','Bramhall','202-346-4589','rbramhallw@netscape.com','69967 Hazelcrest Alley','PO Box 96813','Washington','DC',20051)
,(34,'bcolvinex','zS9\!*\,s"g~A|B','Bendicty','Colvine','941-300-0341','bcolvinex@cyberchimps.com','07589 Tony Court','Suite 7','Sarasota','FL',34276)
,(35,'clawrancey','gJ1_}Cgh','Cobb','Lawrance','862-485-2967','clawrancey@shutterfly.com','519 Schiller Road','8th Floor','Paterson','NJ',7544)
,(36,'hmelvillez','hL9''Bb!x|O!','Hatty','Melville','937-659-6471','hmelvillez@networkadvertising.org','5 Buhler Crossing','Room 1938','Dayton','OH',45403)
,(37,'czorzenoni10','wP6.B{MXse','Charmain','Zorzenoni','601-307-8293','czorzenoni10@ed.gov','7938 International Way','Suite 22','Jackson','MS',39296)
,(38,'kstopford11','mL5`&"#.$>0zF>g','Karlen','Stopford','571-773-0031','kstopford11@so-net.ne.jp','4 Division Parkway','6th Floor','Arlington','VA',22234)
,(39,'wroseman12','xH2+pMVNd=D4K''7A','West','Roseman','205-610-9527','wroseman12@edublogs.org','260 Merry Hill','Apt 1219','Birmingham','AL',35244)
,(40,'jmullan13','iL3/t>M(','Jennica','Mullan','719-260-4000','jmullan13@fda.gov','29 Katie Crossing','Suite 59','Pueblo','CO',81010)
,(41,'nstaley14','kM0?B@z%~4Xy{8C','Netta','Staley','718-706-5952','nstaley14@chronoengine.com','9464 Division Crossing','PO Box 9688','Brooklyn','NY',11225)
,(42,'lbartoszewicz15','bV8&vD}eQ1MT0p>','Lilli','Bartoszewicz','801-387-9678','lbartoszewicz15@blinklist.com','821 Iowa Center','Suite 5','Ogden','UT',84409)
,(43,'rhellis16','hA9>(yaI|!9dCvI}','Roxi','Hellis','202-702-1173','rhellis16@smugmug.com','259 Kenwood Court','Room 1800','Washington','DC',20078)
,(44,'avarian17','rL2|bG)w','Alyssa','Varian','304-723-1276','avarian17@godaddy.com','5885 Schiller Street','Room 1947','Huntington','WV',25711)
,(45,'dfiddy18','mJ2%/HV~Z)NKx','Daphna','Fiddy','218-450-5363','dfiddy18@multiply.com','8844 Rieder Lane','Suite 65','Duluth','MN',55805)
,(46,'tmerryman19','oB0(>$3O4','Tucker','Merryman','720-630-1141','tmerryman19@fastcompany.com','37371 Starling Center','Room 1755','Denver','CO',80223)
,(47,'nmacginney1a','tH0''?F+i**P}2s','Niel','MacGinney','540-888-4992','nmacginney1a@sphinn.com','346 Maryland Circle','Room 1517','Roanoke','VA',24048)
,(48,'rornelas1b','fG0+@+GM','Rubi','Ornelas','786-851-0477','rornelas1b@washington.edu','208 Lillian Parkway','PO Box 73824','Miami','FL',33233)
,(49,'eortmann1c','yY4?=fg7p?''lD8y1','Esra','Ortmann','805-980-6671','eortmann1c@economist.com','325 Cody Alley','7th Floor','San Luis Obispo','CA',93407)
,(50,'chewkin1d','wF4&1j0*','Cordi','Hewkin','302-666-3080','chewkin1d@dailymail.co.uk','286 Farmco Pass','Suite 90','Wilmington','DE',19886)
,(51,'tmaryman1e','mL8&i7?rQ>`7P8','Torry','Maryman','303-212-5535','tmaryman1e@elpais.com','0 Milwaukee Park','Suite 87','Denver','CO',80217)
,(52,'omcmenemy1f','hG2#WQB8fH*j','Olimpia','McMenemy','302-121-1030','omcmenemy1f@wikispaces.com','4808 Kensington Terrace','4th Floor','Newark','DE',19725)
,(53,'thuntall1g','qF4(6%xc5,%K','Trumaine','Huntall','913-395-6700','thuntall1g@dell.com','7 Express Alley','Suite 12','Kansas City','KS',66160)
,(54,'rsymmons1h','oS3"r80$ri','Rene','Symmons','302-647-6798','rsymmons1h@hhs.gov','2489 Grover Court','PO Box 51711','Newark','DE',19714)
,(55,'mhanvey1i','fH6?|.SUI*','Mic','Hanvey','202-846-0227','mhanvey1i@reference.com','06922 Scofield Alley','Room 1308','Alexandria','VA',22309)
,(56,'rcherm1j','lH6\?6&\wwc','Reynolds','Cherm','813-151-1324','rcherm1j@zdnet.com','83 Aberg Park','Apt 1889','Clearwater','FL',33758)
,(57,'bschleswigholstein1k','yG1!Dt1yJ4','Boyd','Schleswig-Holstein','434-296-6727','bschleswigholstein1k@wufoo.com','52 Maryland Court','Apt 1114','Manassas','VA',22111)
,(58,'epevie1l','aT8&318$a_Y','Ellene','Pevie','214-750-2575','epevie1l@spotify.com','1622 Prentice Terrace','Suite 34','Dallas','TX',75342)
,(59,'ceudall1m','vZ2@xL)9AJ','Codee','Eudall','718-561-5590','ceudall1m@theatlantic.com','49564 Blue Bill Park Parkway','Room 1630','Bronx','NY',10474)
,(60,'bretallick1n','sV1=Q!Ytw','Bronny','Retallick','337-781-8148','bretallick1n@bigcartel.com','78 Warbler Plaza','Suite 57','Lake Charles','LA',70616)
,(61,'ccroxford1o','jN5.14ipe`z2<e','Cris','Croxford','260-385-7213','ccroxford1o@technorati.com','88 Jenna Plaza','PO Box 72600','Fort Wayne','IN',46805)
,(62,'lpiccop1p','dE8/vo\+>DF','Lissy','Piccop','916-568-1054','lpiccop1p@webs.com','2 Glendale Crossing','Suite 99','Sacramento','CA',94297)
,(63,'hwordington1q','eV7/!q/hiXo}b','Hamlin','Wordington','502-778-3380','hwordington1q@symantec.com','4 Redwing Circle','Room 1712','Louisville','KY',40298)
,(64,'mgrimsdell1r','gB0+y#`\Ol_Ul9','Marline','Grimsdell','727-217-7862','mgrimsdell1r@pen.io','01065 Scott Drive','PO Box 87581','Largo','FL',34643)
,(65,'orayhill1s','uY8<*~VK06E>Pe','Ophelie','Rayhill','212-555-5623','orayhill1s@answers.com','1609 Spaight Way','Suite 84','New York City','NY',10275)
,(66,'shanaby1t','mT2/(%IY','Sheffy','Hanaby','417-886-7492','shanaby1t@nih.gov','12111 Almo Trail','Apt 414','Springfield','MO',65898)
,(67,'brollo1u','vT6.BElEt/Zl)Z','Bealle','Rollo','402-175-4200','brollo1u@google.cn','12 Mifflin Terrace','Apt 1320','Omaha','NE',68117)
,(68,'ccoulter1v','aE1|)1|=''WUg+m','Charleen','Coulter','601-927-0966','ccoulter1v@hp.com','576 Pond Park','PO Box 43277','Hattiesburg','MS',39404)
,(69,'mmallatratt1w','jT7<NMIr4SyyGf','Markos','Mallatratt','619-487-3315','mmallatratt1w@dagondesign.com','68 Del Mar Terrace','PO Box 77967','San Diego','CA',92160)
,(70,'rstuttard1x','cH6*?gB{zM','Reggy','Stuttard','951-253-3060','rstuttard1x@auda.org.au','5918 Merry Lane','Apt 1550','Riverside','CA',92513)
,(71,'tallcroft1y','zF2_66KroEFZ(Na','Tate','Allcroft','319-531-0284','tallcroft1y@xinhuanet.com','863 Kim Parkway','Apt 738','Cedar Rapids','IA',52405)
,(72,'escorton1z','hE9|C0?_jt%K','Elberta','Scorton','804-607-7343','escorton1z@dedecms.com','5971 Maywood Junction','Room 1590','Richmond','VA',23203)
,(73,'shedley20','aU9{pUfRs"s98,0','Sigrid','Hedley','317-695-2864','shedley20@github.io','79 School Plaza','Room 1116','Indianapolis','IN',46266)
,(74,'agarth21','lO9#)~pysf*','Alexandre','Garth','661-919-6166','agarth21@sakura.ne.jp','6 Fair Oaks Avenue','Suite 54','Palmdale','CA',93591)
,(75,'hduding22','pM4>LAH}','Hilly','Duding','330-819-1890','hduding22@joomla.org','35 School Street','Apt 1352','Akron','OH',44315);
set identity_insert Advisees off

-- Insert statements for Modality table
insert into Modality(MeetingType, MeetingTypeDesc) values
('Face to Face', 'A face to face meeting that requires the advisee and advisor to be in the same physical location, at the same time.')
, ('Virtual', 'A virtual appointment that can be held either on Zoom, Teams, or another agreed upon form of virtual meeting type.');

-- Insert statements for Buildings table
set identity_insert Buildings on
insert into Buildings(BuildingID, BuildingTitle, BuildingAddress1, BuildingAddress2, BuildingCity, BuildingState, BuildingZip) values
(1,'Starbucks', '162 Cateret St', null,'Beaufort','SC',29010),
(2,'Sebastion''s Hole In The Wall', '111 Girthy Dr', null,'Hardeeville','SC',29909),
(3,'Hilton Head Unofficial Library', '159 Whoop Whoop Ave', null,'Hilton Head Island','SC',29915),
(4,'Burger King', '3247 That Guy St', null,'Hilton Head Island','SC',29915),
(5,'McDonald''s', '121 Mom Way','Suite 38','Hardeeville','SC',29909),
(6,'Rio Chico', '101 Street St', null,'Beaufort','SC',29901),
(7,'Beaufort Zoo', '6233 Wormhole Ave','PO Box 77661','Beaufort','SC',29901),
(8,'The Bomb Diggity', '92 Bombardier Rd','Suite 53','Hardeeville','SC',29909),
(9,'Wendy''s', '87 Red Dead Dr', null,'Beaufort','SC',29901),
(10,'Windigos', '3874 Georgia Ave','19th Floor','Beaufort','SC',29901),
(11,'Subway', '121 Overcooked Ln','Room 19','Hilton Head Island','SC',29915),
(12,'Carolina Point', '191 Give Me An A Ln','13th Floor','Bluffton','SC',29910),
(13,'Grande Papel', '121 Fake St','PO Box 81778','Hardeeville','SC',29909),
(14,'Borf and Borfer''s', '1092 Chick Fil A Ave','PO Box 34792','Hilton Head Island','SC',29915),
(15,'Google Eastern HQ', '82 Oreo St','Suite 35','Beaufort','SC',29901),
(16,'Gorgeous Models', '1112 Scarlett Johanson St','Room 1371','Hardeeville','SC',29909),
(17,'Manure''s Fertilizer', '1234 Poop St','11th Floor','Hardeeville','SC',29909),
(18,'Whatsittoya''s', '8231 Pom Pom Ave','The Red Room','Bluffton','SC',29910),
(19,'Hargray Building', '1 University Blvd', null,'Bluffton','SC',29910),
(20,'Science and Technology Building', '2 University Blvd', null,'Bluffton','SC',29910),
(21,'Library', '3 University Blvd', null,'Bluffton','SC',29910),
(22,'Campus Center', '4 University Blvd', null,'Bluffton','SC',29910);
set identity_insert Buildings off

-- Insert statements for AppointmentType table
set identity_insert AppointmentType on
insert into AppointmentType(AppointmentTypeID, TypeName, TypeDescription) values
(1, 'Advising', 'Meeting used to discuss upcoming academic decisions regarding course choices and overall degree completion.'),
(2, 'Office Hours', 'Meeting used to discuss course material and content further than in a normal class session. Also used for tutoring between the advisor and the advisee.'),
(3, 'Career Development', 'Used to discuss career decisions and mentoring between the advisor and advisee.')
set identity_insert AppointmentType off

-- Insert statements for Locations table
set identity_insert Locations on
insert into Locations(LocationID, BuildingID, LocationDescription) values
(1, 1, 'Lobby'),
(2, 1, 'Private meeting room'),
(3, 1, 'Courtyard outside'),
(4, 2, 'Room 898'),
(5, 3, 'Room 234'),
(6, 3, 'Conference room'),
(7, 3, 'Courtyard outside'),
(8, 3, 'By the bookcases'),
(9, 4, 'Dining area'),
(10, 5, 'Dining area'),
(11, 6, 'Dining area'),
(12, 7, 'Around the exhibits'),
(13, 7, 'Room 271'),
(14, 8, 'Guest meeting hall'),
(15, 9, 'Dining area'),
(16, 10, 'Dining area'),
(17, 11, 'Dining area'),
(18, 12, 'Private meeting hall'),
(19, 12, 'Room 1'),
(20, 12, 'VIP Room'),
(21, 13, 'DIY Room'),
(22, 14, 'Borfing room'),
(23, 14, 'Borfering room'),
(24, 14, 'Borfesting room'),
(25, 14, 'Borfiesting room'),
(26, 15, 'Google''s play room'),
(27, 15, 'Google''s conference room'),
(28, 15, 'Google''s private room'),
(29, 16, 'The Model Room'),
(30, 16, 'VIP room'),
(31, 16, 'The room that shall not be named'),
(32, 17, 'Private meeting hall'),
(33, 17, 'Outside courtyard'),
(34, 17, 'By the cow pens'),
(35, 18, 'Room 271'),
(36, 18, 'Private meeting room'),
(37, 18, 'Conference room'),
(38, 18, 'Outside by the Koi pond'),
(39, 18, 'Walking trail by the back gate exit'),
(40, 19, 'Computer lab'),
(41, 19, 'Downstairs lecture hall'),
(42, 19, 'Room 221'),
(43, 19, 'Room 215'),
(44, 20, 'Benches on the side of the building facing the library'),
(45, 20, 'Computer lab'),
(46, 20, 'Room 217'),
(47, 20, 'Room 213'),
(48, 20, 'Room 80085'),
(49, 21, 'Computer lab'),
(50, 21, 'Private study room 1'),
(51, 21, 'Private study room 2'),
(52, 21, 'Private study room 3'),
(53, 21, 'Private study room 4'),
(54, 21, 'Conference room 2nd floor'),
(55, 22, 'Tables in front of the self serve store'),
(56, 22, 'Chick-Fil-A'),
(57, 22, 'Courtyard between cafeteria and the library'),
(58, 22, 'Outside cafeteria'),
(59, 22, 'Room 271'),
(60, 22, 'Cafeteria');
set identity_insert Locations off

--Insert statements for Advisors table
set identity_insert Advisors on
insert into Advisors(AdvisorID,AdvisorUsername,AdvisorPassword,AdvisorFName,AdvisorLName,AdvisorPhone,AdvisorEmail, DefaultLocation) values
 (1,'palans0','pJ6}_oln$7C_','Philippe','Alans','781-772-9325','palans0@technorati.com', 1)
,(2,'esoutherill1','eO1\M/L)EBj$','Estele','Southerill','482-675-2282','esoutherill1@dailymail.co.uk', 5)
,(3,'jfudger2','vO2,Y6HSHn','Jo','Fudger','792-705-5953','jfudger2@networkadvertising.org', 2)
,(4,'cwaddams3','yU2$pS6ki','Claribel','Waddams','954-712-9905','cwaddams3@blogs.com', 3)
,(5,'tdebernardi4','fQ0*B)g5f"','Timofei','De Bernardi','654-322-8405','tdebernardi4@yelp.com', 4)
,(6,'mboothroyd5','iL4,)QfOBC>A2','Michelina','Boothroyd','301-815-1002','mboothroyd5@fema.gov', 5)
,(7,'hdunnaway6','mY7?pB+.+W+EbH','Hamilton','Dunnaway','946-217-2790','hdunnaway6@europa.eu', 17)
,(8,'vpierro7','bI8*gBf=i@Vd','Valaree','Pierro','855-615-0256','vpierro7@seesaa.net', 22)
,(9,'rjahn8','uO2_OV''9zc','Ronda','Jahn','335-318-1768','rjahn8@mysql.com', 60)
,(10,'gosharry9','cU1)bB#|@A}!>','Gilberto','O''Sharry','900-756-1238','gosharry9@ihg.com', 51)
,(11,'rlippingwella','yF3\iEgfj\j,R#u','Ruthann','Lippingwell','998-823-1291','rlippingwella@shinystat.com', 32)
,(12,'wbattertonb','lG7!v>T6XhgL','Werner','Batterton','685-150-7240','wbattertonb@nih.gov', 21)
,(13,'rmaxworthyc','nA0}oEvt#f0T@','Roselia','Maxworthy','394-509-1990','rmaxworthyc@alibaba.com', 34)
,(14,'rglasserd','hK9`ZZ_iirR+h0i!','Rachael','Glasser','458-469-7812','rglasserd@infoseek.co.jp', 37)
,(15,'ifeverse','cU1,Q.v/>.Lw','Isabeau','Fevers','774-127-7297','ifeverse@bigcartel.com', 42)
,(16,'gstpierref','oI8\`HCW','Guy','St. Pierre','260-134-4746','gstpierref@samsung.com', 31)
,(17,'rashbridgeg','mX4|\YdRYsSG}','Remy','Ashbridge','496-809-3825','rashbridgeg@spiegel.de', 27)
,(18,'tsellarsh','rR5>5~QW{iABSi','Tandi','Sellars','656-423-7842','tsellarsh@qq.com', 43)
,(19,'ofarrelli','cC1.0WW=','Ossie','Farrell','828-593-2341','ofarrelli@wix.com', 18)
,(20,'ifeenanj','pN3"OCXvp.','Iseabal','Feenan','974-710-6514','ifeenanj@cbc.ca', 12)
,(21,'mheinemannk','zV2`AF\@`','Mercie','Heinemann','921-956-9650','mheinemannk@google.fr', 58)
,(22,'jlehouxl','xS0.ju}C@!Z!z','Jabez','Le Houx','444-362-7411','jlehouxl@example.com', 26)
,(23,'byouhillm','cA8.9&<7d','Barnie','Youhill','149-552-0534','byouhillm@nytimes.com', 36)
,(24,'kcasinin','cU1,g9O`\luAd"K','Kelly','Casini','827-905-7384','kcasinin@slate.com', 46)
,(25,'mmingameo','lS8$yi(QeK0*NW','Melly','Mingame','414-445-5553','mmingameo@wordpress.org', 53);
set identity_insert Advisors off

