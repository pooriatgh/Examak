function GetProperExam() {
    var level = 3
    var isRandom = false
    var category = 'travel'
    var data = { 'level': level, 'isRandom': isRandom, 'category': category }

    $.post("/speaking/GetProperExam",
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
    var speakingId = $("#speakingId").val()
    var data = { 'answer': answer, 'speakingId': speakingId }
    $.post("/speaking/SubmitAnswer",
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
    var speakingId = $("#speakingId").val()
    var data = { 'speakingId': speakingId }
    $.post("/speaking/GetCorrection",
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

    $.post("/speaking/GetAvailable",
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

    $.post("/speaking/GetInProgress",
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

    $.post("/speaking/GetDone",
        null, function (result) {
            alert(result)
            if (result == "nok") {
                window.location.replace("/home/about");
            }
            else {
            }
        });
}
