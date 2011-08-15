function getFooterStuff() {

    newDiv = $("<div/>");
    newDiv.attr("class", "footer");
    newDiv.append("<p>Unauthorized duplication of any content on this site may result in sudden death or dismemberment.</p>");
    return newDiv;
}

$(document).ready(function() {
$("body").append(getFooterStuff());
});

