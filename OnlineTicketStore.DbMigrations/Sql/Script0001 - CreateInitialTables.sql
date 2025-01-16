CREATE TABLE public."Category"
(
    "Id"   SMALLSERIAL NOT NULL,
    "Name" TEXT        NOT NULL,
    PRIMARY KEY ("Id")
);

CREATE TABLE public."Product"
(
    "Id"          BIGSERIAL NOT NULL,
    "Name"        TEXT      NOT NULL,
    "CategoryId"  smallint  NOT NULL,
    "Description" TEXT,
    "ImageFile"   BYTEA,
    "Price"       NUMERIC   NOT NULL,
    PRIMARY KEY ("Id"),
    FOREIGN KEY ("CategoryId")
        REFERENCES public."Category" ("Id")
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

CREATE
OR REPLACE PROCEDURE public.spaddproduct(
    IN p_name TEXT,
    IN p_categoryid smallint,
    IN p_description TEXT,
    IN p_imagefile BYTEA,
    IN p_price NUMERIC,
    OUT productid BIGINT)
LANGUAGE 'plpgsql'
AS $$
BEGIN
INSERT INTO public."Product" ("Name", "CategoryId", "Description", "ImageFile", "Price")
VALUES (p_name, p_categoryid, p_description, p_imagefile, p_price) RETURNING "Id"
INTO productid;
END;
$$;
