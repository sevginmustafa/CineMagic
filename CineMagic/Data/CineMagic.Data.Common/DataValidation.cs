namespace CineMagic.Data.Common
{
    public static class DataValidation
    {
        public static class ContactForm
        {
            public const int NameMaxLength = 100;
            public const int SubjectMaxLength = 100;
            public const int MessageMaxLength = 5000;
        }

        public static class Movie
        {
            public const int TitleMaxLength = 100;
            public const int PosterPathMaxLength = 200;
            public const int TrailerPathMaxLength = 200;
            public const int IMDBLinkMaxLength = 100;
            public const int TaglineMaxLength = 150;
            public const int OverviewMaxLength = 1000;
        }

        public static class Backdrop
        {
            public const int BackdropPathMaxLength = 2000;
        }

        public static class Person
        {
            public const int NameMaxLength = 100;
            public const int ProfilePicPathMaxLength = 200;
            public const int BiographyMaxLength = 20000;
            public const int BirthplaceMaxLength = 300;
        }

        public static class Author
        {
            public const int NameMaxLength = 50;
        }

        public static class Country
        {
            public const int NameMaxLength = 50;
        }

        public static class Language
        {
            public const int NameMaxLength = 50;
        }

        public static class Genre
        {
            public const int NameMaxLength = 30;
        }

        public static class Comment
        {
            public const int ContentMinLength = 3;
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

        public static class Privacy
        {
            public const int ContentMaxLength = 20000;
        }
    }
}
