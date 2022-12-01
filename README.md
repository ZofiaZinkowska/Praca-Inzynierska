# The "plant register" application

## Prerequisites

* Installed and running Docker daemon. You can check that docker is available by running `docker -v` command.
* Support for Docker Compose. You can check it's availability by running `docker-compose -v` command.

It is recommended to use Docker Desktop application (https://www.docker.com/products/docker-desktop/), because it comes with all prerequisites and works out-of-the-box.

## Installation and usage

1. Clone git repository https://github.com/ZofiaZinkowska/Praca-Inzynierska.git

    ```
    git clone https://github.com/ZofiaZinkowska/Praca-Inzynierska.git
    ```

2. Go to `./Praca Inzynierska/aplikacja webowa - praca inzynierska/` folder

    ```
    cd './Praca-Inzynierska/aplikacja webowa - praca inzynierska/'
    ```

3. Build required Docker images

    ```
    docker-compose build
    ```

4. To start the application run the following Docker Compose command

    ```
    docker-compose up -d
    ```

    Once the command completes, the application will be available at http://localhost:5173/.
    
    _This command can be used to ensure the service is running at any time._

4. To stop the application run the following Docker Compose command

    ```
    docker-compose down
    ```
