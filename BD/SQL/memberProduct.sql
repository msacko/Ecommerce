CREATE DATABASE IF NOT EXISTS db_a149cb_ecom;
USE db_a149cb_ecom;
CREATE TABLE IF NOT EXISTS MemberProduct (id int unsigned not null auto_increment,
memberId int unsigned not null,
productId int unsigned not null,
date datetime not null,
status varchar(30),
primary key (id));
