DROP DATABASE IF EXISTS intereal;
CREATE DATABASE intereal;
DROP USER IF EXISTS intereal_admin;
CREATE USER intereal_admin@'%' IDENTIFIED BY '1234';
GRANT ALL PRIVILEGES ON intereal.* to intereal_admin@'%';
FLUSH PRIVILEGES;
USE intereal;
DROP TABLE IF EXISTS users;
CREATE TABLE users(
	user_id VARCHAR(20) NOT NULL,
	user_pw VARCHAR(50) NOT NULL,
	user_name VARCHAR(30) NOT NULL,
	address VARCHAR(50) NOT NULL,
	email_address VARCHAR(20) NOT NULL,
	payment_id VARCHAR(20) NOT NULL,
	role BOOLEAN NOT NULL,
	PRIMARY KEY (user_id)
);
DROP TABLE IF EXISTS model;
CREATE TABLE model
(
	model_id VARCHAR(20) NOT NULL,
	user_id VARCHAR(20) NOT NULL,
	model_file VARCHAR(30) NOT NULL,
	add_date DATE NOT NULL,
	roomInfo_file VARCHAR(30) NOT NULL,
	roomname VARCHAR(20) NOT NULL,
	PRIMARY KEY (model_id),
	FOREIGN KEY (user_id) REFERENCES USERs(user_id)
);
DROP TABLE IF EXISTS product;
CREATE TABLE product
(
	product_id VARCHAR(20) NOT NULL,
	product_name VARCHAR(20) NOT NULL,
	product_file VARCHAR(30) NOT NULL,
	company VARCHAR(20) NOT NULL,
	width FLOAT NOT NULL,
	height FLOAT NOT NULL,
	depth FLOAT NOT NULL,
	color VARCHAR(20) NOT NULL,
	category VARCHAR(20) NOT NULL,
	PRIMARY KEY (product_id)
);
DROP TABLE IF EXISTS cart;
CREATE TABLE cart
(
	cart_id VARCHAR(20) NOT NULL,
	user_id VARCHAR(20) NOT NULL,
	product_id VARCHAR(20) NOT NULL,
	FOREIGN KEY (user_id) REFERENCES USERs(user_id),
	FOREIGN KEY (product_id) REFERENCES PRODUCT(product_id)
);
DROP TABLE IF EXISTS payment;
CREATE TABLE payment
(
	payment_id VARCHAR(20) NOT NULL,
	user_id VARCHAR(20) NOT NULL,
	postal_code VARCHAR(20) NOT NULL,
	card_company VARCHAR(10) NOT NULL,
	card_number VARCHAR(20) NOT NULL,
	valid_month INTEGER NOT NULL,
	valid_year INTEGER NOT NULL,
	CVC INTEGER NOT NULL,
	payment_pw VARCHAR(50) NOT NULL,
	PRIMARY KEY (payment_id),
	FOREIGN KEY (user_id) REFERENCES USERs(user_id)
);
DROP TABLE IF EXISTS purchase;
CREATE TABLE purchase
(
	purchase_id VARCHAR(20) NOT NULL,
	user_id VARCHAR(20) NOT NULL,
	product_id VARCHAR(20) NOT NULL,
	payment_id VARCHAR(20) NOT NULL,
	purchase_date DATE NOT NULL,
	purchase_status VARCHAR(10) NOT NULL,
	total_cost INTEGER NOT NULL,
	FOREIGN KEY (user_id) REFERENCES USERs(user_id),
	FOREIGN KEY (product_id) REFERENCES PRODUCT(product_id),
	FOREIGN KEY (payment_id) REFERENCES PAYMENT(payment_id)
);
DROP TABLE IF EXISTS keyword;
CREATE TABLE keyword
(
	keyword_id VARCHAR(20) NOT NULL,
	product_id VARCHAR(20) NOT NULL,
	FOREIGN KEY (product_id) REFERENCES PRODUCT(product_id)
);
INSERT INTO users(user_id, user_pw, user_name, address, email_address, payment_id, role) VALUES('user1', PASSWORD('user1'), 'user1', 'address1', 'user1@mail.com', 'payment_1', 0);
INSERT INTO users(user_id, user_pw, user_name, address, email_address, payment_id, role) VALUES('user2', PASSWORD('user2'), 'user2', 'address2', 'user2@mail.com', 'payment_2', 0);
INSERT INTO users(user_id, user_pw, user_name, address, email_address, payment_id, role) VALUES('user3', PASSWORD('user3'), 'user3', 'address3', 'user3@mail.com', 'payment_4', 0);
INSERT INTO users(user_id, user_pw, user_name, address, email_address, payment_id, role) VALUES('user4', PASSWORD('user4'), 'user4', 'address4', 'user4@mail.com', 'payment_5', 0);
INSERT INTO users(user_id, user_pw, user_name, address, email_address, payment_id, role) VALUES('user5', PASSWORD('user5'), 'user5', 'address5', 'user5@mail.com', 'payment_6', 0);
INSERT INTO users(user_id, user_pw, user_name, address, email_address, payment_id, role) VALUES('user6', PASSWORD('user6'), 'user6', 'address6', 'user6@mail.com', 'payment_7', 1);

