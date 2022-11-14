CREATE TABLE transactions (
    id bigint NOT NULL,
    uuid uuid NOT NULL,
    "timestamp" timestamp with time zone NOT NULL,
    amount bigint NOT NULL,
    description text NOT NULL,
    added timestamp with time zone NOT NULL,
    account text NOT NULL
);

CREATE SEQUENCE transactions_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER TABLE ONLY transactions ALTER COLUMN id SET DEFAULT nextval('transactions_id_seq'::regclass);

ALTER TABLE ONLY transactions ADD CONSTRAINT transactions_pk PRIMARY KEY (id);

CREATE INDEX transactions_amount_index ON transactions USING btree (amount);

CREATE INDEX transactions_description_index ON transactions USING btree (description);

CREATE UNIQUE INDEX transactions_timestamp_amount_description_uindex ON transactions USING btree ("timestamp", amount, description);

CREATE INDEX transactions_timestamp_index ON transactions USING btree ("timestamp" DESC);

CREATE UNIQUE INDEX transactions_uuid_uindex ON transactions USING btree (uuid);

CREATE FUNCTION readable(bigint) RETURNS numeric
	LANGUAGE SQL
AS $$
SELECT CAST($1 AS numeric) / 1000000
$$;