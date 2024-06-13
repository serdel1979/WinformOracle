/*
PRIMERO CREO EL USUARIO SIENDO ADMIN Y LE DOY LOS PERMISOS

ALTER USER sergio IDENTIFIED BY admin DEFAULT TABLESPACE system TEMPORARY TABLESPACE temp QUOTA UNLIMITED ON system;


grant create session to sergio;
grant create table to sergio;
grant create view to sergio;
grant create PROCEDURE to sergio;
grant create sequence to sergio;

GRANT CREATE TRIGGER TO sergio;
GRANT CREATE SEQUENCE TO sergio;
*/


CREATE TABLE categorias (
    codigo_ca NUMBER(3) GENERATED ALWAYS AS IDENTITY,
    descripcion VARCHAR2(30),
    PRIMARY KEY (codigo_ca)
);


CREATE SEQUENCE seq_codigo_ca
START WITH 1
INCREMENT BY 1;

CREATE TABLE categorias (
    codigo_ca NUMBER(3),
    descripcion VARCHAR2(30),
    PRIMARY KEY (codigo_ca)
);

CREATE OR REPLACE TRIGGER trg_codigo_ca
BEFORE INSERT ON categorias
FOR EACH ROW
BEGIN
    SELECT seq_codigo_ca.NEXTVAL INTO :NEW.codigo_ca FROM dual;
END;



--SELECT * FROM v$version;


create table productos(
codigo_pro NUMBER(3),
descripcion varchar2(50),
marca varchar2(30),
medida varchar2(5),
stock decimal(10,2),
activo char(1),
PRIMARY KEY (codigo_pro));

CREATE SEQUENCE seq_codigo_prod
START WITH 1
INCREMENT BY 1;


CREATE OR REPLACE TRIGGER trg_codigo_prod
BEFORE INSERT ON productos
FOR EACH ROW
BEGIN
    SELECT seq_codigo_prod.NEXTVAL INTO :NEW.codigo_pro FROM dual;
END;


--agrego clave foranea en productos
ALTER TABLE productos
ADD categoria_id NUMBER(3);

ALTER TABLE productos
ADD CONSTRAINT fk_categoria
FOREIGN KEY (categoria_id)
REFERENCES categorias(codigo_ca);



create view vista_productos as
select p.codigo_pro, p.descripcion,p.marca,p.medida,p.stock,p.activo,c.descripcion as categoria
from productos p
inner join categorias c on c.codigo_ca = p.categoria_id;









--averiguo service_name para la cadena de conexion
--SELECT value FROM v$parameter WHERE name = 'service_names';
