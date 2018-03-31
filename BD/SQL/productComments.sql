CREATE DATABASE IF NOT EXISTS db_a149cb_ecom;
USE db_a149cb_ecom;
CREATE TABLE IF NOT EXISTS ProductComments (id int unsigned not null auto_increment,
productId int unsigned not null,
memberId int unsigned not null,
comment text not null,
date datetime not null,
primary key (id));
