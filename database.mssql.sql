
-- SQLINES DEMO *** orward Engineering

/* SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0; */
/* SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0; */
/* SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION'; */

-- SQLINES DEMO *** ------------------------------------
-- Schema POC_RABBITMQ
-- SQLINES DEMO *** ------------------------------------

-- SQLINES DEMO *** ------------------------------------
-- Schema POC_RABBITMQ


-- SQLINES DEMO *** ------------------------------------
-- SQLINES DEMO *** tb_customers`
-- SQLINES DEMO *** ------------------------------------
-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE tb_customers (
  [id] BIGINT NOT NULL IDENTITY,
  [document] VARCHAR(20) NOT NULL,
  [name] VARCHAR(60) NOT NULL,
  [email] VARCHAR(120) NOT NULL,
  [birthday] DATE NOT NULL,
  [created_at] DATETIME2(7) NULL,
  [updated_at] DATETIME2(7) NULL,
  PRIMARY KEY ([id]),
  CONSTRAINT [document_UNIQUE] UNIQUE ([document] ASC) ,
  CONSTRAINT [email_UNIQUE] UNIQUE ([email] ASC) )
;


-- SQLINES DEMO *** ------------------------------------
-- SQLINES DEMO *** tb_products`
-- SQLINES DEMO *** ------------------------------------
-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE tb_products (
  [id] BIGINT NOT NULL IDENTITY,
  [name] VARCHAR(80) NOT NULL,
  [code] VARCHAR(20) NOT NULL,
  [created_at] DATETIME2(7) NULL,
  [updated_at] DATETIME2(7) NULL,
  PRIMARY KEY ([id]),
  CONSTRAINT [code_UNIQUE] UNIQUE ([code] ASC) )
;


-- SQLINES DEMO *** ------------------------------------
-- SQLINES DEMO *** tb_skus`
-- SQLINES DEMO *** ------------------------------------
-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE tb_skus (
  [id] BIGINT NOT NULL IDENTITY,
  [product_id] BIGINT NOT NULL,
  [unit] VARCHAR(20) NOT NULL,
  [brand] VARCHAR(40) NOT NULL,
  [barcode] VARCHAR(16) NOT NULL,
  [price] DECIMAL(20,2) NOT NULL,
  [created_at] DATETIME2(7) NULL,
  [updated_at] DATETIME2(7) NULL,
  PRIMARY KEY ([id])
  ,
  CONSTRAINT [barcode_UNIQUE] UNIQUE ([barcode] ASC) ,
  CONSTRAINT [fk_tb_skus_tb_products]
    FOREIGN KEY ([product_id])
    REFERENCES tb_products ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;

CREATE INDEX [fk_tb_skus_tb_products_idx] ON tb_skus ([product_id] ASC);


-- SQLINES DEMO *** ------------------------------------
-- SQLINES DEMO *** tb_orders`
-- SQLINES DEMO *** ------------------------------------
-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE tb_orders (
  [id] BIGINT NOT NULL IDENTITY,
  [customer_id] BIGINT NOT NULL,
  [number] VARCHAR(20) NOT NULL,
  [date_exp] DATETIME2(7) NOT NULL,
  [authorize_date]DATETIME2(7) NULL,
  [created_at] DATETIME2(7) NULL,
  [updated_at] DATETIME2(7) NULL,
  PRIMARY KEY ([id])
  ,
  CONSTRAINT [number_UNIQUE_2] UNIQUE ([number] ASC) ,
  CONSTRAINT [authorize_code_UNIQUE] UNIQUE ([authorize_code] ASC) ,
  CONSTRAINT [fk_tb_orders_tb_customers1]
    FOREIGN KEY ([customer_id])
    REFERENCES tb_customers ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;

CREATE INDEX [fk_tb_orders_tb_customers1_idx] ON tb_orders ([customer_id] ASC);


-- SQLINES DEMO *** ------------------------------------
-- SQLINES DEMO *** tb_order_items`
-- SQLINES DEMO *** ------------------------------------
-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE tb_order_items (
  [id] BIGINT NOT NULL IDENTITY,
  [order_id] BIGINT NOT NULL,
  [product_id] BIGINT NOT NULL,
  [sequence] SMALLINT CHECK ([sequence] > 0) NOT NULL,
  [amount] DECIMAL(20,2) NOT NULL,
  [cost] DECIMAL(20,2) NOT NULL,
  [created_at] DATETIME2(7) NULL,
  [updated_at] DATETIME2(7) NULL,
  PRIMARY KEY ([id])
  ,
  CONSTRAINT [fk_tb_order_items_tb_orders1]
    FOREIGN KEY ([order_id])
    REFERENCES tb_orders ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [fk_tb_order_items_tb_products1]
    FOREIGN KEY ([product_id])
    REFERENCES tb_products ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;

CREATE INDEX [fk_tb_order_items_tb_orders1_idx] ON tb_order_items ([order_id] ASC);
CREATE INDEX [fk_tb_order_items_tb_products1_idx] ON tb_order_items ([product_id] ASC);


-- SQLINES DEMO *** ------------------------------------
-- SQLINES DEMO *** tb_invoices`
-- SQLINES DEMO *** ------------------------------------
-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE tb_invoices (
  [id] BIGINT NOT NULL IDENTITY,
  [order_id] BIGINT NOT NULL,
  [customer_id] BIGINT NOT NULL,
  [number] VARCHAR(20) NOT NULL,
  [date_exp] DATE NOT NULL,
  [created_at] DATETIME2(7) NULL,
  [updated_at] VARCHAR(45) NULL,
  PRIMARY KEY ([id])
  ,
  CONSTRAINT [number_UNIQUE_1] UNIQUE ([number] ASC)
  ,
  CONSTRAINT [fk_tb_invoices_tb_orders1]
    FOREIGN KEY ([order_id])
    REFERENCES tb_orders ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [fk_tb_invoices_tb_customers1]
    FOREIGN KEY ([customer_id])
    REFERENCES tb_customers ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;

CREATE INDEX [fk_tb_invoices_tb_orders1_idx] ON tb_invoices ([order_id] ASC);
CREATE INDEX [fk_tb_invoices_tb_customers1_idx] ON tb_invoices ([customer_id] ASC);


-- SQLINES DEMO *** ------------------------------------
-- SQLINES DEMO *** tb_invoice_items`
-- SQLINES DEMO *** ------------------------------------
-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE tb_invoice_items (
  [id] BIGINT NOT NULL IDENTITY,
  [invoice_id] BIGINT NOT NULL,
  [product_id] BIGINT NOT NULL,
  [sequence] SMALLINT CHECK ([sequence] > 0) NOT NULL,
  [amount] DECIMAL(20,2) NOT NULL,
  [cost] DECIMAL(20,2) NOT NULL,
  [created_at] DATETIME2(7) NULL,
  [updated_at] DATETIME2(7) NULL,
  PRIMARY KEY ([id])
  ,
  CONSTRAINT [fk_tb_invoice_items_tb_invoices1]
    FOREIGN KEY ([invoice_id])
    REFERENCES tb_invoices ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [fk_tb_invoice_items_tb_products1]
    FOREIGN KEY ([product_id])
    REFERENCES tb_products ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;

CREATE INDEX [fk_tb_invoice_items_tb_invoices1_idx] ON tb_invoice_items ([invoice_id] ASC);
CREATE INDEX [fk_tb_invoice_items_tb_products1_idx] ON tb_invoice_items ([product_id] ASC);


/* SET SQL_MODE=@OLD_SQL_MODE; */
/* SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS; */
/* SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS; */
