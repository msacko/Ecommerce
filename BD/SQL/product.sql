CREATE DATABASE IF NOT EXISTS db_a149cb_ecom;
USE db_a149cb_ecom;
CREATE TABLE IF NOT EXISTS Product (id int unsigned not null auto_increment,
noProd int unsigned not null,
name varchar(50) not null,
description text not null,
categoryId int,
price varchar(10),
status varchar(30),
nbStock int,
country varchar(50),
primary key (id));
