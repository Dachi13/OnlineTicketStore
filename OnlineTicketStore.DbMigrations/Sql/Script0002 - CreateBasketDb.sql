CREATE TABLE public."Baskets"
(
    "Id" bigint NOT NULL,
    "TicketId" bigint NOT NULL,
    "Amount" integer NOT NULL DEFAULT 1,
    "Price" numeric,
    "DiscountId" bigint,
    "IsDeleted" boolean NOT NULL DEFAULT false,
    PRIMARY KEY ("Id")
);