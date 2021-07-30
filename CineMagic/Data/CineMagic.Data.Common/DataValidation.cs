namespace CineMagic.Data.Common
{
    public static class DataValidation
    {
        public static class User
        {
            public const int UsernameMinLength = 3;
            public const int UsernameMaxLength = 30;
            public const int FullNameMinLength = 5;
            public const int FullNameMaxLength = 100;
        }

        public static class Movie
        {
            public const int TitleMaxLength = 100;
            public const int PosterPathMaxLength = 200;
            public const int TrailerPathMaxLength = 200;
            public const int IMDBLinkMaxLength = 100;
            public const int RuntimeMaxLength = 10;
            public const int TaglineMaxLength = 100;
            public const int OverviewMaxLength = 1000;
            public const int LanguageMaxLength = 50;
        }

        public static class Person
        {
            public const int NameMaxLength = 100;
            public const int ProfilePicPathMaxLength = 200;
        }

        public static class Country
        {
            public const int NameMaxLength = 50;
        }

        public static class City
        {
            public const int NameMaxLength = 90;
        }

        public static class Genre
        {
            public const int NameMaxLength = 30;
        }

        public static class Comment
        {
            public const int ContentMaxLength = 1000;
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
    }
}
