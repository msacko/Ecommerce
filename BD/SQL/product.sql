CREATE DATABASE IF NOT EXISTS db_ecommerce;
USE db_ecommerce;
CREATE TABLE IF NOT EXISTS Product (id int unsigned not null auto_increment,
noProd int unsigned not null,
nom varchar(50) not null,
description text not null,
categoryId int,
prix int,
etat varchar(20),
nbStock int,
pays varchar(30),
primary key (id));
