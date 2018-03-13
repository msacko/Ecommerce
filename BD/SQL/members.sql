CREATE DATABASE db_ecommerce;
USE db_ecommerce;
CREATE TABLE Member (id int unsigned not null auto_increment, 
firstname varchar(20) not null,
lastname varchar(20) not null,
email varchar(20) not null,
phone varchar(20),
address varchar(100),
pays varchar(30),
date timestamp not null,
last_connection timestamp not null,
password char(64),
primary key (id));