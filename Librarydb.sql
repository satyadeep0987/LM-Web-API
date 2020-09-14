use Project

create table users(
	uid int primary key identity,
	uname varchar(50) not null,
	ucontact bigint not null,
	uaddress varchar(100) not null,
)

create table books(
	bid int primary key identity,
	bname varchar(100) not null,
	bgener varchar(50) not null,
	bprice bigint not null,
)

create table bookissue(
	iis int primary key identity,
	uid int foreign key references users (uid),
	bid int foreign key references books (bid),
	issuedate varchar(50) not null,
	noofdays int not null,
	expreturndate varchar(50) not null,
	actualreturndate varchar(50),
	fine bigint,
)

drop table bookissue