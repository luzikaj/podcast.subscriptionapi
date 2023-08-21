# Podcast Subscription API

## Description

This project is a simple implementation of simulating an API to start and stop user subscription within podcast system.

With fixed 4 hour limit on design, implementation, tests and NFR's (containerization via docker image, comprehensive Readme, github repo creation), the design had to be simplified.

## Assumptions

Due to the above requirements, some assumptions had been made in order to provide a working solution:

* `start`  and `stop` methods were part of requirements, hence the decision to make them both POST methods
* User currently only has a single subscription in the system - cannot add multiple per user
* User is distinguished by ones email address, which is passed via custom HTTP Header - X-Custom-Auth
* `X-Custom-Auth` header simulates Authorization (usually we would switch to real IAM solution to handle user authentication and authorization)
* The persistence layer is abstracted via repository service, so at any point in time it could be substituted with real database logic (SQL/NoSQL database, MessageQueue, or other)

## Evolution

To make it more production ready, next steps to improve the code would be:

* switch to more persistent data storage
* modify the endpoints to allow multiple subscriptions
    * change the concept to more rest approach, treating subscription as an entity, thus changing the endpoints to:
        * PUT /subscription
        * DELETE /subscription/{:id}
    * or add the body payload to POST endpoints with subscription id and/or additional metadata
* implement proper authentication/authorization (OAuth, Bearer token, etc.)
* add logging, metrics, healthchecks

## Usage

### Build docker image

    docker build . -t luzikaj/podcast.subscriptionapi:0.1

### Run docker image

    docker run -it -p 8080:80 luzikaj/podcast.subscriptionapi:0.1

### Run docker in development mode
    docker run -it -p 8080:80 -e ASPNET_ENVIRONMENT='Development' -e DOTNET_ENVIRONMENT='Development'  luzikaj/podcast.subscriptionapi:0.1

This will allow to access SwaggerUI under `http://localhost:8080/swagger/index.html`
