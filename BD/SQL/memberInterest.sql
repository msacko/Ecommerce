CREATE DATABASE IF NOT EXISTS db_a149cb_ecom;
USE db_a149cb_ecom;
CREATE TABLE IF NOT EXISTS MemberInterest (id int unsigned not null auto_increment,
memberId int unsigned not null,
categoryProductId int unsigned not null,
primary key (id));
