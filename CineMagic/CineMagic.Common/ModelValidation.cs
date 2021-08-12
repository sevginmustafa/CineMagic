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
            public const int TitleMaxLength = 100;
            public const int PosterPathMaxLength = 200;
            public const int TrailerPathMaxLength = 200;
            public const int IMDBLinkMaxLength = 100;
            public const int RuntimeMaxLength = 10;
            public const int TaglineMaxLength = 150;
            public const int OverviewMaxLength = 1000;
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

            public const string ProfilePicPathDisplayName = "Profile Image Path";
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
