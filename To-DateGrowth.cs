// To-date growth time intelligence script for DAX measures

var dateColumn = "'Date'[Date]";
var nestedFolder = "Time Intelligence\\";

foreach (var m in Selected.Measures) { 

    var words = m.Name.Split(' ');          // Extract the last word from the measure name
    var lastWord = words.Length > 0 ? words[words.Length - 1] : m.Name;

    // Destination folder name
    var destinationFolder = nestedFolder + "To-date growth\\" + lastWord + "\\";

    var table = m.Table; // Measure's table reference

    // Create the PMTD measure
    var pmtdMeasureName = "PMTD " + m.Name;
    var pmtdDaxExpression = "CALCULATE([MTD " + m.Name + "], DATEADD(" + dateColumn + ", -1, MONTH))";

    var pmtdMeasure = table.AddMeasure(
        pmtdMeasureName,           // Name of the new PMTD measure
        pmtdDaxExpression,         // DAX expression for PMTD
        destinationFolder          // Display Folder
    );
    pmtdMeasure.FormatString = "0.00"; // Format string

    // Create the MOMTD measure
    var momtdMeasureName = "MOMTD " + m.Name;
    var daxExpression = @"
    VAR __ValueCurrentPeriod = [MTD " + m.Name + @"]
    VAR __ValuePreviousPeriod = [PMTD " + m.Name + @"]
    VAR __Result =
        IF (
            NOT ISBLANK ( __ValueCurrentPeriod ) && NOT ISBLANK ( __ValuePreviousPeriod ),
            __ValueCurrentPeriod - __ValuePreviousPeriod
        )
    RETURN
        __Result";

    var momtdMeasure = table.AddMeasure(
        momtdMeasureName,      // Name of the new measure
        daxExpression,         // DAX expression
        destinationFolder      // Display Folder
    );
    momtdMeasure.FormatString = "0.00"; // Format string

    // Create the MOMTD % measure
    var momtdPctMeasureName = "MOMTD % " + m.Name;
    var momtdPctDaxExpression = "DIVIDE([MOMTD " + m.Name + "], [PMTD " + m.Name + "])";

    var momtdPctMeasure = table.AddMeasure(
        momtdPctMeasureName,         // Name of the new MOMTD % measure
        momtdPctDaxExpression,       // DAX expression for MOMTD %
        destinationFolder            // Display Folder
    );
    momtdPctMeasure.FormatString = "0.00%"; // Format as percentage

    // Create the PQTD measure
    var pqtdMeasureName = "PQTD " + m.Name;
    var pqtdDaxExpression = "CALCULATE([QTD " + m.Name + "], DATEADD(" + dateColumn + ", -1, QUARTER))";

    var pqtdMeasure = table.AddMeasure(
        pqtdMeasureName,           // Name of the new PQTD measure
        pqtdDaxExpression,         // DAX expression for PQTD
        destinationFolder          // Display Folder
    );
    pqtdMeasure.FormatString = "0.00"; // Format string

    // Create the QOQTD measure
    var qoqtdMeasureName = "QOQTD " + m.Name;
    daxExpression = @"
    VAR __ValueCurrentPeriod = [QTD " + m.Name + @"]
    VAR __ValuePreviousPeriod = [PQTD " + m.Name + @"]
    VAR __Result =
        IF (
            NOT ISBLANK ( __ValueCurrentPeriod ) && NOT ISBLANK ( __ValuePreviousPeriod ),
            __ValueCurrentPeriod - __ValuePreviousPeriod
        )
    RETURN
        __Result";

    var qoqtdMeasure = table.AddMeasure(
        qoqtdMeasureName,      // Name of the new measure
        daxExpression,         // DAX expression
        destinationFolder      // Display Folder
    );
    qoqtdMeasure.FormatString = "0.00"; // Format string

    // Create the QOQTD % measure
    var qoqtdPctMeasureName = "QOQTD % " + m.Name;
    var qoqtdPctDaxExpression = "DIVIDE([QOQTD " + m.Name + "], [PQTD " + m.Name + "])";

    var qoqtdPctMeasure = table.AddMeasure(
        qoqtdPctMeasureName,         // Name of the new QOQTD % measure
        qoqtdPctDaxExpression,       // DAX expression for QOQTD %
        destinationFolder            // Display Folder
    );
    qoqtdPctMeasure.FormatString = "0.00%"; // Format as percentage

    // Create the PYTD measure
    var pytdMeasureName = "PYTD " + m.Name;
    var pytdDaxExpression = "CALCULATE([YTD " + m.Name + "], DATEADD(" + dateColumn + ", -1, YEAR))";

    var pytdMeasure = table.AddMeasure(
        pytdMeasureName,           // Name of the new PYTD measure
        pytdDaxExpression,         // DAX expression for PYTD
        destinationFolder          // Display Folder
    );
    pytdMeasure.FormatString = "0.00"; // Format string

    // Create the YOYTD measure
    var yoytdMeasureName = "YOYTD " + m.Name;
    daxExpression = @"
    VAR __ValueCurrentPeriod = [YTD " + m.Name + @"]
    VAR __ValuePreviousPeriod = [PYTD " + m.Name + @"]
    VAR __Result =
        IF (
            NOT ISBLANK ( __ValueCurrentPeriod ) && NOT ISBLANK ( __ValuePreviousPeriod ),
            __ValueCurrentPeriod - __ValuePreviousPeriod
        )
    RETURN
        __Result";

    var yoytdMeasure = table.AddMeasure(
        yoytdMeasureName,      // Name of the new measure
        daxExpression,         // DAX expression
        destinationFolder      // Display Folder
    );
    yoytdMeasure.FormatString = "0.00"; // Format string

    // Create the YOYTD % measure
    var yoytdPctMeasureName = "YOYTD % " + m.Name;
    var yoytdPctDaxExpression = "DIVIDE([YOYTD " + m.Name + "], [PYTD " + m.Name + "])";

    var yoytdPctMeasure = table.AddMeasure(
        yoytdPctMeasureName,         // Name of the new YOYTD % measure
        yoytdPctDaxExpression,       // DAX expression for YOYTD %
        destinationFolder            // Display Folder
    );
    yoytdPctMeasure.FormatString = "0.00%"; // Format as percentage
}
