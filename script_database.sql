-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mystore
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mystore
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mystore` DEFAULT CHARACTER SET utf8 ;
USE `mystore` ;

-- -----------------------------------------------------
-- Table `mystore`.`tb_customers`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mystore`.`tb_customers` (
  `id` BIGINT(20) NOT NULL AUTO_INCREMENT,
  `document` VARCHAR(20) NOT NULL,
  `name` VARCHAR(60) NOT NULL,
  `email` VARCHAR(120) NOT NULL,
  `birthday` DATE NOT NULL,
  `created_at` DATETIME NULL,
  `updated_at` DATETIME NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `document_UNIQUE` (`document` ASC) ,
  UNIQUE INDEX `email_UNIQUE` (`email` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mystore`.`tb_products`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mystore`.`tb_products` (
  `id` BIGINT(20) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(80) NOT NULL,
  `code` VARCHAR(20) NOT NULL,
  `created_at` DATETIME NULL,
  `updated_at` DATETIME NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `code_UNIQUE` (`code` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mystore`.`tb_skus`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mystore`.`tb_skus` (
  `id` BIGINT(20) NOT NULL AUTO_INCREMENT,
  `product_id` BIGINT(20) NOT NULL,
  `unit` VARCHAR(20) NOT NULL,
  `brand` VARCHAR(40) NOT NULL,
  `barcode` VARCHAR(16) NOT NULL,
  `price` DECIMAL(20,2) NOT NULL,
  `created_at` DATETIME NULL,
  `updated_at` DATETIME NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_tb_skus_tb_products_idx` (`product_id` ASC) ,
  UNIQUE INDEX `barcode_UNIQUE` (`barcode` ASC) ,
  CONSTRAINT `fk_tb_skus_tb_products`
    FOREIGN KEY (`product_id`)
    REFERENCES `mystore`.`tb_products` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mystore`.`tb_orders`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mystore`.`tb_orders` (
  `id` BIGINT(20) NOT NULL AUTO_INCREMENT,
  `customer_id` BIGINT(20) NOT NULL,
  `number` VARCHAR(20) NOT NULL,
  `date_exp` DATETIME NOT NULL,
  `authorize_code` VARCHAR(60) NULL,
  `status_id` SMALLINT UNSIGNED NOT NULL,
  `created_at` DATETIME NULL,
  `updated_at` DATETIME NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_tb_orders_tb_customers1_idx` (`customer_id` ASC) ,
  UNIQUE INDEX `number_UNIQUE` (`number` ASC) ,
  UNIQUE INDEX `authorize_code_UNIQUE` (`authorize_code` ASC) ,
  CONSTRAINT `fk_tb_orders_tb_customers1`
    FOREIGN KEY (`customer_id`)
    REFERENCES `mystore`.`tb_customers` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mystore`.`tb_order_items`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mystore`.`tb_order_items` (
  `id` BIGINT(20) NOT NULL,
  `order_id` BIGINT(20) NOT NULL,
  `product_id` BIGINT(20) NOT NULL,
  `sequence` SMALLINT UNSIGNED NOT NULL,
  `amount` DECIMAL(20,2) NOT NULL,
  `cost` DECIMAL(20,2) NOT NULL,
  `created_at` DATETIME NULL,
  `updated_at` DATETIME NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_tb_order_items_tb_orders1_idx` (`order_id` ASC) ,
  INDEX `fk_tb_order_items_tb_products1_idx` (`product_id` ASC) ,
  CONSTRAINT `fk_tb_order_items_tb_orders1`
    FOREIGN KEY (`order_id`)
    REFERENCES `mystore`.`tb_orders` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_tb_order_items_tb_products1`
    FOREIGN KEY (`product_id`)
    REFERENCES `mystore`.`tb_products` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mystore`.`tb_invoices`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mystore`.`tb_invoices` (
  `id` BIGINT(20) NOT NULL AUTO_INCREMENT,
  `order_id` BIGINT(20) NOT NULL,
  `customer_id` BIGINT(20) NOT NULL,
  `number` VARCHAR(20) NOT NULL,
  `date_exp` DATE NOT NULL,
  `created_at` DATETIME NULL,
  `updated_at` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_tb_invoices_tb_orders1_idx` (`order_id` ASC) ,
  UNIQUE INDEX `number_UNIQUE` (`number` ASC) ,
  INDEX `fk_tb_invoices_tb_customers1_idx` (`customer_id` ASC) ,
  CONSTRAINT `fk_tb_invoices_tb_orders1`
    FOREIGN KEY (`order_id`)
    REFERENCES `mystore`.`tb_orders` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_tb_invoices_tb_customers1`
    FOREIGN KEY (`customer_id`)
    REFERENCES `mystore`.`tb_customers` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mystore`.`tb_invoice_items`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mystore`.`tb_invoice_items` (
  `id` BIGINT(20) NOT NULL AUTO_INCREMENT,
  `invoice_id` BIGINT(20) NOT NULL,
  `product_id` BIGINT(20) NOT NULL,
  `sequence` SMALLINT UNSIGNED NOT NULL,
  `amount` DECIMAL(20,2) NOT NULL,
  `cost` DECIMAL(20,2) NOT NULL,
  `created_at` DATETIME NULL,
  `updated_at` DATETIME NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_tb_invoice_items_tb_invoices1_idx` (`invoice_id` ASC) ,
  INDEX `fk_tb_invoice_items_tb_products1_idx` (`product_id` ASC) ,
  CONSTRAINT `fk_tb_invoice_items_tb_invoices1`
    FOREIGN KEY (`invoice_id`)
    REFERENCES `mystore`.`tb_invoices` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_tb_invoice_items_tb_products1`
    FOREIGN KEY (`product_id`)
    REFERENCES `mystore`.`tb_products` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
