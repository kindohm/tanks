function getServiceUrl() {
    var url = new String();
    url = document.URL;
    var index = url.lastIndexOf('\/', url.length - 1);
    var first = url.substr(0, index + 1);
    return first + "TopScoreData.aspx";
}

//topScoresUrl = "http://localhost:62213/TopScoreData.aspx";
//topScoresUrl = "http://www.kindohm.com/tanks/TopScoreData.aspx";
topScoresUrl = getServiceUrl();

mapUrl = "http://maps.google.com/maps?q=";

function processTopScoresData(scoreData) {

    $("#loading").show("fast");
    $("#scores").hide("fast");
    $("#scores").empty();

    rowClass = "scoreRow";
    table = $("<table/>");
    table.attr("cellpadding", "0");
    table.attr("cellspacing", "0");
    table.attr("class", "scoreTable");
    table.append('<tr><th>&nbsp;</th>' +
    '<th>Player</th>' +
    '<th>Location</th>' +
    '<th>Date</th>' +
    '<th><a href="javascript:statSort(\'score\', \'Score\', 25);">Score</a></th>' +
    '<th><a href="javascript:statSort(\'rounds\', \'Rounds\', 25);">Rounds</a></th>' +
    '<th><a href="javascript:statSort(\'kills\', \'Kills\', 25);">Kills</a></th>' +
    '<th><a href="javascript:statSort(\'shotPercent\', \'Shot %\', 25);">Shot %</a></th>' +
    '<th><a href="javascript:statSort(\'hitsTaken\', \'Hits Taken\', 25);">Hits Taken</a></th></tr>');
    count = 1;

    $("score", scoreData).each(function() {
        tr = $("<tr/>");
        tr.attr("class", rowClass);
        td = $("<td/>");
        td.append(count.toString());
        tr.append(td);

        td = getUserCell($(this));
        tr.append(td);

        td = $("<td/>");
        if ($(this).attr("location").length > 0) {
            newMapUrl = mapUrl + $(this).attr("location");
            td.append('<a href="' + newMapUrl + '" target="_blank">' + $(this).attr("location") + '</a>');
        }
        else {
            td.append($(this).attr("location"));
        }
        tr.append(td);

        td = $("<td/>");
        td.append($(this).attr("gameDate"));
        tr.append(td);
        td = $("<td/>");
        td.append($(this).attr("gameScore"));
        td.attr("class", "right");
        tr.append(td);
        td = $("<td/>");
        td.append($(this).attr("rounds"));
        td.attr("class", "right");
        tr.append(td);
        td = $("<td/>");
        td.append($(this).attr("kills"));
        td.attr("class", "right");
        tr.append(td);
        td = $("<td/>");
        td.append($(this).attr("shotPercent"));
        td.attr("class", "right");
        tr.append(td);
        td = $("<td/>");
        td.append($(this).attr("hitsTaken"));
        td.attr("class", "right");
        tr.append(td);


        table.append(tr);
        count = count + 1;
        if (rowClass == "scoreRow") {
            rowClass = "scoreRowAlt";
        }
        else {
            rowClass = "scoreRow";
        }
    });

    $("#scores").append(table);
    $("#scores").show("fast");
    $("#loading").hide("fast");

}

function getUserCell(element) {
    td = $("<td/>");
    var url = new String();
    url = element.attr("url");

    var name = element.attr("userName");
    
    if (url.length > 0) {
        td.append('<a target="_blank" href="' + url + '">' + name + '</a>');
    }
    else {
        td.append(name);
    }
    return td;
}

function statSort(column, displayName, count) {

    statsUrl = topScoresUrl
    if (column.length > 0) {
        statsUrl = topScoresUrl + "?sort=" + column + "&count=" + count;
    }

    if (displayName == null || displayName.length == 0) {
        displayName = "Score";
    }

    $("#sortDisplay").empty();
    
    $("#sortDisplay").append("<p>Sorted by " + displayName + "</p>");

    $.ajax({
        type: "GET",
        url: statsUrl,
        dataType: "xml",
        success: function(data) {
            processTopScoresData(data);
        }
    });
}

function loadStats() {

    statSort('Score', 'Score', 25);

}

function loadTopScore() {

    statsUrl = topScoresUrl + "?count=1";
    $.ajax({
        type: "GET",
        url: statsUrl,
        dataType: "xml",
        success: function(data) {
        processTopScore(data);
        }
    });

}

function processTopScore(topData) {

    var newSpan = $("<span/>");
    $("score", topData).each(function() {

        if ($(this).attr("url").length > 0) {
            var anch = $("<a/>");
            anch.attr("href", $(this).attr("url"));
            anch.append($(this).attr("userName"));
            newSpan.append(anch);
        }
        else {
            newSpan.append($(this).attr("userName"));
        }


        locationString = ' ';
        if ($(this).attr("location") != null
        && $(this).attr("location").length > 0) {
            newMapUrl = mapUrl + $(this).attr("location");
            locationString = ' from <a target="_blank" href="' + newMapUrl + '">' + $(this).attr("location") + '</a> ';
        }
        newSpan.append(locationString + '[' + $(this).attr("gameScore") + ' points on ' + $(this).attr("gameDate") + ']');

    });

    $("#topScore").empty();
    $("#topScore").append(newSpan);

}