INSERT INTO model(model_id, user_id, model_file, add_date, roomInfo_file, roomname) VALUES('model_1', 'user1', 'model_1.obj', SYSDATE(), 'roomInfo_model_1.txt', 'room1');
INSERT INTO model(model_id, user_id, model_file, add_date, roomInfo_file, roomname) VALUES('model_2', 'user1', 'model_2.obj', SYSDATE(), 'roomInfo_model_2.txt', 'room2');
INSERT INTO model(model_id, user_id, model_file, add_date, roomInfo_file, roomname) VALUES('model_3', 'user2', 'model_3.obj', SYSDATE(), 'roomInfo_model_3.txt', 'room3');
INSERT INTO model(model_id, user_id, model_file, add_date, roomInfo_file, roomname) VALUES('model_4', 'user4', 'model_4.obj', SYSDATE(), 'roomInfo_model_4.txt', 'room4');

INSERT INTO product(product_id, product_name, product_file, company, width, height, depth, color, category) VALUES('product_1', 'product_name_1', 'product_1.obj', 'IKEA', 10, 10, 10, 'BLACK', 'TABLE');
INSERT INTO product(product_id, product_name, product_file, company, width, height, depth, color, category) VALUES('product_2', 'product_name_2', 'product_2.obj', 'IKEA', 3, 1.5, 0.5, 'WHITE', 'DRAWER');
INSERT INTO product(product_id, product_name, product_file, company, width, height, depth, color, category) VALUES('product_3', 'product_name_3', 'product_3.obj', 'IKEA', 2.3, 4.4, 7.2, 'RED', 'CHAIR');
INSERT INTO product(product_id, product_name, product_file, company, width, height, depth, color, category) VALUES('product_4', 'product_name_4', 'product_4.obj', 'IKEA', 10, 10, 10, 'BLUE', 'TABLE');
INSERT INTO product(product_id, product_name, product_file, company, width, height, depth, color, category) VALUES('product_5', 'product_name_5', 'product_5.obj', 'HANSEM', 4, 1, 1.1, 'YELLOW', 'TABLE');
INSERT INTO product(product_id, product_name, product_file, company, width, height, depth, color, category) VALUES('product_6', 'product_name_6', 'product_6.obj', 'HANSEM', 1.3, 0.8, 0.8, 'BROWN', 'CHAIR');
INSERT INTO product(product_id, product_name, product_file, company, width, height, depth, color, category) VALUES('product_7', 'product_name_7', 'product_7.obj', 'HANSEM', 3, 1, 1, 'BROWN', 'LIGHT');
INSERT INTO product(product_id, product_name, product_file, company, width, height, depth, color, category) VALUES('product_8', 'product_name_8', 'product_8.obj', 'ILOOM', 1.8, 1.8, 2, 'BROWN', 'CLOSET');
INSERT INTO product(product_id, product_name, product_file, company, width, height, depth, color, category) VALUES('product_9', 'product_name_9', 'product_9.obj', 'ILOOM', 1.5, 1, 1.5, 'WHITE', 'TABLE');

INSERT INTO cart(cart_id, user_id, product_id) VALUES('cart_1', 'user1', 'product_1');
INSERT INTO cart(cart_id, user_id, product_id) VALUES('cart_1', 'user1', 'product_3');
INSERT INTO cart(cart_id, user_id, product_id) VALUES('cart_1', 'user1', 'product_4');
INSERT INTO cart(cart_id, user_id, product_id) VALUES('cart_2', 'user3', 'product_2');
INSERT INTO cart(cart_id, user_id, product_id) VALUES('cart_2', 'user3', 'product_3');
INSERT INTO cart(cart_id, user_id, product_id) VALUES('cart_2', 'user3', 'product_8');
INSERT INTO cart(cart_id, user_id, product_id) VALUES('cart_3', 'user4', 'product_5');
INSERT INTO cart(cart_id, user_id, product_id) VALUES('cart_3', 'user4', 'product_6');

