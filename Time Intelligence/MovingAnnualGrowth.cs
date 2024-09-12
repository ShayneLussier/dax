// Moving annual growth time intelligence script for DAX measures
// Create the following measures: MAT, PMAT, MATG, MATG%

var dateColumn = "'Date'[Date]"; // Replace with the name of your date table.
var nestedFolder = "Time Intelligence\\"; // Parent folder name.

foreach (var m in Selected.Measures) { 

    var words = m.Name.Split(' ');
    var lastWord = words.Length > 0 ? words[words.Length - 1] : m.Name;

    // Destination folder using the last word of m.Name
    var destinationFolder = nestedFolder + "Moving annual growth\\" + lastWord + "\\";

    var table = m.Table; // Measure's table reference

    // Create the MAT measure (Moving Annual Total)
    var matMeasureName = "MAT " + m.Name;
    var matDaxExpression = @"
    CALCULATE (
        " + m.DaxObjectName + @", 
        DATESINPERIOD(
            " + dateColumn + @", 
            MAX(" + dateColumn + @"), 
            -1, 
            YEAR
        )
    )";

    var matMeasure = table.AddMeasure(
        matMeasureName,
        matDaxExpression,
        destinationFolder + m.Name
    );
    matMeasure.FormatString = "0.00"; // Format string

    // Create the PMAT (Previous Moving Annual Total) measure
    var pmatMeasureName = "PMAT " + m.Name;

    var pmatDaxExpression = @"
        CALCULATE (
            [MAT " + m.Name + @"],
            DATEADD (
                " + dateColumn + @", 
                -1, 
                YEAR
            )
        )";

    var pmatMeasure = table.AddMeasure(
        pmatMeasureName,
        pmatDaxExpression,
        destinationFolder + m.Name
    );
    pmatMeasure.FormatString = "0.00";  // Format string

    // Create the MATG (Moving Annual Total Growth) measure
    var matgMeasureName = "MATG " + m.Name;

    var matgDaxExpression = @"
        VAR __ValueCurrentPeriod = [MAT " + m.Name + @"]
        VAR __ValuePreviousPeriod = [PMAT " + m.Name + @"]
        VAR __Result =
            IF (
                NOT ISBLANK ( __ValueCurrentPeriod ) && NOT ISBLANK ( __ValuePreviousPeriod ),
                __ValueCurrentPeriod - __ValuePreviousPeriod
            )
        RETURN
            __Result";

    var matgMeasure = table.AddMeasure(
        matgMeasureName,
        matgDaxExpression,
        destinationFolder + m.Name
    );
    matgMeasure.FormatString = "0.00"; // Format string

    // Create the MATG % (Moving Annual Total Growth Percentage) measure
    var matgPctMeasureName = "MATG % " + m.Name;

    var matgPctDaxExpression = @"
        DIVIDE (
            [MATG " + m.Name + @"],
            [PMAT " + m.Name + @"]
        )";

    var matgPctMeasure = table.AddMeasure(
        matgPctMeasureName,
        matgPctDaxExpression,
        destinationFolder + m.Name
    );
    matgPctMeasure.FormatString = "0.00%";  // Format as percentage

}
