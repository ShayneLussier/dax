// Create the following measures: Label, Chart, Percentage, CF

var parameterTable = "'Scope Parameter'[Scope]";

foreach (var m in Selected.Measures) { 

    var words = m.Name.Split(' ');          // Extract the last word from the measure name
    var lastWord = words.Length > 0 ? words[words.Length - 1] : m.Name;

    // Create dynamic label for measures
    var table = m.Table; // Measure's table reference

    // Create the Label measure name and DAX expression
    var labelMeasureName = m.Name + " - Label";
    var daxExpression = @"
    VAR SelectedPeriod = SELECTEDVALUE(" + parameterTable + @")
    RETURN
        SWITCH(
            SelectedPeriod,
            ""MTD"", [MTD " + m.Name + @"],
            ""MOMTD"", [MOMTD " + m.Name + @"],
            ""MOM"", [MOM " + m.Name + @"],
            ""QTD"", [QTD " + m.Name + @"],
            ""QOQTD"", [QOQTD " + m.Name + @"],
            ""QOQ"", [QOQ " + m.Name + @"],
            ""YTD"", [YTD " + m.Name + @"],
            ""YOYTD"", [YOYTD " + m.Name + @"],
            ""YOY"", [YOY " + m.Name + @"],
            ""MAT"", [MAT " + m.Name + @"],
            BLANK()
        )
    ";

    // Add the Label measure to the table
    var labelMeasure = table.AddMeasure(
        labelMeasureName,           // Name of the new Label measure
        daxExpression,             // DAX expression for Label
        lastWord + "\\" + m.Name + "\\"      // Display Folder
    );
    
    // Set format string for the new measure
    labelMeasure.FormatString = "0.00"; // Format string
}