// Create the following measures: Label, Chart, Percentage, CF
// This script should be run after Time Intelligence

var parameterTable = "'Scope Parameter'[Scope]";

foreach (var m in Selected.Measures) { 

    var words = m.Name.Split(' ');  // Extract the last word from the measure name
    var lastWord = words.Length > 0 ? words[words.Length - 1] : m.Name;

    var table = m.Table; // Measure's table reference

    // Create the Label measure name and DAX expression
    var labelMeasureName = m.Name + " - Label";
    var labelDaxExpression = @"
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

    // Add the Chart measure to the table
    var labelMeasure = table.AddMeasure(
        labelMeasureName,
        labelDaxExpression,
        lastWord + "\\" + m.Name + "\\"
    );
    
    labelMeasure.FormatString = "0.00"; // Format string

    // Create the Chart measure name and DAX expression
    var chartMeasureName = m.Name + " - Chart";
    var chartDaxExpression = @"
    VAR SelectedPeriod = SELECTEDVALUE(" + parameterTable + @")
    RETURN
        SWITCH(
            SelectedPeriod,
            ""MTD"", [MTD " + m.Name + @"],
            ""MOMTD"", [MTD " + m.Name + @"],
            ""MOM"", [MTD " + m.Name + @"],
            ""QTD"", [QTD " + m.Name + @"],
            ""QOQTD"", [QTD " + m.Name + @"],
            ""QOQ"", [QTD " + m.Name + @"],
            ""YTD"", [YTD " + m.Name + @"],
            ""YOYTD"", [YTD " + m.Name + @"],
            ""YOY"", [YTD " + m.Name + @"],
            ""MAT"", [MAT " + m.Name + @"],
            BLANK()
        )
    ";

    // Add the Chart measure to the table
    var chartMeasure = table.AddMeasure(
        chartMeasureName,
        chartDaxExpression,
        lastWord + "\\" + m.Name + "\\"
    );
    
    chartMeasure.FormatString = "0.00"; // Format string

    // Create the Percentage measure name and DAX expression
    var percentageMeasureName = m.Name + " - Percentage";
    var percentageDaxExpression = @"
    VAR SelectedPeriod = SELECTEDVALUE(" + parameterTable + @")
    VAR Label = 
        SWITCH(
            SelectedPeriod,
            ""MTD"", BLANK(),
            ""MOMTD"", [MOMTD % " + m.Name + @"],
            ""MOM"", [MOM % " + m.Name + @"],
            ""QTD"", BLANK(),
            ""QOQTD"", [QOQTD % " + m.Name + @"],
            ""QOQ"", [QOQ % " + m.Name + @"],
            ""YTD"", BLANK(),
            ""YOYTD"", [YOYTD % " + m.Name + @"],
            ""YOY"", [YOY % " + m.Name + @"],
            ""MAT"", [MATG % " + m.Name + @"],
            BLANK()
        )
    RETURN
        IF(
            NOT ISBLANK(Label),
            IF(
                Label > 0,
                ""+"" & FORMAT(Label, ""0.00%""),
                FORMAT(Label, ""0.00%"")
            ),
            BLANK()
        )
    ";

    // Add the Percentage measure to the table
    var percentageMeasure = m.Table.AddMeasure(
        percentageMeasureName,
        percentageDaxExpression,
        lastWord + "\\" + m.Name + "\\"
    );

    percentageMeasure.FormatString = "0.00%"; // Percentage format

    // Create the CF (Conditional Formatting) measure name and DAX expression
    var cfMeasureName = m.Name + " - CF";
    var cfDaxExpression = @"
    IF(
        [" + m.Name + @" - Label] > 0, 
        ""#00CD79"",  // Green for positive
        IF(
            [" + m.Name + @" - Label] < 0, 
            ""#D64550"",  // Red for negative
            ""#000000""   // Black for zero or other cases
        )
    )
    ";

    // Add the CF measure to the table
    var cfMeasure = m.Table.AddMeasure(
        cfMeasureName,
        cfDaxExpression,
        lastWord + "\\" + m.Name + "\\"
    );

}