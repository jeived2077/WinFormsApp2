СКРИПТ создание бд и добавление данных
-- Creating database for materials management
CREATE DATABASE IF NOT EXISTS mosaic_materials;
USE mosaic_materials;

-- Creating table for material types
CREATE TABLE material_type (
    material_type_id INT AUTO_INCREMENT PRIMARY KEY,
    type_name VARCHAR(50) NOT NULL,
    loss_percentage DECIMAL(5,2) NOT NULL,
    UNIQUE (type_name)
);

-- Creating table for suppliers
CREATE TABLE supplier (
    supplier_id INT AUTO_INCREMENT PRIMARY KEY,
    supplier_name VARCHAR(100) NOT NULL,
    supplier_type VARCHAR(10) NOT NULL,
    inn VARCHAR(12) NOT NULL,
    rating INT NOT NULL CHECK (rating >= 0 AND rating <= 10),
    start_date DATE NOT NULL,
    UNIQUE (inn)
);

-- Creating table for materials
CREATE TABLE material (
    material_id INT AUTO_INCREMENT PRIMARY KEY,
    material_name VARCHAR(100) NOT NULL,
    material_type_id INT NOT NULL,
    unit_price DECIMAL(10,2) NOT NULL CHECK (unit_price >= 0),
    stock_quantity DECIMAL(10,2) NOT NULL CHECK (stock_quantity >= 0),
    min_quantity DECIMAL(10,2) NOT NULL CHECK (min_quantity >= 0),
    package_quantity INT NOT NULL CHECK (package_quantity > 0),
    unit_measure VARCHAR(10) NOT NULL,
    FOREIGN KEY (material_type_id) REFERENCES material_type(material_type_id) ON DELETE RESTRICT,
    UNIQUE (material_name)
);

-- Creating table for material-supplier relationships
CREATE TABLE material_supplier (
    material_id INT NOT NULL,
    supplier_id INT NOT NULL,
    PRIMARY KEY (material_id, supplier_id),
    FOREIGN KEY (material_id) REFERENCES material(material_id) ON DELETE CASCADE,
    FOREIGN KEY (supplier_id) REFERENCES supplier(supplier_id) ON DELETE CASCADE
);

-- Creating table for product types
CREATE TABLE product_type (
    product_type_id INT AUTO_INCREMENT PRIMARY KEY,
    type_name VARCHAR(50) NOT NULL,
    product_coefficient DECIMAL(5,2) NOT NULL,
    UNIQUE (type_name)
);

-- Inserting data into material_type
INSERT INTO material_type (type_name, loss_percentage) VALUES
('Пластичные материалы', 0.12),
('Добавка', 0.20),
('Электролит', 0.15),
('Глазурь', 0.30),
('Пигмент', 0.25);

-- Inserting data into supplier
INSERT INTO supplier (supplier_name, supplier_type, inn, rating, start_date) VALUES
('БрянскСтройресурс', 'ЗАО', '9432455179', 8, '2015-12-20'),
('Стройкомплект', 'ЗАО', '7803888520', 7, '2017-09-13'),
('Железногорская руда', 'ООО', '8430391035', 7, '2016-12-23'),
('Белая гора', 'ООО', '4318170454', 8, '2019-05-27'),
('Тульский обрабатывающий завод', 'ООО', '7687851800', 7, '2015-06-16'),
('ГорТехРазработка', 'ПАО', '6119144874', 9, '2021-12-27'),
('Сапфир', 'ОАО', '1122170258', 3, '2022-04-10'),
('ХимБытСервис', 'ПАО', '8355114917', 5, '2022-03-13'),
('ВоронежРудоКомбинат', 'ОАО', '3532367439', 8, '2023-11-11'),
('Смоленский добывающий комбинат', 'ОАО', '2362431140', 3, '2018-11-23'),
('МосКарьер', 'ПАО', '4159215346', 2, '2012-07-07'),
('КурскРесурс', 'ЗАО', '9032455179', 4, '2021-07-23'),
('Нижегородская разработка', 'ОАО', '3776671267', 9, '2016-05-23'),
('Речная долина', 'ОАО', '7447864518', 8, '2015-06-25'),
('Карелия добыча', 'ПАО', '9037040523', 6, '2017-03-09'),
('Московский ХимЗавод', 'ПАО', '6221520857', 4, '2015-05-07'),
('Горная компания', 'ЗАО', '2262431140', 3, '2020-12-22'),
('Минерал Ресурс', 'ООО', '4155215346', 7, '2015-05-22'),
('Арсенал', 'ЗАО', '3961234561', 5, '2010-11-25'),
('КамчаткаСтройМинералы', 'ЗАО', '9600275878', 7, '2016-12-20');

