// Growth time intelligence script for DAX measures

var dateColumn = "'Date'[Date]";
var nestedFolder = "Time Intelligence\\";

foreach (var m in Selected.Measures) { 

    var words = m.Name.Split(' ');          // Extract the last word from the measure name
    var lastWord = words.Length > 0 ? words[words.Length - 1] : m.Name;

    // Destination folder using the last word of m.Name
    var destinationFolder = nestedFolder + "Growth\\" + lastWord + "\\";

    var table = m.Table; // Measure's table reference

    // Create the measure for Previous Month Sales
    var pmMeasureName = "PM " + m.Name;

    var pmDaxExpression = @"
        CALCULATE (
            " + m.DaxObjectName + @",
            DATEADD ( 'Date'[Date], -1, MONTH )
        )";

    var pmMeasure = table.AddMeasure(
        pmMeasureName,      // Name of the new Previous Month Sales measure
        pmDaxExpression,    // DAX expression for Previous Month Sales
        destinationFolder + m.Name   // Display Folder
    );
    pmMeasure.FormatString = "0.00"; // Format string

    // Create the MOM Sales measure
    var momMeasureName = "MOM " + m.Name;

    var momDaxExpression = @"
        VAR __ValueCurrentPeriod = " + m.DaxObjectName + @"
        VAR __ValuePreviousPeriod = [PM " + m.Name + @"]
        VAR __Result =
            IF (
                NOT ISBLANK ( __ValueCurrentPeriod ) && NOT ISBLANK ( __ValuePreviousPeriod ),
                __ValueCurrentPeriod - __ValuePreviousPeriod
            )
        RETURN
            __Result";

    var momMeasure = table.AddMeasure(
        momMeasureName,      // Name of the new MOM Sales measure
        momDaxExpression,    // DAX expression for MOM Sales
        destinationFolder + m.Name    // Display Folder
    );
    momMeasure.FormatString = "0.00"; // Format string

    // Create the MOM % Sales measure
    var momPctMeasureName = "MOM % " + m.Name;

    var momPctDaxExpression = @"
        DIVIDE (
            [MOM " + m.Name + @"],
            [PM " + m.Name + @"]
        )";

    var momPctMeasure = table.AddMeasure(
        momPctMeasureName,      // Name of the new MOM % Sales measure
        momPctDaxExpression,    // DAX expression for MOM % Sales
        destinationFolder + m.Name       // Display Folder
    );
    momPctMeasure.FormatString = "0.00%"; // Format as percentage

    // Create the measure for Previous Quarter Sales
    var pqMeasureName = "PQ " + m.Name;

    var pqDaxExpression = @"
        CALCULATE (
            " + m.DaxObjectName + @",
            DATEADD ( 'Date'[Date], -1, QUARTER )
        )";

    var pqMeasure = table.AddMeasure(
        pqMeasureName,      // Name of the new Previous Quarter Sales measure
        pqDaxExpression,    // DAX expression for Previous Quarter Sales
        destinationFolder + m.Name   // Display Folder
    );
    pqMeasure.FormatString = "0.00"; // Format string

    // Create the QOQ Sales measure
    var qoqMeasureName = "QOQ " + m.Name;

    var qoqDaxExpression = @"
        VAR __ValueCurrentPeriod = " + m.DaxObjectName + @"
        VAR __ValuePreviousPeriod = [PQ " + m.Name + @"]
        VAR __Result =
            IF (
                NOT ISBLANK ( __ValueCurrentPeriod ) && NOT ISBLANK ( __ValuePreviousPeriod ),
                __ValueCurrentPeriod - __ValuePreviousPeriod
            )
        RETURN
            __Result";

    var qoqMeasure = table.AddMeasure(
        qoqMeasureName,       // Name of the new QOQ Sales measure
        qoqDaxExpression,     // DAX expression for QOQ Sales
        destinationFolder + m.Name     // Display Folder
    );
    qoqMeasure.FormatString = "0.00"; // Format string

    // Create the QOQ % Sales measure
    var qoqPctMeasureName = "QOQ % " + m.Name;

    var qoqPctDaxExpression = @"
        DIVIDE ( 
            [QOQ " + m.Name + @"],
            [PQ " + m.Name + @"]
        )";

    var qoqPctMeasure = table.AddMeasure(
        qoqPctMeasureName,    // Name of the new QOQ % Sales measure
        qoqPctDaxExpression,  // DAX expression for QOQ % Sales
        destinationFolder + m.Name     // Display Folder
    );
    qoqPctMeasure.FormatString = "0.00%"; // Format as percentage

    // Create the measure for Previous Year Sales
    var pyMeasureName = "PY " + m.Name;

    var pyDaxExpression = @"
        CALCULATE (
            " + m.DaxObjectName + @",
            DATEADD ( 'Date'[Date], -1, YEAR )
        )";

    var pyMeasure = table.AddMeasure(
        pyMeasureName,      // Name of the new Previous Year Sales measure
        pyDaxExpression,    // DAX expression for Previous Year Sales
        destinationFolder + m.Name   // Display Folder
    );
    pyMeasure.FormatString = "0.00"; // Format string

    // Create the YOY Sales measure
    var yoyMeasureName = "YOY " + m.Name;

    var yoyDaxExpression = @"
        VAR __ValueCurrentPeriod = " + m.DaxObjectName + @"
        VAR __ValuePreviousPeriod = [PY " + m.Name + @"]
        VAR __Result =
            IF (
                NOT ISBLANK ( __ValueCurrentPeriod ) && NOT ISBLANK ( __ValuePreviousPeriod ),
                __ValueCurrentPeriod - __ValuePreviousPeriod
            )
        RETURN
            __Result";

    var yoyMeasure = table.AddMeasure(
        yoyMeasureName,      // Name of the new YOY Sales measure
        yoyDaxExpression,    // DAX expression for YOY Sales
        destinationFolder + m.Name    // Display Folder
    );
    yoyMeasure.FormatString = "0.00"; // Format string

    // Create the YOY % Sales measure
    var yoyPctMeasureName = "YOY % " + m.Name;

    var yoyPctDaxExpression = @"
        DIVIDE (
            [YOY " + m.Name + @"],
            [PY " + m.Name + @"]
        )";

    var yoyPctMeasure = table.AddMeasure(
        yoyPctMeasureName,      // Name of the new YOY % Sales measure
        yoyPctDaxExpression,    // DAX expression for YOY % Sales
        destinationFolder + m.Name       // Display Folder
    );
    yoyPctMeasure.FormatString = "0.00%"; // Format as percentage


}