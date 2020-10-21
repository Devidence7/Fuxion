# Fuxion

## Installation
### In Windows:
### 1. Install Rabbit MQ

* Open Power Shell in admin mode

* Install Choco (https://chocolatey.org/docs/installation)

```
Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
```

```
choco install rabbitmq
```

* Go to ...\RabbitMQ Server\rabbitmq_server-3.8.5\sbin and execute in cmd (in administrative mode):

```
rabbitmq-plugins enable rabbitmq_management
```

* Now you should be able to connect to http://localhost:15672/

### 2. Install Telerik UI for WPF for Visual Studio

### 3. Install SwitchStartupProject Extension in Visual Studio

### 4. With the extension: Launch Ordinem -> This launchs 4 projects