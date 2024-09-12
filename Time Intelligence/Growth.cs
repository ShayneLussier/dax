// Growth time intelligence script for DAX measures
// Create the following measures: PM, MOM, MOM%, PQ, QOQ, QOQ%, PY, YOY, YOY%

var dateColumn = "'Date'[Date]"; // Replace with the name of your date table.
var nestedFolder = "Time Intelligence\\"; // Parent folder name.

foreach (var m in Selected.Measures) { 

    var words = m.Name.Split(' ');
    var lastWord = words.Length > 0 ? words[words.Length - 1] : m.Name;

    // Destination folder using the last word of m.Name
    var destinationFolder = nestedFolder + "Growth\\" + lastWord + "\\";

    var table = m.Table; // Measure's table reference

    // Create the measure for Current Month Sales
    var cmMeasureName = "CM " + m.Name;
    var cmDaxExpression = @"
        CALCULATE (
            [" + m.Name + @"],
            DATESMTD ( " + dateColumn + @" )
        )";
    var cmMeasure = table.AddMeasure(
        cmMeasureName,
        cmDaxExpression,
        destinationFolder + m.Name
    );
    cmMeasure.FormatString = "0.00"; // Format string

    // Create the measure for Previous Month Sales using the new DAX formula
    var pmMeasureName = "PM " + m.Name;
    var pmDaxExpression = @"
        VAR MaxDate = MAX(" + dateColumn + @")
        VAR StartDatePM = DATE(YEAR(MaxDate), MONTH(MaxDate) - 1, 1)
        VAR EndDatePM = EOMONTH(StartDatePM, 0)
        RETURN
        CALCULATE(
            [" + m.Name + @"],
            DATESBETWEEN(" + dateColumn + @", StartDatePM, EndDatePM)
        )";
    var pmMeasure = table.AddMeasure(
        pmMeasureName,
        pmDaxExpression,
        destinationFolder + m.Name
    );
    pmMeasure.FormatString = "0.00";

    // Create the MOM Sales measure
    var momMeasureName = "MOM " + m.Name;
    var momDaxExpression = @"
        VAR __ValueCurrentPeriod = [CM " + m.Name + @"]
        VAR __ValuePreviousPeriod = [PM " + m.Name + @"]
        VAR __Result =
            IF (
                NOT ISBLANK ( __ValueCurrentPeriod ) && NOT ISBLANK ( __ValuePreviousPeriod ),
                __ValueCurrentPeriod - __ValuePreviousPeriod
            )
        RETURN
            __Result";
    var momMeasure = table.AddMeasure(
        momMeasureName,
        momDaxExpression,
        destinationFolder + m.Name
    );
    momMeasure.FormatString = "0.00";

    // Create the MOM % Sales measure
    var momPctMeasureName = "MOM % " + m.Name;
    var momPctDaxExpression = @"
        DIVIDE (
            [MOM " + m.Name + @"],
            [PM " + m.Name + @"]
        )";
    var momPctMeasure = table.AddMeasure(
        momPctMeasureName,
        momPctDaxExpression,
        destinationFolder + m.Name
    );
    momPctMeasure.FormatString = "0.00%";

    // Measure for Current Quarter Sum of Sales
    var currentQuarterMeasureName = "CQ " + m.Name;
    var currentQuarterDaxExpression = @"
        CALCULATE (
            [" + m.Name + @"],
            DATESQTD ( " + dateColumn + @" )
        )";
    var currentQuarterMeasure = table.AddMeasure(
        currentQuarterMeasureName,
        currentQuarterDaxExpression,
        destinationFolder + m.Name
    );
    currentQuarterMeasure.FormatString = "0.00"; // Format string

    // Create the measure for Previous Quarter Sales using the new DAX formula
    var pqMeasureName = "PQ " + m.Name;
    var pqDaxExpression = @"
        VAR MaxDate = MAX(" + dateColumn + @")
        VAR CurrentQuarter = QUARTER(MaxDate)
        VAR CurrentYear = YEAR(MaxDate)
        VAR StartDatePQ = 
            DATE(
                IF(CurrentQuarter = 1, CurrentYear - 1, CurrentYear),
                IF(CurrentQuarter = 1, 10, 
                    IF(CurrentQuarter = 2, 1,
                        IF(CurrentQuarter = 3, 4, 7))),
                1
            )
        VAR EndDatePQ = 
            DATE(
                IF(CurrentQuarter = 1, CurrentYear - 1, CurrentYear),
                IF(CurrentQuarter = 1, 12, 
                    IF(CurrentQuarter = 2, 3,
                        IF(CurrentQuarter = 3, 6, 9))),
                DAY(EOMONTH(
                    DATE(
                        IF(CurrentQuarter = 1, CurrentYear - 1, CurrentYear),
                        IF(CurrentQuarter = 1, 12, 
                            IF(CurrentQuarter = 2, 3,
                                IF(CurrentQuarter = 3, 6, 9))),
                        1
                    ),
                    0
                ))
            )
        RETURN
        CALCULATE(
            [" + m.Name + @"],
            DATESBETWEEN(" + dateColumn + @", StartDatePQ, EndDatePQ)
        )";
    var pqMeasure = table.AddMeasure(
        pqMeasureName,
        pqDaxExpression,
        destinationFolder + m.Name
    );
    pqMeasure.FormatString = "0.00";

    // Create the QOQ Sales measure
    var qoqMeasureName = "QOQ " + m.Name;
    var qoqDaxExpression = @"
        VAR __ValueCurrentPeriod = [CQ " + m.Name + @"]
        VAR __ValuePreviousPeriod = [PQ " + m.Name + @"]
        VAR __Result =
            IF (
                NOT ISBLANK ( __ValueCurrentPeriod ) && NOT ISBLANK ( __ValuePreviousPeriod ),
                __ValueCurrentPeriod - __ValuePreviousPeriod
            )
        RETURN
            __Result";
    var qoqMeasure = table.AddMeasure(
        qoqMeasureName,
        qoqDaxExpression,
        destinationFolder + m.Name
    );
    qoqMeasure.FormatString = "0.00";

    // Create the QOQ % Sales measure
    var qoqPctMeasureName = "QOQ % " + m.Name;
    var qoqPctDaxExpression = @"
        DIVIDE ( 
            [QOQ " + m.Name + @"],
            [PQ " + m.Name + @"]
        )";
    var qoqPctMeasure = table.AddMeasure(
        qoqPctMeasureName,
        qoqPctDaxExpression,
        destinationFolder + m.Name
    );
    qoqPctMeasure.FormatString = "0.00%";

    // Measure for Current Year Sum of Sales
    var currentYearMeasureName = "CY " + m.Name;
    var currentYearDaxExpression = @"
        CALCULATE (
            [" + m.Name + @"],
            DATESYTD ( " + dateColumn + @" )
        )";
    var currentYearMeasure = table.AddMeasure(
        currentYearMeasureName,
        currentYearDaxExpression,
        destinationFolder + m.Name
    );
    currentYearMeasure.FormatString = "0.00"; // Format string

    // Create the measure for Previous Year Sales
    var pyMeasureName = "PY " + m.Name;
    var pyDaxExpression = @"
        CALCULATE(
            [" + m.Name + @"],
            DATESBETWEEN(
                " + dateColumn + @",
                DATE(YEAR(MAX(" + dateColumn + @")) - 1, 1, 1),
                DATE(YEAR(MAX(" + dateColumn + @")) - 1, 12, 31)
            )
        )";
    var pyMeasure = table.AddMeasure(
        pyMeasureName,
        pyDaxExpression,
        destinationFolder + m.Name
    );
    pyMeasure.FormatString = "0.00";

    // Create the YOY Sales measure
    var yoyMeasureName = "YOY " + m.Name;
    var yoyDaxExpression = @"
        VAR __ValueCurrentPeriod = [CY " + m.Name + @"]
        VAR __ValuePreviousPeriod = [PY " + m.Name + @"]
        VAR __Result =
            IF (
                NOT ISBLANK ( __ValueCurrentPeriod ) && NOT ISBLANK ( __ValuePreviousPeriod ),
                __ValueCurrentPeriod - __ValuePreviousPeriod
            )
        RETURN
            __Result";
    var yoyMeasure = table.AddMeasure(
        yoyMeasureName,
        yoyDaxExpression,
        destinationFolder + m.Name
    );
    yoyMeasure.FormatString = "0.00";

    // Create the YOY % Sales measure
    var yoyPctMeasureName = "YOY % " + m.Name;
    var yoyPctDaxExpression = @"
        DIVIDE (
            [YOY " + m.Name + @"],
            [PY " + m.Name + @"]
        )";
    var yoyPctMeasure = table.AddMeasure(
        yoyPctMeasureName,
        yoyPctDaxExpression,
        destinationFolder + m.Name
    );
    yoyPctMeasure.FormatString = "0.00%";

}
