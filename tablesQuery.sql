create table ROLES
(
	ID int identity(1,1) primary key,
	[NAME] nvarchar(30) not null unique
);

create table WALLETS
(
	ID int identity(1,1) primary key,
	[MoneySum] decimal (18,3) default(0.0) 
)

create table ACCOUNTS
(
	ID int identity(1,1) primary key,
    EMAIL nvarchar(50) not null unique,
    [PASSWORD] nvarchar(50) not null,
    ROLEID int foreign key references ROLES(ID),
	WALLETID int foreign key references WALLETS(ID),
	unique (WALLETID)
);

create table PARKING
(
	ID int identity(1,1) primary key,
	[ADDRESS] nvarchar(100) not null,
	LATITUDE decimal not null,
	LONGITUDE decimal not null,
	CAPACITY int not null,
	COST_PER_HOUR decimal not null
);

create table PARKING_SETTINGS
(
	PARKINGID int foreign key references PARKING(ID) primary key,
	FORPEOPLEWITHDISABILITIES int check (FORPEOPLEWITHDISABILITIES in(0,1)) default (0),
	ALLTIMESERVICE int check (ALLTIMESERVICE in(0,1)) default (0),
	CCTV int check (CCTV in(0,1)) default (0),
	LEAVETHECARKEYS int check (LEAVETHECARKEYS in(0,1)) default(0)
);

create table PARKING_RAITING
(
	ID int identity(1,1) primary key,
	USERID int foreign key references ACCOUNTS(ID),
	PARKING_ID int foreign key references PARKING(ID),
	USER_RATING decimal,
	unique(USERID, PARKING_ID)
);

create table ORDERS
(
	ORDER_ID int identity(1,1) primary key,
	ORDER_START_DATE datetime not null,
	ORDER_END_DATE datetime not null,
	ORDER_STATUS int check(ORDER_STATUS in (1,2,3)) default (1),
	ORDER_USER_ID int foreign key references ACCOUNTS(ID),
	ORDER_PARKING_ID int foreign key references PARKING(ID),
);

insert into ROLES 
values
('User'),
('Admin'),
('Dispatcher');

drop table ORDERS;
drop table PARKING_RAITING;
drop table PARKING_SETTINGS;
drop table PARKING;
drop table ACCOUNTS;
drop table WALLETS;
drop table ROLES;
