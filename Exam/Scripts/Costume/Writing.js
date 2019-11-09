function GetProperExam() {
    var level = 3
    var isRandom = false
    var category = 'travel'
    var data = { 'level': level, 'isRandom': isRandom, 'category': category }
    
    $.post("/writing/GetProperExam",
        data, function (result) {
            alert(result)
            if (result == "nok") {
                window.location.replace("/home/about");
            }
            else {
            }
        });
}

function SubmitAnswer() {
    var answer = $("#answer").val()
    var writingId = $("#writingId").val()
    var data = { 'answer': answer, 'writingId': writingId }
    $.post("/writing/SubmitAnswer",
        data, function (result) {
            alert(result)
            if (result == "nok") {
                window.location.replace("/home/about");
            }
            else {
            }
        });
}

function GetCorrection() {
    var writingId = $("#writingId").val()
    var data = {  'writingId': writingId }
    $.post("/writing/GetCorrection",
        data, function (result) {
            alert(result)
            if (result == "nok") {
                window.location.replace("/home/about");
            }
            else {
            }
        });
}

function GetAvailable() {

    $.post("/writing/GetAvailable",
        null, function (result) {
            alert(result)
            if (result == "nok") {
                window.location.replace("/home/about");
            }
            else {
            }
        });
}

function GetInProgress() {

    $.post("/writing/GetInProgress",
        null, function (result) {
            alert(result)
            if (result == "nok") {
                window.location.replace("/home/about");
            }
            else {
            }
        });
}


function GetInProgress() {

    $.post("/writing/GetDone",
        null, function (result) {
            alert(result)
            if (result == "nok") {
                window.location.replace("/home/about");
            }
            else {
            }
        });
}

