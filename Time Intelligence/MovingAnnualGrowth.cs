// Moving annual growth time intelligence script for DAX measures
// Create the following measures: MAT, PMAT, MATG, MATG%

var dateColumn = "'Date'[Date]";
var nestedFolder = "Time Intelligence\\";

foreach (var m in Selected.Measures) { 

    var words = m.Name.Split(' ');          // Extract the last word from the measure name
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
        matMeasureName,           // Name of the new MAT measure
        matDaxExpression,         // DAX expression for MAT
        destinationFolder + m.Name          // Display Folder
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
        pmatMeasureName,            // Name of the new PMAT measure
        pmatDaxExpression,          // DAX expression for PMAT
        destinationFolder + m.Name           // Display Folder
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
        matgMeasureName,           // Name of the new MATG measure
        matgDaxExpression,         // DAX expression for MATG
        destinationFolder + m.Name          // Display Folder
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
        matgPctMeasureName,        // Name of the new MATG % measure
        matgPctDaxExpression,      // DAX expression for MATG %
        destinationFolder + m.Name          // Display Folder
    );
    matgPctMeasure.FormatString = "0.00%";  // Format as percentage

}
