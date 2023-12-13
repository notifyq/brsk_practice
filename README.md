# Практика

## Установка postgresql
sudo apt update

sudo apt install postgresql

sudo -u postgres psql

create user <имя_пользователя> with password '<пароль>';

CREATE DATABASE <имя_базы>;

GRANT ALL PRIVILEGES ON DATABASE <имя_базы> TO <имя_пользователя>;

sudo systemctl restart postgresql

### Настройка удаленного доступа

sudo nano /etc/postgresql/12/main/postgresql.conf

поменять #listen_addresses = 'localhost' на listen_addresses = '*'

### Разрешение подключения из сети

sudo nano /etc/postgresql/12/main/pg_hba.conf


добавить host all <имя_пользователя> <ip_адрес_другой_машины> md5

sudo systemctl restart postgresql

## Перенос проекта на машину:

scp -r /path/to/your/application username@your_server_ip:/path/to/destination


## Публикация проекта:

mkdir /home/(user_name)/(название папки)

dotnet publish -o /home/(user_name)/(название папки)

## Демонизация systemctl

### Создание сервиса:

sudo nano /etc/systemd/system/app.service


### Конфигурация файла:


[Unit] 

Description= mvcnew webapp


[Service] 

WorkingDirectory=/home/(user_name)/(название папки)

ExecStart=/usr/bin/dotnet /home/(user_name)/(название папки)/(название_файла).dll

Restart=always

#Restart service after 10 seconds if the dotnet service crashes:

RestartSec=10

SyslogIdentifier=mvcnew

Environment=ASPNETCORE_ENVIRONMENT=Production


[Install]

WantedBy=multi-user.target



### Команды для запуска:

sudo systectl enable app.service

sudo systectl start app.service


## Настройка nginx

server {

    listen 443 ssl;
    
    server_name myproject_server_api;

    ssl_certificate /usr/local/share/ca-certificates/example_api.crt;
    
    ssl_certificate_key /usr/local/share/ca-certificates/example_api.key;

    location / {
    
        proxy_pass         https://localhost:8080;
        
        proxy_http_version 1.1;
        
        proxy_set_header   Upgrade $http_upgrade;
        
        proxy_set_header   Connection keep-alive;
        
        proxy_set_header   Host $host;
        
        proxy_cache_bypass $http_upgrade;
        
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        
        proxy_set_header   X-Forwarded-Proto $scheme;
        
    }
}
