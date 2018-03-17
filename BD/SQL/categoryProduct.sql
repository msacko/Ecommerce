CREATE DATABASE IF NOT EXISTS db_ecommerce;
USE db_ecommerce;
CREATE TABLE IF NOT EXISTS CategoryProduct (id int unsigned not null auto_increment,
nom varchar(30),
description text,
primary key (id));
