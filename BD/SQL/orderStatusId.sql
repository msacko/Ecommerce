CREATE DATABASE IF NOT EXISTS db_a149cb_ecom;
USE db_a149cb_ecom;
CREATE TABLE IF NOT EXISTS OrderStatusId (id int unsigned not null auto_increment,
name varchar(30) not null,
description text not null,
primary key (id));
