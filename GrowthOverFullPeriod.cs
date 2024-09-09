// Growth over full period time intelligence script for DAX measures

var dateColumn = "'Date'[Date]";
var nestedFolder = "Time Intelligence\\";

foreach (var m in Selected.Measures) { 

    var words = m.Name.Split(' ');          // Extract the last word from the measure name
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
        pmcMeasureName,            // Name of the new PMC measure
        pmcDaxExpression,          // DAX expression for PMC
        destinationFolder + m.Name          // Display Folder
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
        mtdopmMeasureName,       // Name of the new MTDOPM measure
        mtdopmDaxExpression,     // DAX expression for MTDOPM
        destinationFolder + m.Name        // Display Folder
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
        mtdopmPctMeasureName,    // Name of the new MTDOPM % measure
        mtdopmPctDaxExpression,  // DAX expression for MTDOPM %
        destinationFolder + m.Name        // Display Folder
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
        pqcMeasureName,      // Name of the new PQC measure
        pqcDaxExpression,    // DAX expression for PQC
        destinationFolder + m.Name    // Display Folder
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
        qtdopqMeasureName,       // Name of the new QTDOPQ measure
        qtdopqDaxExpression,     // DAX expression for QTDOPQ
        destinationFolder + m.Name        // Display Folder
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
        qtdopqPctMeasureName,    // Name of the new QTDOPQ % measure
        qtdopqPctDaxExpression,  // DAX expression for QTDOPQ %
        destinationFolder + m.Name        // Display Folder
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
        pycMeasureName,      // Name of the new PYC measure
        pycDaxExpression,    // DAX expression for PYC
        destinationFolder + m.Name    // Display Folder
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
        ytdopyMeasureName,      // Name of the new YTDOPY measure
        ytdopyDaxExpression,    // DAX expression for YTDOPY
        destinationFolder + m.Name       // Display Folder
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
        ytdopyPctMeasureName,    // Name of the new YTDOPY % measure
        ytdopyPctDaxExpression,  // DAX expression for YTDOPY %
        destinationFolder + m.Name        // Display Folder
    );
    ytdopyPctMeasure.FormatString = "0.00%";  // Format as percentage

}