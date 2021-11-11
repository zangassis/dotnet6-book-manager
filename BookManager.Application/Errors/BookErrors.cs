public static class BookErrors
{
    public static string Error100 { get; set; } = "You must submit at least one item to be processed.";
    public static string Error200 { get; set; } = "It is necessary to send the Title.";
    public static string Error300 { get; set; } = "It is necessary to send the Author.";
    public static string Error400 { get; set; } = "It is necessary to send the Description.";
    public static string Error500 { get; set; } = "The submitted id does not exist or is invalid.";
}