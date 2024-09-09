foreach(var c in Selected.Columns) {

// Creates a SUM measure for every currently selected column and hide the column.
    var sumMeasure = c.Table.AddMeasure(
        "Sum of " + c.Name,                             // Name
        "SUM(" + c.DaxObjectFullName + ")",             // DAX expression
        c.Name                                          // Display Folder
    );
    
    sumMeasure.FormatString = "0.00";                   // Format the measure

// Creates a AVERAGE measure for every currently selected column and hide the column.
    var averageMeasure = c.Table.AddMeasure(
        "Average of " + c.Name,                         // Name
        "AVERAGE(" + c.DaxObjectFullName + ")",         // DAX expression
        c.Name                                          // Display Folder
    );
    
    averageMeasure.FormatString = "0.00";               // Format the measure

// Creates a MAXIMUM measure for every currently selected column and hide the column.
    var maxMeasure = c.Table.AddMeasure(
        "Max of " + c.Name,                             // Name
        "MAX(" + c.DaxObjectFullName + ")",             // DAX expression
        c.Name                                          // Display Folder
    );
    
    maxMeasure.FormatString = "0.00";                   // Format the measure

// Creates a MINIMUM measure for every currently selected column and hide the column.
    var minMeasure = c.Table.AddMeasure(
        "Min of " + c.Name,                             // Name
        "MIN(" + c.DaxObjectFullName + ")",             // DAX expression
        c.Name                                          // Display Folder
    );
    
    minMeasure.FormatString = "0.00";                   // Format the measure

// Creates a COUNT measure for every currently selected column and hide the column.
    var countMeasure = c.Table.AddMeasure(
        "Count of " + c.Name,                           // Name
        "COUNT(" + c.DaxObjectFullName + ")",           // DAX expression
        c.Name                                          // Display Folder
    );
    
    countMeasure.FormatString = "0";                    // Format the measure

// Creates a COUNT(DISTINCT) measure for every currently selected column and hide the column.
    var distinctCountMeasure = c.Table.AddMeasure(
        "Distinct Count of " + c.Name,                  // Name
        "DISTINCTCOUNT(" + c.DaxObjectFullName + ")",   // DAX expression
        c.Name                                          // Display Folder
    );
    
    distinctCountMeasure.FormatString = "0";            // Format the measure

}
