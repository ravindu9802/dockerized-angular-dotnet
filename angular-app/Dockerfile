# STEP 1:
# build angular app using production config
FROM node:latest AS build

# define working directory
WORKDIR /app

# copy package.json and package-lock.json workdir
COPY package*.json ./

# run a clean dependency install
RUN npm ci

# install angular cli
RUN npm install -g @angular/cli

# copy all files
COPY . .

# build application
RUN npm run build --configuration=production


#STEP 2:
# use nginx image to serve angular app
FROM nginx:latest

# copy .nginx config file to replace default nginx config
COPY ./nginx.conf /etc/nginx/conf.d/default.conf

# copy build files to replace default nginx content
COPY --from=build /app/dist/angular-app/browser /usr/share/nginx/html

# expose port 80
EXPOSE 80


# BUILD: docker build -t angular-nodejs .
# RUN: docker run -d -p 8080:80 angular-nodejs