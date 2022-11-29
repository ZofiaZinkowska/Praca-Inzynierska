FROM node:16.17.1-alpine
WORKDIR /app
EXPOSE 5173

COPY package.json .
COPY package-lock.json .
RUN npm ci

COPY . .
RUN npm run build
ENTRYPOINT ["npm", "run", "prod"]