// Declare a proxy to reference the hub.
var chat = $.connection.chatHub;

function broadcastMsg(name, message) {
    // Html encode display name and message.
    var encodedName = $('<div />').text(name).html();
    var encodedMsg = $('<div />').text(message).html();
    $('#discussion').append('<li><strong>' + encodedName
        + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
};

function setNewName(name) {
    $('#displayname').val(name);
}

function sendMsgToAll() {
    $('#sendmessage').click(function() {
        // Call the SentToAll method on the hub.
        chat.server.sendToAll($('#displayname').val(), $('#message').val());
        $('#message').val('').focus();
    });
};

function ConnectUser(userName) {
    chat.server.connect($('#displayname').val());
};

function notifyAboutNewUser(name) {
    var encodedName = $('<div />').text(name).html();
    $('#discussion').append('<li>'+ general.newUsr + encodedName + '</li>');
};

function userLeft(name) {
    var encodedName = $('<div />').text(name).html();
    $('#discussion').append('<li>' +  encodedName + general.usrLeft + '</li>');
}

$(function init() {
    chat.client.broadcastMessage = broadcastMsg;
    chat.client.notifyAboutNewUser = notifyAboutNewUser;
    chat.client.setNewName = setNewName;
    chat.client.notifyAboutLeftUser = userLeft;
    // Get the user name and store it to prepend to messages.
    $('#displayname').val(prompt(general.entrName, ''));
    // Set initial focus to message input box.
    $('#message').focus();
    // Start the connection.
    $.connection.hub.start().done(ConnectUser);

    sendMsgToAll();
    var notify = notifyAboutNewUser;

});
