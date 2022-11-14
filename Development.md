## Postgres

Export (dump) database schema

```
docker exec -i <container_name> /bin/bash -c "PGPASSWORD=<password> pg_dump --username postgres --schema-only money" > dump.sql
```

Insert a transaction

```
INSERT INTO transactions(
    uuid,
    "timestamp",
    amount,
    description,
    added,
    account
)
VALUES (
    '7b728037-8068-4016-90d3-54070d7b3e0b',
    '2022-11-12 18:08:54Z',
    1000000,
    'description',
    '2022-11-12 18:09:42Z',
    'account'
);
```