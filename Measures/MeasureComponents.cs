// Create the following measures: Label, Percentage, CF, Previous Period
// This script should be run after Time Intelligence

var parameterTable = "'Scope Parameter'[Scope]"; // Name of your scope parameter. File in 'Dax Expressions/Scope.dax'

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
    // Conditional Formatting according the Label
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

    // Create the Previous Period measure name and DAX expression
    var previousPeriodMeasureName = m.Name + " - Previous Period";
    var previousPeriodDaxExpression = @"
    // Use as Error Bar Upper Bound with Bar Charts
    VAR SelectedPeriod = SELECTEDVALUE(" + parameterTable + @")
    RETURN
        SWITCH(
            SelectedPeriod,
            ""MTD"", [PMTD " + m.Name + @"],
            ""QTD"", [PQTD " + m.Name + @"],
            ""YTD"", [PYTD " + m.Name + @"],
            ""MAT"", [PMAT " + m.Name + @"],
            BLANK()
        )
    ";

    // Add the Chart measure to the table
    var previousPeriodMeasure = table.AddMeasure(
        previousPeriodMeasureName,
        previousPeriodDaxExpression,
        lastWord + "\\" + m.Name + "\\"
    );
    
    previousPeriodMeasure.FormatString = "0.00"; // Format string

}