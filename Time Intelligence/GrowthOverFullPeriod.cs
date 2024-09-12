// Growth over full period time intelligence script for DAX measures
// Create the following measures: PMC, MTDOPM, MTDOPM%, PQC, QTDOPQ, QTDOPQ%, PYC, YTDOPY, YTDOPY%

var dateColumn = "'Date'[Date]"; // Replace with the name of your date table.
var nestedFolder = "Time Intelligence\\"; // Parent folder name.

foreach (var m in Selected.Measures) { 

    var words = m.Name.Split(' ');
    var lastWord = words.Length > 0 ? words[words.Length - 1] : m.Name;

    // Destination folder using the last word of m.Name
    var destinationFolder = nestedFolder + "Growth over full period\\" + lastWord + "\\";

    var table = m.Table; // Measure's table reference

    // Create the PMC (Previous Month Comparison) measure
    var pmcMeasureName = "PMC " + m.Name;

    var pmcDaxExpression = @"
        CALCULATE (
            [" + m.Name + @"],
            PARALLELPERIOD (
                " + dateColumn + @",
                -1,
                MONTH
            )
        )";

    var pmcMeasure = table.AddMeasure(
        pmcMeasureName,
        pmcDaxExpression,
        destinationFolder + m.Name
    );
    pmcMeasure.FormatString = "0.00";  // Format string

    // Create the MTDOPM (Month-to-Date Over Previous Month) Sales measure
    var mtdopmMeasureName = "MTDOPM " + m.Name;

    var mtdopmDaxExpression = @"
        VAR __ValueCurrentPeriod = [MTD " + m.Name + @"]
        VAR __ValuePreviousPeriod = [PMC " + m.Name + @"]
        VAR __Result =
            IF (
                NOT ISBLANK ( __ValueCurrentPeriod ) && NOT ISBLANK ( __ValuePreviousPeriod ),
                __ValueCurrentPeriod - __ValuePreviousPeriod
            )
        RETURN
            __Result";

    var mtdopmMeasure = table.AddMeasure(
        mtdopmMeasureName,
        mtdopmDaxExpression,
        destinationFolder + m.Name
    );
    mtdopmMeasure.FormatString = "0.00";  // Format string

    // Create the MTDOPM % (Month-to-Date Over Previous Month Percentage) Sales measure
    var mtdopmPctMeasureName = "MTDOPM % " + m.Name;

    var mtdopmPctDaxExpression = @"
        DIVIDE ( 
            [MTDOPM " + m.Name + @"],
            [PMC " + m.Name + @"]
        )";

    var mtdopmPctMeasure = table.AddMeasure(
        mtdopmPctMeasureName,
        mtdopmPctDaxExpression,
        destinationFolder + m.Name
    );
    mtdopmPctMeasure.FormatString = "0.00%";  // Format as percentage

    // Create the PQC (Previous Quarter Cumulative) Sales measure
    var pqcMeasureName = "PQC " + m.Name;

    var pqcDaxExpression = @"
        CALCULATE (
            [QTD " + m.Name + @"],
            PARALLELPERIOD ( 'Date'[Date], -1, QUARTER )
        )";

    var pqcMeasure = table.AddMeasure(
        pqcMeasureName,
        pqcDaxExpression,
        destinationFolder + m.Name
    );
    pqcMeasure.FormatString = "0.00"; // Format string


    // Create the QTDOPQ (Quarter-to-Date Over Previous Quarter) Sales measure
    var qtdopqMeasureName = "QTDOPQ " + m.Name;

    var qtdopqDaxExpression = @"
        VAR __ValueCurrentPeriod = [QTD " + m.Name + @"]
        VAR __ValuePreviousPeriod = [PQC " + m.Name + @"]
        VAR __Result =
            IF (
                NOT ISBLANK ( __ValueCurrentPeriod ) && NOT ISBLANK ( __ValuePreviousPeriod ),
                __ValueCurrentPeriod - __ValuePreviousPeriod
            )
        RETURN
            __Result";

    var qtdopqMeasure = table.AddMeasure(
        qtdopqMeasureName,
        qtdopqDaxExpression,
        destinationFolder + m.Name
    );
    qtdopqMeasure.FormatString = "0.00";  // Format string

    // Create the QTDOPQ % (Quarter-to-Date Over Previous Quarter Percentage) Sales measure
    var qtdopqPctMeasureName = "QTDOPQ % " + m.Name;

    var qtdopqPctDaxExpression = @"
        DIVIDE ( 
            [QTDOPQ " + m.Name + @"],
            [PQC " + m.Name + @"]
        )";

    var qtdopqPctMeasure = table.AddMeasure(
        qtdopqPctMeasureName,
        qtdopqPctDaxExpression,
        destinationFolder + m.Name
    );
    qtdopqPctMeasure.FormatString = "0.00%";  // Format as percentage

    // Create the PYC (Previous Year Cumulative) Sales measure
    var pycMeasureName = "PYC " + m.Name;

    var pycDaxExpression = @"
        CALCULATE (
            [YTD " + m.Name + @"],
            PARALLELPERIOD ( 'Date'[Date], -1, YEAR )
        )";

    var pycMeasure = table.AddMeasure(
        pycMeasureName,
        pycDaxExpression,
        destinationFolder + m.Name
    );
    pycMeasure.FormatString = "0.00"; // Format string


    // Create the YTDOPY (Year-to-Date Over Previous Year) Sales measure
    var ytdopyMeasureName = "YTDOPY " + m.Name;

    var ytdopyDaxExpression = @"
        VAR __ValueCurrentPeriod = [YTD " + m.Name + @"]
        VAR __ValuePreviousPeriod = [PYC " + m.Name + @"]
        VAR __Result =
            IF (
                NOT ISBLANK ( __ValueCurrentPeriod ) && NOT ISBLANK ( __ValuePreviousPeriod ),
                __ValueCurrentPeriod - __ValuePreviousPeriod
            )
        RETURN
            __Result";

    var ytdopyMeasure = table.AddMeasure(
        ytdopyMeasureName,
        ytdopyDaxExpression,
        destinationFolder + m.Name
    );
    ytdopyMeasure.FormatString = "0.00"; // Format string

    // Create the YTDOPY % (Year-to-Date Over Previous Year Percentage) Sales measure
    var ytdopyPctMeasureName = "YTDOPY % " + m.Name;

    var ytdopyPctDaxExpression = @"
        DIVIDE ( 
            [YTDOPY " + m.Name + @"],
            [PYC " + m.Name + @"]
        )";

    var ytdopyPctMeasure = table.AddMeasure(
        ytdopyPctMeasureName,
        ytdopyPctDaxExpression,
        destinationFolder + m.Name
    );
    ytdopyPctMeasure.FormatString = "0.00%";  // Format as percentage

}