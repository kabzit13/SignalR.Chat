// Declare a proxy to reference the hub.
var chat = $.connection.chatHub;

function broadcastMsg(name, message) {
    // Html encode display name and message.
    var encodedName = $('<div />').text(name).html();
    var encodedMsg = $('<div />').text(message).html();
    // Add the message to the page.
    $('#discussion').append('<li><strong>' + encodedName
        + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
};

function setNewName(name) {
    $('#displayname').val(name);
}

function sendMsgToAll() {
    $('#sendmessage').click(function() {
        // Call the Send method on the hub.
        chat.server.sendToAll($('#displayname').val(), $('#message').val());
        // Clear text box and reset focus for next comment.
        $('#message').val('').focus();
    });
};

function ConnectUser(userName) {
    chat.server.connect($('#displayname').val());
};

function notifyAboutNewUser(name) {
    var encodedMsg = $('<div />').text(name).html();
    // Add the message to the page.
    $('#discussion').append('<li>New user in chat with name:&nbsp;&nbsp;' + encodedMsg + '</li>');
};

$(function init() {
    // Create a function that the hub can call to broadcast messages.
    chat.client.broadcastMessage = broadcastMsg;
    chat.client.notifyAboutNewUser = notifyAboutNewUser;
    chat.client.setNewName = setNewName;
    // Get the user name and store it to prepend to messages.
    $('#displayname').val(prompt(general.entrName, ''));
    // Set initial focus to message input box.
    $('#message').focus();
    // Start the connection.
    $.connection.hub.start().done(ConnectUser);

    sendMsgToAll();
    var notify = notifyAboutNewUser;

});
