
$(document).ready(function () {
    $('#textMessage').prop("disabled", true);
    $('#sendBtn').prop("disabled", true);
    $('#join').prop("disabled", true);
});


function getNicknameVal() {

    var nickname = document.getElementById("nickname").value;
    document.getElementById("nicknameMonitor").value = nickname;

    $('#join').prop("disabled", false);

    $('#nickname').prop("disabled", true);
    $('#nicknameBtn').prop("disabled", true);


}

function joinChat() {
    console.log("join................................");

    $('#textMessage').prop("disabled", false);
    $('#sendBtn').prop("disabled", false);
}