INSERT INTO payment(payment_id, user_id, postal_code, card_company, card_number, valid_month, valid_year, CVC, payment_pw) VALUES('payment_1', 'user1', '12345', 'Kookmin', '1234-1234-1234-1234', 11, 24, 123, PASSWORD('1234'));
INSERT INTO payment(payment_id, user_id, postal_code, card_company, card_number, valid_month, valid_year, CVC, payment_pw) VALUES('payment_2', 'user2', '12345', 'Kookmin', '1234-1234-1234-1234', 11, 24, 123, PASSWORD('1234'));
INSERT INTO payment(payment_id, user_id, postal_code, card_company, card_number, valid_month, valid_year, CVC, payment_pw) VALUES('payment_3', 'user2', '12345', 'Hana', '1234-1234-1234-1234', 11, 24, 123, PASSWORD('1234'));
INSERT INTO payment(payment_id, user_id, postal_code, card_company, card_number, valid_month, valid_year, CVC, payment_pw) VALUES('payment_4', 'user3', '12345', 'Shinhan', '1234-1234-1234-1234', 11, 24, 123, PASSWORD('1234'));
INSERT INTO payment(payment_id, user_id, postal_code, card_company, card_number, valid_month, valid_year, CVC, payment_pw) VALUES('payment_5', 'user4', '12345', 'Woori', '1234-1234-1234-1234', 11, 24, 123, PASSWORD('1234'));
INSERT INTO payment(payment_id, user_id, postal_code, card_company, card_number, valid_month, valid_year, CVC, payment_pw) VALUES('payment_6', 'user5', '12345', 'Kookmin', '1234-1234-1234-1234', 11, 24, 123, PASSWORD('1234'));
INSERT INTO payment(payment_id, user_id, postal_code, card_company, card_number, valid_month, valid_year, CVC, payment_pw) VALUES('payment_7', 'user6', '12345', 'Shinhan', '1234-1234-1234-1234', 11, 24, 123, PASSWORD('1234'));

INSERT INTO purchase(purchase_id, user_id, product_id, payment_id, purchase_date, purchase_status, total_cost) VALUES('purchase_1', 'user1', 'product_1', 'payment_1', SYSDATE(), 'completed', 139990);
INSERT INTO purchase(purchase_id, user_id, product_id, payment_id, purchase_date, purchase_status, total_cost) VALUES('purchase_1', 'user1', 'product_3', 'payment_1', SYSDATE(), 'completed', 139990);
INSERT INTO purchase(purchase_id, user_id, product_id, payment_id, purchase_date, purchase_status, total_cost) VALUES('purchase_1', 'user1', 'product_4', 'payment_1', SYSDATE(), 'completed', 139990);
INSERT INTO purchase(purchase_id, user_id, product_id, payment_id, purchase_date, purchase_status, total_cost) VALUES('purchase_2', 'user2', 'product_2', 'payment_2', SYSDATE(), 'shipping', 55550);
INSERT INTO purchase(purchase_id, user_id, product_id, payment_id, purchase_date, purchase_status, total_cost) VALUES('purchase_3', 'user2', 'product_4', 'payment_2', SYSDATE(), 'completed', 11120);
INSERT INTO purchase(purchase_id, user_id, product_id, payment_id, purchase_date, purchase_status, total_cost) VALUES('purchase_4', 'user2', 'product_3', 'payment_3', SYSDATE(), 'shipping', 455000);
INSERT INTO purchase(purchase_id, user_id, product_id, payment_id, purchase_date, purchase_status, total_cost) VALUES('purchase_5', 'user3', 'product_2', 'payment_4', SYSDATE(), 'shipping', 51000);
INSERT INTO purchase(purchase_id, user_id, product_id, payment_id, purchase_date, purchase_status, total_cost) VALUES('purchase_6', 'user4', 'product_5', 'payment_5', SYSDATE(), 'completed', 23000);
INSERT INTO purchase(purchase_id, user_id, product_id, payment_id, purchase_date, purchase_status, total_cost) VALUES('purchase_7', 'user5', 'product_6', 'payment_6', SYSDATE(), 'shipping', 42300);
INSERT INTO purchase(purchase_id, user_id, product_id, payment_id, purchase_date, purchase_status, total_cost) VALUES('purchase_8', 'user5', 'product_8', 'payment_6', SYSDATE(), 'completed', 19990);
INSERT INTO purchase(purchase_id, user_id, product_id, payment_id, purchase_date, purchase_status, total_cost) VALUES('purchase_9', 'user5', 'product_9', 'payment_6', SYSDATE(), 'completed', 122500);

INSERT INTO keyword(keyword_id, product_id) VALUES('modern','product_1');
INSERT INTO keyword(keyword_id, product_id) VALUES('modern','product_3');
INSERT INTO keyword(keyword_id, product_id) VALUES('modern','product_4');
INSERT INTO keyword(keyword_id, product_id) VALUES('modern','product_5');
INSERT INTO keyword(keyword_id, product_id) VALUES('modern','product_6');
INSERT INTO keyword(keyword_id, product_id) VALUES('modern','product_7');
INSERT INTO keyword(keyword_id, product_id) VALUES('antique','product_2');
INSERT INTO keyword(keyword_id, product_id) VALUES('antique','product_8');
INSERT INTO keyword(keyword_id, product_id) VALUES('antique','product_9');
INSERT INTO keyword(keyword_id, product_id) VALUES('classic','product_1');
INSERT INTO keyword(keyword_id, product_id) VALUES('classic','product_2');

SELECT * FROM users;
SELECT * FROM model;
SELECT * FROM product;
SELECT * FROM cart;
SELECT * FROM purchase;
SELECT * FROM payment;
SELECT * FROM keyword;