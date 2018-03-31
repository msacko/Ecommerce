CREATE DATABASE IF NOT EXISTS db_a149cb_ecom;
USE db_a149cb_ecom;
CREATE TABLE IF NOT EXISTS ProductFile (id int unsigned not null auto_increment,
productId int unsigned not null,
path varchar(100),
isProfile bit,
date datetime not null,
primary key (id));
