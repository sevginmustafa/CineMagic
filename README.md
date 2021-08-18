# ASP.Net Core CineMagic
[![Build Status](https://nikolayit.visualstudio.com/AspNetCoreTemplate/_apis/build/status/NikolayIT.ASP.NET-Core-Template?branchName=master)](https://nikolayit.visualstudio.com/AspNetCoreTemplate/_build/latest?definitionId=15&branchName=master)
[![GitHub license](https://img.shields.io/github/license/sevginmustafa/CineMagic?color=brightgreen)](https://github.com/stanislavstoyanov99/CinemaWorld/blob/master/LICENSE)

**CineMagic** is my defense project for **ASP.NET Core** course at [SoftUni](https://softuni.bg/). It is a ready-to-use ASP.NET Core application.

# Link
https://cinemagic.azurewebsites.net

## :pencil2: Project Description
CineMagic is a web application written using ASP.NET Core that provides a modern graphical interface with a lot of functionality that can be useful for the users. At the top, there is a convenient navigation bar with the following menus: "Home", "Movies", "Contacts", "Genres", "Countries", "Actors", "Directors" last four with drop-down menus. On the right side there is a global search engine which searches by movie titles and people's names by returning data based on the user-specified "keyword" in a separate page with all the results found for the categories: movies, actors and directors. At the bottom of each page there is a footer containing some grids for filtering movies by year of release with options for the last five years, as well as the five most popular genres. There are three social media buttons through which you can connect us via: Facebook, Twitter, LinkedIn, YouTube and Google+.

**_"Home" page description_**
In a interactive banner section is displayed one of the ten latest movies from the database at random, on the principle of automatic slideshow alternate four backdrop images! The home page provides horizontal tab with three button sections for: recently added movies, the most popular movies, and the movies with the highest user ratings. In parallel for each section, the top movie for each of the charts is shown with more detailed information and with the possibility to play a trailer of the respective movie!
In the lower section, the latest ten movies are shown, based on an automatic slideshow with "previous" and "next" buttons for manual change! Separately, if the user is logged in, the movies he has added to his Watchlist (if any) are also displayed.

**_"Movies" page description:_**
The page consists of a paginated table with very detailed information about all the movies in alphabetical order! A tab panel containing the letters of the Latin alphabet has been added, as well as an option to illustrate the titles starting with numbers by pressing the "0 - 9" button. For the convenience of users, a search engine has been added, which searches the entire database for titles containing the set "keyword". Also added a field for filtering the data in the specific page, which, unlike the search engine, filters the data searching by "keyword" in absolutely all columns of the table.



## Unit tests Code coverage

![Code coverage]()

## :hammer_and_wrench: Build with
* [ASP.NET CORE 5.0](https://github.com/dotnet/aspnetcore)
* ASP.NET Core areas
* [Entity Framework CORE 5.0](https://github.com/dotnet/efcore)
* [MSSQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)
* [SendGrid](https://github.com/sendgrid)
* [HtmlSanitizer](https://github.com/mganss/HtmlSanitizer)
* [TinyMCE](https://github.com/tinymce/)
* [Bootstrap](https://github.com/twbs/bootstrap)
* AJAX Requests
* [jQuery](https://github.com/jquery/jquery) and any kind of jQuery plugins like: [star-rating-svg](https://github.com/nashio/star-rating-svg), [multiple-select](https://github.com/wenzhixin/multiple-select)
* JavaScript and JS animations
* [xUnit](https://github.com/xunit/xunit)
* [Moq](https://github.com/moq/moq)
* In-Memmory Cache


## :floppy_disk: Database Diagram
![]()

## Author

[Sevgin Mustafa](https://github.com/sevginmustafa)
- Facebook: [@Севгин Мустафа](https://www.facebook.com/profile.php?id=100004996548202)

## Template authors

- [Nikolay Kostov](https://github.com/NikolayIT)
- [Vladislav Karamfilov](https://github.com/vladislav-karamfilov)
- [Stoyan Shopov](https://github.com/StoyanShopov)


## LICENCE

This project is licensed with the [MIT license](LICENSE)
