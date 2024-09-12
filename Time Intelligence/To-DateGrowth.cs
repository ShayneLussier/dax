// To-date growth time intelligence script for DAX measures
// Create the following measures: PMTD, MOMTD, MOMTD%, PQTD, QOQTD, QOQTD%, PYTD, YOYTD, YOYTD%

var dateColumn = "'Date'[Date]"; // Replace with the name of your date table.
var nestedFolder = "Time Intelligence\\"; // Parent folder name.

foreach (var m in Selected.Measures) { 

    var words = m.Name.Split(' ');
    var lastWord = words.Length > 0 ? words[words.Length - 1] : m.Name;

    // Destination folder name
    var destinationFolder= nestedFolder + "To-date growth\\" + lastWord + "\\";

    var table = m.Table; // Measure's table reference

    // Create the PMTD measure
    var pmtdMeasureName = "PMTD " + m.Name;
    var pmtdDaxExpression = "CALCULATE([MTD " + m.Name + "], DATEADD(" + dateColumn + ", -1, MONTH))";

    var pmtdMeasure = table.AddMeasure(
        pmtdMeasureName,
        pmtdDaxExpression,
        destinationFolder + m.Name
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
        momtdMeasureName,
        daxExpression,
        destinationFolder + m.Name
    );
    momtdMeasure.FormatString = "0.00"; // Format string

    // Create the MOMTD % measure
    var momtdPctMeasureName = "MOMTD % " + m.Name;
    var momtdPctDaxExpression = "DIVIDE([MOMTD " + m.Name + "], [PMTD " + m.Name + "])";

    var momtdPctMeasure = table.AddMeasure(
        momtdPctMeasureName,
        momtdPctDaxExpression,
        destinationFolder + m.Name
    );
    momtdPctMeasure.FormatString = "0.00%"; // Format as percentage

    // Create the PQTD measure
    var pqtdMeasureName = "PQTD " + m.Name;
    var pqtdDaxExpression = "CALCULATE([QTD " + m.Name + "], DATEADD(" + dateColumn + ", -1, QUARTER))";

    var pqtdMeasure = table.AddMeasure(
        pqtdMeasureName,
        pqtdDaxExpression,
        destinationFolder + m.Name
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
        qoqtdMeasureName,
        daxExpression,
        destinationFolder + m.Name
    );
    qoqtdMeasure.FormatString = "0.00"; // Format string

    // Create the QOQTD % measure
    var qoqtdPctMeasureName = "QOQTD % " + m.Name;
    var qoqtdPctDaxExpression = "DIVIDE([QOQTD " + m.Name + "], [PQTD " + m.Name + "])";

    var qoqtdPctMeasure = table.AddMeasure(
        qoqtdPctMeasureName,
        qoqtdPctDaxExpression,
        destinationFolder + m.Name
    );
    qoqtdPctMeasure.FormatString = "0.00%"; // Format as percentage

    // Create the PYTD measure
    var pytdMeasureName = "PYTD " + m.Name;
    var pytdDaxExpression = "CALCULATE([YTD " + m.Name + "], DATEADD(" + dateColumn + ", -1, YEAR))";

    var pytdMeasure = table.AddMeasure(
        pytdMeasureName,
        pytdDaxExpression,
        destinationFolder + m.Name
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
        yoytdMeasureName,
        daxExpression,
        destinationFolder + m.Name
    );
    yoytdMeasure.FormatString = "0.00"; // Format string

    // Create the YOYTD % measure
    var yoytdPctMeasureName = "YOYTD % " + m.Name;
    var yoytdPctDaxExpression = "DIVIDE([YOYTD " + m.Name + "], [PYTD " + m.Name + "])";

    var yoytdPctMeasure = table.AddMeasure(
        yoytdPctMeasureName,
        yoytdPctDaxExpression,
        destinationFolder + m.Name
    );
    yoytdPctMeasure.FormatString = "0.00%"; // Format as percentage
}
