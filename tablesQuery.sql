create table ROLES
(
	ID int identity(1,1) primary key,
	[NAME] nvarchar(30) not null unique
);

insert into ROLES 

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
    ROLE_ID int foreign key references ROLES(ID),
	WALLET_ID int foreign key references WALLETS(ID),
	unique (WALLET_ID)
);

create table PARKINGS
(
	ID int identity(1,1) primary key,
	[ADDRESS] nvarchar(100) not null,
	LATITUDE decimal not null,
	LONGITUDE decimal not null,
	CAPACITY int not null,
	COST_PER_HOUR decimal not null
);

create table PARKING_RAITING
(
	ID int identity(1,1) primary key,
	USERID int foreign key references ACCOUNTS(ID),
	PARKING_ID int foreign key references PARKINGS(ID),
	USER_RATING decimal,
	unique(USERID, PARKING_ID)
);

create table ORDERS
(
	ORDER_ID int identity(1,1) primary key,
	ORDER_START_DATE datetime not null,
	ORDER_END_DATE datetime not null,
	ORDER_USER_ID int foreign key references ACCOUNTS(ID),
	ORDER_PARKING_ID int foreign key references PARKINGS(ID),
);

drop table ORDERS;
drop table PARKING_RAITING;
drop table PARKINGS;
drop table ACCOUNTS;
drop table ROLES;