-- Inserting data into material
INSERT INTO material (material_name, material_type_id, unit_price, stock_quantity, min_quantity, package_quantity, unit_measure) VALUES
('Глина', 1, 15.29, 1570.00, 5500.00, 30, 'кг'),
('Каолин', 1, 18.20, 1030.00, 3500.00, 25, 'кг'),
('Гидрослюда', 1, 17.20, 2147.00, 3500.00, 25, 'кг'),
('Монтмориллонит', 1, 17.67, 3000.00, 3000.00, 30, 'кг'),
('Перлит', 2, 13.99, 150.00, 1000.00, 50, 'л'),
('Стекло', 2, 2.40, 3000.00, 1500.00, 500, 'кг'),
('Дегидратированная глина', 2, 21.95, 3000.00, 2500.00, 20, 'кг'),
('Шамот', 2, 27.50, 2300.00, 1960.00, 20, 'кг'),
('Техническая сода', 3, 54.55, 1200.00, 1500.00, 25, 'кг'),
('Жидкое стекло', 3, 76.59, 500.00, 1500.00, 15, 'кг'),
('Кварц', 4, 375.96, 1500.00, 2500.00, 10, 'кг'),
('Полевой шпат', 4, 15.99, 750.00, 1500.00, 100, 'кг'),
('Краска-раствор', 5, 200.90, 1496.00, 2500.00, 5, 'л'),
('Порошок цветной', 5, 84.39, 511.00, 1750.00, 25, 'кг'),
('Кварцевый песок', 2, 4.29, 3000.00, 1600.00, 50, 'кг'),
('Жильный кварц', 2, 18.60, 2556.00, 1600.00, 25, 'кг'),
('Барий углекислый', 4, 303.64, 340.00, 1500.00, 25, 'кг'),
('Бура техническая', 4, 125.99, 165.00, 1300.00, 25, 'кг'),
('Углещелочной реагент', 3, 3.45, 450.00, 1100.00, 25, 'кг'),
('Пирофосфат натрия', 3, 700.99, 356.00, 1200.00, 25, 'кг');

-- Inserting data into product_type
INSERT INTO product_type (type_name, product_coefficient) VALUES
('Тип продукции 1', 1.20),
('Тип продукции 2', 8.59),
('Тип продукции 3', 3.45),
('Тип продукции 4', 5.60);

-- Inserting data into material_supplier
INSERT INTO material_supplier (material_id, supplier_id)
SELECT m.material_id, s.supplier_id
FROM material m
JOIN supplier s ON s.supplier_name IN (
    SELECT Поставщик
    FROM (
        SELECT 'Краска-раствор' AS Наименование_материала, 'Арсенал' AS Поставщик UNION
        SELECT 'Каолин', 'Железногорская руда' UNION
        SELECT 'Каолин', 'ВоронежРудоКомбинат' UNION
        SELECT 'Стекло', 'Арсенал' UNION
        -- Add all other material-supplier pairs from Material_suppliers_import.xlsx
        SELECT 'Полевой шпат', 'Смоленский добывающий комбинат'
    ) ms
    WHERE ms.Наименование_материала = m.material_name
);
