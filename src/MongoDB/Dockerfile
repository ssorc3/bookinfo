FROM mongo
WORKDIR /app/data/
COPY ratings_data /app/data/
COPY script.sh /docker-entrypoint-initdb.d/
RUN chmod +x /docker-entrypoint-initdb.d/script.sh