CREATE DATABASE IF NOT EXISTS db_ecommerce;
USE db_ecommerce;
CREATE TABLE IF NOT EXISTS OrderStatusId (id int unsigned not null auto_increment,
nom varchar(30) not null,
description text not null,
primary key (id));
