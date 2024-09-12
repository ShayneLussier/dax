// To-date total time intelligence script for DAX measures
// Create the following measures: MTD, QTD, YTD

var dateColumn = "'Date'[Date]"; // Replace with the name of your date table.
var nestedFolder = "Time Intelligence\\"; // Parent folder name.

foreach (var m in Selected.Measures) { 

    var words = m.Name.Split(' ');          // Extract the last word from the measure name
    var lastWord = words.Length > 0 ? words[words.Length - 1] : m.Name;

    // Destination folder name
    var destinationFolder = nestedFolder + "To-date total\\" + lastWord + "\\";

    // Creates a YTD measure for every selected measure
    var ytdMeasure = m.Table.AddMeasure(
        "YTD " + m.Name,
        "CALCULATE (" + m.DaxObjectName + ", DATESYTD (" + dateColumn + "))",
        destinationFolder + m.Name
    );

    ytdMeasure.FormatString = "0.00";      // Format string

    // Creates a QTD measure for every selected measure
    var qtdytdMeasure = m.Table.AddMeasure(
        "QTD " + m.Name,
        "CALCULATE (" + m.DaxObjectName + ", DATESQTD (" + dateColumn + "))",
        destinationFolder + m.Name
    );
    qtdytdMeasure.FormatString = "0.00";      // Format string

    // Creates a MTD measure for every selected measure
    var mtdytdMeasure = m.Table.AddMeasure(
        "MTD " + m.Name,
        "CALCULATE (" + m.DaxObjectName + ", DATESMTD (" + dateColumn + "))",
        destinationFolder + m.Name
    );
    mtdytdMeasure.FormatString = "0.00";      // Format string

}