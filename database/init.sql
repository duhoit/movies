DROP ROLE IF EXISTS psqlman;
CREATE ROLE psqlman WITH
	LOGIN
	NOSUPERUSER
	NOCREATEDB
	NOCREATEROLE
	INHERIT
	NOREPLICATION
	CONNECTION LIMIT -1
	PASSWORD 'Countdown10';


CREATE DATABASE movies;
\c movies;

CREATE TABLE public."movie"
(
    "id" text NOT NULL,
    "title" text,
    "isCompleted" boolean,
    "genre" date,
    PRIMARY KEY ("Id")
);

ALTER TABLE IF EXISTS public."movie"
    OWNER to psqlman;