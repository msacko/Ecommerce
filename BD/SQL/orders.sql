CREATE DATABASE IF NOT EXISTS db_ecommerce;
USE db_ecommerce;
CREATE TABLE IF NOT EXISTS Orders (id int unsigned not null auto_increment,
memberId int unsigned not null,
productId int unsigned not null,
date datetime not null,
orderStatusId int unsigned not null,
primary key (id));
