# Практика

## Перенос проекта на машину:

scp -r /path/to/your/application username@your_server_ip:/path/to/destination


## Публикация проекта:

mkdir /home/(user_name)/(название папки)

dotnet publish -o /home/(user_name)/(название папки)

## Демонизация systemctl

Создание сервиса:
sudo nano /etc/systemd/system/app.service

Конфигурация файла:
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
