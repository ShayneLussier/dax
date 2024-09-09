// To-date total time intelligence script for DAX measures

// Alias the date column
var dateColumn = "'Date'[Date]";
var nestedFolder = "Time Intelligence\\";

foreach (var m in Selected.Measures) { 

    // Extract the last word from the measure name
    var words = m.Name.Split(' '); // Assuming space is the delimiter
    var lastWord = words.Length > 0 ? words[words.Length - 1] : m.Name;

    // Destination folder using the last word of m.Name
    var destinationFolder = nestedFolder + "To-date total\\" + lastWord + "\\";

    // Creates a YTD measure for every selected measure
    var ytdMeasure = m.Table.AddMeasure(
        "YTD " + m.Name,        // Name
        "CALCULATE (" + m.DaxObjectName + ", DATESYTD (" + dateColumn + "))",      // DAX expression
        destinationFolder + m.Name          // Destination Folder
    );

    ytdMeasure.FormatString = "0.00";      // Format the measure

    // Creates a QTD measure for every selected measure
    var qtdytdMeasure = m.Table.AddMeasure(
        "QTD " + m.Name,        // Name
        "CALCULATE (" + m.DaxObjectName + ", DATESQTD (" + dateColumn + "))",      // DAX expression
        destinationFolder + m.Name
    );
    qtdytdMeasure.FormatString = "0.00";      // Format the measure

    // Creates a MTD measure for every selected measure
    var mtdytdMeasure = m.Table.AddMeasure(
        "MTD " + m.Name,        // Name
        "CALCULATE (" + m.DaxObjectName + ", DATESMTD (" + dateColumn + "))",      // DAX expression
        destinationFolder + m.Name
    );
    mtdytdMeasure.FormatString = "0.00";      // Format the measure

}