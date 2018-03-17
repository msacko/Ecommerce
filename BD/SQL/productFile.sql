CREATE DATABASE IF NOT EXISTS db_ecommerce;
USE db_ecommerce;
CREATE TABLE IF NOT EXISTS ProductFile (id int unsigned not null auto_increment,
productId int unsigned not null,
path varchar(100),
isProfile bit,
date datetime not null,
primary key (id));
