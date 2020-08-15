set -e
mongoimport --collection ratings --db bookinfo mongodb://localhost /app/data/ratings_data