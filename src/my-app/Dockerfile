# Етап 1: Базовий образ для побудови
FROM node:18 AS build

# Встановлення робочої директорії
WORKDIR /app

# Копіюємо package.json та package-lock.json
COPY package*.json ./

# Встановлюємо залежності
RUN npm install

# Копіюємо весь код проєкту
COPY . .

# Збираємо production-збірку
RUN npm run build

# Етап 2: Образ для розгортання
FROM nginx:alpine

# Копіюємо зібрані файли у Nginx
COPY --from=build /app/build /usr/share/nginx/html

# Експонуємо порт
EXPOSE 80

# Запуск Nginx
CMD ["nginx", "-g", "daemon off;"]
