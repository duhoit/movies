DROP ROLE IF EXISTS postgres;
CREATE ROLE postgres WITH
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
    "id" serial PRIMARY KEY,
    "title" text,
    "isCompleted" boolean,
    "genre" text
);


ALTER TABLE IF EXISTS public."movie"
    OWNER to postgres;