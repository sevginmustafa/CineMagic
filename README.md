# ASP.Net Core CineMagic
[![Build Status](https://dev.azure.com/sevgin1996/CineMagic/_apis/build/status/sevginmustafa.CineMagic?branchName=main)](https://dev.azure.com/sevgin1996/CineMagic/_build/latest?definitionId=2&branchName=main)
[![GitHub license](https://img.shields.io/github/license/sevginmustafa/CineMagic?color=brightgreen)](https://github.com/sevginmustafa/CineMagic/blob/main/LICENSE)

**CineMagic** is my defense project for **ASP.NET Core** course at [SoftUni](https://softuni.bg/). It is a ready-to-use ASP.NET Core application.

# Link
https://cinemagic.azurewebsites.net

## :pencil2: Project Description
CineMagic is a web application written using ASP.NET Core that provides a modern graphical interface with a lot of functionality that can be useful for the users. At the top, there is a convenient navigation bar with the following menus: "Home", "Movies", "Contacts", "Genres", "Countries", "Actors", "Directors" last four with drop-down menus. On the right side there is a global search engine which searches by movie titles and people's names by returning data based on the user-specified "keyword" in a separate page with all the results found for the categories: movies, actors and directors. At the bottom of each page there is a footer containing some grids for filtering movies by year of release with options for the last five years, as well as the five most popular genres. There are three social media buttons through which you can connect us via: Facebook, Twitter, LinkedIn, YouTube and Google+. There is an integration of fast pop-up form for Login and Register. Each single movie page provides the ability for signed-in users to add/remove movie to/from their Watchlist, rate movies with full or half stars with possible values from 1 to 5, and also the can add comments for all movies, actors and directors!

**_"Home" page description_**
In a interactive banner section is displayed one of the ten latest movies from the database at random, on the principle of automatic slideshow with four backdrop images! The home page provides horizontal tab with three button sections for: recently added movies, the most popular movies, and the highest rated movies. In parallel for each section, the top movie for each of the charts is shown with more detailed information and with the possibility to play a trailer of the respective movie!
In the lower section, the latest ten movies are shown, based on an automatic slideshow with Previous" and "Next" buttons for manual change! Separately, if the user is logged in, the movies he has added to his Watchlist (if any) are also displayed.

**_"Movies" page description:_**
The page consists of a paginated table with 20 movies per page and very detailed information about all the movies sorted in alphabetical order! A tab panel containing the letters of the Latin alphabet has been added, as well as an option to illustrate the titles starting with numbers by pressing the "0-9" button. For the convenience of users, a search engine has been added, which searches the entire database for titles containing the set "keyword". Also added a field for filtering the data in the specific page, which, unlike the search engine, filters the data searching by "keyword" in absolutely all columns of the table.

**_"Actors" and "Directors" pages description:_**
Both pages work on the same principle! They can be accessed from the site's navigation bar, and via a drop-down menu the user can choose one of three options:
- "All Actors/Directors" - all Actors/Directors are displayed in a tabular version, similar to the one used in the "Movies" page, also containing a search engine and a filter field;
- "Born Today" - displays all Actors/Directors who were born on this day, with the subsequent ability to filter the results by gender;
- "Most Popular Actors/Directors!" - a page with the 50 most popular Actors/Directors is displayed.

**_"Contacts" page description:_**
When accessing the page from the navigation bar, a contact form is displayed, through which users can contact the organization or individual providing the website to send their questions, inquiries, as well as to report any problems in their work with the site. The page also provides a phone number and email for feedback!

**_"Administration" area description:_**
Here, like any admin area, users who are registered as site administrators can change its settings, add, edit and delete entities, as well as reply to questions and inquiries addressed to the site by users through the contact form! There is also an option to gather data from Internet and filling the database through the option "Gather Data", by submitting the "Start" (filled in automatically based on the index of the last added movie) and "End" indexes;


## Unit tests Code coverage

![Code coverage](https://github.com/sevginmustafa/CineMagic/blob/main/test%20coverage.png)

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


## :floppy_disk: DB Diagram
![](https://github.com/sevginmustafa/CineMagic/blob/main/CineMagicDbDiagram.png)

## Author

[Sevgin Mustafa](https://github.com/sevginmustafa)
- Facebook: [@Севгин Мустафа](https://www.facebook.com/profile.php?id=100004996548202)

## Template authors

- [Nikolay Kostov](https://github.com/NikolayIT)
- [Vladislav Karamfilov](https://github.com/vladislav-karamfilov)
- [Stoyan Shopov](https://github.com/StoyanShopov)


## LICENCE

This project is licensed with the [MIT license](LICENSE)
