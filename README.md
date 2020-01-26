# Questor
Questor is a test proof-of-concept project, based on **Asp.Net Core MVC**, which can make requests to different search engines, such as **Google** or **Yandex** and show the results. All searches results are stored in a database, thus can be retreived by "offline" search function.

## Overview
Questor consists of 3 main projects:
* Questor.Web
* Questor.Infrastructure
* Questor.Core

### Questor.Core
A main business logic project, which contains specifications for services, implementations of which are used to maintain primary and auxiliary logic of application. Main service is an `ISearchService` and an 'ISearchEngine'. `ISearchService` describes a main search logic and 'ISearchEngine' is implemented by concrete search engines, such as "GoogleSearchEngine". Main logic of receiving search results for each search engine is described in `ISearchEngine` interface implementations and based on site scraping. Despite search engine type, each of them has `Selector` property. `Selector` describes how to retrieve search results from response. Each engine also has a `Search` method. For all engines this method is quite similar, but has some difference (eg. Bing won't send valid results without cookies in search request).    

### Questor.Infrastructure
A project, which contains infrastructure components, such as repositories, commands, migrations for database and so on.

### Questor.Web
An UI project, which also describes startup of application and contains main entry point.

### Libraries usage
Questor uses some third-party libraries such as:
* Autofac as IoC-container
* MediatR as mediator for commands and queries
* AutoMapper for object mapping
* AngleSharp for HTML-parsing
* Polly for retry functionality

## Run
To run this application just use `docker-compose` commands, which will run an application and a storage (msslq-server-2019) contaniners:
```
docker-compose build
docker-compose up
```
After up complete application can be accessed via :8080 port

# Restrictions

Due to reasons of being simple, Questor uses scraping for getting search results, instead of using official search engines API and do not implenet any proxying logic. So, attempt to do a lot of request may results with error.

## Improvements 
- [ ] more unit tests
- [ ] additional tests (functional, integration)
- [ ] https support
- [ ] full-text search through saved results
- [ ] additional search engines support
- [ ] cached requests and UI improvements
