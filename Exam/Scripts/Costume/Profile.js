
function GetTransaction() {

    $.post("/Profile/GetTransactionList",
        null, function (result) {
            alert(result)
            if (result == "nok") {
                window.location.replace("/home/about");
            }
            else {
            }
        });
}

function GetTicketList() {

    $.post("/Ticket/GetTicketList",
        null, function (result) {
            alert(result)
            if (result == "nok") {
                window.location.replace("/home/about");
            }
            else {
            }
        });
}

function GetticketSublist(ticketId) {
    var data = { 'ticketId': ticketId }
    $.post("/Ticket/GetticketSublist",
        data, function (result) {
            if (result == "nok") {
                window.location.replace("/home/about");
            }
            else {
            }
        });
}



function ShowTicketThread(ticketRootId) {
    var ticketRootId = ticketRootId
    var data = { 'ticketRootId': ticketRootId }
    $.post("/writing/ShowTicketThread",
        data, function (result) {
            alert(result)
            if (result == "nok") {
                window.location.replace("/home/about");
            }
            else {
            }
        });
}
