namespace CineMagic.Common
{
    public static class ModelValidation
    {
        public static class User
        {
            public const int UsernameMinLength = 3;
            public const int UsernameMaxLength = 30;

            public const int FullNameMinLength = 5;
            public const int FullNameMaxLength = 100;

            public const string UsernameErrorMessage = "Username should be between {2} and {1} symbols";

            public const string FullNameErrorMessage = "Full Name should be between {2} and {1} symbols";
        }

        public static class ContactForm
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;

            public const int SubjectMinLength = 5;
            public const int SubjectMaxLength = 100;

            public const int MessageMinLength = 5;
            public const int MessageMaxLength = 5000;

            public const string NameErrorMessage = "Name should be between {2} and {1} symbols";

            public const string SubjectErrorMessage = "Subject should be between {2} and {1} symbols";

            public const string MessageErrorMessage = "Message should be between {2} and {1} symbols";
        }

        public static class Movie
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 100;

            public const int PosterPathMinLength = 10;
            public const int PosterPathMaxLength = 200;

            public const int TrailerPathMinLength = 10;
            public const int TrailerPathMaxLength = 200;

            public const int IMDBLinkMinLength = 10;
            public const int IMDBLinkMaxLength = 100;

            public const int TaglineMinLength = 3;
            public const int TaglineMaxLength = 150;

            public const int OverviewMinLength = 10;
            public const int OverviewMaxLength = 1000;

            public const int RuntimeMinValue = 50;
            public const int RuntimeMaxValue = 300;

            public const int PopularityMaxValue = 100;

            public const string TitleErrorMessage = "Title should be between {2} and {1} symbols";

            public const string PosterPathErrorMessage = "Poster Image Path should be between {2} and {1} symbols";

            public const string TrailerPathErrorMessage = "Trailer Path should be between {2} and {1} symbols";

            public const string IMDBLinkErrorMessage = "IMDB Link should be between {2} and {1} symbols";

            public const string TaglineErrorMessage = "Tagline should be between {2} and {1} symbols";

            public const string OverviewErrorMessage = "Overview should be between {2} and {1} symbols";

            public const string RuntimeErrorMessage = "Runtime should be between {1} and {2} minutes";

            public const string PopularityErrorMessage = "Popularity should not be higher than {2}";

            public const string PosterPathDisplayName = "Poster Image Path";

            public const string TrailerPathDisplayName = "Trailer Path";

            public const string IMDBLinkDisplayName = "Link to IMDB";

            public const string ReleaseDatekDisplayName = "Release Date";
        }

        public static class Person
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 100;

            public const int ProfilePicPathMinLength = 10;
            public const int ProfilePicPathMaxLength = 200;

            public const int BiographyMinLength = 10;
            public const int BiographyMaxLength = 20000;

            public const int BirthplaceMinLength = 3;
            public const int BirthplaceMaxLength = 300;

            public const int PopularityMaxValue = 100;

            public const string NameErrorMessage = "Name should be between {2} and {1} symbols";

            public const string ProfilePicPathErrorMessage = "Profile Pic Path should be between {2} and {1} symbols";

            public const string BiographyErrorMessage = "Biography should be between {2} and {1} symbols";

            public const string BirthplaceErrorMessage = "Birthplace should be between {2} and {1} symbols";

            public const string PopularityErrorMessage = "Popularity should not be higher than {2}";

            public const string ProfilePicPathDisplayName = "Profile Image Path";

            public const string DirectorDisplayName = "Director";

            public const string DirectorIdError = "Please select director.";
        }

        public static class Author
        {
            public const int NameMaxLength = 50;
        }

        public static class Country
        {
            public const int NameMaxLength = 50;

            public const string CountryDisplayName = "Country";

            public const string CountriesDisplayName = "Production Countries";

            public const string CountryIdError = "Please select production country.";
        }

        public static class Language
        {
            public const int NameMaxLength = 50;

            public const string LanguageDisplayName = "Language";

            public const string LanguagesDisplayName = "Languages";

            public const string LanguageIdError = "Please select language.";
        }

        public static class City
        {
            public const int NameMaxLength = 90;
        }

        public static class Genre
        {
            public const int NameMaxLength = 30;

            public const string GenreDisplayName = "Genre";

            public const string GenresDisplayName = "Genres";

            public const string GenreIdError = "Please select genre.";
        }

        public static class Comment
        {
            public const int ContentMinLength = 3;
            public const int ContentMaxLength = 1000;

            public const string ContentErrorMessage = "Content should be between {2} and {1} symbols";
        }

        public static class Character
        {
            public const int CharacterNameMaxLength = 500;
        }

        public static class Review
        {
            public const int TitleMaxLength = 100;

            public const int DescriptionMaxLength = 1500;
        }

        public static class Privacy
        {
            public const int ContentMaxLength = 20000;
        }
    }
}
