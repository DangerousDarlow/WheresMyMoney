-- transactions --

CREATE TABLE transactions (
    uuid uuid NOT NULL,
    "timestamp" timestamp with time zone NOT NULL,
    amount bigint NOT NULL,
    description text NOT NULL,
    added timestamp with time zone NOT NULL,
    account text NOT NULL
);

ALTER TABLE ONLY transactions ADD CONSTRAINT transactions_pk PRIMARY KEY (uuid);

CREATE UNIQUE INDEX transactions_uuid_uindex ON transactions USING btree (uuid);

CREATE INDEX transactions_timestamp_index ON transactions USING btree ("timestamp" DESC);

CREATE INDEX transactions_amount_index ON transactions USING btree (amount);

CREATE INDEX transactions_description_index ON transactions USING btree (description);

CREATE UNIQUE INDEX transactions_timestamp_amount_description_uindex ON transactions USING btree ("timestamp", amount, description);


-- tags --

CREATE TABLE tags (
    uuid uuid NOT NULL,
    name text NOT NULL
);

ALTER TABLE ONLY tags ADD CONSTRAINT tags_pk PRIMARY KEY (uuid);

CREATE UNIQUE INDEX tags_uuid_uindex ON tags USING btree (uuid);

CREATE UNIQUE INDEX tags_name_uindex ON tags USING btree (name);


-- functions --

CREATE FUNCTION readable(bigint) RETURNS numeric
	LANGUAGE SQL
AS $$
SELECT CAST($1 AS numeric) / 1000000
$$;