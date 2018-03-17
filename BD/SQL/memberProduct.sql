CREATE DATABASE IF NOT EXISTS db_ecommerce;
USE db_ecommerce;
CREATE TABLE IF NOT EXISTS MemberProduct (id int unsigned not null auto_increment,
memberId int unsigned not null,
productId int unsigned not null,
date datetime not null,
status varchar(30),
primary key (id));
