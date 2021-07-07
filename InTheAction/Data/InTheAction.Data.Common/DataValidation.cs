namespace InTheAction.Data.Common
{
    public static class DataValidation
    {
        public static class Movie
        {
            public const int TitleMaxLength = 100;
            public const int CoverImageUrlMaxLength = 200;
            public const int TrailerUrlMaxLength = 200;
            public const int TMDBLinkMaxLength = 100;
            public const int RuntimeMaxLength = 10;
            public const int DescriptionMaxLength = 700;
            public const int LanguageMaxLength = 50;
        }

        public static class Person
        {
            public const int NameMaxLength = 100;
            public const int CoverImageUrlMaxLength = 200;
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
            public const int CharacterNameMaxLength = 100;
        }

        public static class Review
        {
            public const int TitleMaxLength = 100;
            public const int DescriptionMaxLength = 1500;
        }
    }
}
