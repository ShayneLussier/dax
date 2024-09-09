// Time intelligence script for DAX measures

// Alias the date column
var dateColumn = "'Date'[Date]";
var nestedFolder = "Time Intelligence\\To-date Total\\";

foreach(var m in Selected.Measures){ 

// Creates a TOTALYTD measure for every selected measure.
    m.Table.AddMeasure(
    m.Name + " YTD",        // Name
        "CALCULATE (" + m.DaxObjectName + ", DATESYTD (" + dateColumn + "))",      // DAX expression
        nestedFolder + m.Name    // Destination Folder
    );


// Creates a QTD measure for every selected measure.
    m.Table.AddMeasure(
    m.Name + " QTD",        // Name
        "CALCULATE (" + m.DaxObjectName + ", DATESQTD (" + dateColumn + "))",      // DAX expression
        nestedFolder + m.Name    // Destination Folder
    );


// Creates a MTD measure for every selected measure.
    m.Table.AddMeasure(
    m.Name + " MTD",        // Name
        "CALCULATE (" + m.DaxObjectName + ", DATESMTD (" + dateColumn + "))",      // DAX expression
        nestedFolder + m.Name    // Destination Folder
    );
    
}