CREATE DATABASE IF NOT EXISTS db_ecommerce;
USE db_ecommerce;
CREATE TABLE IF NOT EXISTS Member (id int unsigned not null auto_increment, 
firstname varchar(20) not null,
lastname varchar(20) not null,
email varchar(20) not null,
phone varchar(20),
address varchar(100),
pays varchar(30),
date datetime not null,
last_connection datetime not null,
password char(64),
primary key (id));
