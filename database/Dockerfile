FROM postgres:latest
ENV POSTGRES_USER=postgres
ENV POSTGRES_PASSWORD=Countdown10
ENV POSTGRES_DB=movies

# Copy the initialization script to create the default user
COPY init.sql /docker-entrypoint-initdb.d/
