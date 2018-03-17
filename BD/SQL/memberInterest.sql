CREATE DATABASE IF NOT EXISTS db_ecommerce;
USE db_ecommerce;
CREATE TABLE IF NOT EXISTS MemberInterest (id int unsigned not null auto_increment,
memberId int unsigned not null,
categoryProductId int unsigned not null,
primary key (id));
