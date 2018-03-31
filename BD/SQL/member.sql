CREATE DATABASE IF NOT EXISTS db_a149cb_ecom;
USE db_a149cb_ecom;
CREATE TABLE IF NOT EXISTS Member (id int unsigned not null auto_increment, 
firstname varchar(100) not null,
lastname varchar(100) not null,
email varchar(100) not null,
phone varchar(20),
address varchar(100),
country varchar(50),
date datetime not null,
last_connection datetime not null,
password char(64),
primary key (id));
