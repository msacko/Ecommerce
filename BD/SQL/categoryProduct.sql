CREATE DATABASE IF NOT EXISTS db_a149cb_ecom;
USE db_a149cb_ecom;
CREATE TABLE IF NOT EXISTS CategoryProduct (id int unsigned not null auto_increment,
name varchar(30),
description text,
primary key (id));
