// Create the following measures: SUM, AVERAGE, MAX, MIN, COUNT, DISTINCTCOUNT

foreach(var c in Selected.Columns) {

    // Creates a SUM measure for every currently selected column and hide the column.
    var sumMeasure = c.Table.AddMeasure(
        "Sum of " + c.Name,           
        "SUM(" + c.DaxObjectFullName + ")",
        c.Name + "\\Sum of " + c.Name + "\\"
    );
    
    sumMeasure.FormatString = "0.00";                   // Format the measure

    // Creates a AVERAGE measure for every currently selected column and hide the column.
    var averageMeasure = c.Table.AddMeasure(
        "Average of " + c.Name,       
        "AVERAGE(" + c.DaxObjectFullName + 
        c.Name + "\\Average of " + c.Name + 
    );
    
    averageMeasure.FormatString = "0.00";               // Format the measure

    // Creates a MAXIMUM measure for every currently selected column and hide the column.
    var maxMeasure = c.Table.AddMeasure(
        "Max of " + c.Name,           
        "MAX(" + c.DaxObjectFullName + ")",
        c.Name + "\\Max of " + c.Name + "\\"
    );
    
    maxMeasure.FormatString = "0.00";                   // Format the measure

    // Creates a MINIMUM measure for every currently selected column and hide the column.
    var minMeasure = c.Table.AddMeasure(
        "Min of " + c.Name,           
        "MIN(" + c.DaxObjectFullName + ")",
        c.Name + "\\Min of " + c.Name + "\\"
    );
    
    minMeasure.FormatString = "0.00";                   // Format the measure

    // Creates a COUNT measure for every currently selected column and hide the column.
    var countMeasure = c.Table.AddMeasure(
        "Count of " + c.Name,         
        "COUNT(" + c.DaxObjectFullName + ")
        c.Name + "\\Count of " + c.Name + "\
    );
    
    countMeasure.FormatString = "0";                    // Format the measure

    // Creates a COUNT(DISTINCT) measure for every currently selected column and hide the column.
    var distinctCountMeasure = c.Table.AddMeasure(
        "Distinct Count of " + c.Name,
        "DISTINCTCOUNT(" + c.DaxObjectFullN
        c.Name + "\\Distinct Count of " + c.
    );
    
    distinctCountMeasure.FormatString = "0";            // Format the measure

}
